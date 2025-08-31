// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Nation.Models;

public sealed class Province : Division
{
   public Province() { }

   public Province(int id, int code, string iso, string name, string native, int population)
      : base(id, code, iso, name, native, population) { }
}