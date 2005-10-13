/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 26/09/2004
 */

using System;
using System.IO;

namespace Ch3Etah.Core.Config
{
	/// <summary>
	/// Description of FileSystemHelper.	
	/// </summary>
	public class PathResolver
	{
		private static char[] _pathSeparators = {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar};
		
		public static string GetRelativePath(string fromPath, string toPath) {
			return GetRelativePath(fromPath, toPath, Path.DirectorySeparatorChar);
		}
		
		public static string GetRelativeUrl(string fromPath, string toPath) {
			return GetRelativePath(fromPath, toPath, Path.AltDirectorySeparatorChar);
		}
		
		private static string GetRelativePath(string fromPath, string toPath, char separatorChar) {
			
			if (fromPath == "" || toPath == "" ) {
				return toPath;
			}
			
			string[] basePath = Path.GetFullPath(fromPath).Split(_pathSeparators);
			string[] destPath = Path.GetFullPath(toPath).Split(_pathSeparators);
			
			int x = 0;
			for ( ; x < basePath.Length && x < destPath.Length; ++x) {
				if (basePath[x] != destPath[x]) {
					break;
				}
			}
			
			if (x == 0) {
				return Path.GetFullPath(toPath);
			}
			
			string relativePath = ".";
			
			for (int y = x ; y < basePath.Length; ++y) {
				relativePath += separatorChar + "..";
			}
			
			for (int y = x ; y < destPath.Length; ++y) {
				relativePath += separatorChar + destPath[y];
			}
			
			return relativePath;
		}
		
		public static string GetPluralForm(string word) {
			if (word.EndsWith("y")) {
				if (   word.EndsWith("ay") 
					|| word.EndsWith("ey") 
					|| word.EndsWith("oy") 
					|| word.EndsWith("iy")
					|| word.EndsWith("uy")) {
					return word + "s";
				}
				else {
					return word.Remove(word.Length-1, 1) + "ies";
				}
			}
			else if (word.EndsWith("f")) {
				return word.Remove(word.Length-1, 1) + "ves";
			}
			else if (word.EndsWith("fe")) {
				return word.Remove(word.Length-2, 2) + "ves";
			}
			else if (word.EndsWith("x")) {
				return word + "es";
			}
			else {
				return word + "s";
			}
		}
	}
}
