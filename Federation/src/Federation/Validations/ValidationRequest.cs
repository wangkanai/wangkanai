// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

namespace Wangkanai.Federation.Validations;

public class ValidationRequest
{
	public required string Issuer { get; set; }
	public int AccessTokenLifetime { get; set; }
	public required ClaimsPrincipal Subject { get; set; }
	public required string SessionId { get; set; }

	public required ResourceValidationResult ValidationResources { get; set; }
}
