using NUnit.Framework;
using System;

namespace euler
{
	[TestFixture()]
	public class P39Test
	{
		private P39.IntegralRightTriangle IRT(int a, int b, int c) {
			return new P39.IntegralRightTriangle(a, b, c);
		}

		[Test()]
		public void TestMatches()
		{
			Assert.That(P39.Matches(120), Is.EquivalentTo(new []{IRT(20,48,52), IRT(24,45,51), IRT(30,40,50)}));
		}

		[Test()]
		[Ignore]
		public void TestSolve()
		{
			Console.WriteLine(P39.Solve(1000));
		}
	}
}

