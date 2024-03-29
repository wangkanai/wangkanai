﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

public sealed class CommandArgument
{
	public string?      Value          => Values.FirstOrDefault();
	public List<string> Values         { get; } = new();
	public string?      Name           { get; set; }
	public string?      Description    { get; set; }
	public bool         ShowInHelpText { get; set; }
	public bool         MultipleValues { get; set; }
}