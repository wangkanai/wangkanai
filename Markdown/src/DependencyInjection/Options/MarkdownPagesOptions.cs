// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.Markdown.Models;

namespace Wangkanai.Markdown.DependencyInjection.Options;

public class MarkdownPagesOptions : IEnumerable<ICompatibilitySwitch>
{
	private readonly IReadOnlyList<ICompatibilitySwitch> _switches = Array.Empty<ICompatibilitySwitch>();

	private string _root = "/Markdowns";

	public MarkdownConventionCollection Conventions { get; internal set; } = new();

	public string RootDirectory
	{
		get => _root;
		set
		{
			value.ThrowIfNullOrWhitespace<ArgumentException>(Resources.ArgumentCannotBeNullOrEmpty);
			if (value[0] != '/')
				throw new ArgumentException(Resources.PathMustBeRootRelativePath, nameof(value));

			_root = value;
		}
	}

	public IEnumerator<ICompatibilitySwitch> GetEnumerator()
		=> _switches.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> _switches.GetEnumerator();
}