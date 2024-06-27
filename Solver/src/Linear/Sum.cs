// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
