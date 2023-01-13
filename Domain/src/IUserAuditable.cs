// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

namespace Wangkanai.Domain;

public interface IUserAuditable
{
	DateTime  CreatedDate { get; set; }
	DateTime? UpdatedDate { get; set; }
	string    CreatedBy   { get; set; }
	string    UpdatedBy   { get; set; }
}