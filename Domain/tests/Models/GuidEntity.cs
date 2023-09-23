// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Models;

public class GuidEntity : KeyGuidEntity
{
	public GuidEntity() => Id = Guid.NewGuid();
}

public class TransientGuidEntity : KeyGuidEntity
{
	public TransientGuidEntity() => Id = Guid.Empty;
}