// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Domain.Messages;

public interface ICancellableHandlerAsync<in T> where T : IMessage
{
	Task HandleAsync(T message, CancellationToken token = default);
}