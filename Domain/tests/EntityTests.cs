// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Tests;

public class EntityTests
{
	[Fact]
	public void NewGuidEntity_ShouldHaveId()
	{
		var entity = new GuidEntity();
		Assert.NotEqual(Guid.NewGuid(), entity.Id);
	}

	[Fact]
	public void GuidEntity_IsTransient_ShouldBeTrue()
	{
		var entity = new TransientGuidEntity();
		Assert.True(entity.IsTransient());
	}       
	
	[Fact]
	public void GuidEntity_IsTransient_ShouldBeFalse()
	{
		var entity = new GuidEntity();
		Assert.False(entity.IsTransient());
	}

	[Fact]
	public void NewIntEntity_ShouldHaveId()
	{
		var entity = new IntEntity();
		Assert.NotEqual(0, entity.Id);
	}
	
	[Fact]
	public void IntEntity_IsTransient_ShouldBeFalse()
	{
		var entity = new IntEntity();
		Assert.False(entity.IsTransient());
	}

	[Fact]
	public void IntEntity_IsTransient_ShouldBeTrue()
	{
		var entity = new TransientIntEntity();
		Assert.True(entity.IsTransient());
	}
}