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
 *   Date: 9/6/2004
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Metadata.OREntities;
using Ch3Etah.Core.CodeGen.PackageLib;

namespace Ch3Etah.Core.ProjectLib {
	/// <summary>
	/// Represents a Ch3Etah project file and its sub-objects.
	/// </summary>
	[XmlRoot(ElementName="Ch3EtahProject")]
	public class Project {
		// TODO: RESOURCES
		// Extending VS - Custom tools that run when file changes, etc..: 
		//		http://msdn.microsoft.com/msdnmag/issues/02/10/nettopten/
		// .NET canvas implementations:
		//		http://www.cs.umd.edu/hcil/piccolo/download/index.shtml <-- looks very professional
		//		http://www.codeproject.com/cs/miscctrl/canvas.asp
		// PropertyBrowser stuff:
		//		http://www.codeproject.com/cs/miscctrl/bending_property.asp

		#region Constructors and Member Variables

		private const int FILE_VERSION_MAJOR = 0;
		private const int FILE_VERSION_MINOR = 52;
		
		private string _loadedState;
		private Guid _guid = Guid.NewGuid();
		private string _name = "";
		private string _fileName = "";
		private int _fileVersionMajor = FILE_VERSION_MAJOR;
		private int _fileVersionMinor = FILE_VERSION_MINOR;
		private string _metadataBaseDir = "";
		private string _templatePackageBaseDir = "";
		private string _outputBaseDir = "";
		private MetadataFileCollection _metadataFiles = new MetadataFileCollection();
		private InputParameterCollection _inputParameters = new InputParameterCollection();
		private GeneratorCommandCollection _GeneratorCommands = new GeneratorCommandCollection();
		//private CustomDataItemCollection _customDataItems = new CustomDataItemCollection();
		private DataSourceCollection _dataSources = new DataSourceCollection();
		private MetadataFileObserverCollection _metadataFileObservers = new MetadataFileObserverCollection();

		#endregion Constructors and Member Variables

		public static Project CurrentProject = null;

		#region Properties

		#region Name and GUID

		[XmlAttribute()]
		[Browsable(false)]
		public Guid Guid {
			get { return _guid; }
			set { _guid = value; }
		}

		// Provide a read-only property for the PropertyBrowser
		public Guid GUID {
			get { return Guid; }
		}

		[Description("A descriptive name for the project.")]
		[Browsable(false)]
		public string Name {
			get {
				if (_name != String.Empty) {
					return _name;
				}
				if (FileName != String.Empty) {
					return Path.GetFileName(FileName);
				}
				else {
					return "UNTITLED";
				}
			}
		}

		#endregion Name and GUID

		#region IsDirty
		public bool IsDirty
		{
			get
			{
				StringWriter writer = new StringWriter();
				Save(writer);
				string currentState = writer.ToString();
				
				return _loadedState != currentState;
			}
		}
		#endregion IsDirty

		#region FileName

		[XmlIgnore()]
		public string FileName {
			get { return _fileName; }
		}

		#endregion FileName

		#region FileVesion

		[XmlAttribute(), Browsable(false)]
		public int FileVersionMajor {
			get { return FILE_VERSION_MAJOR; }
			set { _fileVersionMajor = value; }
		}

		[XmlAttribute(), Browsable(false)]
		public int FileVersionMinor {
			get { return FILE_VERSION_MINOR; }
			set { _fileVersionMinor = value; }
		}

		[Browsable(false)]
		public bool IsFileVersionCompatible {
			get {
				return (_fileVersionMajor <= FILE_VERSION_MAJOR
				        && _fileVersionMinor <= FILE_VERSION_MINOR);
			}
		}

		#endregion FileVesion

		#region Directories

		/// <summary>
		/// Base directory to search for metadata files referenced
		/// in this project.
		/// </summary>
		[Category("Paths")]
		[Editor("Ch3Etah.Design.CustomUI.RelativeDirectoryDialog,Ch3Etah.Design", typeof (UITypeEditor))]
		public string MetadataBaseDir {
			get { return _metadataBaseDir; }
			set { _metadataBaseDir = value; }
		}

