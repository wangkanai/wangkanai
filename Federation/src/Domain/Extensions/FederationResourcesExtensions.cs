// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Models;

public static class FederationResourcesExtensions
{
	public static IEnumerable<string> ToScopeNames(this FederationResources resources)
	{
		var names = resources.Resources.Select(x => x.Name).ToList();
		names.AddRange(resources.Scopes.Select(x => x.Name));
		if (resources.OfflineAccess)
			names.Add(DomainConstants.StandardScopes.OfflineAccess);

		return names;
	}
}
