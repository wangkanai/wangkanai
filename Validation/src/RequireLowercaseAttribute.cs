// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequireLowercaseAttribute : ValidationAttribute
{
	public RequireLowercaseAttribute()
		: base(() => "Lowercase is required") { }

	public override bool IsValid(object? value)
		=> value switch
		{
			null => true, // Required duty
			string actual => actual.Any(char.IsLower),
			_ => false
		};
}
