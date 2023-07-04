## Federation: The Ultimate Authentication and Authorization Server for ASP.NET Core

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.federation)](https://www.nuget.org/packages/wangkanai.federation)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.federation?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.federation)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

Welcome to `Federation`, your one-stop solution for robust, flexible, and secure Authentication and Authorization in
ASP.NET Core.
It's not just a framework, it's your trusted partner for crafting seamless OAuth 2.1 and OpenID Connect integrations.

In the interconnected world of web services, managing authentication and authorization can be daunting.
But not with `Federation`. This library goes beyond the basics, offering a flexible permission policy that tailors
security to your application's unique needs.

`Federation` provides comprehensive support for Json Web Tokens (JWT) and external authentication providers.
It elegantly handles user authentication and consent, ensuring your users' data is secure while providing a seamless
login experience.

Authorization is a breeze with `Federation`. With support for Authorization Code Grant with PKCE, Client Credentials
Grant, and
Refresh Token Flow, you have full control over how your users access your application.

But the real magic lies in the `Federation` Identity Model Structure.
From Identity User, Role, and Client to Identity Resource, Scope, Directory, Group, Permission, and Tenant, `Federation`
provides a fine-grained, systematic approach to managing identities.
This means you can focus on what you do best: building amazing applications.

### Planned Features

* General
    - Json Web Token (Jwt)
* Authentication
    - Handle user authentication/consent
    - External authentication provider
* Authorization
    - Authorization Code Grant with PKCE
    - Client Credentials Grant
    - Refresh Token Flow
* Identity Model Structure
    - Identity User
    - Identity Role
    - Identity Client
    - Identity Resource
    - Identity Scope
    - Identity Directory
    - Identity Group
    - Identity Permission
    - Identity Tenant (to be determined #620)

### Become a Part of Our Community

If 1Federation1 has made your Authentication and Authorization workflows more efficient, secure, or just plain easier,
we'd love to hear about it!
Please consider giving us a star ⭐ on GitHub. Your stars not only motivate us, but they also help other developers
discover 1Federation1.

For any inquiries, suggestions, or contributions, feel free to open an issue or a pull request.
Together, let's redefine what's possible with Authentication and Authorization in ASP.NET Core!