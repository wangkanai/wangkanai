// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Options;

#nullable enable

public class TableConfiguration
{
	public string  Name   { get; }
	public string? Schema { get; }

	public TableConfiguration(string name)
		=> Name = name;

	public TableConfiguration(string name, string schema)
		: this(name)
		=> Schema = schema;
}