using NUnit.Framework;
using System;

namespace euler
{
	[TestFixture()]
	public class P60Test
	{
		[Test()]
		public void TestCase()
		{
			Assert.That(P60.Solve(4), Is.EquivalentTo(new [] { 3, 7, 109, 673 }));
		}
	}
}

