// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Wangkanai.Validation.Extensions;

public static class ValidationExtensions
{
	public static IList<ValidationResult> Validate<T>(this T model) where T : class
	{
		var result = new List<ValidationResult>();
		var context = new ValidationContext(model, null, null);

		Validator.TryValidateObject(model, context, result, true);
		if (model is IValidatableObject validatable)
			validatable.Validate(context);

		return result;
	}

	public static ICollection<ValidationResult> Validate<T>(this T model, object value, PropertyInfo property) where T : class
	{
		var result = new List<ValidationResult>();
		var context = new ValidationContext(model, null, null) { MemberName = property.Name };

		Validator.TryValidateProperty(value, context, result);

		return result;
	}

	public static void Print(this ICollection<ValidationResult> results, ITestOutputHelper output)
	{
		output.WriteLine($"Validations: {results.Count}");
		foreach (var result in results)
			output.WriteLine($"\t{result.MemberNames.First()} : {result.ErrorMessage}");
	}
}
