// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver.Linear;

public class Variable : IVariable
{
	public string Name { get; init; }
	public double Value { get; set; }

	public static LinearExpression operator +(Variable left, double right)
		=> new VariableWrapper(left) + right;

	public static LinearExpression operator +(double left, Variable right)
		=> right + left;

	public static LinearExpression operator +(Variable left, LinearExpression right)
		=> new VariableWrapper(left) + right;

	public static LinearExpression operator +(Variable left, Variable right)
		=> new VariableWrapper(left) + new VariableWrapper(right);

	public static LinearExpression operator +(LinearExpression left, Variable right)
		=> left + new VariableWrapper(right);
}
