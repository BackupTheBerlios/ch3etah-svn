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
 *   Date: 5/10/2004
 */

using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Ch3Etah.Core.ProjectLib;
using ICSharpCode.TextEditor.Document;

namespace Ch3Etah.Gui.DocumentHandling {
	/// <summary>
	/// Description of MetadataFileEditor.	
	/// </summary>
	public class MetadataFileEditor : TextFileEditor {

		#region Constructors and member variables

		private MetadataFile _metadataFile;

		public MetadataFileEditor() : base() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Debug.WriteLine("Setting Xml Mode...");
			txtDocument.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
			txtDocument.Enabled = false;
		}

		public MetadataFileEditor(MetadataFile metadataFile) : base(metadataFile.GetFullPath()) {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			_metadataFile = metadataFile;
			txtDocument.Enabled = true;
		}

		#endregion Constructors and member variables

		#region Windows Forms Designer generated code

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			SuspendLayout();
			// 
			// txtDocument
			// 
			txtDocument.EnableFolding = true;
			txtDocument.LineViewerStyle = LineViewerStyle.FullRow;
			txtDocument.Name = "txtDocument";
			txtDocument.Size = new Size(758, 629);
			// 
			// MetadataFileEditor
			// 
			Name = "MetadataFileEditor";
			Size = new Size(758, 629);
			ResumeLayout(false);

		}

		#endregion

		#region SelectedObject

		public override object SelectedObject {
			get { return _metadataFile; }
			set {
				_metadataFile = (MetadataFile) value;
				FileName = _metadataFile.GetFullPath();
				if (_metadataFile != null) {
					txtDocument.Enabled = true;
				}
				else {
					txtDocument.Enabled = false;
				}
//				txtDocument.Document.FoldingManager = new FoldingManager(txtDocument.Document, );
			}
		}

		#endregion SelectedObject

		#region CommitChanges

		public override bool CommitChanges() {
			if (GetText().ToUpper().IndexOf("ENCODING") > 0 && txtDocument.Text.ToUpper().IndexOf("UTF-8") < 0) {
				MessageBox.Show(
					"WARNING: Text files are saved by this program using UTF-8 encoding. Specifying an encoding attribute other than UTF-8 for your xml file may cause errors when reloading the file.",
					"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(GetText());
			_metadataFile.LoadXml(doc);
			bool ok = base.CommitChanges();
			if (ok) {
				_metadataFile.FileName = _metadataFile.GetRelativePath(FileName);
			}
			return ok;
		}

		#endregion CommitChanges
	}
}