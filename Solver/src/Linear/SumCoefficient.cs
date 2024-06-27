// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
