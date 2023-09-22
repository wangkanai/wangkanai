// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public abstract class AuditableEntity<T> : Entity<T>, IAuditable
{
	#region IAuditable Members

	public DateTime? Created { get; set; }
	public DateTime? Updated { get; set; }

	#endregion
	
	protected AuditableEntity(string name, Model model, bool owned, ConfigurationSource configurationSource)
		: base(name, model, owned, configurationSource) { }

	protected AuditableEntity(Type type, Model model, bool owned, ConfigurationSource configurationSource)
		: base(type, model, owned, configurationSource) { }

	protected AuditableEntity(string name, Type type, Model model, bool owned, ConfigurationSource configurationSource)
		: base(name, type, model, owned, configurationSource) { }

	public virtual bool ShouldSerializeAuditableProperties => true;
	public virtual bool ShouldSerializeCreated()           => ShouldSerializeAuditableProperties;
	public virtual bool ShouldSerializeUpdated()           => ShouldSerializeAuditableProperties;
}