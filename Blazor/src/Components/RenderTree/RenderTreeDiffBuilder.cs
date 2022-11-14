// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Wangkanai.Blazor.Components.HotReload;
using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components.RenderTree;

internal static class RenderTreeDiffBuilder
{
    enum DiffAction { Match, Insert, Delete }

    public const int SystemAddedAttributeSequenceNumber = int.MinValue;

    public static RenderTreeDiff ComputeDiff(
        Renderer                    renderer,
        RenderBatchBuilder          batchBuilder,
        int                         componentId,
        ArrayRange<RenderTreeFrame> oldTree,
        ArrayRange<RenderTreeFrame> newTree)
    {
        var editsBuffer            = batchBuilder.EditsBuffer;
        var editsBufferStartLength = editsBuffer.Count;

        var diffContext = new DiffContext(renderer, batchBuilder, componentId, oldTree.Array, newTree.Array);
        AppendDiffEntriesForRange(ref diffContext, 0, oldTree.Count, 0, newTree.Count);

        var editsSegment = editsBuffer.ToSegment(editsBufferStartLength, editsBuffer.Count);
        var result       = new RenderTreeDiff(componentId, editsSegment);
        return result;
    }

    public static void DisposeFrames(RenderBatchBuilder batchBuilder, ArrayRange<RenderTreeFrame> frames)
        => DisposeFramesInRange(batchBuilder, frames.Array, 0, frames.Count);

