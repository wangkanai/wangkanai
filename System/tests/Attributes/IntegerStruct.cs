// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Attributes;

struct NegativeIntegerStructDefault { }

[NegativeInteger]
struct NegativeIntegerStructExist { }

[NegativeInteger("error", true)]
struct NegativeIntegerStructError { }

[PositiveInteger]
struct PositiveIntegerStructExist { }
