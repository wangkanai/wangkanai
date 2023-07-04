// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Responses;

public class DeviceAuthorizationResponse
{
	public string DeviceCode              { get; set; }
	public string UserCode                { get; set; }
	public string VerificationUri         { get; set; }
	public string VerificationUriComplete { get; set; }
	public int    DeviceCodeLifetime      { get; set; }
	public int    Interval                { get; set; }
}