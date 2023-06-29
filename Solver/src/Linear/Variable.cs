// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver.Linear;

public class Variable : IVariable
{
	public string Name  { get; init; }
	public double Value { get; set; }

	public static LinearExpression operator +(Variable left, double right)
	{
		return new VariableWrapper(left) + right;
	}

	public static LinearExpression operator +(double left, Variable right)
	{
		return right + left;
	}

	public static LinearExpression operator +(Variable left, LinearExpression right)
	{
		return new VariableWrapper(left) + right;
	}

	public static LinearExpression operator +(Variable left, Variable right)
	{
		return new VariableWrapper(left) + new VariableWrapper(right);
	}

	public static LinearExpression operator +(LinearExpression left, Variable right)
	{
		return left + new VariableWrapper(right);
	}
}