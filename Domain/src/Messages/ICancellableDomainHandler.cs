// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Domain.Messages;

/// <summary>Represents a handler for processing domain messages that supports cancellation.</summary>
/// <typeparam name="T">The type of the domain message, which must implement <see cref="IDomainMessage"/>.</typeparam>
public interface ICancellableDomainHandler<in T> where T : IDomainMessage
{
	/// <summary>Handles the processing of a domain message with support for cancellation.</summary>
	/// <typeparam name="T">The type of the domain message being handled, implementing <see cref="IDomainMessage"/>.</typeparam>
	/// <param name="message">The domain message to process.</param>
	/// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task Handle(T message, CancellationToken token = default);
}

/// <summary>Represents an asynchronous handler for processing domain messages that supports cancellation.</summary>
/// <typeparam name="T">The type of the domain message, which must implement <see cref="IDomainMessage"/>.</typeparam>
public interface ICancellableDomainHandlerAsync<in T> where T : IDomainMessage
{
	/// <summary>Handles the processing of a domain message asynchronously with support for cancellation.</summary>
	/// <typeparam name="T">The type of the domain message being handled, implementing <see cref="IDomainMessage"/>.</typeparam>
	/// <param name="message">The domain message to process.</param>
	/// <param name="token">A cancellation token that can be used by other objects or threads to observe cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation of processing the domain message.</returns>
	Task HandleAsync(T message, CancellationToken token = default);
}
