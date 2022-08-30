// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Solver.Linear;

public class LinearExpression
{
    public virtual double DoVisit(Dictionary<Variable, double> coefficients, double multiplier)
    {
        return 0;
    }

    #region positive

    public double Visit(Dictionary<Variable, double> coefficients)
    {
        return DoVisit(coefficients, 1.0);
    }

    public static LinearExpression operator +(LinearExpression left, LinearExpression right)
    {
        return new Sum(left, right);
    }

    public static LinearExpression operator +(LinearExpression left, double right)
    {
        return new SumCoefficient(left, right);
    }

    public static LinearExpression operator +(double left, LinearExpression right)
    {
        return new SumCoefficient(right, left);
    }

    #endregion

    #region negitive

    public static LinearExpression operator -(LinearExpression left, LinearExpression right)
    {
        return new Sum(left, new ProductCoefficient(right, -1.0));
    }

    public static LinearExpression operator -(LinearExpression left, double right)
    {
        return new SumCoefficient(left, -right);
    }

    public static LinearExpression operator -(double left, LinearExpression right)
    {
        return new SumCoefficient(new ProductCoefficient(right, -1.0), left);
    }

    public static LinearExpression operator -(LinearExpression left)
    {
        return new ProductCoefficient(left, -1.0);
    }

    #endregion

    #region multiply

    public static LinearExpression operator *(LinearExpression left, double right)
    {
        return new ProductCoefficient(left, right);
    }

    public static LinearExpression operator *(double left, LinearExpression right)
    {
        return new ProductCoefficient(right, left);
    }

    #endregion
}