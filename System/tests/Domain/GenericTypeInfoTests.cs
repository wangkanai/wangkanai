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
		Assert.Throws<NullReferenceException>(() => _null.AllSubclasses);
	}

	[Fact]
	public void ParentToParent()
	{
		Assert.NotEqual(parent.Type, parent.Type.BaseType);
		Assert.Equal(parent.Type, parent.AllSubclasses.First());
	}

	[Fact]
	public void ChildToParent()
	{
		Assert.Contains(parent.Type, child.AllSubclasses);
		Assert.Contains(child.Type, child.AllSubclasses);
	}

	[Fact]
	public void AssignNewTypeName()
	{
		var custom = parent.WithTypeName("custom");
		Assert.Equal("custom", custom.TypeName);
	}
}