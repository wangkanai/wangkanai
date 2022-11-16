// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;

namespace Wangkanai.Blazor.Components;

public readonly struct ElementReference
{
    private static long _nextIdForWebAssemblyOnly = 1;

    public string Id { get; }

    public ElementReferenceContext? Context { get; }

    public ElementReference(string id, ElementReferenceContext? context)
    {
        Id      = id;
        Context = context;
    }

    public ElementReference(string id) : this(id, null)
    {
    }

    internal static ElementReference CreateWithUniqueId(ElementReferenceContext? context)
    {
        return new(CreateUniqueId(), context);
    }

    private static string CreateUniqueId()
    {
        if (OperatingSystem.IsBrowser())
        {
            var id = Interlocked.Increment(ref _nextIdForWebAssemblyOnly);
            return id.ToString(CultureInfo.InvariantCulture);
        }

        return Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture);
    }
}