    private static void AppendDiffEntriesForRange(
        ref DiffContext diffContext,
        int             oldStartIndex, int oldEndIndexExcl,
        int             newStartIndex, int newEndIndexExcl)
    {
        var                               origOldStartIndex     = oldStartIndex;
        var                               origNewStartIndex     = newStartIndex;
        var                               hasMoreOld            = oldEndIndexExcl > oldStartIndex;
        var                               hasMoreNew            = newEndIndexExcl > newStartIndex;
        var                               prevOldSeq            = -1;
        var                               prevNewSeq            = -1;
        var                               oldTree               = diffContext.OldTree;
        var                               newTree               = diffContext.NewTree;
        var                               matchWithNewTreeIndex = -1; // Only used when action == DiffAction.Match
        Dictionary<object, KeyedItemInfo> keyedItemInfos        = null;

        try
        {
            while (hasMoreOld || hasMoreNew)
            {
                DiffAction action;

                #region "Read keys and sequence numbers"

                int    oldSeq, newSeq;
                object oldKey, newKey;
                if (hasMoreOld)
                {
                    ref var oldFrame = ref oldTree[oldStartIndex];
                    oldSeq = oldFrame.SequenceField;
                    oldKey = KeyValue(ref oldFrame);
                }
                else
                {
                    oldSeq = int.MaxValue;
                    oldKey = null;
                }

                if (hasMoreNew)
                {
                    ref var newFrame = ref newTree[newStartIndex];
                    newSeq = newFrame.SequenceField;
                    newKey = KeyValue(ref newFrame);
                }
                else
                {
                    newSeq = int.MaxValue;
                    newKey = null;
                }

                #endregion

                // If there's a key on either side, prefer matching by key not sequence
                if (oldKey != null || newKey != null)
                {
                    #region "Get diff action by matching on key"

                    keyedItemInfos ??= BuildKeyToInfoLookup(diffContext, origOldStartIndex, oldEndIndexExcl, origNewStartIndex, newEndIndexExcl);

                    if (Equals(oldKey, newKey))
                    {
                        action                = DiffAction.Match;
                        matchWithNewTreeIndex = newStartIndex;
                    }
                    else
                    {
                        var oldKeyItemInfo    = oldKey != null ? keyedItemInfos[oldKey] : new KeyedItemInfo(-1, -1);
                        var newKeyItemInfo    = newKey != null ? keyedItemInfos[newKey] : new KeyedItemInfo(-1, -1);
                        var oldKeyIsInNewTree = oldKeyItemInfo.NewIndex >= 0;
                        var newKeyIsInOldTree = newKeyItemInfo.OldIndex >= 0;

                        if (oldKeyIsInNewTree && newKeyIsInOldTree)
                        {
                            action                = DiffAction.Match;
                            matchWithNewTreeIndex = oldKeyItemInfo.NewIndex;

                            keyedItemInfos[oldKey] = oldKeyItemInfo.WithOldSiblingIndex(diffContext.SiblingIndex);
                            keyedItemInfos[newKey] = newKeyItemInfo.WithNewSiblingIndex(diffContext.SiblingIndex);
                        }
                        else if (newKey == null)
                            action = oldKeyIsInNewTree ? DiffAction.Insert : DiffAction.Delete;
                        else
                            action = newKeyIsInOldTree ? DiffAction.Delete : DiffAction.Insert;

                        Debug.Assert(action switch
                        {
                            DiffAction.Insert => hasMoreNew,
                            DiffAction.Delete => hasMoreOld,
                            _                 => true,
                        }, "The chosen diff action is illegal because we've run out of items on the side being inserted/deleted");
                    }

                    #endregion
                }
                else
                {
                    #region "Get diff action by matching on sequence number"

                    if (oldSeq == newSeq)
                    {
                        action                = DiffAction.Match;
                        matchWithNewTreeIndex = newStartIndex;
                    }
                    else
                    {
                        var oldLoopedBack = oldSeq <= prevOldSeq;
                        var newLoopedBack = newSeq <= prevNewSeq;
                        if (oldLoopedBack == newLoopedBack)
                        {
                            // Both sequences are proceeding through the same loop block, so do a simple
                            // preordered merge join (picking from whichever side brings us closer to being
                            // back in sync)
                            action = newSeq < oldSeq ? DiffAction.Insert : DiffAction.Delete;

                            if (oldLoopedBack)
                            {
                                // If both old and new have now looped back, we must reset their 'looped back'
                                // tracker so we can treat them as proceeding through the same loop block
                                prevOldSeq = -1;
                                prevNewSeq = -1;
                            }
                        }
                        else if (oldLoopedBack)
                        {
                            // Old sequence looped back but new one didn't
                            // The new sequence either has some extra trailing elements in the current loop block
                            // which we should insert, or omits some old trailing loop blocks which we should delete
                            // TODO: Find a way of not recomputing this next flag on every iteration
                            var newLoopsBackLater = false;
                            for (var testIndex = newStartIndex + 1; testIndex < newEndIndexExcl; testIndex++)
                            {
                                if (newTree[testIndex].SequenceField < newSeq)
                                {
                                    newLoopsBackLater = true;
                                    break;
                                }
                            }

                            // If the new sequence loops back later to an earlier point than this,
                            // then we know it's part of the existing loop block (so should be inserted).
                            // If not, then it's unrelated to the previous loop block (so we should treat
                            // the old items as trailing loop blocks to be removed).
                            action = newLoopsBackLater ? DiffAction.Insert : DiffAction.Delete;
                        }
                        else
                        {
                            // New sequence looped back but old one didn't
                            // The old sequence either has some extra trailing elements in the current loop block
                            // which we should delete, or the new sequence has extra trailing loop blocks which we
                            // should insert
                            // TODO: Find a way of not recomputing this next flag on every iteration
                            var oldLoopsBackLater = false;
                            for (var testIndex = oldStartIndex + 1; testIndex < oldEndIndexExcl; testIndex++)
                            {
                                if (oldTree[testIndex].SequenceField < oldSeq)
                                {
                                    oldLoopsBackLater = true;
                                    break;
                                }
                            }

                            // If the old sequence loops back later to an earlier point than this,
                            // then we know it's part of the existing loop block (so should be removed).
                            // If not, then it's unrelated to the previous loop block (so we should treat
                            // the new items as trailing loop blocks to be inserted).
                            action = oldLoopsBackLater ? DiffAction.Delete : DiffAction.Insert;
                        }
                    }

                    #endregion
                }

                #region "Apply diff action"

                switch (action)
                {
                    case DiffAction.Match:
                        AppendDiffEntriesForFramesWithSameSequence(ref diffContext, oldStartIndex, matchWithNewTreeIndex);
                        oldStartIndex = NextSiblingIndex(oldTree[oldStartIndex], oldStartIndex);
                        newStartIndex = NextSiblingIndex(newTree[newStartIndex], newStartIndex);
                        hasMoreOld    = oldEndIndexExcl > oldStartIndex;
                        hasMoreNew    = newEndIndexExcl > newStartIndex;
                        prevOldSeq    = oldSeq;
                        prevNewSeq    = newSeq;
                        break;
                    case DiffAction.Insert:
                        InsertNewFrame(ref diffContext, newStartIndex);
                        newStartIndex = NextSiblingIndex(newTree[newStartIndex], newStartIndex);
                        hasMoreNew    = newEndIndexExcl > newStartIndex;
                        prevNewSeq    = newSeq;
                        break;
                    case DiffAction.Delete:
                        RemoveOldFrame(ref diffContext, oldStartIndex);
                        oldStartIndex = NextSiblingIndex(oldTree[oldStartIndex], oldStartIndex);
                        hasMoreOld    = oldEndIndexExcl > oldStartIndex;
                        prevOldSeq    = oldSeq;
                        break;
                }

                #endregion
            }

            #region "Write permutations list"

            if (keyedItemInfos != null)
            {
                var hasPermutations = false;
                foreach (var keyValuePair in keyedItemInfos)
                {
                    var value = keyValuePair.Value;
                    if (value.OldSiblingIndex >= 0 && value.NewSiblingIndex >= 0)
                    {
                        hasPermutations = true;
                        diffContext.Edits.Append(
                            RenderTreeEdit.PermutationListEntry(value.OldSiblingIndex, value.NewSiblingIndex));
                    }
                }

                if (hasPermutations)
                    diffContext.Edits.Append(RenderTreeEdit.PermutationListEnd());
            }

            #endregion
        }
        finally
        {
            if (keyedItemInfos != null)
            {
                keyedItemInfos.Clear();
                diffContext.KeyedItemInfoDictionaryPool.Return(keyedItemInfos);
            }
        }
    }

