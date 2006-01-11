using System;

namespace Ch3Etah.Core
{
	internal class Utility
	{
		private Utility() {}

		/// <summary>
		/// Gets the plural form of a word.
		/// </summary>
		public static string GetPluralForm(string word) {
			if (word.EndsWith("y")) {
				if (word.EndsWith("ay") || word.EndsWith("ey") || word.EndsWith("oy") || word.EndsWith("iy") || word.EndsWith("uy")) {
					return word+ "s";
				}
				else {
					return word.Remove(word.Length-1,1) + "ies";
				}
			}
			else if (word.EndsWith("f")) {
				return word.Remove(word.Length-1,1) + "ves";
			}
			else if (word.EndsWith("fe")) {
				return word.Remove(word.Length-2,2) + "ves";
			}
			else if (word.EndsWith("x") || word.EndsWith("s")) {
				return word + "es";
			}
			else {
				return word + "s";
			}
		}

	}
}
