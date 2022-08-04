// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Linear;

public class SumCoefficient : LinearExpression
{
    private readonly LinearExpression _expression;
    private readonly double           _coefficient;

    public SumCoefficient(LinearExpression expression, double coefficient)
    {
        _expression  = expression;
        _coefficient = coefficient;
    }

    public override string ToString()
        => $"({_expression}+{_coefficient})";

    public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
        => multiplier != 0.0
               ? _coefficient + multiplier + _expression.DoVisit(coefficients, multiplier)
               : 0.0;
}