// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Tests.Models;

public class IntEntity : Entity<int>
{
	public IntEntity() => Id = 1;
}

public class IntEntityTransient : Entity<int> { }