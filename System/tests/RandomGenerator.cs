// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

public class RandomGenerator
{
   private readonly int[]? _numbers;
   private readonly int    _size;

   public RandomGenerator(int size, bool keep)
   {
      _size = size;
      if (keep)
         _numbers = new int[size];
   }

   public void Generate()
   {
      for (var i = 0; i < _size; i++)
      {
         var number = StaticRandom.Next(100_000);
         if (_numbers != null)
         {
            _numbers[i] = number;
         }
      }
   }

   public override bool Equals(object? obj)
   {
      obj.ThrowIfNull();
      var other = obj as RandomGenerator;
      for (var i = 0; i < _size; i++)
         if (_numbers![i] != other!._numbers![i])
         {
            return false;
         }

      return true;
   }
}