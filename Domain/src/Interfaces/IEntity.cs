// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents a generic entity interface that defines a unique identifier and allows determination of whether the entity is transient. This interface is used as a base contract for entities within a domain-driven design context.</summary>
/// <typeparam name="T">The type of the unique identifier for the entity. It must implement <see cref="IComparable{T}"/> and
/// <see cref="IEquatable{T}"/>. </typeparam>
public interface IEntity<T> where T : IComparable<T>, IEquatable<T>
{
   /// <summary>Gets or sets the unique identifier for the entity. This property is used to uniquely distinguish an individual entity within the domain. The type of this identifier depends on the generic type parameter specified when implementing the
   /// <see cref="IEntity{T}"/> or derived <see cref="Entity{T}"/> base class.</summary>
   T Id { get; set; }

   /// <summary>Determines whether the current entity is transient. An entity is considered transient if it does not yet have a definitive identifier assigned to it. Typically, transient entities are newly created and have not been persisted to a data store.</summary>
   /// <returns>Returns <c>true</c> if the entity is transient; otherwise, <c>false</c>.</returns>
   bool IsTransient();
}