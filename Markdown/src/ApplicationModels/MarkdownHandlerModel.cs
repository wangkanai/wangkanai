// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Wangkanai.Markdown.ApplicationModels;

[DebuggerDisplay("MarkdownHandlerModel: Name={" + nameof(MarkdownHandlerModel.Name) + "}")]
public class MarkdownHandlerModel : ICommonModel
{
	public MarkdownHandlerModel(
		MethodInfo handlerMethod,
		IReadOnlyList<object> attributes)
	{
		MethodInfo = handlerMethod.ThrowIfNull();
		Attributes = attributes.ThrowIfNull();

		Parameters = new List<MarkdownParameterModel>();
		Properties = new Dictionary<object, object?>();
	}

	public MarkdownHandlerModel(MarkdownHandlerModel other)
	{
		other.ThrowIfNull();

		MethodInfo = other.MethodInfo;
		HandlerName = other.HandlerName;
		HttpMethod = other.HttpMethod;
		Name = other.Name;
		Page = other.Page;

		Attributes = new List<object>(other.Attributes);
		Properties = new Dictionary<object, object?>(other.Properties);
		Parameters = new List<MarkdownParameterModel>(other.Parameters.Select(p => new MarkdownParameterModel(p) { Handler = this }));
	}

	public MethodInfo MethodInfo { get; }

	public string HttpMethod { get; set; } = default!;
	public string? HandlerName { get; set; }
	public string Name { get; set; } = default!;

	public IList<MarkdownParameterModel> Parameters { get; }
	public MarkdownApplicationModel Page { get; set; } = default!;
	public IReadOnlyList<object> Attributes { get; }
	public IDictionary<object, object?> Properties { get; }

	MemberInfo ICommonModel.MemberInfo => MethodInfo;
}
