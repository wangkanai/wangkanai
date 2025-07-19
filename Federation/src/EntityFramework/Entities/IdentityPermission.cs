// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Entities;

[Flags]
public enum IdentityPermission
{
	None = 0,      // 0000
	View = 1 << 0, // 0001
	Edit = 1 << 1, // 0010
	Create = 1 << 2, // 0100
	Delete = 1 << 3, // 1000
	All = ~None   // 1111
}
