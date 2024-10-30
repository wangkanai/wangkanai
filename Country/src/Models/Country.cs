// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Nation.Models;

public sealed class Country : Entity<int>
{
	public string Iso        { get; set; }
	public string Name       { get; set; }
	public string Native     { get; set; }
	public int    Population { get; set; }
}
