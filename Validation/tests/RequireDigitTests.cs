// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;

namespace Wangkanai.Validation;

public class RequireDigitTests
{
	private readonly ITestOutputHelper _output;
	private readonly PropertyInfo _password = DigitModel.GetProperty(nameof(DigitModel.Password));

	public RequireDigitTests(ITestOutputHelper output)
	{
		_output = output;
	}

	[Fact]
	public void Mix()
	{
		var vm = new DigitModel { Password = "pass1234" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Empty(validations);
	}

	[Fact]
	public void Alphabet()
	{
		var vm = new DigitModel { Password = "password" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Collection(validations, v => v.ErrorMessage = "Digit is required");
	}

	[Fact]
	public void Numeric()
	{
		var vm = new DigitModel { Password = "123456" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		//Assert.Collection(validations, v=>v.ErrorMessage = "Digit is required");
		Assert.Empty(validations);
	}

	[Fact]
	public void Unique()
	{
		var vm = new DigitModel { Password = "aaa" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Collection(validations, v => v.ErrorMessage = "Digit is required");
	}

	[Fact]
	public void Duplicate()
	{
		var vm = new DigitModel { Password = "pass1234" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Empty(validations);
	}

	[Fact]
	public void Special()
	{
		var vm = new DigitModel { Password = "pass1234" };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Empty(validations);
	}

	[Fact]
	public void Empty()
	{
		var vm = new DigitModel { Password = string.Empty };

		var validations = vm.Validate(vm.Password, _password);
		validations.Print(_output);

		Assert.Collection(validations, v => v.ErrorMessage = "Digit is required");
	}
}
