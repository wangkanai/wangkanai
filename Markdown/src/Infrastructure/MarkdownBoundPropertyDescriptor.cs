// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Wangkanai.Markdown.Infrastructure;

public class MarkdownBoundPropertyDescriptor : ParameterDescriptor, IPropertyInfoParameterDescriptor
{
	public PropertyInfo Property { get; set; } = default!;

	PropertyInfo IPropertyInfoParameterDescriptor.PropertyInfo => Property;
}