    private static Dictionary<object, KeyedItemInfo> BuildKeyToInfoLookup(DiffContext diffContext, int oldStartIndex, int oldEndIndexExcl, int newStartIndex, int newEndIndexExcl)
    {
        var result  = diffContext.KeyedItemInfoDictionaryPool.Get();
        var oldTree = diffContext.OldTree;
        var newTree = diffContext.NewTree;

        while (oldStartIndex < oldEndIndexExcl)
        {
            ref var frame = ref oldTree[oldStartIndex];
            var     key   = KeyValue(ref frame);
            if (key != null)
            {
                if (result.ContainsKey(key))
                    ThrowExceptionForDuplicateKey(key, frame);

                result[key] = new KeyedItemInfo(oldStartIndex, -1);
            }

            oldStartIndex = NextSiblingIndex(frame, oldStartIndex);
        }

        while (newStartIndex < newEndIndexExcl)
        {
            ref var frame = ref newTree[newStartIndex];
            var     key   = KeyValue(ref frame);
            if (key != null)
                if (!result.TryGetValue(key, out var existingEntry))
                    result[key] = new KeyedItemInfo(-1, newStartIndex);
                else
                {
                    if (existingEntry.NewIndex >= 0)
                        ThrowExceptionForDuplicateKey(key, frame);

                    result[key] = new KeyedItemInfo(existingEntry.OldIndex, newStartIndex);
                }

            newStartIndex = NextSiblingIndex(frame, newStartIndex);
        }

        return result;
    }

    private static void ThrowExceptionForDuplicateKey(object key, in RenderTreeFrame frame)
    {
        switch (frame.FrameTypeField)
        {
            case RenderTreeFrameType.Component:
                throw new InvalidOperationException($"More than one sibling of component '{frame.ComponentTypeField}' has the same key value, '{key}'. Key values must be unique.");

            case RenderTreeFrameType.Element:
                throw new InvalidOperationException($"More than one sibling of element '{frame.ElementNameField}' has the same key value, '{key}'. Key values must be unique.");

            default:
                throw new InvalidOperationException($"More than one sibling has the same key value, '{key}'. Key values must be unique.");
        }
    }

    private static object KeyValue(ref RenderTreeFrame frame)
    {
        switch (frame.FrameTypeField)
        {
            case RenderTreeFrameType.Element:
                return frame.ElementKeyField;
            case RenderTreeFrameType.Component:
                return frame.ComponentKeyField;
            default:
                return null;
        }
    }

