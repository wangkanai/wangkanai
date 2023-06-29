// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Domain.Events;

public interface IEventPublisher
{
	Task Publish<T>(T @event, CancellationToken token = default)
		where T : class, IEvent;
}