// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Webmaster.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class MetaDescriptionAttribute(string description)
	: Attribute
{
	private readonly string _description = description;
}
