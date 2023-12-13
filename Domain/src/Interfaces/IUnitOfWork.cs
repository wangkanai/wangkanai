// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

namespace Wangkanai.Domain;

public interface IUnitOfWork
{
	int Commit();
}

public interface IAsyncUnitOfWork
{
	Task<int> CommitAsync();
}