#region Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion

namespace Adapdev.IO
{
	using System;
	using System.IO;

	public class FileUtil
	{
		private FileUtil()
		{
		}

		public static void CreateFile(string filePath, string content)
		{
			FileStream fs = null;
			StreamWriter sw = null;
			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(filePath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(filePath));
				}
				Console.WriteLine("creating file: " + filePath);
				fs = new FileStream(filePath, FileMode.Create);
				sw = new StreamWriter(fs);
				sw.Write(content);
			}
			catch (Exception e)
			{
				Console.WriteLine("FileUtil::CreateFile " + e.Message);
			}
			finally
			{
				if (fs != null)
				{
					sw.Close();
					fs.Close();
				}
			}
		}

		public static string ReadFile(string filePath)
		{
			StreamReader sr = null;
			string content = "";
			try
			{
				sr = new StreamReader(filePath);
				content = sr.ReadToEnd();
			}
			catch (Exception e)
			{
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
			return content;
		}
	}
}