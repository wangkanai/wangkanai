// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Wangkanai.Markdown.ApplicationModels;

[DebuggerDisplay("MarkdownPropertyModel: Name={PropertyName}")]
public class MarkdownPropertyModel : ParameterModelBase, ICommonModel
{
	public MarkdownPropertyModel(
		PropertyInfo          propertyInfo,
		IReadOnlyList<object> attributes)
		: base(propertyInfo.PropertyType, attributes)
	{
		PropertyInfo = propertyInfo.ThrowIfNull();
	}

	public MarkdownPropertyModel(MarkdownPropertyModel other)
		: base(other)
	{
		other.ThrowIfNull();

		Page         = other.Page;
		BindingInfo  = other.BindingInfo == null ? null : new BindingInfo(other.BindingInfo);
		PropertyInfo = other.PropertyInfo;
	}

	public MarkdownApplicationModel Page         { get; set; } = default!;
	public PropertyInfo             PropertyInfo { get; }

	public string PropertyName
	{
		get => Name;
		set => Name = value;
	}

	MemberInfo ICommonModel.MemberInfo => PropertyInfo;
}