		/// <summary>
		/// Base directory to search for template files referenced
		/// in this project.
		/// </summary>
		[Category("Paths")]
		[Editor("Ch3Etah.Design.CustomUI.PhysicalPathDialog,Ch3Etah.Design", typeof (UITypeEditor))]
		public string TemplatePackageBaseDir {
			get { return _templatePackageBaseDir; }
			set { _templatePackageBaseDir = value; }
		}

		/// <summary>
		/// Base directory for output files from this project.
		/// </summary>
		[Category("Paths")]
		[Editor("Ch3Etah.Design.CustomUI.RelativeDirectoryDialog,Ch3Etah.Design", typeof (UITypeEditor))]
		public string OutputBaseDir {
			get { return _outputBaseDir; }
			set { _outputBaseDir = value; }
		}

		#endregion Directories

		#region MetadataFileObservers

		internal MetadataFileObserverCollection MetadataFileObservers {
			get { return _metadataFileObservers; }
		}

		#endregion MetadataFileObservers

		#region Collections of sub-objects

		[Category("Code Generation")]
		[Browsable(false)]
		public MetadataFileCollection MetadataFiles {
			get {
				_metadataFiles.Project = this;
				return _metadataFiles;
			}
			set { _metadataFiles = value; }
		}

		[Category("Code Generation")]
		[Browsable(true)]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		public InputParameterCollection InputParameters {
			get { return _inputParameters; }
			set { _inputParameters = value; }
		}

		[Category("Code Generation")]
		[Browsable(false)]
		public GeneratorCommandCollection GeneratorCommands {
			get {
				_GeneratorCommands.Project = this;
				return _GeneratorCommands;
			}
			set { _GeneratorCommands = value; }
		}

//		[Category("Code Generation")]
//		[Browsable(false)]
//		public CustomDataItemCollection CustomDataItems {
//			get {
//				_customDataItems.Project = this;
//				return _customDataItems;
//			}
//			set {
//				_customDataItems = value;
//			}
//		}

		[Category("Code Generation")]
		[Browsable(false)]
		public DataSourceCollection DataSources {
			get {
				_dataSources.Project = this;
				return _dataSources;
			}
			set { _dataSources = value; }
		}

		#endregion Collections of sub-objects

		#endregion Properties

		#region Static Properties

		private static object _isLoading = false;

		internal static bool IsLoading {
			get { return (bool) _isLoading; }
			set { _isLoading = value; }
		}

		#endregion Static Properties

		#region Load

		/// <summary>
		/// Loads a Ch3Etah project from an XML file.
		/// </summary>
		/// <param name="fileName"></param>
		public static Project Load(string fileName) {
			Debug.WriteLine("Attempting to load project file '" + fileName + "'");
			FileInfo file = new FileInfo(fileName);
			if (!file.Exists) {
				throw new Exception(fileName + " does not exist.");
			}
			lock (_isLoading) {
				_isLoading = true;
				try {
					using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
						Project project = Load(stream, fileName);
						stream.Close();
						Debug.WriteLine("Successfully loaded project file '" + fileName + "'");
						return project;
					}
				}
				catch (UnrecognizedProjectFileFormatException) 
				{
					throw new UnrecognizedProjectFileFormatException(fileName);
				}
				catch (IncompatibleProjectVersionException) 
				{
					throw new IncompatibleProjectVersionException(fileName);
				}
				finally {
					_isLoading = false;
				}
			}
		}

