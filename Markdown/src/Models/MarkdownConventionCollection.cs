// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Wangkanai.Markdown.Models;

public class MarkdownConventionCollection : Collection<IMarkdownConvention>
{
	private readonly IServiceProvider? _serviceProvider;
	private          MvcOptions?       _mvcOptions;

	public MarkdownConventionCollection()
		: this((IServiceProvider?)null) { }

	public MarkdownConventionCollection(IList<IMarkdownConvention> conventions)
		: base(conventions) { }

	internal MarkdownConventionCollection(IServiceProvider? serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	internal MvcOptions MvcOptions
	{
		get
		{
			_mvcOptions ??= _serviceProvider?.GetRequiredService<IOptions<MvcOptions>>().Value;
			return _mvcOptions;
		}
	}

	internal static void EnsureValidPageName(string pageName, string argumentName = "pageName")
	{
		pageName.ThrowIfNullOrWhitespace<ArgumentException>("Value cannot be null or empty.", argumentName);

		if (pageName[0] != '/' || pageName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
			throw new ArgumentException(
				$"'{pageName}' is not a valid page name. A page name is path relative to the Markdown Pages root directory that starts with a leading forward slash ('/') and does not contain the file extension e.g \" /Users /Edit\".",
				argumentName);
	}
}