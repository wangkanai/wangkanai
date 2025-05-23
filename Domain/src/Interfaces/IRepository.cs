// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Threading.Tasks;

namespace Wangkanai.Domain;

public interface IRepository<in T> : IDisposable
	where T : class
{
	IUnitOfWork UnitOfWork { get; }

	void Attach(T item);
	void Add(T    item);
	void Update(T item);
	void Delete(T item);
}

public interface IAsyncRepository<T> : IAsyncDisposable
	where T : class
{
	IUnitOfWorkAsync UnitOfWork { get; }

	Task<T> AttachAsync(T item);
	Task<T> AddAsync(T    item);
	Task<T> UpdateAsync(T item);
	Task<T> DeleteAsync(T item);
}
