// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an abstract base class that specializes the <see cref="Entity{T}"/> class with a string as its identifier type. This class provides a foundation for defining domain entities that utilize string-based unique identifiers.</summary>
/// <remarks>The <c>KeyStringEntity</c> class inherits the behavior and characteristics of the generic <see cref="Entity{T}"/> class, tailored to operate with a string identifier. It serves as a common base for entities in domain-driven design that require a string key for identification.</remarks>
public abstract class KeyStringEntity : Entity<string>;