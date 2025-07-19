// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Runtime.Serialization;

namespace Wangkanai.Federation.Models;

public enum FlowTypes
{
	[EnumMember(Value = FlowType.AuthorizationCode)]
	AuthorizationCode = 0,
}

public static class FlowType
{
	public const string AuthorizationCode = "authorization_code";
}