		/// <summary>
		/// Loads a Ch3Etah project from a stream using XML Serialization.
		/// </summary>
		/// <param name="stream"></param>
		public static Project Load(Stream stream, string originalFileName) {
			CheckFileVersionCompatibility(stream);
			stream = UpgradeProjectVersion(stream);
			
			TextReader reader = new StreamReader(stream);
			XmlSerializer ser = new XmlSerializer(typeof (Project));
			Project project = (Project) ser.Deserialize(reader);
			
			project._fileName = originalFileName;
			
			project.SetLoadedState();
			//project.LoadMetadataFiles();

			CurrentProject = project;
			return project;
		}
		
		private static void CheckFileVersionCompatibility(Stream stream)
		{
			Debug.WriteLine("Checking project file version compatibility.");
			XmlDocument doc = new XmlDocument();
			doc.Load(stream);
			stream.Position = 0;
			
			XmlNode project = GetProjectNode(doc);
			
			int major = 0;
			int minor = 0;
			try
			{
				major = int.Parse(project.Attributes["FileVersionMajor"].Value);
				minor = int.Parse(project.Attributes["FileVersionMinor"].Value);
			}
			catch
			{
				throw new IncompatibleProjectVersionException();
			}

			if (major > FILE_VERSION_MAJOR
				|| (major == FILE_VERSION_MAJOR && minor > FILE_VERSION_MINOR))
			{
				throw new IncompatibleProjectVersionException();
			}
		}
		
		private static Stream UpgradeProjectVersion(Stream stream)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(stream);
			XmlNode project = GetProjectNode(doc);
			
			UpgrateProjectDatasources(project);
			
			MemoryStream newStream = new MemoryStream();
			doc.Save(newStream);
			newStream.Position = 0;
			//TextReader r = new StreamReader(newStream);
			//Debug.WriteLine(r.ReadToEnd());
			//newStream.Position = 0;
			return newStream;
		}
		
		private static void UpgrateProjectDatasources(XmlNode projectNode)
		{
			XmlNode dataSources = projectNode.SelectSingleNode("DataSources");
			if (dataSources == null) return;

			foreach (XmlNode datasource in dataSources.SelectNodes("DataSource"))
			{
				XmlAttribute xsiType = datasource.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
				XmlNode dsName = datasource.SelectSingleNode("Name");
				if (xsiType == null && dsName != null) 
				{
					Debug.WriteLine("Adding xsi:type attribute to " + dsName.Value + " data source.");
					xsiType = datasource.OwnerDocument.CreateAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					datasource.Attributes.Append(xsiType);
					xsiType.Value = "OleDbDataSource";
				}
			}
		}

		private static XmlNode GetProjectNode(XmlDocument doc)
		{
			XmlNodeList nodeList = doc.SelectNodes("Ch3EtahProject");
			if (nodeList.Count <= 0)
			{
				throw new UnrecognizedProjectFileFormatException();
			}
			return nodeList[0];
		}
		#endregion Load

		#region Save

		/// <summary>
		/// Saves the current project.
		/// </summary>
		public void Save() {
			if (FileName == "") {
				throw
					new InvalidOperationException(
						"No file name was specified to save the project. If this is a new project, call the save method passing a file name."
						);
			}
			Save(FileName);
		}

		/// <summary>
		/// Saves the current project to an XML file.
		/// </summary>
		/// <param name="fileName"></param>
		public void Save(string fileName) {
			Debug.WriteLine("Attempting to save project file '" + fileName + "'");
			FileInfo file = new FileInfo(fileName);
			if (!file.Directory.Exists) {
				file.Directory.Create();
			}
			using (FileStream stream = new FileStream(fileName, FileMode.Create)) {
				Save(stream);
				_fileName = fileName;
				stream.Close();
				Debug.WriteLine("Successfully saved project file '" + fileName + "'");
			}
		}

		/// <summary>
		/// Saves the current project to a stream using XML Serialization.
		/// </summary>
		public void Save(Stream stream) {
			TextWriter writer = null;

			writer = new StreamWriter(stream);
			Save(writer);
		}
		
		public void Save(TextWriter writer)
		{
			XmlSerializer ser = new XmlSerializer(typeof (Project));
			ser.Serialize(writer, this);
		}
		#endregion Save

		#region GetFullPaths

		public string GetFullMetadataPath() {
			return GetFullPath(MetadataBaseDir);
		}

		public string GetFullTemplatePath() {
			return GetFullPath(TemplatePackageBaseDir);
		}

		public string GetFullOutputPath() {
			return GetFullPath(OutputBaseDir);
		}

		private string GetFullPath(string fileName) {
			if (Path.IsPathRooted(fileName))
			{
				return fileName;
			}
			else
			{
				return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(FileName), fileName));
			}
