// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components.Rendering;

internal sealed class RenderBatchBuilder : IDisposable
{
    // A value that, if changed, causes expiry of all ParameterView instances issued
    // for this RenderBatchBuilder. This is to prevent invalid reads from arrays that
    // may have been returned to the shared pool.

    // Primary result data
    public ArrayBuilder<RenderTreeDiff> UpdatedComponentDiffs   { get; } = new();
    public ArrayBuilder<int>            DisposedComponentIds    { get; } = new();
    public ArrayBuilder<ulong>          DisposedEventHandlerIds { get; } = new();

    // Buffers referenced by UpdatedComponentDiffs
    public ArrayBuilder<RenderTreeEdit>  EditsBuffer           { get; } = new(64);
    public ArrayBuilder<RenderTreeFrame> ReferenceFramesBuffer { get; } = new(64);

    // State of render pipeline
    public Queue<RenderQueueEntry> ComponentRenderQueue   { get; } = new();
    public Queue<int>              ComponentDisposalQueue { get; } = new();

    // Scratch data structure for understanding attribute diffs.
    public Dictionary<string, int> AttributeDiffSet { get; } = new();

    public int ParameterViewValidityStamp { get; private set; }

    internal StackObjectPool<Dictionary<object, KeyedItemInfo>> KeyedItemInfoDictionaryPool { get; } = new(10, () => new Dictionary<object, KeyedItemInfo>());

    public void Dispose()
    {
        EditsBuffer.Dispose();
        ReferenceFramesBuffer.Dispose();
        UpdatedComponentDiffs.Dispose();
        DisposedComponentIds.Dispose();
        DisposedEventHandlerIds.Dispose();
    }

    public void ClearStateForCurrentBatch()
    {
        // This method is used to reset the builder back to a default state so it can
        // begin building the next batch. That means clearing all the tracked state, but
        // *not* clearing ComponentRenderQueue because that may hold information about
        // the next batch we want to build. We shouldn't ever need to clear
        // ComponentRenderQueue explicitly, because it gets cleared as an aspect of
        // processing the render queue.

        EditsBuffer.Clear();
        ReferenceFramesBuffer.Clear();
        UpdatedComponentDiffs.Clear();
        DisposedComponentIds.Clear();
        DisposedEventHandlerIds.Clear();
        AttributeDiffSet.Clear();
    }

    public RenderBatch ToBatch()
    {
        return new(
            UpdatedComponentDiffs.ToRange(),
            ReferenceFramesBuffer.ToRange(),
            DisposedComponentIds.ToRange(),
            DisposedEventHandlerIds.ToRange());
    }

    public void InvalidateParameterViews()
    {
        // Wrapping is fine because all that matters is whether a snapshotted value matches
        // the current one. There's no plausible case where it wraps around and happens to
        // increment all the way back to a previously-snapshotted value on the exact same
        // call that's checking the value.
        if (ParameterViewValidityStamp == int.MaxValue)
            ParameterViewValidityStamp = int.MinValue;
        else
            ParameterViewValidityStamp++;
    }
}