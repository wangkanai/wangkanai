// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

public interface IEventHandler<in T> : IHandler<T> where T : IEvent { }