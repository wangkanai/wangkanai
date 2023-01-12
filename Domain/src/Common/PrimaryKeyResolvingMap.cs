// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;

namespace Wangkanai.Domain.Common;

public class PrimaryKeyResolvingMap
{
    private readonly Dictionary<IEntity, IEntity> _resolvingMap = new Dictionary<IEntity, IEntity>();

    public void AddPair(IEntity transient, IEntity persistent)
    {
        _resolvingMap[transient] = persistent;
    }

    public void ResolvePrimaryKeys()
    {
        foreach (var pair in _resolvingMap)
        {
            if (pair.Key.Id == Guid.Empty && pair.Value.Id != Guid.Empty)
            {
                pair.Key.Id = pair.Value.Id;

                if (pair.Key is IUserAuditable transient && pair.Value is IUserAuditable presistent)
                {
                    transient.CreatedBy   = presistent.CreatedBy;
                    transient.CreatedDate = presistent.CreatedDate;
                    transient.UpdatedBy   = presistent.UpdatedBy;
                    transient.UpdatedDate = presistent.UpdatedDate;
                }
            }
        }
    }
}