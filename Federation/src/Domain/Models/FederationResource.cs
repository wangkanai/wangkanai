// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;

namespace Wangkanai.Federation.Models;

/// <summary>
/// Models a user federation resource.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class FederationResource : BaseResource
{
	private string DebuggerDisplay => Name ?? $"{{{typeof(FederationResource)}}}";

	/// <summary>
	/// specifies whether the user can de-select the scope of the consent screen.
	/// </summary>
	public bool Required { get; set; }

	/// <summary>
	/// Specifies whether the consent screen will emphasize this scope.
	/// </summary>
	public bool Emphasize { get; set; } = false;

	/// <summary>
	/// initializes a new instance of the <see cref="FederationResource"/> class.
	/// </summary>
	public FederationResource() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="FederationResource"/> class.
	/// </summary>
	/// <param name="name">The name</param>
	/// <param name="claims">List of associated user claims that should be included when this resource is requested.</param>
	public FederationResource(string name, IEnumerable<string> claims)
		: this(name, name, claims) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="FederationResource"/> class.
	/// </summary>
	/// <param name="name">The name</param>
	/// <param name="displayName">The display name</param>
	/// <param name="claims">List of associated user claims that should be included when this resource is requested.</param>
	public FederationResource(string name, string displayName, IEnumerable<string> claims)
	{
		name.ThrowIfNull();
		claims.ThrowIfEmpty("Must provide at least one claim type");

		Name = name;
		DisplayName = displayName;

		foreach (var claim in claims)
			Claims.Add(claim);
	}
}
