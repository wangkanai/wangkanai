// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequireFalseAttribute : ValidationAttribute
{
	public RequireFalseAttribute()
		: base(() => "Unchecked is required") { }

	public override bool IsValid(object value)
	{
		return value switch
		       {
			       null        => true,
			       bool actual => actual == false,
			       _           => false
		       };
	}
}