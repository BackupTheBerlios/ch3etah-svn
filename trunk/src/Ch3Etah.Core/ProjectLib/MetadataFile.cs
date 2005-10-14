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
 *   Date: 7/9/2004
 *   Time: 8:36 PM
 */

using System;
using System.Drawing.Design;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.Exceptions;
using Ch3Etah.Core.Config;

namespace Ch3Etah.Core.ProjectLib
{
	/// <summary>
	/// Represents an individual metadata file in a Ch3Etah project.
	/// </summary>
	public class MetadataFile
	{
		#region Constructors and Member Variables
		
		private Guid _guid = Guid.NewGuid();
		private string _name = "";
		private string _fileName = "";
		private MetadataEntityCollection _metadata;
		private Project _project;
		
		private bool _isNew = true;
		private bool _loadedAsRawXml = true;
		
		internal const string METADATA_BASE_TAG = "Metadata";
		
		public MetadataFile()
		{
		}
		
		public MetadataFile(Project project) {
			if (project != null) {
				this.SetProject(project);
			}
		}

		public MetadataFile(Project project, string fileName) {
			if (project != null) {
				this.SetProject(project);
			}
			Load(fileName);
		}
		#endregion Constructors and Member Variables
		
		#region Properties
		#region Project
		[XmlIgnore()]
		[Browsable(false)]
		public Project Project {
			get {
				return _project;
			}
		}
		internal void SetProject(Project project) {
			Trace.Assert(project.FileName != null, "Project should not be null.");
			_project = project;
		}
		#endregion Project
		
		#region Name and GUID
		[XmlAttribute()]
		[Browsable(false)]
		public Guid Guid {
			get {
				return _guid;
			}
			set {
				_guid = value;
			}
		}
		
		// Provide a read-only property for the PropertyBrowser
		public Guid GUID {
			get {
				return this.Guid;
			}
		}
		
		[Browsable(false)]
		public string Name {
			get {
				if (_name != "") {
					return _name;
				}
				if (FileName != "") {
					return Path.GetFileName(FileName);
				}
				else {
					return "UNTITLED";
				}
			}
		}
		#endregion Name and ID
		
		#region IsDirty
		[Browsable(false)]
		public bool IsDirty {
			get {
				return this._isNew || MetadataEntities.IsDirty;
			}
		}
		#endregion IsDirty
		
		#region Metadata file info
		[Category("File Info")]
		public string FileName {
			get {
				//return GetRelativePath(_fileName);
				return _fileName;
			}
			set {
				_fileName = value;
			}
		}
		
		[XmlIgnore()]
		[Browsable(false)]
		public MetadataEntityCollection MetadataEntities {
			get {
				if ( _metadata == null ) {
					if ( FileName == String.Empty ) {
						_metadata = new MetadataEntityCollection();
					} else {
						Load();
					}
				}
				_metadata.OwningMetadataFile = this;
				return _metadata;
			}
			set { _metadata = value; }
		}
		#endregion Metadata file info
		#endregion Properties
		
		#region Load
		public static MetadataFile Load(Project project, string fileName) {
			return new MetadataFile(project, fileName);
		}
		
		public void Load() {
			Load(FileName);
		}
		
		public void Load(string fileName) {
			Debug.WriteLine("Attempting to load metadata file '" + fileName + "'");
			string fullPath = GetFullPath(fileName);
            FileInfo file = new FileInfo(fullPath);
            if (!file.Exists){
				throw new FileNotFoundException(fullPath + " does not exist. Make sure this project's MetadataBaseDir property is correct and that the file exists in the specified directory.");
			}
			XmlDocument document = new XmlDocument();
			//FileStream input = File.OpenRead(fullPath);
			document.Load(fullPath);
			LoadXml(document);
			
			FileName = fileName;
		}
		
		public void LoadXml(XmlDocument document) {
			MetadataEntities = null;
			
			if (document.DocumentElement.Name == METADATA_BASE_TAG) {
				LoadEntitiesFromNode(document.DocumentElement);
			}
			else {
				LoadDocumentAsRawXml(document);
			}
			_isNew = false;
			_metadata.OwningMetadataFile = this;
		}
		
