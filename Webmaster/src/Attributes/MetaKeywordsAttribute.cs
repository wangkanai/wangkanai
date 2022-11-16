// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Filters;

namespace Wangkanai.Webmaster.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class MetaKeywordsAttribute : Attribute
{
    private readonly List<string> _keywords;

    public MetaKeywordsAttribute(params string[] keywords)
    {
        _keywords = new List<string>(keywords);
    }
}

public abstract class MetaAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Check.NotNull(context);
    }
}