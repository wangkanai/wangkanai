// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Entities;

public class IdentityClientRedirectUri : IdentityClientRedirectUri<Guid>
{
	public IdentityClientRedirectUri() { }

	public IdentityClientRedirectUri(Guid clientId, string redirectUri) : this()
	{
		ClientId = clientId;
		RedirectUri = redirectUri;
	}
}

public class IdentityClientRedirectUri<TKey> where TKey : IEquatable<TKey>
{
	public IdentityClientRedirectUri() { }

	public IdentityClientRedirectUri(TKey clientId, string redirectUri) : this()
	{
		ClientId = clientId;
		RedirectUri = redirectUri;
	}


	public virtual TKey Id { get; set; }
	public virtual TKey ClientId { get; set; }
	public virtual IdentityClient Client { get; set; }
	public virtual string RedirectUri { get; set; }
}
