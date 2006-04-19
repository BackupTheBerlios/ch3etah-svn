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
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Security.Policy;
using SecurityPermission = System.Security.Permissions.SecurityPermission;

namespace Adapdev
{
	using log4net;

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	/// 
	public class AppDomainManager : LongLivingMarshalByRefObject
	{
		protected string domainName = String.Empty;
		protected AppDomain domain = null;
		protected string shadowCopyPath = AppDomain.CurrentDomain.BaseDirectory;
		protected AppDomainManager remote = null;
		protected DomainType domainType = DomainType.Local;
		protected string guid = String.Empty;
		protected bool unloaded = false;
		private string basedir = String.Empty;

		// create the logger
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Creates a new <see cref="AppDomainManager"/> instance.
		/// </summary>
		public AppDomainManager()
		{
			domain = AppDomain.CurrentDomain;
			basedir = AppDomain.CurrentDomain.BaseDirectory;
		}

		/// <summary>
		/// Creates a new <see cref="AppDomainManager"/> instance.
		/// </summary>
		/// <param name="shadowCopyDirectories">Shadow copy directories.</param>
		public AppDomainManager(params string[] shadowCopyDirectories) : this(AppDomain.CurrentDomain.BaseDirectory, "Test", String.Empty, shadowCopyDirectories){}

		/// <summary>
		/// Creates a new <see cref="AppDomainManager"/> instance.
		/// </summary>
		/// <param name="basedir">Basedir.</param>
		/// <param name="configFile">Name of the configuration file to use.</param>
		/// <param name="shadowCopyDirectories">Shadow copy directories.</param>
		public AppDomainManager(string basedir, string configFile, params string[] shadowCopyDirectories) : this(basedir, "Test", configFile, shadowCopyDirectories){}
		
		/// <summary>
		/// Creates a new <see cref="AppDomainManager"/> instance.
		/// </summary>
		/// <param name="basedir">The base directory to use.</param>
		/// <param name="shadowCopyDirectories">Shadow copy directories.</param>
		public AppDomainManager(string basedir, params string[] shadowCopyDirectories) : this(basedir, "Test", shadowCopyDirectories){}

		/// <summary>
		/// Creates a new <see cref="AppDomainManager"/> instance.
		/// </summary>
		/// <param name="domainName">Name of the domain.</param>
		/// <param name="configurationFile">Configuration file.</param>
		/// <param name="shadowCopyDirectories">Shadow copy directories.</param>
		public AppDomainManager(string basedir, string domainName, string configurationFile, params string[] shadowCopyDirectories)
		{
			this.domainName = domainName;
			this.domainType = DomainType.Remote;

			if(log.IsDebugEnabled) log.Debug("Loading new AppDomainManager");

			Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
			Evidence evidence = new Evidence(baseEvidence);

			AppDomainSetup setup = new AppDomainSetup();
			setup.ApplicationBase = basedir;
			setup.ApplicationName = "Test";
			if(configurationFile.Length > 0) setup.ConfigurationFile = configurationFile;

			if(log.IsDebugEnabled)
			{
				log.Debug("ApplicationBase: " + setup.ApplicationBase);
				log.Debug("ApplicationName: " + setup.ApplicationName);
				log.Debug("ConfigurationFile: " + setup.ConfigurationFile);
			}

			if(shadowCopyDirectories != null && shadowCopyDirectories.Length >= 0)
			{
				guid = System.Guid.NewGuid().ToString();
				setup.ShadowCopyFiles = "true";
				setup.CachePath = this.GetCachePath();
				foreach (string shadowCopyDirectory in shadowCopyDirectories)
				{
					string dir = String.Empty;

					if(File.Exists(shadowCopyDirectory))
					{
						// if it's a file, grab the directory name
						FileInfo f = new FileInfo(shadowCopyDirectory);
						dir = f.DirectoryName;
					}
					else
					{
						dir = shadowCopyDirectory;
					}
					
					if(setup.ShadowCopyDirectories != null && setup.ShadowCopyDirectories.Length > 0)
					{
						setup.ShadowCopyDirectories += ";" + dir;
					}
					else
					{
						setup.ShadowCopyDirectories += dir;
					}
				}
			}

			domain = AppDomain.CreateDomain(this.domainName, evidence, setup);
			remote = (AppDomainManager)domain.CreateInstanceAndUnwrap(typeof(AppDomainManager).Assembly.FullName, typeof(AppDomainManager).FullName);
		}

