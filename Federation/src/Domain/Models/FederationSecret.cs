// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class FederationSecret
{
	public DateTime? Expiration  { get; set; }
	public string?   Description { get; set; }
	public string?   Value       { get; }
	public string    Type        { get; } = DomainConstants.SecretTypes.SharedSecret;

	public FederationSecret() { }

	public FederationSecret(string value, DateTime? expiration = null)
		: this()
	{
		Value      = value;
		Expiration = expiration;
	}

	public FederationSecret(string value, string description, DateTime? expiration = null)
		: this()
	{
		Description = description;
		Value       = value;
		Expiration  = expiration;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			var hash = Convert.ToInt32(0x11);
			hash = hash * Convert.ToInt32(0x17) + (Value?.GetHashCode() ?? 0);
			hash = hash * Convert.ToInt32(0x17) + (Type?.GetHashCode() ?? 0);

			return hash;
		}
	}

	public override bool Equals(object? obj)
	{
		var other = obj as FederationSecret;

		if (obj == null)
			return false;
		if (other == null)
			return false;
		if (ReferenceEquals(other, this))
			return true;

		return string.Equals(other.Type, Type, StringComparison.Ordinal) &&
		       string.Equals(other.Value, Value, StringComparison.Ordinal);
	}
}
