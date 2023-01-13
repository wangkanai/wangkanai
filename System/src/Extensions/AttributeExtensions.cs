// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class AttributeExtensions
{
	[DebuggerStepThrough]
    public static string[] SplitString(this Attribute attribute, string original, char separator)
        => string.IsNullOrEmpty(original)
               ? Array.Empty<string>()
               : original.Split(separator)
                         .Select(x => x.Trim())
                         .Where(x => !string.IsNullOrEmpty(x))
                         .ToArray();
}