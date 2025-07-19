// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Wangkanai.Markdown;

[DebuggerDisplay("{DebuggerDisplayString,nq}")]
public class MarkdownActionDescriptor : ActionDescriptor
{
	public string RelativePath { get; set; } = default!;
	public string ViewEnginePath { get; set; } = default!;
	public string? AreaName { get; set; }

	public MarkdownActionDescriptor() { }

	public MarkdownActionDescriptor(MarkdownActionDescriptor other)
	{
		other.ThrowIfNull();

		RelativePath = other.RelativePath;
		ViewEnginePath = other.ViewEnginePath;
		AreaName = other.AreaName;
	}

	internal virtual CompiledMarkdownActionDescriptor? CompiledMarkdownDescriptor { get; set; }

	internal Task<CompiledMarkdownActionDescriptor>? CompiledMarkdownActionDescriptorTask { get; set; }


	private string DebuggerDisplayString
		=> $"{nameof(ViewEnginePath)} = {ViewEnginePath}, {nameof(RelativePath)} = {RelativePath}";

	public override string? DisplayName
	{
		get
		{
			if (base.DisplayName == null && ViewEnginePath != null)
				base.DisplayName = ViewEnginePath;

			return base.DisplayName;
		}

		set => base.DisplayName = value.ThrowIfNull();
	}
}
