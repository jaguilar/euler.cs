using System;

namespace euler
{
	public static class Util
	{
		public static int Digits(int n) {
			return (int) Math.Floor(Math.Log10(n)) + 1;
		}

		/// <summary>
		/// The nth digit of a number, zero being the least significant digit.
		/// </summary>
		public static int Digit(int n, int d) {
			return (n / ((int)Math.Pow(10, d))) % 10;
		}
	}
}

