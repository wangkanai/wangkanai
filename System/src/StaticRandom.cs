// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

/// <summary>Represents a static class that provides random number generation functionality.</summary>
public static class StaticRandom
{
   private static readonly Random Random = new();
   private static readonly object Lock   = new();

   /// <summary>Generates a random integer.</summary>
   /// <returns>A random integer.</returns>
   public static int Next()
   {
      lock (Lock)
         return Random.Next();
   }

   /// <summary>Generates a random integer.</summary>
   /// <returns>A random integer.</returns>
   public static int Next(int max)
   {
      lock (Lock)
         return Random.Next(max);
   }

   /// <summary>Generates a random integer.</summary>
   /// <returns>A random integer.</returns>
   public static int Next(int min, int max)
   {
      lock (Lock)
         return Random.Next(min, max);
   }

   /// <summary>Generates a random double between 0.0 and 1.0.</summary>
   /// <returns>A random double between 0.0 and 1.0.</returns>
   public static double NextDouble()
   {
      lock (Lock)
         return Random.NextDouble();
   }

   /// <summary>Generates random bytes and fills the provided byte array with the generated values.</summary>
   /// <param name="buffer">The byte array to fill with random values.</param>
   public static void NextBytes(byte[] buffer)
   {
      lock (Lock)
         Random.NextBytes(buffer);
   }

   /// <summary> Generates random bytes and fills the provided byte array with the generated values.</summary>
   /// <param name="buffer">The byte array to fill with random values.</param>
   public static void NextBytes(Span<byte> buffer)
   {
      lock (Lock)
         Random.NextBytes(buffer);
   }

   /// <summary> Generates random bytes and fills the provided byte array with the generated values.</summary>
   /// <param name="buffer">The byte array to fill with random values.</param>
   public static void NextBytes(Memory<byte> buffer)
   {
      lock (Lock)
         Random.NextBytes(buffer.Span);
   }
}