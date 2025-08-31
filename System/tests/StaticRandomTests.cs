// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

public class StaticRandomTests
{
   [Fact]
   public void CheckDifferentSources()
   {
      var grabbers = new RandomGenerator[100];

      Parallel.ForEach(grabbers, (grabber, state, index) =>
      {
         grabbers[index] = new(30, true);
         grabbers[index].Generate();
      });

      Parallel.ForEach(grabbers, (grabber, state, index) =>
      {
         for (var local = index + 1; local < grabbers.Length; local++)
            if (grabbers[index].Equals(grabbers[local]))
            {
               Assert.Fail("Random number generator is not random");
            }
      });
   }

   [Fact]
   public void NextShouldReturnDifferentValues()
   {
      var length = 100;
      var values = new int[length];
      for (var i = 0; i < values.Length; i++)
         values[i] = StaticRandom.Next();

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("Next should return different values");
         }
   }

   [Fact]
   public void NextIntShouldReturnDifferentValues()
   {
      var length = 100;
      var values = new int[length];
      for (var i = 0; i < values.Length; i++)
         values[i] = StaticRandom.Next();

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("Next should return different values");
         }
   }

   [Fact]
   public void NextIntMaxShouldReturnDifferentValues()
   {
      var length = 100;
      var max    = 1_000_000;
      var values = new int[length];
      for (var i = 0; i < values.Length; i++)
         values[i] = StaticRandom.Next(max);

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("Next should return different values");
         }
   }

   [Fact]
   public void NextIntMinMaxShouldReturnDifferentValues()
   {
      var length = 100;
      var min    = 1_000;
      var max    = 1_000_000;
      var values = new int[length];
      for (var i = 0; i < values.Length; i++)
         values[i] = StaticRandom.Next(min, max);

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("Next should return different values");
         }
   }

   [Fact]
   public void NextDoubleShouldNotAlwaysReturnInts()
   {
      var length = 10;
      for (var i = 0; i < length; i++)
      {
         var d = StaticRandom.NextDouble();
         if (Math.Abs((int)d - d) > 0)
         {
            return;
         }
      }

      Assert.Fail("NextDouble shouldn't return integer");
   }


   [Fact]
   public void NextBytesShouldReturnDiffValueAsArray()
   {
      var length = 4;
      var values = new byte[length];
      StaticRandom.NextBytes(values);

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("NextBytes should return different values");
         }
   }

   [Fact]
   public void NextBytesShouldReturnDiffValueAsSpan()
   {
      var length = 2;
      var values = new byte[length];
      StaticRandom.NextBytes(values.AsSpan());

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("NextBytes should return different values");
         }
   }

   [Fact]
   public void NextBytesShouldReturnDiffValueAsMemory()
   {
      var length = 4;
      var values = new byte[length];
      StaticRandom.NextBytes(values.AsMemory());

      for (var i = 0; i < values.Length; i++)
      for (var j = i + 1; j < values.Length; j++)
         if (values[i] == values[j])
         {
            Assert.Fail("NextBytes should return different values");
         }
   }
}