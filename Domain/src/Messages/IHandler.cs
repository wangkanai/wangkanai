// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

namespace Wangkanai.System.Domain.Messages;

public interface IHandler<in T> where T : IMessage
{
    Task Handle(T message);
}