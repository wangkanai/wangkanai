// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

namespace Wangkanai.Domain;

public interface IEntity : IEntity<Guid> { }

public interface IEntity<T>
{
	T    Id { get; set; }
	bool IsTransient();
}