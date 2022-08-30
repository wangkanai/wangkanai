// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Linear;

public class ProductCoefficient : LinearExpression
{
    private readonly double           _coefficient;
    private readonly LinearExpression _expression;

    public ProductCoefficient(LinearExpression expression, double coefficient)
    {
        _expression  = expression;
        _coefficient = coefficient;
    }

    public override string ToString()
    {
        return $"({_expression}*{_coefficient})";
    }

    public override double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
    {
        var currentMultiplier = multiplier * _coefficient;
        return currentMultiplier != 0.0
                   ? _expression.DoVisit(coefficients, currentMultiplier)
                   : 0.0;
    }
}