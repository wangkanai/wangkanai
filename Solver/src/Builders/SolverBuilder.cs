// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver;

public partial class Solver
{
    public static Solver CreateSolver(SolverType type) 
        => new(type);
}