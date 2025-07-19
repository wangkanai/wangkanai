// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver;

public partial class Solver(SolverType type) : ISolver
{
	public SolverType Type { get; } = type;

	public void Dispose() { }
}
