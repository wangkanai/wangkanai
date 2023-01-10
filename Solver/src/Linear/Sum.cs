// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Linear;

public class Sum : LinearExpression
{
	private readonly LinearExpression _left;
	private readonly LinearExpression _right;

	public Sum(LinearExpression left, LinearExpression right)
	{
		_left  = left;
		_right = right;
	}

	public override string ToString()
	{
		return $"({_left}) + ({_right})";
	}

	public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
	{
		return multiplier != 0.0
			       ? _left.DoVisit(coefficients, multiplier) + _right.DoVisit(coefficients, multiplier)
			       : 0.0;
	}
}