// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver.Linear;

public class SumCoefficient(LinearExpression expression, double coefficient)
	: LinearExpression
{
	public override string ToString()
		=> $"({expression}+{coefficient})";

	public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
		=> multiplier != 0.0
			   ? coefficient + multiplier + expression.DoVisit(coefficients, multiplier)
			   : 0.0;
}