    private static void AppendAttributeDiffEntriesForRange(
        ref DiffContext diffContext,
        int             oldStartIndex, int oldEndIndexExcl,
        int             newStartIndex, int newEndIndexExcl)
    {
        var hasMoreOld = oldEndIndexExcl > oldStartIndex;
        var hasMoreNew = newEndIndexExcl > newStartIndex;
        var oldTree    = diffContext.OldTree;
        var newTree    = diffContext.NewTree;

        while (hasMoreOld || hasMoreNew)
        {
            var oldSeq           = hasMoreOld ? oldTree[oldStartIndex].SequenceField : int.MaxValue;
            var newSeq           = hasMoreNew ? newTree[newStartIndex].SequenceField : int.MaxValue;
            var oldAttributeName = oldTree[oldStartIndex].AttributeNameField;
            var newAttributeName = newTree[newStartIndex].AttributeNameField;

            if (oldSeq == newSeq &&
                string.Equals(oldAttributeName, newAttributeName, StringComparison.Ordinal))
            {
                AppendDiffEntriesForAttributeFrame(ref diffContext, oldStartIndex, newStartIndex);

                oldStartIndex++;
                newStartIndex++;
                hasMoreOld = oldEndIndexExcl > oldStartIndex;
                hasMoreNew = newEndIndexExcl > newStartIndex;
            }
            else if (oldSeq < newSeq)
            {
                if (oldSeq == SystemAddedAttributeSequenceNumber)
                {
                    AppendAttributeDiffEntriesForRangeSlow(
                        ref diffContext,
                        oldStartIndex, oldEndIndexExcl,
                        newStartIndex, newEndIndexExcl);
                    return;
                }

                RemoveOldFrame(ref diffContext, oldStartIndex);

                oldStartIndex = NextSiblingIndex(oldTree[oldStartIndex], oldStartIndex);
                hasMoreOld    = oldEndIndexExcl > oldStartIndex;
            }
            else if (oldSeq > newSeq)
            {
                InsertNewFrame(ref diffContext, newStartIndex);

                newStartIndex = NextSiblingIndex(newTree[newStartIndex], newStartIndex);
                hasMoreNew    = newEndIndexExcl > newStartIndex;
            }
            else
            {
                AppendAttributeDiffEntriesForRangeSlow(
                    ref diffContext,
                    oldStartIndex, oldEndIndexExcl,
                    newStartIndex, newEndIndexExcl);
                return;
            }
        }
    }

    private static void AppendAttributeDiffEntriesForRangeSlow(
        ref DiffContext diffContext,
        int             oldStartIndex, int oldEndIndexExcl,
        int             newStartIndex, int newEndIndexExcl)
    {
        var oldTree = diffContext.OldTree;
        var newTree = diffContext.NewTree;

        for (var i = newStartIndex; i < newEndIndexExcl; i++)
            diffContext.AttributeDiffSet[newTree[i].AttributeNameField] = i;

        for (var i = oldStartIndex; i < oldEndIndexExcl; i++)
        {
            var oldName = oldTree[i].AttributeNameField;
            if (diffContext.AttributeDiffSet.TryGetValue(oldName, out var matchIndex))
            {
                AppendDiffEntriesForAttributeFrame(ref diffContext, i, matchIndex);
                diffContext.AttributeDiffSet.Remove(oldName);
            }
            else
                RemoveOldFrame(ref diffContext, i);
        }

        foreach (var kvp in diffContext.AttributeDiffSet)
            InsertNewFrame(ref diffContext, kvp.Value);

        diffContext.AttributeDiffSet.Clear();
    }

    private static int NextSiblingIndex(in RenderTreeFrame frame, int frameIndex)
    {
        switch (frame.FrameTypeField)
        {
            case RenderTreeFrameType.Component:
                return frameIndex + frame.ComponentSubtreeLengthField;
            case RenderTreeFrameType.Element:
                return frameIndex + frame.ElementSubtreeLengthField;
            case RenderTreeFrameType.Region:
                return frameIndex + frame.RegionSubtreeLengthField;
            default:
                return frameIndex + 1;
        }
    }

