// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a contract for publishing domain events within the application.
/// This interface is responsible for dispatching events to all applicable
/// subscribers, enabling asynchronous handling of domain-related concerns.
/// </summary>
public interface IDomainEventPublisher
{
	/// <summary>Publishes a domain event of a specified type, allowing subscribers to handle it asynchronously.</summary>
	/// <typeparam name="T">The type of the domain event being published. Must implement IGuidDomainEvent.</typeparam>
	/// <param name="event">The domain event to be published.</param>
	/// <param name="token">An optional cancellation token to cancel the publish operation.</param>
	/// <returns>A task representing the asynchronous operation of publishing the event.</returns>
	Task Publish<T>(T @event, CancellationToken token = default) where T : class, IGuidDomainEvent;
}
