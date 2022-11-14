// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Rendering;

internal readonly struct ParameterViewLifetime
{
    private readonly RenderBatchBuilder _owner;
    private readonly int                _stamp;

    public static readonly ParameterViewLifetime Unbound;

    public ParameterViewLifetime(RenderBatchBuilder owner)
    {
        _owner = owner;
        _stamp = owner.ParameterViewValidityStamp;
    }

    public void AssertNotExpired()
    {
        if (_owner != null && _owner.ParameterViewValidityStamp != _stamp)
            throw new InvalidOperationException($"The {nameof(ParameterView)} instance can no longer be read because it has expired. {nameof(ParameterView)} can only be read synchronously and must not be stored for later use.");
    }
}