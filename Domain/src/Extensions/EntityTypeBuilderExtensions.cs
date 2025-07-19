// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Provides extension methods for configuring entity type properties using
/// the <see cref="EntityTypeBuilder{TEntity}"/> from Entity Framework Core.
/// These methods help apply conventions and default values for specific
/// properties of entities that implement certain domain-driven design interfaces.
/// </summary>
public static class EntityTypeBuilderExtensions
{
	/// <summary>
	/// Configures the entity type to generate a new Guid value for the Id property
	/// when a new entity is added to the database context. This method is specifically
	/// designed to work with entities that implement the <see cref="IEntity{T}"/> interface
	/// with a Guid type parameter.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="IEntity{Guid}"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void NewGuidOnAdd<T>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity<Guid>
		=> builder.Property(x => x.Id)
				  .ValueGeneratedOnAdd();

	/// <summary>
	/// Configures the entity type to generate a new key value for the Id property
	/// when a new entity is added to the database context. This method is specifically
	/// designed to work with entities that implement the <see cref="IEntity{T}"/> interface
	/// with an integer type parameter.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="IEntity{int}"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void NewKeyOnAdd<T>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity<int>
		=> builder.Property(x => x.Id)
				  .ValueGeneratedOnAdd();

	/// <summary>
	/// Configures the entity type to generate a new value for the Id property
	/// when a new entity is added to the database context. This method is designed
	/// to work with entities that implement the <see cref="IEntity{T}"/> interface,
	/// allowing for any comparable and equatable type as the key.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="IEntity{TKey}"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key used by the entity. Must implement <see cref="IComparable{T}"/> and <see cref="IEquatable{T}"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void NewKeyOnAdd<T, TKey>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity<TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		=> builder.Property(x => x.Id)
				  .ValueGeneratedOnAdd();

	/// <summary>
	/// Configures the entity type to set a default value of the current date and time
	/// for the Created property when a new entity is added to the database context.
	/// This method is intended for entities implementing the <see cref="ICreatedEntity"/> interface.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="ICreatedEntity"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void HasDefaultCreated<T>(this EntityTypeBuilder<T> builder)
		where T : class, ICreatedEntity
		=> builder.Property(x => x.Created)
				  .HasDefaultValue(DateTime.Now)
				  .ValueGeneratedOnAdd();

	/// <summary>
	/// Configures the entity type to set a default value for the Updated property
	/// and to mark it as a value that is automatically generated when the entity
	/// is updated. This method is intended for entities that implement the
	/// <see cref="IUpdatedEntity"/> interface.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="IUpdatedEntity"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void HasDefaultUpdated<T>(this EntityTypeBuilder<T> builder)
		where T : class, IUpdatedEntity
		=> builder.Property(x => x.Updated)
				  .HasDefaultValue(DateTime.Now)
				  .ValueGeneratedOnUpdate();

	/// <summary>
	/// Configures the entity type to set default values and value generation strategies
	/// for the Created and Updated properties. The Created property is assigned a default
	/// value of the current date and time and is generated when a new entity is added.
	/// The Updated property is assigned a default value of the current date and time and
	/// is generated or updated when the entity is added or modified.
	/// </summary>
	/// <typeparam name="T">The type of the entity being configured. Must implement <see cref="ICreatedEntity"/> and <see cref="IUpdatedEntity"/>.</typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> used to configure the entity type.</param>
	public static void HasDefaultCreatedAndUpdated<T>(this EntityTypeBuilder<T> builder)
		where T : class, ICreatedEntity, IUpdatedEntity
	{
		builder.HasDefaultCreated();

		builder.Property(x => x.Updated)
			   .HasDefaultValue(DateTime.Now)
			   .ValueGeneratedOnAddOrUpdate();
	}
}
