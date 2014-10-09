using System;
using System.Linq;
using System.Collections.Generic;

namespace euler
{
	public static class P40
	{
		public static IEnumerable<int> Seq() {
			int i = 1;
			while (true) {
				int numDigits = Util.Digits(i);
				for (var d = numDigits - 1; d >= 0; --d) {
					yield return Util.Digit(i, d);
				}
				++i;
			}
		}

		public static int SolveWithDigits(IEnumerable<int> ds) {
			var s = Seq();
			return ds.Select(x => s.ElementAt(x - 1)).Aggregate((acc, x) => acc * x);
		}


		public static int Solve() {
			return SolveWithDigits(Enumerable.Range(0, 6).Select(x => (int)Math.Pow(10, x)));
		}
	}
}

