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
 *   Date: 22/9/2004
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO;
using System.Security.Principal;
using System.Xml;
using System.Xml.Serialization;
using Ch3Etah.Core.CodeGen;
using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.Config;
using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Core.ProjectLib {

	#region CodeGeneratorEngine Enum

//	public enum CodeGeneratorEngine {
//		NVelocity,
//		Xslt,
//		CodeSmith
//	}

	#endregion CodeGeneratorEngine Enum

	#region CodeGenerationMode Enum

	public enum CodeGenerationMode 
	{
		SingleOutput,
		MultipleOutput
	}

	#endregion
	
	/// <summary>
	/// Contains a template and supporting metadata files
	/// which will be executed to generate source code files
	/// using the Ch3Etah.Core.CodeGen abstraction layer.
	/// </summary>
	public class CodeGeneratorCommand : GeneratorCommand, IMetadataFileObserver {
		
		#region Constructors and Member Variables

		private string _package = string.Empty;
		private string _template = string.Empty;
		private string _outputPath = string.Empty;
		private bool _overwrite = true;
		private bool _autoSelectMetadataFiles = true;
		private CodeGenerationMode _codeGenerationMode;
		private string _engine = TransformationEngineFactory.DefaultEngineName;
		private InputParameterCollection _inputParameters = new InputParameterCollection();
		private Guid[] _individualMetadataFileIDs;
		private MetadataFileCollection _individualMetadataFiles;

//		Guid[] _groupedMetadataFileIDs;
//		MetadataFileCollection _groupedMetadataFiles;

		#endregion Constructors and Member Variables

		#region Properties

		#region Template Info

		[Category("Template Information")]
		[TypeConverter("Ch3Etah.Design.Converters.PackageNameConverter,Ch3Etah.Design")]
		public string Package {
			get { return _package; }
			set { _package = value; }
		}

		[Category("Template Information")]
		[TypeConverter("Ch3Etah.Design.Converters.TemplateNameConverter,Ch3Etah.Design")]
		public string Template {
			get { return _template; }
			set { _template = value; }
		}

		#endregion Template Info

		#region OutputPath

		[Category("Code Generation")]
		[Editor("Ch3Etah.Design.CustomUI.XmlFileOpenDialog,Ch3Etah.Design", typeof(UITypeEditor))]
		public string OutputPath {
			get { return _outputPath; }
			set { _outputPath = value; }
		}

		#endregion OutputPath

		#region Engine

		[Category("Code Generation")]
		public string Engine {
			get { return _engine; }
			set { _engine = value; }
		}

		#endregion Engine

		#region Overwrite

		/// <summary>
		/// If this is true, then existing files and/or generated regions will be
		/// overwritten without any prompts.
		/// </summary>
		[Category("Code Generation")]
		public bool Overwrite {
			get { return _overwrite; }
			set { _overwrite = value; }
		}

		#endregion Overwrite

		#region AutoSelectMetadataFiles

		public bool AutoSelectMetadataFiles {
			get { return _autoSelectMetadataFiles; }
			set { _autoSelectMetadataFiles = value; }
		}

		#endregion AutoSelectMetadataFiles

		#region InputParameters

		[Browsable(false)]
		public InputParameterCollection InputParameters {
			get { return _inputParameters; }
			set { _inputParameters = value; }
		}

		#endregion InputParameters

		#region IndividualMetadataFiles

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This field is meant to be used only for XML serialization and" +
		          " deserialization. It should not be accessed programatically.")]
		public Guid[] IndividualMetadataFileIDs {
			get {
				Guid[] ids = new Guid[IndividualMetadataFiles.Count];
				for (int x = 0; x < IndividualMetadataFiles.Count; x++) {
					ids[x] = IndividualMetadataFiles[x].Guid;
				}
				return ids;
			}
			set { _individualMetadataFileIDs = value; }
		}

		[Browsable(false)]
		[XmlIgnore()]
		// FIXME: Specify the EditorAttribute for this property
			//http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconattributesdesign-timesupport.asp
			public MetadataFileCollection IndividualMetadataFiles {
			get {
				if (_individualMetadataFiles == null) {
					_individualMetadataFiles = new MetadataFileCollection();
					//_individualMetadataFiles.Project = this.Project;
					if (_individualMetadataFileIDs != null) {
						foreach (Guid guid in _individualMetadataFileIDs) {
							MetadataFile file = Project.MetadataFiles.GetMetadataFile(guid);
							if (file != null) {
								_individualMetadataFiles.Add(file);
							}
						}
					}
				}
				//_individualMetadataFiles.Project = this.Project;
				return _individualMetadataFiles;
			}
			set { _individualMetadataFiles = value; }
		}

		#endregion IndividualMetadataFiles

		#region GroupedMetadataFiles (DEPRECATED)

