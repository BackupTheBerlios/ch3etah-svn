using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Console {
	
	internal class Program {
		
		private static bool verbose;
		private static string projectFileName;
		private static Project project;
		private static bool cancelGeneration = false;

		/// <summary>
		/// Program's entry-point
		/// </summary>
		/// <remarks>
		/// Exit codes:
		/// <ul>
		///		<li>1: Invalid parameters</li>
		///		<li>2: Invalid file and/or path</li>
		///		<li>3: Invalid project file format</li>
		///		<li>4: Generation engine error</li>
		/// </ul>
		/// </remarks>
		/// <param name="args">
		///	Command line parameters currently supported:
		/// <ul>
		///		<li>&lt;FileName&gt;: Project file to be ran</li>
		///		<li>-v: Verbose mode</li>
		///		<li>-h: Display usage</li>
		/// </ul>
		/// </param>
		[STAThread]
		private static void Main(string[] args) {
			if (CheckParameters(args))
				RunProject();

			if (Environment.ExitCode != 0) {
				System.Console.WriteLine("\n\nPress Enter to quit...");
				System.Console.ReadLine();
			}
			else {
				Thread.Sleep(2000);
			}

		}

		#region Command-line processing and support

		private static bool CheckParameters(string[] args) {
			if (args == null || args.Length < 1) {
				WriteLine("Syntax error: Missing arguments\n\n");
				DisplayUsage();
				Environment.ExitCode = 1;
				return false;
			}

			// Check arguments
			for (int i = 0; i < args.Length; i++) {
				switch (args[i]) {
					case "-v":
						{
							verbose = true;
							break;
						}
					case "-h":
					case "/?":
						{
							DisplayUsage();
							return false;
						}
					default:
						{
							// Check file name
							if (!File.Exists(args[i])) {
								WriteLine("Syntax error: Invalid file name or path ({0})\n\n", args[i]);
								DisplayUsage();
								Environment.ExitCode = 2;
								return false;
							}
							else {
								projectFileName = args[i];
							}
							break;
						}
				}
			}

			return true;
		}

		private static void WriteLine(string format, params object[] args) {
			System.Console.WriteLine(format, args);
		}

		#region Usage

		private static void DisplayUsage() {
			DisplayVersion();
			WriteLine(@"
Syntax:

    {0} <File name> [-v]
    {0} -h|/?

Where:

    <File Name>: Project file to be ran
    -v:          Verbose mode
    -h, /?:      Display this usage screen
",
				typeof (Program).Assembly.GetName().Name);
		}

		private static void DisplayVersion() {
			WriteLine("Ch3Etah command-line utility version {0}\n", typeof (Program).Assembly.GetName().Version);
		}

		#endregion

		#endregion

		#region Project execution

		private static void RunProject() {
			// Adjust verbosity
			SetupVerbosity();

			// Load the project
			if (!LoadProject())
				return;

			// Output the project summary
			DisplayVersion();
			WriteLine("Starting the code generation process\n");
			WriteLine("Project summary");
			WriteLine("---------------\n");
			WriteLine(" Project name: {0}", project.Name);
			WriteLine(" Current dir : {0}", Directory.GetCurrentDirectory());
			WriteLine(" Packages dir: {0}", project.TemplatePackageBaseDir);
			WriteLine(" Metadata dir: {0}\n", project.MetadataBaseDir);
			WriteLine(" Output dir : {0}\n", project.OutputBaseDir);
			WriteLine("Code generation");
			WriteLine("---------------\n");
			
			// Execute the code generator commands
			DateTime start = DateTime.Now;
			foreach (GeneratorCommand command in project.GeneratorCommands) {
				WriteLine(" Generating {0}...", command.Name);
				if (cancelGeneration) {
					WriteLine("\n\nGeneration interrupted.");
					break;
				}
				command.Execute();
			}
			WriteLine("\nTime to generate project: {0}\n", (DateTime.Now - start).ToString());
		}

		private static void SetupVerbosity() {
			if (!verbose)
				Trace.Listeners.Clear();
			else
				Trace.Listeners.Add(new TextWriterTraceListener(System.Console.OpenStandardOutput()));
		}

		private static bool LoadProject() {
			try {
				// Load the project
				project = Project.Load(projectFileName);
				if (!project.IsFileVersionCompatible)
					throw new Exception("Incompatible project file version");

				// Set the current directory to the project's base dir
				Directory.SetCurrentDirectory(Path.GetDirectoryName(projectFileName));

				return true;
			}
			catch (Exception e) {
				WriteLine("Error loading project file \"{0}\": {1}", projectFileName, e.Message);
				return false;
			}
		}

		#endregion
	}
}