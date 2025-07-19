// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Messages;

/// <summary>Defines a contract for handling domain messages of a specified type. This interface is designed to process domain-specific messages within a system.</summary>
/// <typeparam name="T">The type of domain message being handled. This type must implement <see cref="IDomainMessage"/>.</typeparam>
public interface IDomainHandler<in T> where T : IDomainMessage
{
	/// <summary>Handles the processing of a domain message of the specified type. </summary>
	/// <param name="message">The domain message to be handled. The message must be an instance of a type that implements <see cref="IDomainMessage"/>.</param>
	/// <returns>A task that represents the asynchronous operation for handling the domain message.</returns>
	Task Handle(T message);
}

/// <summary>Represents an asynchronous contract for handling domain messages of a specified type. This interface is intended for processing domain-specific messages in an asynchronous manner within a system.</summary>
/// <typeparam name="T">The type of domain message to be handled. This type must implement <see cref="IDomainMessage"/>.</typeparam>
public interface IDomainHandlerAsync<in T> where T : IDomainMessage
{
	/// <summary>Asynchronously handles the processing of a domain message of the specified type.</summary>
	/// <param name="message">The domain message to be handled. The message must be an instance of a type that implements <see cref="IDomainMessage"/>.</param>
	/// <returns>A task that represents the asynchronous operation for handling the domain message.</returns>
	Task HandleAsync(T message);
}
