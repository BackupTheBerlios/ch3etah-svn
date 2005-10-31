using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Ch3Etah.Core {
	/// <summary>
	/// Summary description for FileSystemHelper.
	/// </summary>
	public class FileSystemHelper {
		
		public static void CreateDirectory(FileInfo path) {
			CreateDirectory(path.Directory);
		}
		
		public static void CreateDirectory(DirectoryInfo path) {
			string[] tokens = path.FullName.Split('\\');
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < tokens.Length; i++) {
				sb.Append(tokens[i]);
				sb.Append('\\');
				string partialPath = sb.ToString();
				if (!Directory.Exists(partialPath)) {
					Trace.WriteLine(string.Format("Directory '{0}' not found. Creating now.", partialPath));
					Directory.CreateDirectory(partialPath);
				}
			}
		}

		public static string GetAssemblyPath(Assembly assembly) {
			string codeBase = assembly.CodeBase;
			return codeBase.Substring(8, codeBase.LastIndexOf('/') - 8).Replace('/', '\\');
		}

	}
}