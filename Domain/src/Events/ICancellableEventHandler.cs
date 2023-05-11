// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;

using Wangkanai.System.Domain.Messages;

namespace Wangkanai.System.Domain.Events;

public interface ICancellableEventHandler<in T>
    where T : IMessage
{
    Task Handle(T message, CancellationToken token = default);
}