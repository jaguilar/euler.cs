using System;
using System.Linq;
using System.Collections.Generic;

namespace euler
{
	public class P39
	{
		public class IntegralRightTriangle : Tuple<int, int, int>
		{
			public IntegralRightTriangle(int a, int b, int c) : base(a, b, c)
			{
				if (a > b || b > c) {
					throw new Exception(String.Format("Illegal params {0} {1} {2}", a, b, c));
				}
			}

			public override string ToString()
			{
				return String.Format("Triangle({0} {1} {2})", A, B, C);
			}

			public int A { get { return Item1; } }

			public int B { get { return Item2; }}

			public int C { get { return Item3; }}

			public static IEnumerable<IntegralRightTriangle> Generate()
			{
				double tolerance = 0.000001;
				int b = 1;

				while (true) {
					int a = 1;
					while (a <= b) {
						double c = Math.Sqrt(a * a + b * b);
						int cRounded = (int)Math.Round(c);
						if (Math.Abs(c - cRounded) < tolerance) {
							// Found one.
							yield return new IntegralRightTriangle(a, b, cRounded);
						}
						++a;
					}
					++b;
				}
			}
		}

		// a^2 + b^2 == c^2 && a + b + c == p
		public static IEnumerable<IntegralRightTriangle> Matches(int perimeter)
		{
			return IntegralRightTriangle.Generate().TakeWhile(x => x.B < perimeter).Where(x => x.A + x.B + x.C == perimeter);
		}

		public static int Solve(int maxP) {
			return Enumerable.Range(3, maxP - 2).OrderBy(x => -Matches(x).Count()).First();
		}
	}
}

