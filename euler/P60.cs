using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace euler
{
	public static class P60
	{
		public static int Combine(int x, int y)
		{
			return Util.DigitCat(Enumerable.Concat(Util.DigitSeq(x), Util.DigitSeq(y)));
		}

		public static bool IsPrimePair(Tuple<int, int> t)
		{
			Debug.Assert(t.Item1 < t.Item2);
			return Util.Primes.Contains(Combine(t.Item1, t.Item2)) && Util.Primes.Contains(Combine(t.Item2, t.Item1));
		}

		public static IEnumerable<Tuple<int, int>> PairGen()
		{
			// Skip the first element (2), because it will never be part of a prime pair.
			return Util.Primes.Skip(1).Choose(2).Select(x => new Tuple<int, int>(x.First(), x.Last())).Where(IsPrimePair);
		}

		public class EnumCmp<T> : IComparer<IEnumerable<T>> where T : IComparable {
			public int Compare(IEnumerable<T> x, IEnumerable<T> y) {
				foreach (var c in x.Zip(y, (i, j) => i.CompareTo(j))) {
					if (c != 0) { return c; }
				}
				return x.Count() - y.Count();
			}
		}

		public static IEnumerable<IEnumerable<int>> SetGen(int size)
		{
			// setsOfRank[0] == all two-sets.
			// setsOfRank[1] == all three-sets.
			// etc.
			// setsOfRank[size-2] == sets of the size we're interested in.
			var setsOfRank = new SortedSet<List<int>>[size - 1];
			for (int i = 0; i<setsOfRank.Count(); ++i) {
				setsOfRank[i] = new SortedSet<List<int>>(new EnumCmp<int>());
			}
			int latestPrime = 0;
			var pairsWithLatest = new SortedSet<int>();

			foreach (var pair in PairGen()) {
				if (pair.Item2 != latestPrime) {
					// We've moved on to another prime. Update setsOfRank with the pairs of the current prime.
					for (int i = 0; i < setsOfRank.Length - 1; ++i) {
						foreach (var completeSet in setsOfRank[i].Where(x => x.All(y => pairsWithLatest.Contains(y)))) {
							// For each set where all members are also pairs with the current prime,
							// promote that set to the next rank, with the current prime included.
							var promotedSet = completeSet.Concat(latestPrime.Yield());
							if (promotedSet.Count() < size) {
								setsOfRank[i + 1].Add(new List<int>(promotedSet));
							} else {
								// We've found a set of the appropriate size. Yield it!
								yield return promotedSet;
							}
						}
					}

					// Now we've promoted everything, so update latestPrime.
					latestPrime = pair.Item2;
					pairsWithLatest.Clear();
					Console.WriteLine("Working on {0}", latestPrime);
				}

				pairsWithLatest.Add(pair.Item1);
				setsOfRank[0].Add(new List<int>(new[]{pair.Item1, pair.Item2}));
			}
		}

		public static IEnumerable<int> Solve(int n)
		{
			var set = SetGen(n).First();
			Console.WriteLine(String.Join(",", set));
			return set;
		}

		public static void Main()
		{
			P60.Solve(5);
		}
	}
}

