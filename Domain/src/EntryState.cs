// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

[Flags]
public enum EntryState
{
	Detached  = 1 << 0,
	Unchanged = 1 << 1,
	Added     = 1 << 2,
	Deleted   = 1 << 3,
	Modified  = 1 << 4
}
