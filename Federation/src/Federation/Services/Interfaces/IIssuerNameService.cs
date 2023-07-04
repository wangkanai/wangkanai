// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Services;

/// <summary>
/// The current issuer name access contract
/// </summary>
public interface IIssuerNameService
{
	Task<string> GetCurrentAsync();
}