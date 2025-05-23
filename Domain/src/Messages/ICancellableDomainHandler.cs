// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Domain.Messages;

public interface ICancellableDomainHandler<in T> where T : IDomainMessage
{
	Task Handle(T message, CancellationToken token = default);
}

public interface ICancellableDomainHandlerAsync<in T> where T : IDomainMessage
{
	Task HandleAsync(T message, CancellationToken token = default);
}
