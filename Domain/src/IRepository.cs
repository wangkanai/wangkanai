// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

namespace Wangkanai.Domain;

public interface IRepository : IDisposable
{
	IUnitOfWork UnitOfWork { get; }

	void Attach<T>(T entity) where T : class;
	void Add<T>(T    item) where T : class;
	void Update<T>(T item) where T : class;
	void Delete<T>(T item) where T : class;

	Task<T> AttachAsync<T>(T entity) where T : class;
	Task<T> AddAsync<T>(T    item) where T : class;
	Task<T> UpdateAsync<T>(T item) where T : class;
	Task<T> DeleteAsync<T>(T item) where T : class;
}