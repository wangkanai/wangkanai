// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Domain;

public class AuditTrail<TKey, TUserType, TUserKey> : Entity<TKey>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	public TrailType  Type   { get; set; }
	public TUserKey?  UserId { get; set; }
	public TUserType? User   { get; set; }

	public DateTime Timestamp  { get; set; }
	public string?  PrimaryKey { get; set; }
	public string   EntityName { get; set; }

	public Dictionary<string, object> OldValues      { get; set; } = [];
	public Dictionary<string, object> NewValues      { get; set; } = [];
	public List<string>               ChangedColumns { get; set; } = [];
}
