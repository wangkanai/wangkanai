// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

public class AuditTrail : Entity<Guid>
{
	public TrailType Type { get; set; }
}
