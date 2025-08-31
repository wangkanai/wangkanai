// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an aggregate root in a domain-driven design context. An aggregate root is the primary entry point to interact with aggregates, ensuring all operations on an aggregate are controlled and consistent.</summary>
public interface IAggregateRoot : IAggregateRoot<int>, IKeyIntEntity;

/// <summary>Defines a contract for aggregate roots in a domain-driven design context with a primary key of type int. Ensures all operations on the aggregate are controlled and consistent through the aggregate root.</summary>
public interface IAggregateRoot<T> : IEntity<T> where T : IComparable<T>, IEquatable<T>;