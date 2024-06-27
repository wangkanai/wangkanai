// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver;

public partial class Solver(SolverType type) : ISolver
{
	public SolverType Type { get; } = type;

	public void Dispose() { }
}
