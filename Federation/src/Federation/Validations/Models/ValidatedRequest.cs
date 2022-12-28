// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

namespace Wangkanai.Federation.Validations;

public class ValidatedRequest
{
    public string          Issuer              { get; set; }
    public int             AccessTokenLifetime { get; set; }
    public ClaimsPrincipal Subject             { get; set; }
    public string          SessionId           { get; set; }
}