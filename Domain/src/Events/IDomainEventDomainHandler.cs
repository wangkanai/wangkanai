// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

public interface IDomainEventDomainHandler<in T>
	: IDomainHandler<T>
	where T : IGuidDomainEvent { }
