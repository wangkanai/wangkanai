// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

/// <summary>
/// JSON Web Token Claims
/// Reference
///  - https://auth0.com/docs/secure/tokens/json-web-tokens/json-web-token-claims
///  - https://www.rfc-editor.org/rfc/rfc7519
///  - https://www.iana.org/assignments/jwt/jwt.xhtml#claims
/// </summary>
public static class JwtClaimTypes
{
    /// <summary>
    /// Full name of the user
    /// </summary>
    public const string Name = "name";

    /// <summary>
    /// Subject of the JWT (the user)
    /// </summary>
    public const string Subject = "sub";

    /// <summary>
    /// Roles of the JWT (the user)
    /// </summary>
    public const string Role = "role";

    /// <summary>
    /// Email of the JWT (the user)
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// Issuer of the JWT
    /// </summary>
    public const string Issuer = "iss";

    /// <summary>
    /// Token audience (aud, string): The audience value for the token must match the client ID of the application as defined in your Application's Settings in the Client ID field
    /// </summary>
    public const string Audience = "aud";

    /// <summary>
    /// Nonce (nonce, string): Passing a nonce in the token request is recommended (required for the Implicit Flow) to help prevent replay attacks.
    /// The nonce value in the token must exactly match the original nonce sent in the request.
    /// </summary>
    public const string Nonce = "nonce";

    /// <summary>
    /// Time after which the JWT expires
    /// </summary>
    public const string Expires = "exp";

    /// <summary>
    /// Time at which the JWT was issued; can be used to determine age of the JWT
    /// </summary>
    public const string IssuedAt = "iat";

    /// <summary>
    /// Time before which the JWT must not be accepted for processing
    /// </summary>
    public const string NotBefore = "nbf";

    /// <summary>
    /// Unique identifier; can be used to prevent the JWT from being replayed (allows a token to be used only once)
    /// </summary>
    public const string Jti = "jti";
}