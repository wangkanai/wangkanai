// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

namespace Wangkanai.Federation.Models;

/// <summary>
/// Models a user federation resource.
/// </summary>
[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
public class FederationResource : AbstractResource
{
	private string DebuggerDisplay => Name ?? $"{{{typeof(FederationResource)}}}";

	public FederationResource() { }

	public FederationResource(string name, IEnumerable<string> claims)
		: this(name, name, claims) { }

	public FederationResource(string name, string displayName, IEnumerable<string> claims)
	{
		name.ThrowIfNull();
		claims.ThrowIfEmpty("Must provide at least one claim type");

		Name        = name;
		DisplayName = displayName;

		foreach (var claim in claims)
			Claims.Add(claim);
	}
}
