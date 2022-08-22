﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Abstractions;

public interface IVariable
{
    string Name  { get; }
    double Value { get; }
}