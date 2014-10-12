using System;
using System.Linq;
using System.Collections.Generic;

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

		/// <summary>
		/// Sequence of digits in n, from most to least significant.
		/// </summary>
		public static IEnumerable<int> DigitSeq(int n) {
			for (int i = Digits(n) - 1; i >= 0; --i) {
				yield return Digit(n, i);
			}
		}

		public static IEnumerable<int> Primes() {
			yield return 2;
			HashSet<int> primes = new HashSet<int>();

			int i = 3;
			while (true) {
				if (!primes.Where(x => i % x == 0).Any()) {
					primes.Add(i);
					yield return i;
				}

				i += 2;
			}
		}
	}
}

