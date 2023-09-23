// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Models;

public class IntEntity : KeyIntEntity
{
	public IntEntity() => Id = 1;
}

public class TransientIntEntity : KeyIntEntity { }