		/// <summary>
		/// Adds the assembly.
		/// </summary>
		/// <param name="path">Path.</param>
		public void AddAssembly(string path)
		{
			string assemblyDirectory = Path.GetDirectoryName( path );
			if(log.IsDebugEnabled)log.Debug("Adding " + path);
			
			if(this.domainType == DomainType.Local)
			{
				domain.AppendPrivatePath(assemblyDirectory);
				this.LoadAssembly(path);
			}
			else
			{
				domain.AppendPrivatePath(assemblyDirectory);
				remote.LoadAssembly(path);
			}
		}


		/// <summary>
		/// Adds the assemblies.
		/// </summary>
		/// <param name="paths">Paths.</param>
		public void AddAssemblies(params string[] paths)
		{
			foreach (string path in paths)
			{
				try
				{
					this.AddAssembly(path);
				}
				catch(FileNotFoundException){}
			}
		}

		/// <summary>
		/// Adds all the dlls in the directory to the AppDomain
		/// </summary>
		/// <param name="path">Path.</param>
		public void AddDirectory(string path)
		{
			foreach (string file in Directory.GetFiles(path))
			{
				FileInfo f = new FileInfo(file);
				if(f.Extension == "dll")
				{
					this.AddAssembly(file);
				}
			}
		}

		/// <summary>
		/// Unloads the AppDomain.
		/// </summary>
		public void Unload()
		{
			if(!(this.domainName == String.Empty) && !unloaded)
			{
				if(log.IsDebugEnabled)
				{
					foreach(AssemblyInfo a in this.GetLoadedAssemblies())
					{
						log.Debug("Unloading: " + a.Name + " - " + a.Version + " - " + a.Location);
					}
				}

				// Must unload AppDomain first, before you can delete the shadow copy directory
				AppDomain.Unload(this.domain);

				if(this.guid.Length > 0 && Directory.Exists(this.GetCachePath()))
				{
					Directory.Delete(this.GetCachePath(), true);
				}

				unloaded = true;
				if(log.IsDebugEnabled) log.Debug("Domain unloaded.");
			}			
		}

		/// <summary>
		/// Gets the domain.
		/// </summary>
		/// <value></value>
		public AppDomain Domain
		{
			get { return domain; }
		}

		/// <summary>
		/// Loads the assembly.
		/// </summary>
		/// <param name="path">Path.</param>
		protected void LoadAssembly(string path)
		{
			AssemblyCache.Add(path.ToLower(), path);
		}

		/// <summary>
		/// Gets the loaded assemblies.
		/// </summary>
		public AssemblyInfo[] GetLoadedAssemblies()
		{
			AssemblyInfo[] ass = null;
			if(this.domainType == DomainType.Local){
				ass = new AssemblyInfo[AppDomain.CurrentDomain.GetAssemblies().Length];
				int i = 0;
				foreach (Assembly assembly in domain.GetAssemblies())
				{
					try
					{
						AssemblyInfo ai = new AssemblyInfo();
						ai.CodeBase = assembly.CodeBase;
						ai.FullName = assembly.FullName;
						ai.Location = assembly.Location;
						ai.Name = assembly.GetName().Name;
						ai.Version = assembly.GetName().Version.ToString();
						ass[i] = ai;
						i++;
					}
					// In case it's a dynamic assembly, it can't be analyzed
					catch(Exception){}
				}
			}else
			{
				ass = remote.GetLoadedAssemblies();
			}
			return ass;
		}

		/// <summary>
		/// Determines whether the specified path is loaded.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is loaded; otherwise, <c>false</c>.
		/// </returns>
		public bool IsLoaded(string path)
		{
			if(this.domainType == DomainType.Local)
				return AssemblyCache.ContainsByPath(path);
			else
				return remote.IsLoaded(path);
		}

		/// <summary>
		/// Gets the cache path.
		/// </summary>
		/// <returns></returns>
		internal string GetCachePath()
		{
			if(guid.Length > 0)
				return Path.Combine(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "zanebugcache"),guid);
			return String.Empty;
		}

		/// <summary>
		/// Gets the domain type.
		/// </summary>
		/// <value></value>
		public DomainType DomainType
		{
			get { return domainType; }
		}

		/// <summary>
		/// Gets the object by path.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="classname">Classname.</param>
		/// <returns></returns>
		public object GetObjectByPath(string path, string classname)
		{
			string filename = Path.GetFileNameWithoutExtension(path);
			return this.GetObjectByName(filename, classname);
		}

		/// <summary>
		/// Gets the object by name.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="classname">Classname.</param>
		/// <returns></returns>
		public object GetObjectByName(string name, string classname)
		{
			return domain.CreateInstanceAndUnwrap(name, classname);
		}

	}

	public enum DomainType
	{
		Local,
		Remote
	}
}