    private static void AppendDiffEntriesForFramesWithSameSequence(
        ref DiffContext diffContext,
        int             oldFrameIndex,
        int             newFrameIndex)
    {
        var     oldTree  = diffContext.OldTree;
        var     newTree  = diffContext.NewTree;
        ref var oldFrame = ref oldTree[oldFrameIndex];
        ref var newFrame = ref newTree[newFrameIndex];

        var newFrameType = newFrame.FrameTypeField;
        if (oldFrame.FrameTypeField != newFrameType)
        {
            InsertNewFrame(ref diffContext, newFrameIndex);
            RemoveOldFrame(ref diffContext, oldFrameIndex);
            return;
        }

        switch (newFrameType)
        {
            case RenderTreeFrameType.Text:
            {
                var oldText = oldFrame.TextContentField;
                var newText = newFrame.TextContentField;
                if (!string.Equals(oldText, newText, StringComparison.Ordinal))
                {
                    var referenceFrameIndex = diffContext.ReferenceFrames.Append(newFrame);
                    diffContext.Edits.Append(RenderTreeEdit.UpdateText(diffContext.SiblingIndex, referenceFrameIndex));
                }

                diffContext.SiblingIndex++;
                break;
            }

            case RenderTreeFrameType.Markup:
            {
                var oldMarkup = oldFrame.MarkupContentField;
                var newMarkup = newFrame.MarkupContentField;
                if (!string.Equals(oldMarkup, newMarkup, StringComparison.Ordinal))
                {
                    var referenceFrameIndex = diffContext.ReferenceFrames.Append(newFrame);
                    diffContext.Edits.Append(RenderTreeEdit.UpdateMarkup(diffContext.SiblingIndex, referenceFrameIndex));
                }

                diffContext.SiblingIndex++;
                break;
            }

            case RenderTreeFrameType.Element:
            {
                var oldElementName = oldFrame.ElementNameField;
                var newElementName = newFrame.ElementNameField;
                if (string.Equals(oldElementName, newElementName, StringComparison.Ordinal))
                {
                    var oldFrameAttributesEndIndexExcl = GetAttributesEndIndexExclusive(oldTree, oldFrameIndex);
                    var newFrameAttributesEndIndexExcl = GetAttributesEndIndexExclusive(newTree, newFrameIndex);

                    AppendAttributeDiffEntriesForRange(
                        ref diffContext,
                        oldFrameIndex + 1, oldFrameAttributesEndIndexExcl,
                        newFrameIndex + 1, newFrameAttributesEndIndexExcl);

                    var oldFrameChildrenEndIndexExcl = oldFrameIndex + oldFrame.ElementSubtreeLengthField;
                    var newFrameChildrenEndIndexExcl = newFrameIndex + newFrame.ElementSubtreeLengthField;
                    var hasChildrenToProcess =
                        oldFrameChildrenEndIndexExcl > oldFrameAttributesEndIndexExcl ||
                        newFrameChildrenEndIndexExcl > newFrameAttributesEndIndexExcl;
                    if (hasChildrenToProcess)
                    {
                        diffContext.Edits.Append(RenderTreeEdit.StepIn(diffContext.SiblingIndex));
                        var prevSiblingIndex = diffContext.SiblingIndex;
                        diffContext.SiblingIndex = 0;
                        AppendDiffEntriesForRange(
                            ref diffContext,
                            oldFrameAttributesEndIndexExcl, oldFrameChildrenEndIndexExcl,
                            newFrameAttributesEndIndexExcl, newFrameChildrenEndIndexExcl);
                        AppendStepOut(ref diffContext);
                        diffContext.SiblingIndex = prevSiblingIndex + 1;
                    }
                    else
                        diffContext.SiblingIndex++;
                }
                else
                {
                    RemoveOldFrame(ref diffContext, oldFrameIndex);
                    InsertNewFrame(ref diffContext, newFrameIndex);
                }

                break;
            }

            case RenderTreeFrameType.Region:
            {
                AppendDiffEntriesForRange(
                    ref diffContext,
                    oldFrameIndex + 1, oldFrameIndex + oldFrame.RegionSubtreeLengthField,
                    newFrameIndex + 1, newFrameIndex + newFrame.RegionSubtreeLengthField);
                break;
            }

            case RenderTreeFrameType.Component:
            {
                if (oldFrame.ComponentTypeField == newFrame.ComponentTypeField)
                {
                    var oldParameters         = new ParameterView(ParameterViewLifetime.Unbound, oldTree, oldFrameIndex);
                    var newParametersLifetime = new ParameterViewLifetime(diffContext.BatchBuilder);
                    var newParameters         = new ParameterView(newParametersLifetime, newTree, newFrameIndex);
                    var isHotReload           = HotReloadManager.Default.MetadataUpdateSupported && diffContext.Renderer.IsRenderingOnMetadataUpdate;

                    if (isHotReload && newParameters.HasRemovedDirectParameters(oldParameters))
                    {
                        RemoveOldFrame(ref diffContext, oldFrameIndex);
                        InsertNewFrame(ref diffContext, newFrameIndex);
                    }
                    else
                    {
                        var componentState = oldFrame.ComponentStateField;

                        newFrame.ComponentStateField = componentState;
                        newFrame.ComponentIdField    = componentState.ComponentId;

                        if (!newParameters.DefinitelyEquals(oldParameters) || isHotReload)
                            componentState.SetDirectParameters(newParameters);

                        diffContext.SiblingIndex++;
                    }
                }
                else
                {
                    RemoveOldFrame(ref diffContext, oldFrameIndex);
                    InsertNewFrame(ref diffContext, newFrameIndex);
                }

                break;
            }

            case RenderTreeFrameType.ElementReferenceCapture:
            {
                break;
            }

            default:
                throw new NotImplementedException($"Encountered unsupported frame type during diffing: {newTree[newFrameIndex].FrameTypeField}");
        }
    }

