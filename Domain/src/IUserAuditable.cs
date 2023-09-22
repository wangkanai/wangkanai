// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public interface IUserAuditable: ICreatedEntity, IUpdatedEntity
{
	DateTime Created   { get; set; }
	DateTime Updated   { get; set; }
	string   CreatedBy { get; set; }
	string   UpdatedBy { get; set; }
}