// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Solver.Linear;

public class VariableWrapper(Variable var)
	: LinearExpression
{
	public override string ToString()
		=> var.Name;

	public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
	{
		if (multiplier == 0.0) return 0.0;

		if (coefficients.ContainsKey(var))
			coefficients[var] += multiplier;
		else
			coefficients[var] = multiplier;

		return 0.0;
	}
}
