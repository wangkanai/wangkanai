// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Xunit;

namespace Wangkanai.Domain;

public class GenericTypeInfoTests
{
	private GenericTypeInfo<Parent> parent = new(typeof(Parent));
	private GenericTypeInfo<Child>  child  = new(typeof(Child));

	[Fact]
	public void TypeIsNull()
	{
		GenericTypeInfo<Parent> _null = null!;

		Assert.Throws<NullReferenceException>(() => _null.GetType());
		Assert.Throws<NullReferenceException>(() => _null.GetService<Parent>());
		Assert.Throws<NullReferenceException>(() => _null.IsAssignableTo(nameof(Parent)));
		Assert.Throws<NullReferenceException>(() => _null.GetAllSubclasses());
	}

	[Fact]
	public void ParentToParent()
	{
		Assert.NotEqual(parent.Type, parent.Type.BaseType);
		Assert.Equal(parent.Type, parent.GetAllSubclasses().First());
	}

	[Fact]
	public void ChildToParent()
	{
		Assert.Contains(parent.Type, child.GetAllSubclasses());
		Assert.Contains(child.Type, child.GetAllSubclasses());
	}

	[Fact]
	public void AssignNewTypeName()
	{
		var custom = parent.WithTypeName("custom");
		Assert.Equal("custom", custom.TypeName);
	}
	
	[Fact]
	public void AssignNewService()
	{
		var custom = parent.WithService(new Parent());
		Assert.NotNull(custom.GetService<Parent>());
	}
	
	[Fact]
	public void WithServiceReturnSelf()
	{
		var custom = parent.WithService(new Parent());
		Assert.Equal(parent, custom);
	}
	
	[Fact]
	public void AssignNewMappedType()
	{
		var custom = parent.MapToType<Parent>();
		Assert.Equal(typeof(Parent), custom.MappedType);
	}
	
	[Fact]
	public void AssignNewFactory()
	{
		var custom = parent.WithFactory(() => new Parent());
		Assert.NotNull(custom.Factory);
	}
	
	[Fact]
	public void AssignNewSetupAction()
	{
		var custom = parent.WithSetupAction(p => p.Name = "custom");
		Assert.NotNull(custom.SetupAction);
	}
}