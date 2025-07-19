// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Validations;

public class ValidationResult
{
	public bool IsError { get; set; } = true;
	public string Error { get; set; }
	public string Description { get; set; }
}
