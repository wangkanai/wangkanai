// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Wangkanai.Validation.Extensions;
using Wangkanai.Validation.Models;

namespace Wangkanai.Validation;

public class RequireNonAlphanumericTests
{
   private readonly ITestOutputHelper _output;
   private readonly PropertyInfo      _password = NonAlphanumericModel.GetProperty(nameof(NonAlphanumericModel.Password));

   public RequireNonAlphanumericTests(ITestOutputHelper output) => _output = output;

   [Fact]
   public void Alphabet()
   {
      var vm = new NonAlphanumericModel { Password = "abc" };

      var validations = vm.Validate(vm.Password, _password);
      validations.Print(_output);

      Assert.Collection(validations, v => v.ErrorMessage = "Non Alphanumeric is required");
   }

   [Fact]
   public void Numeric()
   {
      var vm = new NonAlphanumericModel { Password = "123" };

      var validations = vm.Validate(vm.Password, _password);
      validations.Print(_output);

      Assert.Collection(validations, v => v.ErrorMessage = "Non Alphanumeric is required");
   }

   [Fact]
   public void Alphanumeric()
   {
      var vm = new NonAlphanumericModel { Password = "Abc123" };

      var validations = vm.Validate(vm.Password, _password);
      validations.Print(_output);

      Assert.Collection(validations, v => v.ErrorMessage = "Non Alphanumeric is required");
   }

   [Fact]
   public void NonAlphanumeric()
   {
      var vm = new NonAlphanumericModel { Password = "!@#&()–[{}]:;',?/*`~$^+=<>" };

      var validations = vm.Validate(vm.Password, _password);
      validations.Print(_output);

      Assert.Empty(validations);
   }

   [Fact]
   public void Mix()
   {
      var vm = new NonAlphanumericModel { Password = "@bc123!" };

      var validations = vm.Validate(vm.Password, _password);
      validations.Print(_output);

      Assert.Empty(validations);
   }
}