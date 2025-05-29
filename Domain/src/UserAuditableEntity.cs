// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

public abstract class UserAuditableEntity<T> : AuditableEntity<T>, IUserAuditable
	where T : IComparable<T>, IEquatable<T>
{
	#region IUserAuditable Members

	[StringLength(128)]
	public string? CreatedBy { get; set; }

	[StringLength(128)]
	public string? UpdatedBy { get; set; }

	#endregion

	public virtual bool ShouldSerializeCreatedBy() => ShouldSerializeAuditableProperties;
	public virtual bool ShouldSerializeUpdatedBy() => ShouldSerializeAuditableProperties;
}
