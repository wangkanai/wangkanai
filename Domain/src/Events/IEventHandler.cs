// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.System.Domain.Messages;

namespace Wangkanai.System.Domain.Events;

public interface IEventHandler<in T> : IHandler<T> where T : IEvent
{
    
}