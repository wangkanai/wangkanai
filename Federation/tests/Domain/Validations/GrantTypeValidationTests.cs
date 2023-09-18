// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Validations;

public class GrantTypeValidationTests
{
	[Fact]
	public void Validate_Null()
	{
		IEnumerable<string> grantTypes = null!;
		Assert.Throws<ArgumentNullException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_Empty()
	{
		var grantTypes = new List<string>();
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Empty(grantTypes.ValidateGrantTypes());
	}

	[Fact]
	public void Validate_HasSpaces()
	{
		var grantTypes = new List<string>();
		grantTypes.Add("o o");
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_Duplicate()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_Implicit()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_Hybrid()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Hybrid);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_AuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_ImplicitAndHybrid()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Hybrid);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_ImplicitAndAuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_HybridAndAuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Hybrid);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void Validate_All()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Hybrid);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void HashSet_Implicit()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void HashSet_Hybrid()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Hybrid);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void HashSet_AuthorizationCode()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void HashSet_ImplicitAndHybrid()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.Add(GrantType.Hybrid));
	}
	
	[Fact]
	public void HashSet_ImplicitAndAuthorizationCode()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.Add(GrantType.AuthorizationCode));
	}

	[Fact]
	public void HashSet_NullOrEmpty()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		Assert.Throws<ArgumentNullException>(() => grantTypes.Add(null!));
		Assert.Throws<ArgumentEmptyException>(() => grantTypes.Add(string.Empty));
	}
	
	[Fact]
	public void HashSet_HasSpaces()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		Assert.Throws<InvalidOperationException>(() => grantTypes.Add("o o"));
	}
	
	[Fact]
	public void HashSet_Duplicate()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Implicit);
		Assert.Single(grantTypes);
	}

	[Fact]
	public void HashSet_Add_And_Remove()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Remove(GrantType.Implicit);
		Assert.Empty(grantTypes);
	}
	
	[Fact]
	public void HashSet_Add_And_Clear()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Clear();
		Assert.Empty(grantTypes);
	}

	[Fact]
	public void HashSet_Contains()
	{
		var grantTypes = new GrantTypeValidationHashSet();
		grantTypes.Add(GrantType.Implicit);
		Assert.True(grantTypes.Contains(GrantType.Implicit));
	}
}