// REMOVED by Igor @ Oct 13, 2005
//			string oldDirectory = Directory.GetCurrentDirectory();
//			string fullPath = Directory.GetCurrentDirectory();
//			try {
//				if (FileName != "") {
//					Directory.SetCurrentDirectory(Path.GetDirectoryName(FileName));
//				}
//				if (path != "") {
//					fullPath = Path.GetFullPath(path);
//				}
//				//Debug.WriteLine("Project.GetFullPath(): path=" + path + " this.FileName=" + this.FileName + " CurrentDirectory=" + Directory.GetCurrentDirectory() + " fullPath=" + fullPath);
//			}
//			finally {
//				Directory.SetCurrentDirectory(oldDirectory);
//			}
//			return fullPath;
		}

		#endregion GetFullPaths
		
//		private void LoadMetadataFiles()
//		{
//			foreach (MetadataFile file in this.MetadataFiles)
//			{
//				file.Load();
//			}
//		}

		private void SetLoadedState()
		{
			StringWriter writer = new StringWriter();
			Save(writer);
			_loadedState = writer.ToString();
		}

		public Entity GetEntity(String entityName)
		{
			foreach (MetadataFile tmpMetadataFile in Project.CurrentProject.MetadataFiles)
			{
				foreach (IMetadataEntity tmpEntity in tmpMetadataFile.MetadataEntities)
				{
					if (tmpEntity is Entity)
					{
						if (((Entity) tmpEntity).Name.Equals(entityName))
						{
							return (Entity) tmpEntity;
						}
					}
				}
			}

			return null;
		}

		public ArrayList ListPackages()
		{
			try
			{
				return Package.ListPackages(this.GetFullTemplatePath());
			}
			catch (Exception ex)
			{
				Trace.WriteLine("ERROR LOADING PACKAGES:\r\n" + ex.ToString());
				throw ex;
//				Package p = new Package();
//				p.Name = "[ERROR LOADING PACKAGES - Check the 'PackagesBaseDir' of your project.]";
//				p.PackageFileName = p.Name;
//				return new Package[]{p};
			}
		}
		
	}

	public class UnrecognizedProjectFileFormatException : ApplicationException
	{
		public UnrecognizedProjectFileFormatException()
			: base("The file you are trying to load is not in a recognized project file format. Please make sure the file is not corrupt and that you are using the most recent version of CH3ETAH in order to load this project file.")
		{
		}

		public UnrecognizedProjectFileFormatException(string fileName)
			: base(string.Format("The file '{0}' is not in a recognized project file format. Please make sure the file is not corrupt and that you are using the most recent version of CH3ETAH in order to load this project file.", fileName))
		{
		}
	}
	
	public class IncompatibleProjectVersionException : ApplicationException
	{
		public IncompatibleProjectVersionException()
			: base("The project file you are trying to load is not compatible with this version of CH3ETAH. In order to prevent information loss, the project file will not be loaded. Please make sure you are using the most recent version of CH3ETAH in order to load this project file.")
		{
		}

		public IncompatibleProjectVersionException(string fileName)
			: base(string.Format("The file '{0}' is not compatible with this version of CH3ETAH. In order to prevent information loss, the project file will not be loaded. Please make sure you are using the most recent version of CH3ETAH in order to load this project file.", fileName))
		{
		}
	}
}
