// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Nation.Models;

public abstract class Division : Entity<int>
{
	public int    CountryId { get; set; }
	public string Iso       { get; set; }
	public string Name      { get; set; }
	public string Native    { get; set; }
}

public class Parish : Division;

public class Banat : Division;

public class Canton : Division;

public class Community : Division;

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
