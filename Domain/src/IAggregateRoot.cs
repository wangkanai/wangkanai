// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public interface IAggregateRoot : IAggregateRoot<int>, IKeyIntEntity;

public interface IGuidAggregateRoot : IAggregateRoot<Guid>, IKeyGuidEntity;

public interface IAggregateRoot<T> : IEntity<T>;