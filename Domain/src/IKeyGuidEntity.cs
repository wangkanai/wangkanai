// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public interface IKeyByteEntity : IEntity<byte>;

public interface IKeyIntEntity : IEntity<int>;

public interface IKeyLongEntity : IEntity<long>;

public interface IKeyGuidEntity : IEntity<Guid>;

public interface IKeyStringEntity : IEntity<string>;

public interface IEntity<T> // where T : IComparable<T>
{
	T    Id { get; set; }
	bool IsTransient();
}
