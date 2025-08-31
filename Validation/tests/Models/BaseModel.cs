// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

namespace Wangkanai.Validation.Models;

public class BaseModel
{
   public static PropertyInfo GetProperty<T>(string name)
      where T : BaseModel
      => typeof(T).GetProperty(name)!;
}

public class BaseModel<T>
{
   public static PropertyInfo GetProperty(string name)
      => typeof(T).GetProperty(name)!;
}