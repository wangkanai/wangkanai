// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Messages;

public interface IDomainHandler<in T> where T : IDomainMessage
{
	Task Handle(T message);
}

public interface IDomainHandlerAsync<in T> where T : IDomainMessage
{
	Task HandleAsync(T message);
}
