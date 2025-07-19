// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

/// <summary>
/// OpenID Connect Constants
/// - https://openid.net/connect/
/// - https://openid.net/specs/openid-connect-core-1_0.html
/// </summary>
public static class OidcConstants
{
	/// <summary>
	/// OpenID Providers supporting Discovery MUST make a JSON document available at the path formed by concatenating the string /.well-known/openid-configuration to the Issuer.
	/// The syntax and semantics of .well-known are defined in RFC 5785 [RFC5785] and apply to the Issuer value when it contains no path component.
	/// - https://openid.net/specs/openid-connect-discovery-1_0.html
	/// - https://www.rfc-editor.org/rfc/rfc5785
	/// </summary>
	public static class Discovery
	{
		public const string Configuration = ".well-known/openid-configuration";
		public const string Issuer = "issuer";
	}


	/// <summary>
	/// Authorization Servers MUST support the use of the HTTP POST (Best Practise) methods defined in RFC 2616 [RFC2616] at the Authorization Endpoint.
	/// - https://openid.net/specs/openid-connect-core-1_0.html#AuthRequest
	/// </summary>
	public static class AuthenticateRequest
	{
		public const string Scope = "scope";
		public const string ResponseType = "response_type";
		public const string ClientId = "client_id";
		public const string RedirectUri = "redirect_uri";
		public const string State = "state";
		public const string ResponseMode = "response_mode";
		public const string Nonce = "nonce";
		public const string Display = "display";
		public const string Prompt = "prompt";
		public const string MaxAge = "max_age";
		public const string UiLocales = "ui_locales";
		public const string IdTokenHint = "id_token_hint";
		public const string LoginHint = "login_hint";
		public const string AcrValues = "acr_values";
	}

	/// <summary>
	/// Successful Authentication Response
	/// - https://openid.net/specs/openid-connect-core-1_0.html#ImplicitAuthResponse
	/// - https://openid.net/specs/openid-connect-core-1_0.html#HybridAuthResponse
	/// </summary>
	public static class AuthenticateResponse
	{
		public const string AccessToken = "access_token";
		public const string TokenType = "token_type";
		public const string IdToken = "id_token";
		public const string Code = "code";
		public const string State = "state";
		public const string ExpiresIn = "expires_in";
	}

	/// <summary>
	/// Authentication Error Response
	/// If the End-User denies the request or the End-User authentication fails,
	/// the Authorization Server MUST return the error Authorization Response in the fragment component of the Redirection URI
	/// - https://openid.net/specs/openid-connect-core-1_0.html#AuthError
	/// </summary>
	public static class AuthenticateErrors
	{
		public const string InvalidRequest = "invalid_request";
		public const string InteractionRequired = "interaction_required";
		public const string LoginRequired = "login_required";
		public const string AccountSelectionRequired = "account_selection_required";
		public const string ConsentRequired = "consent_required";
		public const string InvalidRequestUri = "invalid_request_uri";
		public const string InvalidRequestObject = "invalid_request_object";
		public const string RequestNotSupported = "request_not_supported";
		public const string RequestUriNotSupported = "request_uri_not_supported";
		public const string RegistrationNotSupported = "registration_not_supported";
		public const string Error = "error";
		public const string ErrorDescription = "error_description";
		public const string ErrorUri = "error_uri";
	}

	public static class TokenRequest { }

	public static class TokenResponse { }

	public static class TokenErrors
	{
		public const string InvalidRequest = "invalid_request";
	}

	public static class TokenTypes { }

	public static class ResponseTypes
	{
		public const string Code = "code";
		public const string Token = "token";
	}

	public static class AuthenticateMethods
	{
		public const string Password = "pwd";
	}
}
