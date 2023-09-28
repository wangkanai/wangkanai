// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Xunit;

namespace Wangkanai.Domain;

public class GenericTypeInfoTests
{
	[Fact]
	public void TypeIsNull()
	{
		GenericTypeInfo<Parent> _null = null!;

		Assert.Throws<NullReferenceException>(() => _null.GetType());
		Assert.Throws<NullReferenceException>(() => _null.GetService<Parent>());
		Assert.Throws<NullReferenceException>(() => _null.IsAssignableTo(nameof(Parent)));
		Assert.Throws<NullReferenceException>(() => _null.AllSubclasses);
	}
}