    private static void AppendDiffEntriesForAttributeFrame(
        ref DiffContext diffContext,
        int             oldFrameIndex,
        int             newFrameIndex)
    {
        var     oldTree  = diffContext.OldTree;
        var     newTree  = diffContext.NewTree;
        ref var oldFrame = ref oldTree[oldFrameIndex];
        ref var newFrame = ref newTree[newFrameIndex];

        var valueChanged = !Equals(oldFrame.AttributeValueField, newFrame.AttributeValueField);
        if (valueChanged)
        {
            InitializeNewAttributeFrame(ref diffContext, ref newFrame);
            var referenceFrameIndex = diffContext.ReferenceFrames.Append(newFrame);
            diffContext.Edits.Append(RenderTreeEdit.SetAttribute(diffContext.SiblingIndex, referenceFrameIndex));

            if (oldFrame.AttributeEventHandlerIdField > 0)
            {
                diffContext.Renderer.TrackReplacedEventHandlerId(oldFrame.AttributeEventHandlerIdField, newFrame.AttributeEventHandlerIdField);
                diffContext.BatchBuilder.DisposedEventHandlerIds.Append(oldFrame.AttributeEventHandlerIdField);
            }
        }
        else if (oldFrame.AttributeEventHandlerIdField > 0)
            newFrame = oldFrame;
    }

    private static void InsertNewFrame(ref DiffContext diffContext, int newFrameIndex)
    {
        var     newTree  = diffContext.NewTree;
        ref var newFrame = ref newTree[newFrameIndex];
        switch (newFrame.FrameTypeField)
        {
            case RenderTreeFrameType.Attribute:
            {
                InitializeNewAttributeFrame(ref diffContext, ref newFrame);
                var referenceFrameIndex = diffContext.ReferenceFrames.Append(newFrame);
                diffContext.Edits.Append(RenderTreeEdit.SetAttribute(diffContext.SiblingIndex, referenceFrameIndex));
                break;
            }
            case RenderTreeFrameType.Component:
            case RenderTreeFrameType.Element:
            {
                InitializeNewSubtree(ref diffContext, newFrameIndex);
                var referenceFrameIndex = diffContext.ReferenceFrames.Append(newTree, newFrameIndex, newFrame.ElementSubtreeLengthField);
                diffContext.Edits.Append(RenderTreeEdit.PrependFrame(diffContext.SiblingIndex, referenceFrameIndex));
                diffContext.SiblingIndex++;
                break;
            }
            case RenderTreeFrameType.Region:
            {
                var regionChildFrameIndex        = newFrameIndex + 1;
                var regionChildFrameEndIndexExcl = newFrameIndex + newFrame.RegionSubtreeLengthField;
                while (regionChildFrameIndex < regionChildFrameEndIndexExcl)
                {
                    InsertNewFrame(ref diffContext, regionChildFrameIndex);
                    regionChildFrameIndex = NextSiblingIndex(newTree[regionChildFrameIndex], regionChildFrameIndex);
                }

                break;
            }
            case RenderTreeFrameType.Text:
            case RenderTreeFrameType.Markup:
            {
                var referenceFrameIndex = diffContext.ReferenceFrames.Append(newFrame);
                diffContext.Edits.Append(RenderTreeEdit.PrependFrame(diffContext.SiblingIndex, referenceFrameIndex));
                diffContext.SiblingIndex++;
                break;
            }
            case RenderTreeFrameType.ElementReferenceCapture:
            {
                InitializeNewElementReferenceCaptureFrame(ref diffContext, ref newFrame);
                break;
            }
            case RenderTreeFrameType.ComponentReferenceCapture:
            {
                InitializeNewComponentReferenceCaptureFrame(ref diffContext, ref newFrame);
                break;
            }
            default:
                throw new NotImplementedException($"Unexpected frame type during {nameof(InsertNewFrame)}: {newFrame.FrameTypeField}");
        }
    }

