// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>
/// Represents a value object in the domain-driven design context.
/// Value objects are immutable and compared based on their properties
/// rather than their identity.
/// </summary>
public interface IValueObject;

/// <summary>
/// Defines the contract for a value object within a domain-driven design context.
/// Value objects are inherently immutable and their equality is determined
/// based on their constituent properties rather than a unique identifier.
/// </summary>
public interface IValueObject<T> where T : class;
