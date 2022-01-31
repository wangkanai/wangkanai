﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Runtime;

public static partial class Math
{
    public static double Divider(double value, double divider)
        => divider != 0 ? value / divider : 0;
}