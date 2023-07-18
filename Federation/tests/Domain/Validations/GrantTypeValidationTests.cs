// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Validations;

public class GrantTypeValidationTests
{
	[Fact]
	public void GrantType_Null()
	{
		IEnumerable<string> grantTypes = null!;
		Assert.Throws<ArgumentNullException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_Empty()
	{
		var grantTypes = new List<string>();
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Empty(grantTypes.ValidateGrantTypes());
	}

	[Fact]
	public void GrantType_HasSpaces()
	{
		var grantTypes = new List<string>();
		grantTypes.Add("o o");
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_Duplicate()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Implicit);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_Implicit()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_Hybrid()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Hybrid);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_AuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Equal(grantTypes, grantTypes.ValidateGrantTypes());
		Assert.Single(grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_ImplicitAndHybrid()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Hybrid);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_ImplicitAndAuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_HybridAndAuthorizationCode()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Hybrid);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
	
	[Fact]
	public void GrantType_All()
	{
		var grantTypes = new List<string>();
		grantTypes.Add(GrantType.Implicit);
		grantTypes.Add(GrantType.Hybrid);
		grantTypes.Add(GrantType.AuthorizationCode);
		Assert.Throws<InvalidOperationException>(() => grantTypes.ValidateGrantTypes());
	}
}