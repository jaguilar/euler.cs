using NUnit.Framework;
using System;
using System.Linq;

namespace euler
{
	[TestFixture()]
	public class P40Test
	{
		[Test()]
		public void TestSeq()
		{
			Console.WriteLine(String.Concat(P40.Seq().Take(40).Select(x=>x.ToString())));
		}

		[Test()]
		public void TestNth() {
			Assert.That(P40.Seq().ElementAt(12), Is.EqualTo(1));
		}

		[Test()]
		public void TestWithDigits() {
			//   v v v
			// 0.1234567...
			// 1 * 3 * 5 = 15.
			Assert.That(P40.SolveWithDigits(new [] { 1, 3, 5 }), Is.EqualTo(15));
		}

		[Test()]
		public void TestSolve() {
			Console.WriteLine(P40.Solve());
		}
	}
}

