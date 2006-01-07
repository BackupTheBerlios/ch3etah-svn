using System;
using System.Diagnostics;
using System.Reflection;

namespace Ch3Etah.Gui
{
	public class Utility
	{
		private Utility()
		{}

		public static string GetCh3EtahVersion() 
		{
			Assembly assembly = typeof(Utility).Assembly;
			AssemblyName name = assembly.GetName();
			return string.Format("{0}.{1}.{2}.{3}",
				name.Version.Major,
				name.Version.Minor,
				name.Version.Build,
				name.Version.Revision);
		}
		
		public static void OpenUrl(string url) 
		{
			string browser = GetDefaultBrowser();
			if (browser != string.Empty)
			{
				Process p = new Process();
				p.StartInfo.FileName = browser;
				p.StartInfo.Arguments = url;
				p.Start();
			}
			else
			{
				Process.Start(url);
			}
		}

		private static string GetDefaultBrowser()
		{
			string browser = string.Empty;
			Microsoft.Win32.RegistryKey key = null;
			try
			{
				key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

				//trim off quotes
				browser = key.GetValue(null).ToString().ToLower().Replace("\"", "");
				if (!browser.EndsWith("exe"))
				{
					//get rid of everything after the ".exe"
					browser = browser.Substring(0, browser.LastIndexOf(".exe")+4);
				}
			}
			finally
			{
				if (key != null) key.Close();
			}
			return browser;
		}

	}
}
