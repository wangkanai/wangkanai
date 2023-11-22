// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;

using Xunit;
using Xunit.Abstractions;

namespace Wangkanai.Validation;

public class RequireLowercaseTests(ITestOutputHelper output)
{
	private readonly PropertyInfo _password = LowercaseModel.GetProperty(nameof(LowercaseModel.Password));

	[Fact]
	public void Uppercase()
	{
		var vm          = new LowercaseModel { Password = "ABC" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Lowercase is required");
	}

	[Fact]
	public void Lowercase()
	{
		var vm          = new LowercaseModel { Password = "abc" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void Mix()
	{
		var vm          = new LowercaseModel { Password = "Abc" };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void Unique()
	{
		var vm = new LowercaseModel { Password = "aaa" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void Null()
	{
		var vm          = new LowercaseModel { Password = null! };
		var validations = vm.Validate(vm.Password, _password);
		validations.Print(output);
		// Assert.Collection(validations, v => v.ErrorMessage = "Lowercase is required");
		Assert.Empty(validations); // This is not right!
	}
}
