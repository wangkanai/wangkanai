// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.System.Domain.Common;

namespace Wangkanai.System.Domain;

public interface IDataEntity<TEntity, TModel>
{
    public TModel  ToModel(TModel   model);
    public TEntity FromModel(TModel model, PrimaryKeyResolvingMap pkMap);
    public void    Patch(TEntity    target);
}