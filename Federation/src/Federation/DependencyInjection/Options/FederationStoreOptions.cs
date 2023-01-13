// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

/// <summary>
/// Used for store specific federation options
/// </summary>
public sealed class FederationStoreOptions
{
	/// <summary>
	/// If set to a positive number, the default OnModelCreating will use this as the max length for any
	/// properties used as keys, i.e. ClientId, ScopeId, SubjectId, SessionId, and ConsentId.
	/// </summary>
	public int MaxLengthForKeys { get; set; }

	/// <summary>
	/// If set to true, the store must protect all personally identifiable information (PII) that it stores.
	/// This will be enforced by requiring the store to implement <see cref="IProtectedUserStore{TKey}"/>.
	/// </summary>
	public bool EncryptData { get; set; }
}