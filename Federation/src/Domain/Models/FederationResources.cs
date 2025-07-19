// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class FederationResources
{
	public bool OfflineAccess { get; set; }
	public ICollection<FederationResource> Resources { get; set; } = new HashSet<FederationResource>();
	public ICollection<FederationResource> ApiResources { get; set; } = new HashSet<FederationResource>();
	public ICollection<FederationScope> Scopes { get; set; } = new List<FederationScope>();

	public FederationResources() { }

	public FederationResources(FederationResources other)
		: this(other.Resources, other.ApiResources, other.Scopes)
	{
		OfflineAccess = other.OfflineAccess;
	}

	public FederationResources(
		IEnumerable<FederationResource> identityResources,
		IEnumerable<FederationResource> apiResources,
		IEnumerable<FederationScope> apiScopes)
	{
		if (identityResources?.Any() == true)
			Resources = new HashSet<FederationResource>(identityResources);
		if (apiResources?.Any() == true)
			ApiResources = new HashSet<FederationResource>(apiResources);
		if (apiScopes?.Any() == true)
			Scopes = new HashSet<FederationScope>(apiScopes);
	}
}
