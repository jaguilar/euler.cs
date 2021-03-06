using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace euler
{

	public static class Util
	{
		public static int Digits(int n)
		{
			return (int)Math.Floor(Math.Log10(n)) + 1;
		}

		/// <summary>
		/// The nth digit of a number, zero being the least significant digit.
		/// </summary>
		public static int Digit(int n, int d)
		{
			return (n / ((int)Math.Pow(10, d))) % 10;
		}

		/// <summary>
		/// Sequence of digits in n, from most to least significant.
		/// </summary>
		public static IEnumerable<int> DigitSeq(int n)
		{
			for (int i = Digits(n) - 1; i >= 0; --i) {
				yield return Digit(n, i);
			}
		}

		public static int DigitCat(IEnumerable<int> s) {
			return s.Aggregate(0, (agg, digit) => agg * 10 + digit);
		}

		// Given a sequence, generate possible permutations of that sequence.
		public static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> seq)
		{
			T[] xs = seq.ToArray();  // Any old ordering will do as the start point of our permuter.
			return IndexPermutations(xs.Length).Select(SelectIndexes(xs));
		}

		private static Func<int[], IEnumerable<T>> SelectIndexes<T>(T[] xs)
		{
			return (ordering => {
				Debug.Assert(ordering.Length <= xs.Length);
				T[] ns = new T[ordering.Length];
				for (int i = 0; i < ordering.Length; ++i) {
					ns[i] = xs[ordering[i]];
				}
				return ns;
			});
		}


		public static void Swap<T>(ref T l, ref T r)
		{
			T temp = r;
			r = l;
			l = temp;
		}

		public static IEnumerable<int[]> IndexCombinations(int n, int c)
		{
			int[] xs = Enumerable.Range(0, c).ToArray();

			while (true) {
				yield return xs.ToArray();

				int pivot = PivotIndex(xs, n);
				if (pivot == -1) {
					yield break;
				}

				int newBase = xs[pivot] + 1;
				for (int i = 0; pivot + i < xs.Length; ++i) {
					xs[pivot + i] = newBase + i;
				}
			}
		}

		private static int PivotIndex(int[] choices, int numOptions)
		{
			int lastOption = numOptions - 1;
			int lastIndex = choices.Length - 1;

			for (int i = lastIndex; i >= 0; --i) {
			 	int distanceToEnd = lastIndex - i;

				// The last index can't be incremented above numOptions - 1.
				// As you move left from the last index, the choices that
				// can be selected decrease by one each time.
				if (choices[i] < lastOption - distanceToEnd) {
					return i;
				}
			}

			return -1;  // No more combinations
		}

		public static IEnumerable<int[]> IndexPermutations(int n)
		{
			int[] xs = Enumerable.Range(0, n).ToArray();

			while (true) {
				yield return xs.ToArray();

				// Rightmost character, which is smaller than the next.
				int left = n - 2;
				while (xs[left] >= xs[left + 1]) {
					--left;
					if (left < 0) {
						yield break;
					}
				}

				int right = -1;
				for (int i = left + 1; i < n; ++i) {
					// If this index is both less than the right index we have AND greater than
					// the value at the left index . . .
					if (right == -1 || (xs[right] > xs[i] && xs[i] > xs[left])) {
						right = i;
					}                   
				}

				Swap(ref xs[left], ref xs[right]);
				Array.Sort(xs, left + 1, xs.Length - (left + 1));
			}
		}

		public class PrimeSeq : IEnumerable<int> {
			private List<int> primes = new List<int>();
			private HashSet<int> primesHash = new HashSet<int>();

			// ONLY for use by the Advance method.
			private IEnumerator<int> internalEnumerator;

			public PrimeSeq()
			{
				internalEnumerator = PrimeGen().GetEnumerator();
			}

			/// <summary>
			/// Special implementation of Contains, which looks at the cache of primes
			/// rather than iterating from the beginning.
			/// </summary>
			public bool Contains(int x) {
				while (primes[primes.Count - 1] < x) {
					Advance();
				}
				return primesHash.Contains(x);
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			public IEnumerator<int> GetEnumerator() {
				return Yielder().GetEnumerator();
			}

			private IEnumerable<int> Yielder() {
				int i = 0;
				while (true) {
					if (primes.Count > i) {
						yield return primes[i];
					} else {
						Advance();
						yield return primes[i];
					}
					++i;
				}
			}

			private void Advance() {
				internalEnumerator.MoveNext();
				primes.Add(internalEnumerator.Current);
				primesHash.Add(internalEnumerator.Current);
			}

			/// <summary>
			/// Generates all primes.
			/// </summary>
			private IEnumerable<int> PrimeGen() {
				yield return 2;

				int i = 3;
				while (true) {
					int limit = (int) Math.Ceiling(Math.Sqrt(i));
					if (!primes.Skip(1).TakeWhile(x => x <= limit).Where(x => i % x == 0).Any()) {
						// Note that since the generator is only used by the Advance method,
						// we can count on the new prime being installed in primes before the
						// next pass through the iteration.
						//
						// Skip(1): None of the numbers we're looking at will be divisible by 2.
						yield return i;
					}
					i += 2;
				}
			}
		}

		static Util() {
			Primes = new PrimeSeq();
		}

		public static PrimeSeq Primes { get; private set; }
	}

	public static class ChooseExt
	{
		/// <summary>
		/// Return all combinations of size c from the elements in Seq.
		/// This is calculated on a streaming basis, so it will return combinations of infinitely-sized
		/// input sequences. The combinations are by index, not by value. It is an error to pass 
		/// a numToChoose greater than the number of elements in the sequence.
		/// </summary>
		/// <param name="seq">A sequence, possibly infinitely long.</param>
		/// <param name="numToChoose">The number of elements to choose from.</param>
		/// <typeparam name="T">The type of object in the sequence we are choosing against.</typeparam>
		public static IEnumerable<IEnumerable<T>> Choose<T>(this IEnumerable<T> seq, int numToChoose) {
			if (numToChoose == 1) {
				foreach (var x in seq) {
					yield return x.Yield();
				}
				yield break;
			}

			// Otherwise, build up a list of possible recursive Chooses. At any given index such that there
			// are at least numToChoose elements in the list, the last index yielded from seq will be one
			// of the members of the combination, and we'll recurse on the list. This has the effect of
			// yielding combinations in sorted order.
			var subList = new List<T>();
			foreach (var x in seq) {
				if (subList.Count >= numToChoose - 1) {
					// Recurse.
					foreach (var subCombination in subList.Choose(numToChoose - 1)) {
						yield return Enumerable.Concat(subCombination, x.Yield());
					}
				}
				subList.Add(x);
			}
		}
	}

	// From StackOverflow, license unknown. Will happily take down if asked.
	public static class IEnumerableExt
	{
		/// <summary>
		/// Wraps this object instance into an IEnumerable&lt;T&gt;
		/// consisting of a single item.
		/// </summary>
		/// <typeparam name="T"> Type of the object. </typeparam>
		/// <param name="item"> The instance that will be wrapped. </param>
		/// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
		public static IEnumerable<T> Yield<T>(this T item)
		{
			yield return item;
		}
	}
}

