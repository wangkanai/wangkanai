// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions.DateTimeFactory;

public static class DateTimeFactoryExtensions
{
	public static DateTime January(this int day, int year) => new(year, 1, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime February(this int day, int year) => new(year, 2, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime March(this int day, int year) => new(year, 3, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime April(this int day, int year) => new(year, 4, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime May(this int day, int year) => new(year, 5, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime June(this int day, int year) => new(year, 6, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime July(this int day, int year) => new(year, 7, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime August(this int day, int year) => new(year, 8, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime September(this int day, int year) => new(year, 9, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime October(this int day, int year) => new(year, 10, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime November(this int day, int year) => new(year, 11, day, 0, 0, 0, DateTimeKind.Utc);
	public static DateTime December(this int day, int year) => new(year, 12, day, 0, 0, 0, DateTimeKind.Utc);
}
