// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Common;

public class PrimaryKeyResolvingMap
{
	private readonly Dictionary<IKeyGuidEntity, IKeyGuidEntity> _resolvingMap = new();

	public void AddPair(IKeyGuidEntity transient, IKeyGuidEntity persistent)
		=> _resolvingMap[transient] = persistent;

	public void ResolvePrimaryKeys()
	{
		foreach (var pair in _resolvingMap)
		{
			if (pair.Key.Id == Guid.Empty && pair.Value.Id != Guid.Empty)
			{
				pair.Key.Id = pair.Value.Id;

				if (pair.Key is IUserAuditable transient && pair.Value is IUserAuditable presistent)
				{
					transient.CreatedBy = presistent.CreatedBy;
					transient.Created   = presistent.Created;
					transient.UpdatedBy = presistent.UpdatedBy;
					transient.Updated   = presistent.Updated;
				}
			}
		}
	}
}