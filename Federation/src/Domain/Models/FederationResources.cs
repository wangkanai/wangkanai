// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class FederationResources
{
	public bool                               OfflineAccess     { get; set; }
	public ICollection<IdentityResource>      IdentityResources { get; set; } = new HashSet<IdentityResource>();
	public ICollection<FederationApiResource> ApiResources      { get; set; } = new HashSet<FederationApiResource>();
	public ICollection<FederationApiScope>    ApiScopes         { get; set; } = new List<FederationApiScope>();

	public FederationResources() { }

	public FederationResources(FederationResources other)
		: this(other.IdentityResources, other.ApiResources, other.ApiScopes)
	{
		OfflineAccess = other.OfflineAccess;
	}

	public FederationResources(
		IEnumerable<IdentityResource>      identityResources,
		IEnumerable<FederationApiResource> apiResources,
		IEnumerable<FederationApiScope>    apiScopes)
	{
		if (identityResources?.Any() == true)
			IdentityResources = new HashSet<IdentityResource>(identityResources);
		if (apiResources?.Any() == true)
			ApiResources = new HashSet<FederationApiResource>(apiResources);
		if (apiScopes?.Any() == true)
			ApiScopes = new HashSet<FederationApiScope>(apiScopes);
	}
}