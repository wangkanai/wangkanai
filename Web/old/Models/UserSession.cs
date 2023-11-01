// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Web;

public class UserSession
{
	public string   Id    { get; set; } = string.Empty;
	public DateTime First { get; set; }
	public DateTime Last  { get; set; }
}