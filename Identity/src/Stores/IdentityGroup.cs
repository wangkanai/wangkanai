// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

public class IdentityGroup : IdentityGroup<string> { }

public class IdentityGroup<TKey> where TKey : IEquatable<TKey> { }