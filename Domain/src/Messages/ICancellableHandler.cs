// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Domain.Messages;

public interface ICancellableHandler<in T> where T : IMessage
{
	Task Handle(T message, CancellationToken token = default);
}

public interface ICancellableHandlerAsync<in T> where T : IMessage
{
	Task HandleAsync(T message, CancellationToken token = default);
}