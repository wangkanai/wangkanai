// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq;
using System.Reflection;

namespace Wangkanai.Universal.Models;

public abstract class Field
{
    public Field() { format = "'{0}':{1}"; }
    protected string format { get; set; }

    internal bool IsNullOrEmpty => Properties.Count() == 0 ? true : false;

    internal PropertyInfo[] Properties
    {
        get
        {
            //var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //var filtered = properties.Where(x => Exist(x.GetValue(this, null))).ToArray();
            return GetType()
                   .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .Where(x => Exist(x.GetValue(this, null)))
                   .ToArray();
        }
    }

    public override string ToString()
    {
        var js = "";
        foreach (var property in Properties)
        {
            js += string.Format(format, property.Name, FormatValue(property.GetValue(this, null)));
            js += !Properties.Last().Equals(property) && Properties.Count() > 1 ? "," : "";
        }

        return js;
    }

    private bool Exist(object value)
    {
        if (value == null) return false;
        if (value is string)
            if (string.IsNullOrEmpty((string)value))
                return false;
        if (value is int)
            if ((int)value == 0)
                return false;
        if (value as bool? == false) return false;
        if (value as float? == 0.0) return false;
        return true;
    }

    private string FormatValue(object value)
    {
        if (value is float || value is int || value is bool) return value.ToString();
        if (value is string) return string.Format("'{0}'", value);
        return null;
    }
}