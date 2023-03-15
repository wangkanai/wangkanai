// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Validations;

public class ValidationTokenRequest : ValidationRequest
{
    public string GrantType                  { get; set; }
    public string RequestedResourceIndicator { get; set; }
    public string Username                   { get; set; }
}