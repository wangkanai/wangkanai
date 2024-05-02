// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Attributes;

// Positive

struct PositiveIntegerStructDefault { }

[PositiveInteger]
struct PositiveIntegerStructExist { }

[PositiveInteger("error", true)]
struct PositiveIntegerStructError { }

// Negative

struct NegativeIntegerStructDefault { }

[NegativeInteger]
struct NegativeIntegerStructExist { }

[NegativeInteger("error", true)]
struct NegativeIntegerStructError { }

// Zero

struct ZeroIntegerStructDefault { }

[ZeroInteger]
struct ZeroIntegerStructExist { }

[ZeroInteger("error", true)]
struct ZeroIntegerStructError { }
