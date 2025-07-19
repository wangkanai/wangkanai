// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver.Abstractions;

public interface ISolver : IDisposable
{
	SolverType Type { get; }
}
