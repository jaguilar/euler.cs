using NUnit.Framework;
using System;

namespace euler
{
	[TestFixture()]
	public class UtilTest
	{
		[Test()]
		public void TestDigits()
		{
			Assert.That(Util.Digits(123), Is.EqualTo(3));
			Assert.That(Util.Digits(0), Is.EqualTo(1));
			Assert.That(Util.Digits(9), Is.EqualTo(1));
			Assert.That(Util.Digits(999), Is.EqualTo(3));
		}

		[Test()]
		public void TestDigit() {
			Assert.That(Util.Digit(123, 0), Is.EqualTo(3));
			Assert.That(Util.Digit(123, 1), Is.EqualTo(2));
			Assert.That(Util.Digit(123, 2), Is.EqualTo(1));
			Assert.That(Util.Digit(123, 3), Is.EqualTo(0));
		}
	}
}

