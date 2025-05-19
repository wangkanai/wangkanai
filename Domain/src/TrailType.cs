// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Specifies the type of operation recorded in an audit trail.</summary>
public enum TrailType : byte
{
	None   = 0,
	Create = 1,
	Update = 2,
	Delete = 3
}