//        [Browsable(false)]
//        [EditorBrowsableAttribute(EditorBrowsableState.Never)]
//        [Obsolete("This field is meant to be used only for XML serialization and" + 
//             " deserialization. It should not be accessed programatically.")]
//        public Guid[] GroupedMetadataFileIDs {
//            get {
//                Guid[] ids = new Guid[GroupedMetadataFiles.Count];
//                for (int x = 0; x < GroupedMetadataFiles.Count; x++) {
//                    ids[x] = GroupedMetadataFiles[x].Guid;
//                }
//                return ids;
//            }
//            set {
//                _groupedMetadataFileIDs = value;
//            }
//        }
//		
//        [Browsable(false)]
//        [XmlIgnore()]
//            // FIXME: Specify the EditorAttribute for this property
//            //http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconattributesdesign-timesupport.asp
//        public MetadataFileCollection GroupedMetadataFiles {
//            get {
//                if ( _groupedMetadataFiles == null) {
//                    _groupedMetadataFiles = new MetadataFileCollection();
//                    //_groupedMetadataFiles.Project = this.Project;
//                    if (_groupedMetadataFileIDs != null) {
//                        foreach (Guid guid in _groupedMetadataFileIDs) {
//                            MetadataFile file = Project.MetadataFiles.GetMetadataFile(guid);
//                            if (file != null) {
//                                _groupedMetadataFiles.Add(file);
//                            }
//                        }
//                    }
//                }
//                //_groupedMetadataFiles.Project = this.Project;
//                return _groupedMetadataFiles;
//            }
//            set {
//                _groupedMetadataFiles = value;
//            }
//        }

		#endregion GroupedMetadataFiles

		#region CodeGenerationMode

		[Browsable(false)]
		[XmlElement("GenerationMode")]
		public CodeGenerationMode CodeGenerationMode {
			get { return _codeGenerationMode; }
			set { _codeGenerationMode = value; }
		}

		#endregion

		#endregion Properties

		#region AutoSelectMetadataFile

		public void AutoSelectMetadataFile(MetadataFile file) {
			if (Template == String.Empty) {
				return;
			}
			CodeGenerator generator = CreateGenerator(file);
			SetGeneratorTemplate(generator);
			foreach (MetadataBrand brand in generator.Template.IndividualMetadataBrands) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if (entity.BrandName == brand.Name ||
					    (entity.BrandName == "XmlEntity" && entity.RootNodeName == brand.Name)) {
						if (!IndividualMetadataFiles.Contains(file)) {
							IndividualMetadataFiles.Add(file);
						}
					}
				}
			}

			#region GroupedMetadataBrand (DEPRECATED)

