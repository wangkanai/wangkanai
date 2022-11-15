// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components.Rendering;

internal sealed class RenderTreeUpdater
{
    public static void UpdateToMatchClientState(RenderTreeBuilder renderTreeBuilder, ulong eventHandlerId, object newFieldValue)
    {
        if (!(newFieldValue is string || newFieldValue is bool))
            return;

        var frames                   = renderTreeBuilder.GetFrames();
        var framesArray              = frames.Array;
        var framesLength             = frames.Count;
        var closestElementFrameIndex = -1;
        for (var frameIndex = 0; frameIndex < framesLength; frameIndex++)
        {
            ref var frame = ref framesArray[frameIndex];
            switch (frame.FrameTypeField)
            {
                case RenderTreeFrameType.Element:
                    closestElementFrameIndex = frameIndex;
                    break;
                case RenderTreeFrameType.Attribute:
                    if (frame.AttributeEventHandlerIdField == eventHandlerId)
                    {
                        if (!string.IsNullOrEmpty(frame.AttributeEventUpdatesAttributeNameField))
                            UpdateFrameToMatchClientState(
                                renderTreeBuilder,
                                framesArray,
                                closestElementFrameIndex,
                                frame.AttributeEventUpdatesAttributeNameField,
                                newFieldValue);

                        return;
                    }

                    break;
            }
        }
    }

    private static void UpdateFrameToMatchClientState(RenderTreeBuilder renderTreeBuilder, RenderTreeFrame[] framesArray, int elementFrameIndex, string attributeName, object attributeValue)
    {
        // Find the attribute frame
        ref var elementFrame               = ref framesArray[elementFrameIndex];
        var     elementSubtreeEndIndexExcl = elementFrameIndex + elementFrame.ElementSubtreeLengthField;
        for (var attributeFrameIndex = elementFrameIndex + 1; attributeFrameIndex < elementSubtreeEndIndexExcl; attributeFrameIndex++)
        {
            ref var attributeFrame = ref framesArray[attributeFrameIndex];
            if (attributeFrame.FrameTypeField != RenderTreeFrameType.Attribute)
                break;

            if (attributeFrame.AttributeNameField == attributeName)
            {
                // Found an existing attribute we can update
                attributeFrame.AttributeValueField = attributeValue;
                return;
            }
        }

        var insertAtIndex  = elementFrameIndex + 1;
        var didInsertFrame = renderTreeBuilder.InsertAttributeExpensive(insertAtIndex, RenderTreeDiffBuilder.SystemAddedAttributeSequenceNumber, attributeName, attributeValue);
        if (!didInsertFrame)
            return;

        framesArray = renderTreeBuilder.GetFrames().Array; // Refresh in case it mutated due to the expansion

        for (var otherFrameIndex = elementFrameIndex; otherFrameIndex >= 0; otherFrameIndex--)
        {
            ref var otherFrame = ref framesArray[otherFrameIndex];
            switch (otherFrame.FrameTypeField)
            {
                case RenderTreeFrameType.Element:
                {
                    var otherFrameSubtreeLength = otherFrame.ElementSubtreeLengthField;
                    var otherFrameEndIndexExcl  = otherFrameIndex + otherFrameSubtreeLength;
                    if (otherFrameEndIndexExcl > elementFrameIndex) // i.e., contains the element we're inserting into
                        otherFrame.ElementSubtreeLengthField = otherFrameSubtreeLength + 1;

                    break;
                }
                case RenderTreeFrameType.Region:
                {
                    var otherFrameSubtreeLength = otherFrame.RegionSubtreeLengthField;
                    var otherFrameEndIndexExcl  = otherFrameIndex + otherFrameSubtreeLength;
                    if (otherFrameEndIndexExcl > elementFrameIndex) // i.e., contains the element we're inserting into
                        otherFrame.RegionSubtreeLengthField = otherFrameSubtreeLength + 1;

                    break;
                }
            }
        }
    }
}