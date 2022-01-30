// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Wangkanai.Webmaster.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MetaKeywordsAttribute : Attribute
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