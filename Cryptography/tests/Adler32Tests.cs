// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


using System.Text;

using Wangkanai.Exceptions;

namespace Wangkanai.Cryptography;

public class Adler32Tests
{
	[Fact]
	public void Wikipedia_Text()
	{
		var checksum = Adler32.Checksum("Wikipedia");
		Assert.Equal(0x11E60398, checksum);
	}

	[Fact]
	public void Wikipedia_Bytes()
	{
		var bytes    = "Wikipedia"u8.ToArray();
		var checksum = Adler32.Checksum(bytes);
		Assert.Equal(0x11E60398, checksum);
	}

	[Fact]
	public void Checksum_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Adler32.Checksum((byte[])null!));
		Assert.Throws<ArgumentNullException>(() => Adler32.Checksum((string)null!));
	}

	[Fact]
	public void Checksum_Empty()
	{
		var empty = string.Empty;
		var bytes = Encoding.ASCII.GetBytes(empty);
		Assert.Throws<ArgumentEmptyException>(() => Adler32.Checksum(empty));
		Assert.Throws<ArgumentEmptyException>(() => Adler32.Checksum(bytes));
	}

	// a = ASCII 97 = 0x61
	[Fact]
	public void Text_a1()
	{
		//             | A             | B
		// round 1 > a |  1 + 97 =  98 |  0 +  98 =  98
		//             | 0x62          | 0x62
		// output = (0x62 << 16) + 0x62 = 0x620000 + 0x000062 = 0x620062 = 6422626
		var text     = "a";
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x620062, checksum); // 0x620062 = 6422626
	}

	[Fact]
	public void Text_a2()
	{
		//             | A             | B
		// round 1 > a |  1 + 97 =  98 |  0 +  98 =  98
		// round 2 > a | 98 + 97 = 195 | 98 + 195 = 293
		//             | 0xC3          | 0x125
		// output = (0x125 << 16) + 0xC3 = 0x1250000 + 0x00000C3 = 0x12500C3 = 19028675
		var text     = "aa";
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x12500C3, checksum);
	}

	[Fact]
	public void Text_a3()
	{
		//             | A              | B
		// round 1 > a |   1 + 97 =  98 |   0 +  98 =  98
		// round 2 > a |  98 + 97 = 195 |  98 + 195 = 293
		// round 3 > a | 195 + 97 = 292 | 293 + 292 = 585
		//             | 0x124		    | 0x249
		// output = (0x249 << 16) + 0x124 = 0x2490000 + 0x0000124 = 0x2490124 = 37889220
		var text     = "aaa";
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x2490124, checksum);
	}

	[Fact]
	public void Text_a4()
	{
		// 		       | A              | B
		// round 1 > a |   1 + 97 =  98 |   0 +  98 =  98
		// round 2 > a |  98 + 97 = 195 |  98 + 195 = 293
		// round 3 > a | 195 + 97 = 292 | 293 + 292 = 585
		// round 4 > a | 292 + 97 = 389 | 585 + 389 = 974
		//             | 0x185		    | 0x3CE
		// output = (0x3CE << 16) + 0x185 = 0x3CE0000 + 0x0000185 = 0x3CE0185 
		var text     = "aaaa";
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x3CE0185, checksum);
	}

	[Fact]
	public void Text_abcd()
	{
		// 		       | A              | B
		// round 1 > a |   1 + 97 =  98 |   0 +  98 =  98
		// round 2 > b |  98 + 98 = 196 |  98 + 196 = 294
		// round 3 > c | 196 + 99 = 295 | 294 + 295 = 589
		// round 4 > d | 295 + 100 = 395| 589 + 395 = 984
		//             | 0x18B		    | 0x3D8
		// output = (0x3D8 << 16) + 0x18B = 0x3D80000 + 0x000018B = 0x3D8018B
		var text     = "abcd";
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x3D8018B, checksum);
	}

	[Fact]
	public void Text_a128()
	{
		var text     = new string('a', 128);
		var checksum = Adler32.Checksum(text);
		Assert.Equal(0x38C03081, checksum); // 0x38C03081 = 952119425
	}
}