// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai.Cryptography;

public class HashTests
{
   [Fact] public void Md5_Null()    => Assert.Throws<ArgumentNullException>(() => Hash.HashMd5(null!));
   [Fact] public void Sha256_Null() => Assert.Throws<ArgumentNullException>(() => Hash.HashSha256(null!));
   [Fact] public void She384_Null() => Assert.Throws<ArgumentNullException>(() => Hash.HashSha384(null!));
   [Fact] public void Sha512_Null() => Assert.Throws<ArgumentNullException>(() => Hash.HashSha512(null!));

   [Fact] public void Md5_Empty()    => Assert.Throws<ArgumentEmptyException>(() => string.Empty.HashMd5());
   [Fact] public void Sha256_Empty() => Assert.Throws<ArgumentEmptyException>(() => string.Empty.HashSha256());
   [Fact] public void She384_Empty() => Assert.Throws<ArgumentEmptyException>(() => string.Empty.HashSha384());
   [Fact] public void Sha512_Empty() => Assert.Throws<ArgumentEmptyException>(() => string.Empty.HashSha512());

   [Fact] public void Md5_Whitespace()    => Assert.Throws<ArgumentWhitespaceException>(() => " ".HashMd5());
   [Fact] public void Sha256_Whitespace() => Assert.Throws<ArgumentWhitespaceException>(() => " ".HashSha256());
   [Fact] public void She384_Whitespace() => Assert.Throws<ArgumentWhitespaceException>(() => " ".HashSha384());
   [Fact] public void Sha512_Whitespace() => Assert.Throws<ArgumentWhitespaceException>(() => " ".HashSha512());
}