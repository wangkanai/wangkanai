// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

public abstract class AuditableEntity<T> : Entity<T>, IAuditable
	where T : IComparable<T>, IEquatable<T>
{
	#region IAuditable Members

	public DateTime? Created { get; set; }
	public DateTime? Updated { get; set; }

	#endregion

	public virtual bool ShouldSerializeAuditableProperties => true;
	public virtual bool ShouldSerializeCreatedDate()       => ShouldSerializeAuditableProperties;
	public virtual bool ShouldSerializeUpdatedDate()       => ShouldSerializeAuditableProperties;
}
