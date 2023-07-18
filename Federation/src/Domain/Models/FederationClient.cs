// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

namespace Wangkanai.Federation.Models;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class FederationClient
{
	private string DebuggerDisplay => ClientId ?? $"{{{typeof(FederationClient)}}}";

	public bool   Enabled              { get; set; } = true;
	public string ClientId             { get; set; } = default!;
	public string ClientName           { get; set; } = default!;
	public string ClientUri            { get; set; } = default!;
	public string Description          { get; set; } = default!;
	public string ProtocolType         { get; set; } = DomainConstants.ProtocolTypes.OpenIdConnect;
	public bool   RequireConsent       { get; set; } = false;
	public bool   AllowRememberConsent { get; set; } = true;
	public bool   RequirePkce          { get; set; } = true;
	public bool   AllowPlainTextPkce   { get; set; } = false;
	public bool   RequireRequestObject { get; set; } = false;
	public bool   RequireDPoP          { get; set; }
	 


	public ICollection<FederationSecret> ClientSecrets { get; set; } = new HashSet<FederationSecret>();
}