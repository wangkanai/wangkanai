// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Wangkanai.Markdown.Infrastructure;

public class MarkdownBoundPropertyDescriptor : ParameterDescriptor, IPropertyInfoParameterDescriptor
{
	public PropertyInfo Property { get; set; } = default!;

	PropertyInfo IPropertyInfoParameterDescriptor.PropertyInfo => Property;
}
