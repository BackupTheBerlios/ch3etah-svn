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
 *   Date: 24/11/2004
 */

using System;
using System.IO;
using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.ProjectLib;

namespace Ch3Etah.Gui.DocumentHandling
{
	/// <summary>
	/// Description of ObjectEditorFactory.
	/// </summary>
	internal class ObjectEditorFactory
	{
		private ObjectEditorFactory()
		{
		}
		
		public static IObjectEditor CreateObjectEditor(object contextObject) {
			return CreateObjectEditor(contextObject, "Default");
		}
		
		public static IObjectEditor CreateObjectEditor(object contextObject, string command) {
			if (contextObject == null) {
				return null;
			}
			
			if (command == "" || command == null) {
				command = "Default";
			}
			
			IObjectEditor editor = null;
			if (contextObject is OleDbDataSource) {
				editor = new OleDbDataSourceEditor();
			}
			else if (contextObject is DataSource) 
			{
				editor = new DataSourceEditor();
			}
			if (contextObject.GetType() == typeof(MetadataFile)) 
			{
				editor = new OREntityEditor();
			}
			if (contextObject.GetType() == typeof(CodeGeneratorCommand)) {
				if (command.ToUpper() == "EDIT") {
					editor = new CodeGenerationTemplateEditor();
				} else {
 					editor = new CodeGeneratorCommandEditor();
				}
 			}
 			if (contextObject is Ch3Etah.Core.ProjectLib.InputParameterCollection) {
 				editor = new InputParameterCollectionEditor((Ch3Etah.Core.ProjectLib.InputParameterCollection) contextObject);
 			}
			if (contextObject is Template)
			{
				string path = ((Template)contextObject).GetFullPath();
				return new TextFileEditor(path);
			}
			if (contextObject is MacroLibrary)
			{
				string path = ((MacroLibrary)contextObject).GetFullPath();
				return new TextFileEditor(path);
			}
			if (editor != null) 
			{
 				editor.SelectedObject = contextObject;
 			}
			return editor;
		}
	}
}
