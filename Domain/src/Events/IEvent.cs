// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

public interface IEvent : IEntity, IMessage
{
	int            Version   { get; set; }
	DateTimeOffset TimeStamp { get; set; }
}

public interface IEvent<T> : IEntity<T>, IMessage
{
	int            Version   { get; set; }
	DateTimeOffset TimeStamp { get; set; }
}