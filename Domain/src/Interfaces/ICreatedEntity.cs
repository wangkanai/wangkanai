// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an entity that tracks the creation date and time.</summary>
/// <remarks>The <see cref="ICreatedEntity"/> interface defines a contract for entities that require the ability to capture and store the timestamp of their creation. </remarks>
public interface ICreatedEntity
{
   /// <summary>Gets or sets the date and time when the entity was created.</summary>
   /// <remarks>The <c>Created</c> property is used to track the creation timestamp of the entity. It is a nullable <see cref="DateTime"/> that can be set upon initialization or updated as needed. It is commonly used in scenarios that require audit or temporal tracking.</remarks>
   DateTime? Created { get; set; }
}