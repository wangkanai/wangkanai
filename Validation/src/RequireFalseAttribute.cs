// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequireFalseAttribute : ValidationAttribute
{
	public RequireFalseAttribute()
		: base(() => "Unchecked is required") { }

	public override bool IsValid(object? value)
		=> value switch
		{
			null => true,
			true => false,
			false => true,
			_ => false
		};
}