//            foreach (MetadataBrand brand in generator.Template.GroupedMetadataBrands) {
//                Debug.WriteLine("AutoSelectMetadataFile: GroupedBrand=" + brand.Name);
//                foreach (IMetadataEntity entity in file.MetadataEntities) {
//                    Debug.WriteLine("    AutoSelectMetadataFile: entity.BrandName=" + entity.BrandName + "    entity.RootNodeName=" + entity.RootNodeName);
//                    if (entity.BrandName == brand.Name ||
//                        (entity.BrandName == "XmlEntity" && entity.RootNodeName == brand.Name)) {
//                        if (!this.GroupedMetadataFiles.Contains(file)) {
//                            this.GroupedMetadataFiles.Add(file);
//                        }
//                    }
//                }
//            }

			#endregion
		}

		#endregion AutoSelectMetadataFile

		#region Execute

		public override void Execute() {
			if (!Enabled) {
				return;
			}
			if (OutputPath == String.Empty) {
				throw
					new InvalidOperationException(
						"The property 'OutputPath' was not specified. You must either specify an output path or a TextWriter object to write to."
						);
			}
			if (_codeGenerationMode == CodeGenerationMode.MultipleOutput) {
				ExecuteIndividually();
			}
			else {
				GenerateFile(null);
			}
		}

		private void ExecuteIndividually() {
			foreach (MetadataFile inputFile in IndividualMetadataFiles) {
				GenerateFile(inputFile);
			}
			ResetGroupedMetadataCache();
		}

		private void GenerateFile(MetadataFile inputFile) {
			if (inputFile == null) {
				Trace.WriteLine("Running '" + Name + "' Code Generator Command");
			}
			else {
				Trace.WriteLine("Running '" + Name + "' Code Generator Command for input file '" + inputFile.Name + "'");
			}
			Trace.Indent();
			try {
				SetOutputBaseFolder();
				try {
					XmlDocument document = PrepareMetadata(inputFile);
					CodeGenerator generator = GetGenerator(document, inputFile);
					string outputPath = generator.Context.Parameters["CodeGenOutputPath"].Value;
					outputPath = Path.GetFullPath(Path.Combine(this.Project.GetFullOutputPath(), outputPath));
					FileSystemHelper.CreateDirectory(new FileInfo(outputPath));
					Trace.WriteLine("GenerateFile(): '" + outputPath + "'");
					Trace.Indent();
					try {
						if (!Overwrite && File.Exists(outputPath)) {
							Trace.WriteLine("File exists. Existing file will not be overwritten.");
							return;
						}
						using (StringWriter stringWriter = new StringWriter()) {
							generator.Generate(document, stringWriter);
							using (StreamWriter outputWriter = new StreamWriter(outputPath)) 
							{
								outputWriter.Write(stringWriter.ToString());
							}
						}
					}
					finally {
						Trace.Unindent();
					}
				}
				finally {
					RestoreOutputBaseFolder();
				}
			}
			finally {
				Trace.Unindent();
			}
		}

		public void GenerateFile(MetadataFile inputFile, TextWriter outputWriter) {
			if (inputFile == null) {
				Trace.WriteLine("Running '" + Name + "' Code Generator Command");
			}
			else {
				Trace.WriteLine("Running '" + Name + "' Code Generator Command for input file '" + inputFile.Name + "'");
			}
			Trace.Indent();
			try {
				XmlDocument document = PrepareMetadata(inputFile);
				CodeGenerator generator = GetGenerator(document, inputFile);
				generator.Generate(document, outputWriter);
				ResetGroupedMetadataCache();
			}
			finally {
				Trace.Unindent();
			}
		}

		#endregion Execute

		#region Prepare generator

		private CodeGenerator GetGenerator(XmlDocument metadata, MetadataFile inputFile) {
			// FIXME: Probably need to refactor generator, package, and template code into seperate classes
			InputParameterCollection parameters = GetContextParameters();
			//Trace.WriteLine("GetGenerator(): Command context has " + parameters.Count + " parameters - " + DateTime.Now.ToString());
			PrepareParameters(metadata, inputFile, parameters);
			CodeGenerator generator = CreateGenerator(inputFile);
			SetGeneratorTemplate(generator);
			SetCommandParameters(generator, parameters);
			return generator;
		}

		private void PrepareParameters(XmlDocument metadata, MetadataFile inputFile, InputParameterCollection parameters) {
			Debug.WriteLine("PrepareParameters(): Command context has " + parameters.Count + " parameters");
			for (int x = 0; x < 10; x++) {
				bool parameterChanged = false;
				foreach (InputParameter parameter in parameters) {
					if (parameter.Value.IndexOf("$") < 0 && parameter.Value.IndexOf("#") < 0) {
						continue;
					}
					//Debug.WriteLine("PrepareParameters(): Preparing parameter '" + parameter.Name + "' Value='" + parameter.Value + "'");
					Trace.Indent();
					try {
						CodeGenerator generator = CreateGenerator(inputFile);
						SetGeneratorTemplate(generator);
						generator.Template.Name = "Prepare Parameter '" + parameter.Name + "' : value '" + parameter.Value + "'";
						generator.Template.Content = new StringReader(parameter.Value);
						//generator.Template = new Template(parameter.Value, );
						//SetGeneratorTemplate(generator);
						SetCommandParameters(generator, parameters);
						//StringReader reader = ;
						//generator.Template = reader;
						StringWriter writer = new StringWriter();
						generator.UseDefaultEngine = true;
						generator.Generate(metadata, writer);
						//Trace.WriteLineIf(parameter.Value != writer.ToString(), "Prepared parameter '" + parameter.Name + "': OldVal='" + parameter.Value + "' NewVal='" + writer.ToString() + "'");
						if (parameter.Value != writer.ToString()) {
							parameter.Value = writer.ToString();
							parameterChanged = true;
						}
					}
					finally {
						Trace.Unindent();
					}
				}
				if (!parameterChanged) {
					break;
				}
			}
		}

		private CodeGenerator CreateGenerator(MetadataFile inputFile) {
			CodeGenerator generator;
			SetTemplateBaseFolder();
			try {
				if (Package != String.Empty) {
					generator = new CodeGenerator(CodeGen.PackageLib.Package.Load(Package));
				}
				else {
					generator = new CodeGenerator(Directory.GetCurrentDirectory());
				}
			}
			finally {
				RestoreTemplateBaseFolder();
			}
			generator.Context.CurrentMetadataFile = inputFile;
			generator.Context.SelectedMetadataFiles = this.IndividualMetadataFiles;
			return generator;
		}

		private void SetGeneratorTemplate(CodeGenerator generator) {
			if (Template == String.Empty) {
				throw new InvalidOperationException("No code generation template was specified.");
			}
			SetTemplateBaseFolder();
			try {
				if (Package == "") {
					generator.Template = new Template(Template, Path.GetFullPath(Template));
				}
				else {
					generator.Template = generator.Package.Templates[Template];
				}
				SetGeneratorEngine(generator);
			}
			finally {
				RestoreTemplateBaseFolder();
			}
		}

		private void SetGeneratorEngine(CodeGenerator generator) {
			generator.Template.Engine = this.Engine;
		}

		private InputParameterCollection GetContextParameters() {
			InputParameterCollection parameters = InputParameters.Clone();

			AddProjectParameters(parameters);
			CheckTemplateParameters(parameters);

			parameters.Add("CodeGenOutputPath", OutputPath);
			parameters.Add("CodeGenSystemDate", DateTime.Now.ToString());
			parameters.Add("CodeGenSystemLogin", WindowsIdentity.GetCurrent().Name);

			return parameters;
		}

		private void AddProjectParameters(InputParameterCollection parameters) {
			if (Project != null) {
				foreach (InputParameter parameter in Project.InputParameters) {
					if (!parameters.Contains(parameter.Name)) {
						parameters.Add(parameter);
					}
				}
			}
		}

		private void CheckTemplateParameters(InputParameterCollection parameters) {
			// TODO: REFACTOR
			// TODO: Add/check parameters from template then package
			SetTemplateBaseFolder();
			try {
				if (_package == "") {
					return;
				}
				else {
					Package package = CodeGen.PackageLib.Package.Load(_package);
					if (package == null || !package.Templates.Contains(_template)) {
						throw new FileNotFoundException("Template '" + _package + "::" + _template + "' not found.");
					}
					else {
						CheckCodeGenParameters(parameters, package.Templates[_template].InputParameters);
						CheckCodeGenParameters(parameters, package.InputParameters);
					}
				}
			}
			finally {
				RestoreTemplateBaseFolder();
			}
		}

		private void CheckCodeGenParameters(InputParameterCollection parameters,
		                                    CodeGen.PackageLib.InputParameterCollection packageParameters) {
			if (packageParameters == null) {
				return;
			}
			foreach (CodeGen.PackageLib.InputParameter packageParameter in packageParameters) {
				if (!parameters.Contains(packageParameter.Name)) {
					if (!packageParameter.IsRequired) {
						parameters.Add(packageParameter.Name, packageParameter.Value);
					}
					else {
						throw new ApplicationException(String.Format("Parameter {0} was not specified.", packageParameter.Name));
					}
				}
			}
		}

		private void SetCommandParameters(CodeGenerator generator, InputParameterCollection parameters) {
			//Debug.WriteLine("SetCommandParameters(): Command context has " + parameters.Count + " parameters");
			Debug.Indent();
			foreach (InputParameter parameter in parameters) {
				//Debug.WriteLine("Adding Context Parameter '" + parameter.Name + "' Value='" + parameter.Value + "' after index " + generator.Context.Parameters.Count);
				generator.Context.Parameters.Add(parameter.Name, parameter.Value);
			}
			Debug.Unindent();
		}

		#endregion Prepare generator

		#region Prepare metadata

		private XmlDocument PrepareMetadata(MetadataFile inputFile) {
			XmlDocument document = new XmlDocument();
			if (inputFile == null) {
				document.LoadXml("<" + MetadataFile.METADATA_BASE_TAG + "></" + MetadataFile.METADATA_BASE_TAG + ">");
			}
			else {
				document.LoadXml(inputFile.SaveXml());
			}
			//document.DocumentElement.InnerXml += AssembleGroupedMetadata();
			AppendGroupedMetadata(document);
			SaveMetadataTempFile(document);
//			return FilterExcludedElements(document);
			return document;
		}

