// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

public interface IDomainEventPublisher
{
	Task Publish<T>(T @event, CancellationToken token = default)
		where T : class, IGuidDomainEvent;
}
