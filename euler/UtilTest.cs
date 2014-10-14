using NUnit.Framework;
using System;
using System.Linq;

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

		[Test()]
		public void TestDigitSeq() {
			Assert.That(Util.DigitSeq(107), Is.EqualTo(new [] { 1, 0, 7 }));
		}

		[Test()]
		public void TestDigitCat() {
			Assert.That(Util.DigitCat( new[]{2,1, 7}), Is.EqualTo(217));
		}

		[Test()]
		public void TestPermutationIndexes() {
			Assert.That(Util.IndexPermutations(2), Is.EquivalentTo(new int[][] { new int[] { 0, 1}, new int[] {1, 0} }));
			Assert.That(Util.IndexPermutations(5), Has.Some.EquivalentTo(new int[]{ 4, 2, 3, 1, 0 }));
			Assert.That(Util.IndexPermutations(5).Distinct().Count(), Is.EqualTo(5 * 4 * 3 * 2 * 1));
		}

		[Test()]
		public void TestPermutations() {
			var permuted = Util.Permutations(new[] { "abc", "def", "ghi" });
			Assert.That(permuted, Has.Some.EquivalentTo(new[]{"def", "abc", "ghi"}));
			Assert.That(permuted.Distinct().Count(), Is.EqualTo(3 * 2 * 1));
		}

		[Test()]
		public void TestCombinations() {
			var c = (new[] { "abc", "def", "ghi" }).Choose(2);
			Assert.That(c, Has.Some.EquivalentTo(new[] { "def", "ghi" }));
			Assert.That(c.Distinct().Count(), Is.EqualTo(3));

			Assert.That(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Choose(4), Has.Some.EquivalentTo(new [] { 3, 5, 6, 7 }));
		}

		[Test()]
		public void TestPrimes() {
			var ps = Util.Primes.TakeWhile(x => x < 100);
			Assert.That(ps, Has.Some.EqualTo(2));
			Assert.That(ps, Has.Some.EqualTo(3));
			Assert.That(ps, Has.Some.EqualTo(7));
			Assert.That(ps, Has.Some.EqualTo(71));
			Assert.That(ps, Has.None.EqualTo(10));
			Assert.That(ps, Has.None.EqualTo(4));
			Assert.That(ps, Has.None.EqualTo(99));

			Assert.That(Util.Primes.Contains(71));
		}
	}
}

