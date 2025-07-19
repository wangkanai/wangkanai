// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Nation.Urbans;

public abstract class Urban : Entity<int>
{
	public int DivisionId { get; set; }
	public string Name { get; set; }
	public string Native { get; set; }
	public string Iso { get; set; }
}
