// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;

namespace Wangkanai.Validation;

public class RequireBooleanTests(ITestOutputHelper output)
{
	private readonly PropertyInfo _wannaFalse = BooleanModel.GetProperty(nameof(BooleanModel.WannaFalse));
	private readonly PropertyInfo _wannaTrue = BooleanModel.GetProperty(nameof(BooleanModel.WannaTrue));
	private readonly PropertyInfo _nullableFalse = BooleanModel.GetProperty(nameof(BooleanModel.NullableFalse));
	private readonly PropertyInfo _nullableTrue = BooleanModel.GetProperty(nameof(BooleanModel.NullableTrue));

	[Fact]
	public void ExpectedTrue_ActualTrue()
	{
		var vm = new BooleanModel { WannaTrue = true };
		var validations = vm.Validate(vm.WannaTrue, _wannaTrue);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void ExpectedTrue_ActualFalse()
	{
		var vm = new BooleanModel { WannaTrue = false };
		var validations = vm.Validate(vm.WannaTrue, _wannaTrue);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Checked is required");
	}

	[Fact]
	public void ExpectedFalse_ActualTrue()
	{
		var vm = new BooleanModel { WannaFalse = true };
		var validations = vm.Validate(vm.WannaFalse, _wannaFalse);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Unchecked is required");
	}

	[Fact]
	public void ExpectedFalse_ActualFalse()
	{
		var vm = new BooleanModel { WannaTrue = false };
		var validations = vm.Validate(vm.WannaFalse, _wannaFalse);
		validations.Print(output);
		Assert.Empty(validations);
	}

	[Fact]
	public void ExpectedTrue_ActualNull()
	{
		var vm = new BooleanModel { NullableTrue = null };
		var validations = vm.Validate(vm.NullableTrue!, _nullableTrue);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Checked is required");
	}

	[Fact]
	public void ExpectedFalse_ActualNull()
	{
		var vm = new BooleanModel { NullableFalse = null };
		var validations = vm.Validate(vm.NullableFalse!, _nullableFalse);
		validations.Print(output);
		Assert.Collection(validations, v => v.ErrorMessage = "Unchecked is required");
	}
}
