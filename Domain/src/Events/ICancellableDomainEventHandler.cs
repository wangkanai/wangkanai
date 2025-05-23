// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

public interface ICancellableEventHandler<in T>
	where T : IDomainMessage
{
	Task Handle(T message, CancellationToken token = default);
}

public interface ICancellableDomainEventHandlerAsync<in T>
	where T : IDomainMessage
{
	Task HandleAsync(T message, CancellationToken token = default);
}
