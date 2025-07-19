// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>
/// Represents a generic repository interface for managing entities of type <typeparamref name="T"/>.
/// Provides basic operations such as attaching, adding, updating, and deleting entities,
/// and supporting transactional unit-of-work patterns.
/// </summary>
/// <typeparam name="T">The type of entity that the repository will manage. Must be a reference type.</typeparam>
public interface IRepository<in T> : IDisposable where T : class
{
	/// <summary>
	/// Represents a transactional unit-of-work property that is associated with
	/// the repository instance. It is used to coordinate changes to multiple
	/// objects and persist them as a single transaction.
	/// </summary>
	/// <remarks>
	/// The UnitOfWork property provides access to an implementation of the
	/// <see cref="IUnitOfWork"/> interface, which encapsulates the responsibility
	/// of committing changes made during a transaction.
	/// </remarks>
	IUnitOfWork UnitOfWork { get; }

	/// <summary>
	/// Attaches the specified entity to the repository context.
	/// This method is commonly used when the entity is already present
	/// and needs to be tracked or associated with the repository.
	/// </summary>
	/// <param name="item">The entity instance to attach. Must be of type <typeparamref name="T"/>.</param>
	void Attach(T item);

	/// <summary>
	/// Adds the specified entity to the repository context.
	/// This method is typically used to mark a new entity for addition to the underlying data store.
	/// </summary>
	/// <param name="item">The entity instance to add. Must be of type <typeparamref name="T"/>.</param>
	void Add(T item);

	/// <summary>
	/// Updates the specified entity in the repository context.
	/// This method is used to modify an existing entity that is already tracked by the repository.
	/// </summary>
	/// <param name="item">The entity instance to update. Must be of type <typeparamref name="T"/>.</param>
	void Update(T item);

	/// <summary>
	/// Deletes the specified entity from the repository context.
	/// This method is used to mark an existing entity for removal from the underlying data store.
	/// </summary>
	/// <param name="item">The entity instance to delete. Must be of type <typeparamref name="T"/>.</param>
	void Delete(T item);
}

/// <summary>
/// Represents an asynchronous repository interface for managing entities of type <typeparamref name="T"/>.
/// Provides operations for attaching, adding, updating, and deleting entities in an asynchronous manner,
/// supporting transactional unit-of-work patterns.
/// </summary>
/// <typeparam name="T">The type of entity that the repository will manage. Must be a reference type.</typeparam>
public interface IAsyncRepository<T> : IAsyncDisposable where T : class
{
	/// <summary>
	/// Provides a property for accessing the transactional unit-of-work
	/// implementation within repository operations.
	/// </summary>
	/// <remarks>
	/// This property delivers an instance of <see cref="IUnitOfWork"/> or
	/// <see cref="IUnitOfWorkAsync"/>, facilitating the coordination and
	/// persistence of entity changes within a transactional context.
	/// The unit-of-work instance ensures atomicity and consistency
	/// for related database operations.
	/// </remarks>
	IUnitOfWorkAsync UnitOfWork { get; }

	/// <summary>
	/// Asynchronously attaches the specified entity to the repository context.
	/// This method is commonly used when the entity is already present
	/// and needs to be tracked or associated with the repository in an asynchronous manner.
	/// </summary>
	/// <param name="item">The entity instance to attach. Must be of type <typeparamref name="T"/>.</param>
	/// <returns>A task that represents the asynchronous attach operation. The task result contains the attached entity.</returns>
	Task<T> AttachAsync(T item);

	/// <summary>
	/// Asynchronously adds the specified entity to the repository context.
	/// This method is typically used to add a new entity instance to the repository
	/// in an asynchronous manner.
	/// </summary>
	/// <param name="item">The entity instance to add. Must be of type <typeparamref name="T"/>.</param>
	/// <returns>A task that represents the asynchronous add operation. The task result contains the added entity.</returns>
	Task<T> AddAsync(T item);

	/// <summary>
	/// Asynchronously updates the specified entity in the repository.
	/// This method is used to modify an existing entity instance and save the changes
	/// in an asynchronous manner.
	/// </summary>
	/// <param name="item">The entity instance to update. Must be of type <typeparamref name="T"/>.</param>
	/// <returns>A task that represents the asynchronous update operation. The task result contains the updated entity.</returns>
	Task<T> UpdateAsync(T item);

	/// <summary>
	/// Asynchronously deletes the specified entity from the repository context.
	/// This method removes the entity instance and marks it for deletion in an asynchronous manner.
	/// </summary>
	/// <param name="item">The entity instance to delete. Must be of type <typeparamref name="T"/>.</param>
	/// <returns>A task that represents the asynchronous delete operation. The task result contains the deleted entity.</returns>
	Task<T> DeleteAsync(T item);
}
