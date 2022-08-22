// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Tests;

public class SolverInitialization
{
    [Fact]
    public void ExpectedEquals()
    {
        // Arrange
        Solver solver = Solver.CreateSolver(SolverType.Linear);
        // Act

        // Assert
        Assert.Equal(SolverType.Linear, solver.Type);
        Assert.NotEqual(SolverType.Cubic, solver.Type);
    }
}