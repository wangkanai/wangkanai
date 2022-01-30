using System.Reflection;
using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;
using Xunit;
using Xunit.Abstractions;

namespace Wangkanai.Validation.Tests;

public class RequireBooleanTests
{
    private readonly ITestOutputHelper _output;
    private readonly PropertyInfo      _wannaTrue  = BooleanModel.GetProperty(nameof(BooleanModel.WannaTrue));
    private readonly PropertyInfo      _wannaFalse = BooleanModel.GetProperty(nameof(BooleanModel.WannaFalse));

    public RequireBooleanTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ExpectedTrue_ActualTrue()
    {
        var vm = new BooleanModel {WannaTrue = true};

        var validations = vm.Validate(vm.WannaTrue, _wannaTrue);
        validations.Print(_output);

        Assert.Empty(validations);
    }

    [Fact]
    public void ExpectedTrue_ActualFalse()
    {
        var vm = new BooleanModel {WannaTrue = false};

        var validations = vm.Validate(vm.WannaTrue, _wannaTrue);
        validations.Print(_output);

        Assert.Collection(validations, v =>
                              v.ErrorMessage = "Checked is required");
    }

    [Fact]
    public void ExpectedFalse_ActualTrue()
    {
        var vm = new BooleanModel {WannaFalse = true};

        var validations = vm.Validate(vm.WannaFalse, _wannaFalse);
        validations.Print(_output);

        Assert.Collection(validations, v =>
                              v.ErrorMessage = "Unchecked is required");
    }

    [Fact]
    public void ExpectedFalse_ActualFalse()
    {
        var vm = new BooleanModel {WannaTrue = false};

        var validations = vm.Validate(vm.WannaFalse, _wannaFalse);
        validations.Print(_output);

        Assert.Empty(validations);
    }
}