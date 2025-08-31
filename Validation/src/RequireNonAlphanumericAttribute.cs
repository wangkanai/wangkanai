// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequireNonAlphanumericAttribute : ValidationAttribute
{
   public RequireNonAlphanumericAttribute()
      : base(() => "Non Alphanumeric is required") { }

   public override bool IsValid(object? value)
      => value switch
         {
            null          => true,
            string actual => !actual.All(char.IsLetterOrDigit),
            _             => false
         };
}