// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain;

namespace Wangkanai.Nation.Models;

public abstract class Division : Entity<int>
{
	public int CountryId { get; set; }
	public string Iso { get; set; }
	public string Name { get; set; }
	public string Native { get; set; }
	public int Population { get; set; }

	public Division() { }

	public Division(int id, int countryId, string iso, string name, string native, int population = 0)
	{
		Id = id;
		CountryId = countryId;
		Iso = iso;
		Name = name;
		Native = native;
		Population = population;
	}
}

public class Municipality : Division;

public class Territory : Division;

public class Prefecture : Division;

public class Department : Division;

public class Barangay : Division;

public class Banner : Division;

public class Hundred : Division;

public class Kampong : Division;

public class Kingdom : Division;

public class Principality : Division;

public class Oblast : Division;

public class Regency : Division;

public class Republic : Division;

public class Riding : Division;

public class Theme : Division;

public class Voivodeship : Division;
