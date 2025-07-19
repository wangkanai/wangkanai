// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

/// <summary>
/// Provides mathematical operations.
/// </summary>
public static class Mathematical
{
	/// <summary>Divides two byte values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static byte Divider(byte value, byte divider)
		=> (byte)(divider != 0 ? value / divider : 0);

	/// <summary>Divides two sbyte values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static sbyte Divider(sbyte value, sbyte divider)
		=> (sbyte)(divider != 0 ? value / divider : 0);

	/// <summary>Divides two short values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static short Divider(short value, short divider)
		=> (short)(divider != 0 ? value / divider : 0);

	/// <summary>Divides two int values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static int Divider(int value, int divider)
		=> divider != 0 ? value / divider : 0;

	/// <summary>Divides two uint values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static uint Divider(uint value, uint divider)
		=> divider != 0 ? value / divider : 0;

	/// <summary>Divides two long values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static long Divider(long value, long divider)
		=> divider != 0 ? value / divider : 0;

	/// <summary>Divides two ulong values. </summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static ulong Divider(ulong value, ulong divider)
		=> divider != 0 ? value / divider : 0;

	/// <summary>Divides two float values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static float Divider(float value, float divider)
		=> System.Math.Abs(divider) >= float.Epsilon ? value / divider : 0;

	/// <summary>Divides two double values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static double Divider(double value, double divider)
		=> System.Math.Abs(divider) >= double.Epsilon ? value / divider : 0;

	/// <summary>Divides two decimal values.</summary>
	/// <param name="value">The dividend.</param>
	/// <param name="divider">The divisor.</param>
	/// <returns>The quotient of the division. If the divisor is 0, returns 0.</returns>
	public static decimal Divider(decimal value, decimal divider)
		=> divider != 0 ? value / divider : 0;
}
