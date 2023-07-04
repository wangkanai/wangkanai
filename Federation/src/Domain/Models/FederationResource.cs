// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

namespace Wangkanai.Federation.Models;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class FederationResource
{
	private string DebuggerDisplay => Name ?? $"{{{typeof(FederationResource)}}}";

	public bool   Enable          { get; set; } = true;
	public string Name            { get; set; }
	public string DisplayName     { get; set; }
	public string Description     { get; set; }
	public bool   ShowInDiscovery { get; set; } = true;

	public ICollection<string>         UserClaims { get; set; } = new HashSet<string>();
	public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
}