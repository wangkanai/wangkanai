// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Solver;
using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver.Linear;

public class VariableWrapper : LinearExpression
{
    private readonly Variable _var;

    public VariableWrapper(Variable var)
        => _var = var;

    public override string ToString()
        => _var.Name;

    public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
    {
        if (multiplier == 0.0) return 0.0;

        if (coefficients.ContainsKey(_var))
            coefficients[_var] += multiplier;
        else
            coefficients[_var] = multiplier;

        return 0.0;
    }
}