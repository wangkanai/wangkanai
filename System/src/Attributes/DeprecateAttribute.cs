// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

namespace Wangkanai;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum |
                AttributeTargets.Interface | AttributeTargets.Constructor | AttributeTargets.Method |
                AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event |
                AttributeTargets.Delegate,
    Inherited = false)]
public sealed class DeprecateAttribute : Attribute
{
    public string? Message { get; }
    public bool    IsError { get; }

    public DeprecateAttribute() { }

    public DeprecateAttribute(string? message)
        => Message = message;

    public DeprecateAttribute(string? message, bool error)
        : this(message)
        => IsError = error;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum |
                AttributeTargets.Interface | AttributeTargets.Constructor | AttributeTargets.Method |
                AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event |
                AttributeTargets.Delegate,
    Inherited = false)]
public class DeprecateAttribute<TNew> : Attribute
{
    public string? Replacement { get; }
    public string? Message     { get; }
    public bool    IsError     { get; }

    public DeprecateAttribute() 
        => Replacement = typeof(TNew).Name;

    public DeprecateAttribute(string? message)
        => Message = message;

    public DeprecateAttribute(string? message, bool error)
        : this(message)
        => IsError = error;
}