    private static void RemoveOldFrame(ref DiffContext diffContext, int oldFrameIndex)
    {
        var     oldTree  = diffContext.OldTree;
        ref var oldFrame = ref oldTree[oldFrameIndex];
        switch (oldFrame.FrameTypeField)
        {
            case RenderTreeFrameType.Attribute:
            {
                diffContext.Edits.Append(RenderTreeEdit.RemoveAttribute(diffContext.SiblingIndex, oldFrame.AttributeNameField));
                if (oldFrame.AttributeEventHandlerIdField > 0)
                {
                    diffContext.BatchBuilder.DisposedEventHandlerIds.Append(oldFrame.AttributeEventHandlerIdField);
                }

                break;
            }
            case RenderTreeFrameType.Component:
            case RenderTreeFrameType.Element:
            {
                var endIndexExcl = oldFrameIndex + oldFrame.ElementSubtreeLengthField;
                DisposeFramesInRange(diffContext.BatchBuilder, oldTree, oldFrameIndex, endIndexExcl);
                diffContext.Edits.Append(RenderTreeEdit.RemoveFrame(diffContext.SiblingIndex));
                break;
            }
            case RenderTreeFrameType.Region:
            {
                var regionChildFrameIndex        = oldFrameIndex + 1;
                var regionChildFrameEndIndexExcl = oldFrameIndex + oldFrame.RegionSubtreeLengthField;
                while (regionChildFrameIndex < regionChildFrameEndIndexExcl)
                {
                    RemoveOldFrame(ref diffContext, regionChildFrameIndex);
                    regionChildFrameIndex = NextSiblingIndex(oldTree[regionChildFrameIndex], regionChildFrameIndex);
                }

                break;
            }
            case RenderTreeFrameType.Text:
            case RenderTreeFrameType.Markup:
            {
                diffContext.Edits.Append(RenderTreeEdit.RemoveFrame(diffContext.SiblingIndex));
                break;
            }
            default:
                throw new NotImplementedException($"Unexpected frame type during {nameof(RemoveOldFrame)}: {oldFrame.FrameTypeField}");
        }
    }

    private static int GetAttributesEndIndexExclusive(RenderTreeFrame[] tree, int rootIndex)
    {
        var descendantsEndIndexExcl = rootIndex + tree[rootIndex].ElementSubtreeLengthField;
        var index                   = rootIndex + 1;
        for (; index < descendantsEndIndexExcl; index++)
            if (tree[index].FrameTypeField != RenderTreeFrameType.Attribute)
                break;

        return index;
    }

    private static void AppendStepOut(ref DiffContext diffContext)
    {
        var previousIndex = diffContext.Edits.Count - 1;
        if (previousIndex >= 0 && diffContext.Edits.Buffer[previousIndex].Type == RenderTreeEditType.StepIn)
            diffContext.Edits.RemoveLast();
        else
            diffContext.Edits.Append(RenderTreeEdit.StepOut());
    }

    private static void InitializeNewSubtree(ref DiffContext diffContext, int frameIndex)
    {
        var frames       = diffContext.NewTree;
        var endIndexExcl = frameIndex + frames[frameIndex].ElementSubtreeLengthField;
        for (var i = frameIndex; i < endIndexExcl; i++)
        {
            ref var frame = ref frames[i];
            switch (frame.FrameTypeField)
            {
                case RenderTreeFrameType.Component:
                    InitializeNewComponentFrame(ref diffContext, i);
                    break;
                case RenderTreeFrameType.Attribute:
                    InitializeNewAttributeFrame(ref diffContext, ref frame);
                    break;
                case RenderTreeFrameType.ElementReferenceCapture:
                    InitializeNewElementReferenceCaptureFrame(ref diffContext, ref frame);
                    break;
                case RenderTreeFrameType.ComponentReferenceCapture:
                    InitializeNewComponentReferenceCaptureFrame(ref diffContext, ref frame);
                    break;
            }
        }
    }

