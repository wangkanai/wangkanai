// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

public class IdentityClient : IdentityClient<string> { }

public class IdentityClient<TKey> where TKey : IEquatable<TKey> { }