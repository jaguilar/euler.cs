using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace euler
{
	public static class P59
	{
		private static HashSet<string> dict = Words();

		public static byte[] XorArray(byte[] orig, byte[] pwd)
		{
			var r = new byte[orig.Length];

			for (int i = 0; i < orig.Length; ++i) {
				r[i] = (byte) (orig[i] ^ pwd[i % pwd.Length]);
			}

			return r;
		}

		public static IEnumerable<byte[]> PwdGen()
		{
			return from a in Enumerable.Range(0, 26) from b in Enumerable.Range(0, 26) from c in Enumerable.Range(0, 26)
			select new byte[] { (byte) ('a' + a), (byte) ('a' + b), (byte) ('a' + c) };
		}

		/// <summary>
		/// Get a hash set of words from the dictionary file on the machine.
		/// </summary>
		public static HashSet<string> Words() {
			var r = new HashSet<string>();
			var reader = new StreamReader("/usr/share/dict/words");
			while (!reader.EndOfStream) {
				r.Add(reader.ReadLine().ToLower());
			}
			return r;
		}

		public static IEnumerable<string> SplitWords(string x) {
			return Regex.Split(x, "\\W");
		}

		public static double RealWordProportion(IEnumerable<string> ss) {
			return (double)(from s in ss where dict.Contains(s.ToLower()) select s).Count() / ss.Count();
		}

		public static void Search()
		{
			var text = (new StreamReader(new FileStream("./P59/p059_cipher.txt", FileMode.Open))).ReadToEnd();
			var encrypted = text.Split(',').Select(x => (byte)int.Parse(x)).ToArray();

			var unfilteredClearTexts = PwdGen().Select(pwd => string.Concat(XorArray(encrypted, pwd).Select(c => Char.ConvertFromUtf32(c))));

			// The clear text with the highest proportion of real words in the first hundred chars comes first.
			var orderedClearTexts = unfilteredClearTexts.OrderBy(x => -RealWordProportion(SplitWords(x.Substring(0, 100))));

			// Experimentally, we found that the first text is the correct answer.
			var answerText = orderedClearTexts.First();
			var answer = Encoding.ASCII.GetBytes(answerText).Select(x => (int) x).Sum();
			Console.WriteLine(answer);
		}
	}
}

