// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

/// <summary>Represents an event handler interface for handling cancellable domain events.</summary>
/// <typeparam name="T">The type of the domain message being handled, which must implement the <see cref="IDomainMessage"/> interface.</typeparam>
public interface ICancellableEventHandler<in T> where T : IDomainMessage
{
	/// <summary>Handles a domain message with a potential to cancel the operation.</summary>
	/// <param name="message">The domain message to be handled. Must implement the <see cref="IDomainMessage"/> interface.</param>
	/// <param name="token">An optional cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task Handle(T message, CancellationToken token = default);
}

/// <summary>Defines an asynchronous event handler interface for processing cancellable domain events.</summary>
/// <typeparam name="T">The type of the domain message to be processed, which must implement the <see cref="IDomainMessage"/> interface.</typeparam>
public interface ICancellableDomainEventHandlerAsync<in T> where T : IDomainMessage
{
	/// <summary>Processes a domain message asynchronously, with support for cancellation of the operation.</summary>
	/// <param name="message">The domain message to be processed. Must implement the <see cref="IDomainMessage"/> interface.</param>
	/// <param name="token">An optional cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task HandleAsync(T message, CancellationToken token = default);
}
