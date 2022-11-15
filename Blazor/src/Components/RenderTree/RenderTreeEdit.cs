// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Runtime.InteropServices;

namespace Wangkanai.Blazor.Components.RenderTree;

[StructLayout(LayoutKind.Explicit)]
public readonly struct RenderTreeEdit
{
    [FieldOffset(0)]  public readonly RenderTreeEditType Type;
    [FieldOffset(4)]  public readonly int                SiblingIndex;
    [FieldOffset(8)]  public readonly int                ReferenceFrameIndex;
    [FieldOffset(8)]  public readonly int                MoveToSiblingIndex;
    [FieldOffset(16)] public readonly string?            RemovedAttributeName;

    private RenderTreeEdit(RenderTreeEditType type) : this()
    {
        Type = type;
    }

    private RenderTreeEdit(RenderTreeEditType type, int siblingIndex) : this()
    {
        Type         = type;
        SiblingIndex = siblingIndex;
    }

    private RenderTreeEdit(RenderTreeEditType type, int siblingIndex, int referenceFrameOrMoveToSiblingIndex) : this()
    {
        Type                = type;
        SiblingIndex        = siblingIndex;
        ReferenceFrameIndex = referenceFrameOrMoveToSiblingIndex;
    }

    private RenderTreeEdit(RenderTreeEditType type, int siblingIndex, string removedAttributeName) : this()
    {
        Type                 = type;
        SiblingIndex         = siblingIndex;
        RemovedAttributeName = removedAttributeName;
    }

    internal static RenderTreeEdit RemoveFrame(int siblingIndex)
    {
        return new(RenderTreeEditType.RemoveFrame, siblingIndex);
    }

    internal static RenderTreeEdit PrependFrame(int siblingIndex, int referenceFrameIndex)
    {
        return new(RenderTreeEditType.PrependFrame, siblingIndex, referenceFrameIndex);
    }

    internal static RenderTreeEdit UpdateText(int siblingIndex, int referenceFrameIndex)
    {
        return new(RenderTreeEditType.UpdateText, siblingIndex, referenceFrameIndex);
    }

    internal static RenderTreeEdit UpdateMarkup(int siblingIndex, int referenceFrameIndex)
    {
        return new(RenderTreeEditType.UpdateMarkup, siblingIndex, referenceFrameIndex);
    }

    internal static RenderTreeEdit SetAttribute(int siblingIndex, int referenceFrameIndex)
    {
        return new(RenderTreeEditType.SetAttribute, siblingIndex, referenceFrameIndex);
    }

    internal static RenderTreeEdit RemoveAttribute(int siblingIndex, string name)
    {
        return new(RenderTreeEditType.RemoveAttribute, siblingIndex, name);
    }

    internal static RenderTreeEdit StepIn(int siblingIndex)
    {
        return new(RenderTreeEditType.StepIn, siblingIndex);
    }

    internal static RenderTreeEdit StepOut()
    {
        return new(RenderTreeEditType.StepOut);
    }

    internal static RenderTreeEdit PermutationListEntry(int fromSiblingIndex, int toSiblingIndex)
    {
        return new(RenderTreeEditType.PermutationListEntry, fromSiblingIndex, toSiblingIndex);
    }

    internal static RenderTreeEdit PermutationListEnd()
    {
        return new(RenderTreeEditType.PermutationListEnd);
    }
}