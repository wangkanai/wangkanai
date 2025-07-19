// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Wangkanai.Webmaster.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class MetaKeywordsAttribute(params string[] keywords)
	: Attribute
{
	private readonly List<string> _keywords = new List<string>(keywords);
}

public abstract class MetaAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		context.ThrowIfNull();
	}
}
