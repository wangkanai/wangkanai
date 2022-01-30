using System.Reflection;
using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;
using Xunit;
using Xunit.Abstractions;

namespace Wangkanai.Validation.Tests;

public class RequireUppercaseTest
{
    private readonly ITestOutputHelper _output;
    private readonly PropertyInfo      _password = UppercaseModel.GetProperty(nameof(UppercaseModel.Password));

    public RequireUppercaseTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Uppercase()
    {
        var vm = new UppercaseModel() {Password = "ABC"};

        var validations = vm.Validate(vm.Password, _password);
        validations.Print(_output);

        Assert.Empty(validations);
    }

    [Fact]
    public void Lowercase()
    {
        var vm = new UppercaseModel {Password = "abc"};

        var validations = vm.Validate(vm.Password, _password);
        validations.Print(_output);
            
        Assert.Collection(validations, v =>
                              v.ErrorMessage = "Uppercase is required");
    }

    [Fact]
    public void Mix()
    {
        var vm = new UppercaseModel {Password = "Abc"};

        var validations = vm.Validate(vm.Password, _password);
        validations.Print(_output);

        Assert.Empty(validations);
    }
}