using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using Ch3Etah.Core;
using Microsoft.Win32;

namespace Ch3Etah.Gui {
	/// <summary>
	/// Performs required registrations to associate Ch3Etah file type in Windows Registry to this copy of Ch3Etah
	/// </summary>
	[RunInstaller(true)]
	public class ShellInstaller : Installer {
		#region Component Designer generated code

		private Container components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new Container();
		}

		#endregion

		public ShellInstaller() {
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		public override void Install(IDictionary stateSaver) {
			base.Install(stateSaver);

			// Adds the required keys to the Registry

			// Prepare paths
			Assembly assembly = this.GetType().Assembly;
			string exePath = string.Format("{0}\\{1}.exe", FileSystemHelper.GetAssemblyPath(assembly), assembly.GetName().Name);
			string consoleExePath = string.Format("{0}\\{1}.exe", FileSystemHelper.GetAssemblyPath(assembly), "Ch3Etah");

			// Extension key
			RegistryKey extKey = GetKey(".ch3", "Ch3Etah.Project");

			// Project key
			RegistryKey projectKey = GetKey("Ch3Etah.Project", "Ch3Etah Project");

			// Icon key
			RegistryKey iconKey = GetKey("DefaultIcon", exePath + ",0", projectKey);

			// "Open" and "Run" command
			RegistryKey shellKey = GetKey("Shell", "open", projectKey);
			RegistryKey openKey = ShellCommandKey("open", "Open", exePath + " \"%1\"", shellKey);
			RegistryKey runKey;
			if (File.Exists(consoleExePath)) {
				runKey = ShellCommandKey("generate", "Generate code", "\"" + consoleExePath + "\" \"%1\"", shellKey);
			}
			else {
				runKey = ShellCommandKey("generate", "Generate code", "\"" + exePath + "\" /run \"%1\"", shellKey);
			}

			// Close all the keys;
			runKey.Close();
			openKey.Close();
			shellKey.Close();
			iconKey.Close();
			projectKey.Close();
			extKey.Close();
			
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private RegistryKey GetKey(string keyName, string defaultValue) {
			return GetKey(keyName, defaultValue, Registry.ClassesRoot);
		}

		private RegistryKey GetKey(string keyName, string defaultValue, RegistryKey parentKey) {
			RegistryKey key = parentKey.OpenSubKey(keyName, true);
			if (key == null)
				key = parentKey.CreateSubKey(keyName);
			key.SetValue(null, defaultValue);
			return key;
		}

		private RegistryKey ShellCommandKey(string commandName, string commandDescription, string commandLine,
		                                    RegistryKey ShellKey) {
			RegistryKey baseKey = GetKey(commandName, commandDescription, ShellKey);
			RegistryKey cmdKey = GetKey("Command", commandLine, baseKey);

			return baseKey;
		}

	}
}