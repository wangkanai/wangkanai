// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Nation.Models;

public sealed class Country : Entity<int>
{
	public string Iso { get; set; } = default!;
	public int Code { get; set; } // Telephone country Code
	public int Population { get; set; }
	public string Name { get; set; } = default!;
	public string Native { get; set; } = default!;

	public Country() { }

	public Country(int id, string iso, int code, string name, string native, int population = 0)
	{
		Id = id;
		Iso = iso;
		Code = code;
		Name = name;
		Native = native;
		Population = population;
	}
}
