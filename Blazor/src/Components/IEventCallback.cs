// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components;

internal interface IEventCallback
{
    bool HasDelegate { get; }

    object? UnpackForRenderTree();
}