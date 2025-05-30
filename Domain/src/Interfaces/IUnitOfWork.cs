// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>
/// Defines a contract for a unit-of-work pattern that allows managing
/// data modifications and persisting them in a single transaction.
/// </summary>
/// <remarks>
/// The unit-of-work pattern ensures atomicity and consistency by encapsulating
/// a set of operations that can be committed together. The interface provides
/// methods for saving changes, which can be implemented for specific data
/// storage technologies.
/// </remarks>
public interface IUnitOfWork
{
	/// <summary>Persists all changes made within the unit of work to the underlying data storage.</summary>
	/// <returns>The number of state entries written to the underlying data storage.</returns>
	int Commit();
}

/// <summary>
/// Defines an asynchronous variant of the unit-of-work pattern that provides
/// mechanisms for managing data modifications and persisting them within
/// a transactional context.
/// </summary>
/// <remarks>
/// The asynchronous unit-of-work pattern extends the traditional unit-of-work
/// by incorporating asynchronous operations, enabling more efficient handling
/// of I/O-bound data operations. This interface is designed to support
/// scenarios where tasks, such as saving changes, need to be performed
/// asynchronously to improve responsiveness and scalability.
/// </remarks>
public interface IUnitOfWorkAsync
{
	/// <summary>Asynchronously persists all changes made within the unit of work to the underlying data storage.</summary>
	/// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the underlying data storage.</returns>
	Task<int> CommitAsync();
}