		private void LoadEntitiesFromNode(XmlNode rootNode) {
			MetadataEntities = null;
			MetadataEntityCollection entities = new MetadataEntityCollection();
			foreach (XmlNode node in rootNode.ChildNodes) {
				IMetadataEntity entity = MetadataEntityFactory.LoadEntity(node);
				entities.Add(entity);
			}
			MetadataEntities = entities;
			_loadedAsRawXml = false;
		}
		
		private void LoadDocumentAsRawXml(XmlDocument document) {
			MetadataEntities = null;
			
			MetadataEntityCollection entities = new MetadataEntityCollection();
			IMetadataEntity entity = MetadataEntityFactory.LoadEntity(document);
			entities.Add(entity);
			
			MetadataEntities = entities;
			_loadedAsRawXml = true;
		}
		#endregion Load

		#region Save
		public void Save() {
			if (FileName == String.Empty) throw new InvalidOperationException("No path was specified to save the metadata.");
			Save(FileName);
		}
		
		public void Save(string fileName) {
			Debug.WriteLine("Attempting to save metadata file '" + fileName + "'");
			if (_metadata == null) throw new FileNotLoadedException();
			
			XmlDocument document = new XmlDocument();
			document.LoadXml(SaveXml());
			document.Save(GetFullPath(fileName));
			
			FileName = fileName;
		}
		
		public string SaveXml() {
			// make sure the file has been loaded.
			this.MetadataEntities.ToString();
			
			if (_loadedAsRawXml && !_isNew) {
				return SaveAsRawXml();
			}
			else {
				return SaveAsCh3EtahXml();
			}
		}
		
		private string SaveAsCh3EtahXml() {
			XmlDocument document = new XmlDocument();
			document.LoadXml("<" + METADATA_BASE_TAG + "></" + METADATA_BASE_TAG + ">");
			document.DocumentElement.InnerXml = MetadataEntities.SaveAsXmlString();
			
			StringWriter writer = new StringWriter();
			document.Save(writer);
			return writer.ToString();
		}
		
		private string SaveAsRawXml() {
			XmlDocument document = new XmlDocument();
			string xml = MetadataEntities.SaveAsXmlString();
			document.LoadXml(xml);
			
			StringWriter writer = new StringWriter();
			document.Save(writer);
			return writer.ToString();
		}
		#endregion Save
		
		#region GetFullPath / GetRelativePath
		public string GetFullPath() {
			return GetFullPath(this.FileName);
		}
		
		private string GetFullPath(string fileName) {
			if (Path.IsPathRooted(fileName))
			{
				return fileName;
			}
			else
			{
				string fullMetadataPath = Project.GetFullMetadataPath();
				string path = Path.Combine(fullMetadataPath, FileName);
				string fullPath = Path.GetFullPath(path);
				return fullPath;
			}
			
// REMOVED by Igor @ Oct 13, 2005
//			string oldBaseFolder = Directory.GetCurrentDirectory();
//			string fullPath = fileName;
//			try {
//				if (Project != null) {
//					string metadataPath = Project.GetFullMetadataPath();
//					if (!Directory.Exists(metadataPath)) {
//						FileSystemHelper.CreateDirectory(new DirectoryInfo(metadataPath));
//					}
//					Directory.SetCurrentDirectory(metadataPath);
//				}
//				Debug.WriteLine("MetadataFile.GetFullPath(): baseDirectory='" + Directory.GetCurrentDirectory() + "' fileName='" + fileName + "'");
//				if (fileName == "") {
//					fullPath = "";
//				}
//				else {
//					fullPath = Path.GetFullPath(fileName);
//				}
//			}
//			finally {
//				Directory.SetCurrentDirectory(oldBaseFolder);
//			}
//			return fullPath;
		}

		public string GetRelativePath(string fileName) {
			string relativePath = "";
			if (Project != null) {
				string baseDirectory = Project.GetFullMetadataPath();
				relativePath = PathResolver.GetRelativePath(baseDirectory, fileName);
			}
			else {
				relativePath = fileName;
			}
			
			return relativePath;
		}
		#endregion GetFullPath / GetRelativePath
		
		#region ToString
		public override string ToString() {
			return this.Name;
		}
		#endregion ToString
		
	}
}
