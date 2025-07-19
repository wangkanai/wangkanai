// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver.Linear;

public class Sum(LinearExpression left, LinearExpression right)
	: LinearExpression
{
	public override string ToString()
		=> $"({left}) + ({right})";

	public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
		=> multiplier != 0.0
			   ? left.DoVisit(coefficients, multiplier) + right.DoVisit(coefficients, multiplier)
			   : 0.0;
}
