// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Services;

/// <summary>
/// The current issuer name access contract
/// </summary>
public interface IIssuerNameService
{
	Task<string> GetCurrentAsync();
}
