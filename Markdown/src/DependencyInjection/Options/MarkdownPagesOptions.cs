// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.Markdown.ApplicationModels;

namespace Wangkanai.Markdown.DependencyInjection.Options;

public class MarkdownPagesOptions : IEnumerable<ICompatibilitySwitch>
{
	private readonly IReadOnlyList<ICompatibilitySwitch> _switches = Array.Empty<ICompatibilitySwitch>();

	private string _root = "/Pages";

	public MarkdownConventionCollection Conventions { get; internal set; } = new();

	public IEnumerator<ICompatibilitySwitch> GetEnumerator() => _switches.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => _switches.GetEnumerator();

	public string RootDirectory
	{
		get => _root;
		set
		{
			value.ThrowIfNullOrEmpty<ArgumentException>(Resources.ArgumentCannotBeNullOrEmpty, nameof(value));
			if (value[0] != '/')
				throw new ArgumentException(Resources.PathMustBeRootRelativePath, nameof(value));

			_root = value;
		}
	}
}
