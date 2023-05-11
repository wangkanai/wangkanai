// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Wangkanai.System.Extensions;

namespace Wangkanai.Federation.Models;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class FederationApi : FederationResource
{
	private string DebuggerDisplay => Name ?? $"{{{typeof(FederationApi)}}}";

	public FederationApi() { }

	public FederationApi(string name)
		: this(name, name, null) { }

	public FederationApi(string name, string displayName)
		: this(name, displayName, null) { }

	public FederationApi(string name, string displayName, IEnumerable<string> claims)
	{
		name.ThrowIfNullOrWhitespace();

		Name        = name;
		DisplayName = displayName;

		if (claims.IsNullOrEmpty())
			return;

		foreach (var claim in claims)
			UserClaims.Add(claim);
	}

	public bool RequireApiIndicator { get; set; }

	public ICollection<FederationSecret> Secrets { get; set; } = new HashSet<FederationSecret>();
	public ICollection<string> Scopes  { get; set; } = new HashSet<string>();

	public ICollection<string> AllowedAccessTokenSigningAlgorithms { get; set; } = new HashSet<string>();
}