//		private XmlDocument FilterExcludedElements(XmlDocument document) {
//			// there seems to be a problem with the XmlTextReader 
//			// that makes it ignore nodes containing encoded XHTML 
//			// (such as "&lt;br&gt;")
//			// Consider looking at XmlNodeReader
//
//			//return document;
//
//			NameTable nt = new NameTable();
//			XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
//			nsmgr.AddNamespace("ch3", "ch3etah.sf.net");
//
//			XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
//			XmlTextReader reader = new XmlTextReader(document.OuterXml, XmlNodeType.Element, context);
//			StringBuilder sb = new StringBuilder();
//
//			Stack stack = new Stack();
//
//			while (reader.Read()) {
//				if (reader.IsStartElement()) {
//					if (stack.Count > 0) {
//						if (!reader.IsEmptyElement) {
//							stack.Push(true);
//						}
//						continue;
//					}
//					string excludeAttribute = reader.GetAttribute("ch3:exclude");
//					if (excludeAttribute != null && excludeAttribute.ToUpper() == "TRUE") {
//						if (!reader.IsEmptyElement) {
//							stack.Push(true);
//						}
//						continue;
//					}
//				}
//				else {
//					if (stack.Count > 0) {
//						stack.Pop();
//						continue;
//					}
//				}
//				if (reader.IsEmptyElement) {
//					sb.AppendFormat("<{0}{1}/>", reader.LocalName, DumpAttributes(reader));
//				}
//				else if (reader.IsStartElement()) {
//					sb.AppendFormat("<{0}{1}>", reader.LocalName, DumpAttributes(reader));
//					if (reader.HasValue) {
//						sb.Append(reader.Value);
//					}
//				}
//				else {
//					sb.AppendFormat("</{0}>", reader.LocalName);
//				}
//			}
//
//			XmlDocument ret = new XmlDocument();
//			ret.LoadXml(sb.ToString());
//
//			return ret;
//		}

