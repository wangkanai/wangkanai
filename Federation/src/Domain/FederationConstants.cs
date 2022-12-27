// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

public static class FederationConstants
{
    public const string DefaultCookieAuthenticationScheme = "federation";

    public static class LocalApi
    {
        public const string AuthenticationScheme = "FederationAccessToken";
        public const string PolicyName           = AuthenticationScheme;
    }
}