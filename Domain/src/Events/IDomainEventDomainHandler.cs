// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

/// <summary>Represents a domain handler interface designed to process domain events that implement the <see cref="IGuidDomainEvent"/>
/// interface. This is a contract for defining event-specific handlers within a domain-driven design framework.</summary>
/// <typeparam name="T">The type of domain event being handled, constrained to types implementing <see cref="IGuidDomainEvent"/>.</typeparam>
public interface IDomainEventDomainHandler<in T> : IDomainHandler<T> where T : IGuidDomainEvent { }