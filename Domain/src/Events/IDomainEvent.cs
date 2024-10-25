// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

public interface IDomainEvent : IKeyIntEntity, IDomainEvent<int>;

public interface IDomainEvent<T> : IEntity<T>, IDomainMessage
	where T : IComparable<T>, IEquatable<T>
{
	int            Version   { get; set; }
	DateTimeOffset TimeStamp { get; set; }
}
