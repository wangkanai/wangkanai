// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Solver.Abstractions;

namespace Wangkanai.Solver.Linear;

public class LinearExpression
{
    public virtual double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
        => 0;

    #region positive

    public double Visit(Dictionary<Variable, double> coefficients)
        => DoVisit(coefficients, 1.0);

    public static LinearExpression operator +(LinearExpression left, LinearExpression right)
        => new Sum(left, right);

    public static LinearExpression operator +(LinearExpression left, double right)
        => new SumCoefficient(left, right);

    public static LinearExpression operator +(double left, LinearExpression right)
        => new SumCoefficient(right, left);

    #endregion

    #region negitive

    public static LinearExpression operator -(LinearExpression left, LinearExpression right)
        => new Sum(left, new ProductCoefficient(right, -1.0));

    public static LinearExpression operator -(LinearExpression left, double right)
        => new SumCoefficient(left, -right);

    public static LinearExpression operator -(double left, LinearExpression right)
        => new SumCoefficient(new ProductCoefficient(right, -1.0), left);

    public static LinearExpression operator -(LinearExpression left)
        => new ProductCoefficient(left, -1.0);

    #endregion

    #region multiply

    public static LinearExpression operator *(LinearExpression left, double right)
        => new ProductCoefficient(left, right);

    public static LinearExpression operator *(double left, LinearExpression right)
        => new ProductCoefficient(right, left);

    #endregion
}