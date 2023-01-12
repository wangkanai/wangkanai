// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

public class IdentityClientCorsOrigin : IdentityClientCorsOrigin<string>
{
	public IdentityClientCorsOrigin()
	{
		Id = Guid.NewGuid().ToString();
	}
}

public class IdentityClientCorsOrigin<TKey> where TKey : IEquatable<TKey>
{
	public TKey                 Id       { get; set; }
	public string               Origin   { get; set; }
	public TKey                 ClientId { get; set; }
	public IdentityClient<TKey> Client   { get; set; }
}