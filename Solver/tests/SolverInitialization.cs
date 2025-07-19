// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver.Tests;

public class SolverInitialization
{
	[Fact]
	public void ExpectedEquals()
	{
		// Arrange
		var solver = Solver.CreateSolver(SolverType.Linear);
		// Act

		// Assert
		Assert.Equal(SolverType.Linear, solver.Type);
		Assert.NotEqual(SolverType.Cubic, solver.Type);
	}
}
