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
 *   Date: 30/9/2004
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using Ch3Etah.Gui.Widgets;

namespace Ch3Etah.Gui {
	public class MainClass {

		public string teste;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static int Main(string[] args) {
			
			SplashScreen.Show();

//			ErrorReporter.Register();

//			try {
//				Application.EnableVisualStyles();
//			}
//			catch {}
			
//			XmlSchemaCollection sc = new XmlSchemaCollection();
//			Assembly assembly = typeof(Ch3Etah.Core.xsd.Metadata).Assembly;
//			Stream stream = assembly.GetManifestResourceStream("Ch3Etah.Core.xsd.Metadata.xsd");
//			XmlTextReader reader = new XmlTextReader(stream);
//			XmlSchema schema = sc.Add(null, @"file://C:\Projetos .NET\ForumAccess\Metodologia200\Ch3Etah\Fontes\src\Core\xsd\metadata.xsd");
			
			switch (args.Length) {
				case 0:
					{
						Application.Run(new MainForm());
						break;
					}
				case 1:
					{
						Application.Run(new MainForm(args[0]));
						break;
					}
				case 2:
					{
						string cmdSwitch = args[1];
						switch (cmdSwitch) {
							case "/run":
								{
									string pathName = args[0];
									if (File.Exists(pathName)) {
										// Argument is a file name - run it
										FileInfo fileInfo = new FileInfo(pathName);
										Application.Run(new MainForm(new string[] {fileInfo.FullName}));
									}
									else if (Directory.Exists(pathName)) {
										// Argument is a directory - run all of the .ch3 files in sequence
										DirectoryInfo dirInfo = new DirectoryInfo(pathName);
										FileInfo[] files = dirInfo.GetFiles("*.ch3");
										string[] fileNames = new string[files.Length];
										for (int i = 0; i < files.Length; i++) {
											FileInfo fileInfo = files[i];
											fileNames[i] = fileInfo.FullName;
										}
										Array.Sort(fileNames);
										Application.Run(new MainForm(fileNames));
									}
									break;
								}
							default:
								{
									MessageBox.Show(String.Format("Invalid switch: {0}", cmdSwitch), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									break;
								}
						}
						break;
					}
			}
			SplashScreen.Hide();
			return 0;
		}
	}

}