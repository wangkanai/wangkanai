// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Audit.Models;

/// <summary>Represents an entity that includes information about its creation datetime. It inherits from the <see cref="KeyGuidEntity"/> base class and implements the
/// <see cref="ICreatedEntity"/> interface.</summary>
public class CreatedEntity : KeyGuidEntity, ICreatedEntity
{
   /// <summary>Gets or sets the date and time when the entity was created.</summary>
   /// <remarks>The <see cref="Created"/> property is nullable and is typically set when the entity is initially added to the data store. It can be used to track the creation timestamp of the entity. This property is configured to have a default value of the current date and time when being added to the database.</remarks>
   public DateTime? Created { get; set; }
}