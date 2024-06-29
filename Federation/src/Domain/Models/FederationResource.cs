// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

namespace Wangkanai.Federation.Models;

/// <summary>
/// Models the common data Api and identity resources.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class Resource
{
	private string DebuggerDisplay => Name ?? $"{{{typeof(Resource)}}}";

	/// <summary>
	/// Indicates if this resource is enabled. Defaults to true.
	/// </summary>
	public bool Enable { get; set; } = true;

	/// <summary>
	/// The unique name of the resource.
	/// </summary>
	public string Name { get; set; } = default!;

	public string DisplayName     { get; set; }
	public string Description     { get; set; }
	public bool   ShowInDiscovery { get; set; } = true;

	public ICollection<string>         UserClaims { get; set; } = new HashSet<string>();
	public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
}

public class FederationResource : Resource
{
	public FederationResource() { }
	public FederationResource(string name) : this(name, name) { }

	public FederationResource(string name, string displayName)
	{
		Name        = name;
		DisplayName = displayName;
	}
}
