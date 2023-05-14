// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.ApplicationModels;

public class MarkdownRouteModel
{
	public MarkdownRouteModel(string relativePath, string viewEnginePath)
		: this(relativePath, viewEnginePath, areaName: null) { }

	public MarkdownRouteModel(string relativePath, string viewEnginePath, string? areaName)
	{
		RelativePath   = relativePath   ?? throw new ArgumentNullException(nameof(relativePath));
		ViewEnginePath = viewEnginePath ?? throw new ArgumentNullException(nameof(viewEnginePath));
		AreaName       = areaName;

		Properties  = new Dictionary<object, object?>();
		Selectors   = new List<SelectorModel>();
		RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
	
	public MarkdownRouteModel(MarkdownRouteModel other)
	{
		ArgumentNullException.ThrowIfNull(other);

		RelativePath              = other.RelativePath;
		ViewEnginePath            = other.ViewEnginePath;
		AreaName                  = other.AreaName;
		RouteParameterTransformer = other.RouteParameterTransformer;

		Properties  = new Dictionary<object, object?>(other.Properties);
		Selectors   = new List<SelectorModel>(other.Selectors.Select(m => new SelectorModel(m)));
		RouteValues = new Dictionary<string, string>(other.RouteValues, StringComparer.OrdinalIgnoreCase);
	}

	public string  RelativePath   { get; }
	public string  ViewEnginePath { get; }
	public string? AreaName       { get; }

	public IDictionary<object, object?> Properties  { get; }
	public IDictionary<string, string>  RouteValues { get; }

	public IList<SelectorModel> Selectors { get; }

	public IOutboundParameterTransformer? RouteParameterTransformer { get; set; }
}