// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public interface IEntity : IEntity<int>
{
    int Id { get; set; }
}

public interface IEntity<T>
{
    T Id { get; set; }
}