//		private string DumpAttributes(XmlTextReader reader) {
//			StringBuilder sb = new StringBuilder();
//			if (reader.MoveToFirstAttribute()) {
//				do {
//					sb.AppendFormat(" {0}=\"{1}\"", reader.LocalName, reader.Value);
//				} while (reader.MoveToNextAttribute());
//			}
//
//			reader.MoveToElement();
//
//			return sb.ToString();
//		}

		private XmlNode _groupedMetadataCache = null;

		private void AppendGroupedMetadata(XmlDocument document) {
			if (_groupedMetadataCache == null) {
				document.DocumentElement.InnerXml += AssembleGroupedMetadata();
//				_groupedMetadataCache = document.DocumentElement.GetElementsByTagName("GroupedMetadata").Item(0);
			}
			else {
				XmlNode node = document.ImportNode(_groupedMetadataCache, true);
				document.DocumentElement.AppendChild(node);
			}
		}

		private void ResetGroupedMetadataCache() {
			_groupedMetadataCache = null;
		}

		private string AssembleGroupedMetadata() {
			Hashtable entityCollections = 
				GroupMetadataEntities(
					_codeGenerationMode == CodeGenerationMode.SingleOutput? 
						IndividualMetadataFiles: Project.MetadataFiles);
			string xml = "";
			foreach (DictionaryEntry entry in entityCollections) {
				string name = entry.Key.ToString();
				MetadataEntityCollection entityCollection = (MetadataEntityCollection) entry.Value;
				if (entityCollection.Count > 0) {
					xml += "<" + name + ">" + entityCollection.SaveAsXmlString() + "</" + name + ">";
				}
			}
			return "<GroupedMetadata>" + xml + "</GroupedMetadata>";
		}

		private Hashtable GroupMetadataEntities(MetadataFileCollection files) {
			Hashtable entityCollections = new Hashtable();
			foreach (MetadataFile file in files) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					string name = PathResolver.GetPluralForm(entity.RootNodeName);
					if (!entityCollections.ContainsKey(name)) {
						entityCollections.Add(name, new MetadataEntityCollection());
					}
					MetadataEntityCollection entityCollection = (MetadataEntityCollection) entityCollections[name];
					entityCollection.Add(entity);
				}
			}
			return entityCollections;
		}

		private void SaveMetadataTempFile(XmlDocument document) {
			string config = ConfigurationSettings.AppSettings.Get("WriteMetadataToTempFile");
			if (config == null || config.ToUpper() != "TRUE")
			{
				Debug.WriteLine("Metadata not written to temp file. To enable temp file creation for troubleshooting purposes, set the 'WriteMetadataToTempFile' in your application config file to 'true'.");
				return;
			}

			string currentDir = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(Path.GetTempPath());
			try {
				string path = Path.GetTempFileName();
				document.Save(path);
				Trace.WriteLine("Metadata saved to temp file '" + path + "'");
				Debug.WriteLine("To disable temp file creation, set the 'WriteMetadataToTempFile' in your application config file to 'false'.");
			}
			finally {
				Directory.SetCurrentDirectory(currentDir);
			}
		}

		#endregion Prepare metadata

		#region GetFullTemplatePath

		public string GetFullTemplatePath() {
			if (_template == "") {
				return "";
			}
			SetTemplateBaseFolder();
			try {
				if (_package == "") {
					return Path.GetFullPath(_template);
				}
				else {
					Package package = CodeGen.PackageLib.Package.Load(_package);
					if (package == null || !package.Templates.Contains(_template)) {
						throw new FileNotFoundException("Template '" + _package + "::" + _template + "' not found.");
					}
					else {
						Directory.SetCurrentDirectory(package.BaseFolder);
						return Path.GetFullPath(package.Templates[_template].FileName);
					}
				}
			}
			finally {
				RestoreTemplateBaseFolder();
			}
		}

		#endregion GetFullTemplatePath

		#region SetOutputBaseFolder / RestoreOutputBaseFolder

		private string _oldOutputBaseFolder = null;

		private void SetOutputBaseFolder() {
			_oldOutputBaseFolder = Directory.GetCurrentDirectory();
			if (Project != null) {
				if (!Directory.Exists(Project.GetFullOutputPath()))
					Directory.CreateDirectory(Project.GetFullOutputPath());
				Directory.SetCurrentDirectory(Project.GetFullOutputPath());
			}
		}

		private void RestoreOutputBaseFolder() {
			if (_oldOutputBaseFolder != null) {
				Directory.SetCurrentDirectory(_oldOutputBaseFolder);
				_oldOutputBaseFolder = null;
			}
		}

		#endregion SetOutputBaseFolder / RestoreOutputBaseFolder

		#region SetTemplateBaseFolder / RestoreTemplateBaseFolder

		private string _oldTemplateBaseFolder = null;

		private void SetTemplateBaseFolder() {
			_oldTemplateBaseFolder = Directory.GetCurrentDirectory();
			if (Project != null) {
				Directory.SetCurrentDirectory(Project.GetFullTemplatePath());
			}
		}

		private void RestoreTemplateBaseFolder() {
			if (_oldTemplateBaseFolder != null) {
				Directory.SetCurrentDirectory(_oldTemplateBaseFolder);
				_oldTemplateBaseFolder = null;
			}
		}

		#endregion SetTemplateBaseFolder / RestoreTemplateBaseFolder

		#region SetProject

		protected internal override void SetProject(Project project) {
			base.SetProject(project);
			if (project != null) {
				project.MetadataFileObservers.Add(this);
			}
		}

		#endregion SetProject

		#region IMetadataFileObserver implementation

		void IMetadataFileObserver.OnMetadataFileAdded(MetadataFile file) {
			if (AutoSelectMetadataFiles) {
				AutoSelectMetadataFile(file);
			}
		}

		void IMetadataFileObserver.OnMetadataFileRemoved(MetadataFile file) {
			if (IndividualMetadataFiles.Contains(file)) {
				IndividualMetadataFiles.Remove(file);
			}
		}

		#endregion IMetadataFileObserver implementation
	}
}
