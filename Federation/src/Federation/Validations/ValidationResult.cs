// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Validations;

public class ValidationResult
{
	public bool   IsError     { get; set; } = true;
	public string Error       { get; set; }
	public string Description { get; set; }
}