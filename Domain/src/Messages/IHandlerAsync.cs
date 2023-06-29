// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

namespace Wangkanai.Domain.Messages;

public interface IHandlerAsync<in T> where T : IMessage
{
	Task HandleAsync(T message);
}