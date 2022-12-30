// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

public class IdentityResource : IdentityResource<string> { }

public class IdentityResource<TKey> where TKey : IEquatable<TKey> { }