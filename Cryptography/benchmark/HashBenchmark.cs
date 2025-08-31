// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Cryptography;

[MemoryDiagnoser]
public class HashBenchmark
{
   private const string text = "F41k7dPvkYUpA5zYUwtWMkV4gwzxGcw7Oh9x9PXIif6o9C7oBSoaJw3c4I5bRyokmp3AMa";

   [Benchmark]
   public void Md5()
   {
      for (var i = 0; i < 100_000; i++)
         text.HashMd5();
   }


   [Benchmark]
   public void Sha256()
   {
      for (var i = 0; i < 100_000; i++)
         text.HashSha256();
   }

   [Benchmark]
   public void Sha384()
   {
      for (var i = 0; i < 100_000; i++)
         text.HashSha384();
   }

   [Benchmark]
   public void Sha512()
   {
      for (var i = 0; i < 100_000; i++)
         text.HashSha512();
   }
}