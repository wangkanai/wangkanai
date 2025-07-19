// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;

using Wangkanai.Federation.Validations;

namespace Wangkanai.Federation.Models;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class FederationClient
{
	private ICollection<string> _allowedGrantTypes;
	private string DebuggerDisplay => ClientId ?? $"{{{typeof(FederationClient)}}}";

	public bool Enabled { get; set; } = true;
	public string ClientId { get; set; } = default!;
	public string ClientName { get; set; } = default!;
	public string ClientUri { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string ProtocolType { get; set; } = DomainConstants.ProtocolTypes.OpenIdConnect;
	public bool RequireConsent { get; set; } = false;
	public bool AllowRememberConsent { get; set; } = true;
	public bool RequirePkce { get; set; } = true;
	public bool AllowPlainTextPkce { get; set; } = false;
	public bool RequireRequestObject { get; set; } = false;
	public bool RequireDPoP { get; set; }


	public ICollection<FederationSecret> ClientSecrets { get; set; } = new HashSet<FederationSecret>();
	private ICollection<string> AllowGrantTypes { get; set; } = new GrantTypeValidationHashSet();

	public ICollection<string> AllowedGrantTypes
	{
		get => _allowedGrantTypes;
		set => _allowedGrantTypes = new GrantTypeValidationHashSet(value.ValidateGrantTypes());
	}
}
