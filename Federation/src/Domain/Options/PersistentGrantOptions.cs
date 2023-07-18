// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Options;

/// <summary>Options for how persisted grants are persisted.</summary>
public sealed class PersistentGrantOptions
{
	/// <summary>Data protect the persisted grants "data" column. </summary>
	public bool ProtectData                         { get; set; } = true;
	
	/// <summary>
	/// Delete one time only refresh tokens when they are used to obtain a new token.
	/// If false, one time only refresh tokens will instead be marked as Consumed.
	/// </summary>
	public bool DeleteOneTimeOnlyRefreshTokensOnUse { get; set; } = true;
}