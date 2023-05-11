// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

namespace Wangkanai.System.Domain;

public interface IUnitOfWork
{
    int       Commit();
    Task<int> CommitAsync();
}