    private static void InitializeNewComponentFrame(ref DiffContext diffContext, int frameIndex)
    {
        var     frames = diffContext.NewTree;
        ref var frame  = ref frames[frameIndex];

        if (frame.ComponentStateField != null)
            throw new InvalidOperationException($"Child component already exists during {nameof(InitializeNewComponentFrame)}");

        var parentComponentId = diffContext.ComponentId;
        diffContext.Renderer.InstantiateChildComponentOnFrame(ref frame, parentComponentId);
        var childComponentState = frame.ComponentStateField;

        // Set initial parameters
        var initialParametersLifetime = new ParameterViewLifetime(diffContext.BatchBuilder);
        var initialParameters         = new ParameterView(initialParametersLifetime, frames, frameIndex);
        childComponentState.SetDirectParameters(initialParameters);
    }

    private static void InitializeNewAttributeFrame(ref DiffContext diffContext, ref RenderTreeFrame newFrame)
    {
        if ((newFrame.AttributeValueField is MulticastDelegate || newFrame.AttributeValueField is EventCallback) &&
            newFrame.AttributeNameField.Length >= 3 &&
            newFrame.AttributeNameField.StartsWith("on", StringComparison.Ordinal))
        {
            diffContext.Renderer.AssignEventHandlerId(ref newFrame);
        }
    }

    private static void InitializeNewElementReferenceCaptureFrame(ref DiffContext diffContext, ref RenderTreeFrame newFrame)
    {
        var newElementReference = ElementReference.CreateWithUniqueId(diffContext.Renderer.ElementReferenceContext);
        newFrame.ElementReferenceCaptureIdField = newElementReference.Id;
        newFrame.ElementReferenceCaptureActionField(newElementReference);
    }

    private static void InitializeNewComponentReferenceCaptureFrame(ref DiffContext diffContext, ref RenderTreeFrame newFrame)
    {
        ref var parentFrame = ref diffContext.NewTree[newFrame.ComponentReferenceCaptureParentFrameIndexField];
        if (parentFrame.FrameTypeField != RenderTreeFrameType.Component)
            throw new InvalidOperationException($"{nameof(RenderTreeFrameType.ComponentReferenceCapture)} frame references invalid parent index.");

        var componentInstance = parentFrame.Component;
        if (componentInstance == null)
            throw new InvalidOperationException($"Trying to initialize {nameof(RenderTreeFrameType.ComponentReferenceCapture)} frame before parent component was assigned.");

        newFrame.ComponentReferenceCaptureActionField(componentInstance);
    }

    private static void DisposeFramesInRange(RenderBatchBuilder batchBuilder, RenderTreeFrame[] frames, int startIndex, int endIndexExcl)
    {
        for (var i = startIndex; i < endIndexExcl; i++)
        {
            ref var frame = ref frames[i];
            if (frame.FrameTypeField == RenderTreeFrameType.Component && frame.ComponentStateField != null)
                batchBuilder.ComponentDisposalQueue.Enqueue(frame.ComponentIdField);
            else if (frame.FrameTypeField == RenderTreeFrameType.Attribute && frame.AttributeEventHandlerIdField > 0)
                batchBuilder.DisposedEventHandlerIds.Append(frame.AttributeEventHandlerIdField);
        }
    }

    private struct DiffContext
    {
        public readonly Renderer                                           Renderer;
        public readonly RenderBatchBuilder                                 BatchBuilder;
        public readonly RenderTreeFrame[]                                  OldTree;
        public readonly RenderTreeFrame[]                                  NewTree;
        public readonly ArrayBuilder<RenderTreeEdit>                       Edits;
        public readonly ArrayBuilder<RenderTreeFrame>                      ReferenceFrames;
        public readonly Dictionary<string, int>                            AttributeDiffSet;
        public readonly StackObjectPool<Dictionary<object, KeyedItemInfo>> KeyedItemInfoDictionaryPool;
        public readonly int                                                ComponentId;
        public          int                                                SiblingIndex;

        public DiffContext(
            Renderer           renderer,
            RenderBatchBuilder batchBuilder,
            int                componentId,
            RenderTreeFrame[]  oldTree,
            RenderTreeFrame[]  newTree)
        {
            Renderer                    = renderer;
            BatchBuilder                = batchBuilder;
            ComponentId                 = componentId;
            OldTree                     = oldTree;
            NewTree                     = newTree;
            Edits                       = batchBuilder.EditsBuffer;
            ReferenceFrames             = batchBuilder.ReferenceFramesBuffer;
            AttributeDiffSet            = batchBuilder.AttributeDiffSet;
            KeyedItemInfoDictionaryPool = batchBuilder.KeyedItemInfoDictionaryPool;
            SiblingIndex                = 0;
        }
    }
}