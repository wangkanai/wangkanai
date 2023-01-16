// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

[Flags]
public enum IdentityPermission
{
	None   = 0,
	View   = 1 << 0,
	Edit   = 1 << 1,
	Create = 1 << 2,
	Delete = 1 << 3,
	All    = ~None
}