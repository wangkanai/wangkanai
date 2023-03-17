// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class Secret
{
	public string    Description { get; set; }
	public string    Value       { get; set; }
	public DateTime? Expiration  { get; set; }
	public string    Type        { get; set; }

	public Secret()
	{
		Type = FederationConstants.SecretTypes.SharedSecret;
	}

	public Secret(string value, DateTime? expiration = null)
		: this()
	{
		Value      = value;
		Expiration = expiration;
	}

	public Secret(string value, string description, DateTime? expiration = null)
		: this()
	{
		Description = description;
		Value       = value;
		Expiration  = expiration;
	}
}