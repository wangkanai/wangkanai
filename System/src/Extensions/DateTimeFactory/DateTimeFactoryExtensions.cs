// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.DateTimeFactory;

public static class DateTimeFactoryExtensions
{
    public static DateTime January(this   int day, int year) => new(year, 1, day);
    public static DateTime February(this  int day, int year) => new(year, 2, day);
    public static DateTime March(this     int day, int year) => new(year, 3, day);
    public static DateTime April(this     int day, int year) => new(year, 4, day);
    public static DateTime May(this       int day, int year) => new(year, 5, day);
    public static DateTime June(this      int day, int year) => new(year, 6, day);
    public static DateTime July(this      int day, int year) => new(year, 7, day);
    public static DateTime August(this    int day, int year) => new(year, 8, day);
    public static DateTime September(this int day, int year) => new(year, 9, day);
    public static DateTime October(this   int day, int year) => new(year, 10, day);
    public static DateTime November(this  int day, int year) => new(year, 11, day);
    public static DateTime December(this  int day, int year) => new(year, 12, day);
}