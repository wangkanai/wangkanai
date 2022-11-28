// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

namespace Wangkanai.Domain;

public interface IRepository : IDisposable
{
    IUnitOfWork UnitOfWork { get; }

    void Attach<T>(T entity) where T : class;
    void Add<T>(T    item) where T : class;
    void Update<T>(T item) where T : class;
    void Delete<T>(T item) where T : class;
}