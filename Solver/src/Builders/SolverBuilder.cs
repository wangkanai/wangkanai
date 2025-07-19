// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver;

public partial class Solver
{
	public static Solver CreateSolver(SolverType type)
		=> new(type);
}
