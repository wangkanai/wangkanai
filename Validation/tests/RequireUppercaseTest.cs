// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;

namespace Wangkanai.Validation;

public class RequireUppercaseTest(ITestOutputHelper output)
{
	private readonly PropertyInfo _password = UppercaseModel.GetProperty(nameof(UppercaseModel.Password));

	[Fact]
	public void Uppercase()
	{
		var vm = new UppercaseModel { Password = "ABC" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void Lowercase()
	{
		var vm = new UppercaseModel { Password = "abc" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Uppercase is required");
	}

	[Fact]
	public void Mix()
	{
		var vm = new UppercaseModel { Password = "Abc" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Empty(validations);
	}
}
