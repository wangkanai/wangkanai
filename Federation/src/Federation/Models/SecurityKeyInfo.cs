// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.IdentityModel.Tokens;

namespace Wangkanai.Federation.Models;

public class SecurityKeyInfo
{
	public SecurityKey Key              { get; set; }
	public string      SigningAlgorithm { get; set; }
}