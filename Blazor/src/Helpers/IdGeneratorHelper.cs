using System;

namespace Wangkanai.Blazor.Helpers;

public static class IdGeneratorHelper
{
    public static string Generate(string prefix)
        => prefix + Guid.NewGuid();
}