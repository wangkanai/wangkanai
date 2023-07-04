// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Validations;

public class ParsedScopeValue
{
	public ParsedScopeValue(string value)
		: this(value, value, null) { }

	public ParsedScopeValue(string value, string name, string parameter)
	{
		Value     = value.ThrowIfNullOrWhitespace();
		Name      = name.ThrowIfNullOrWhitespace();
		Parameter = parameter;
	}

	public string Value     { get; set; }
	public string Name      { get; set; }
	public string Parameter { get; set; }
}