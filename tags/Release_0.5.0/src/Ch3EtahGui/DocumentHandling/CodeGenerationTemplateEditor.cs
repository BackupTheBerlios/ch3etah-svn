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
 *   Date: 12/10/2004
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ch3Etah.Core.ProjectLib;
using Reflector.UserInterface;

namespace Ch3Etah.Gui.DocumentHandling {
	/// <summary>
	/// Description of CodeGenerationTemplateEditor.	
	/// </summary>
	public class CodeGenerationTemplateEditor : UserControl, IObjectEditor {
		private TabPage pageTemplate;
		private TabPage pageOutput;
		private ComboBox cboMetadataFile;
		private TabControl tabControlMain;
		private TextBox txtOutput;
		private Panel panelMetadataFile;

		#region Constructors and member variables

		private CodeGeneratorCommand _generatorCommand;

		#region UI member variables

		protected TextFileEditor templateEditor;
		protected MetadataFileEditor metadataFileEditor;

		//		private MetadataFileSelector tvwGroupedMetadataFiles;

		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar toolBar = new CommandBar(CommandBarStyle.ToolBar);

		private CommandBarItem cbiRunTemplate;

		#endregion UI member variables

		public CodeGenerationTemplateEditor() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			templateEditor = new TextFileEditor();
			pageTemplate.Controls.Add(templateEditor);
			templateEditor.Dock = DockStyle.Fill;

			metadataFileEditor = new MetadataFileEditor();
			panelMetadataFile.Controls.Add(metadataFileEditor);
			metadataFileEditor.Dock = DockStyle.Fill;

//			tvwGroupedMetadataFiles = new MetadataFileSelector();
//			pageGroupedMetadataFiles.Controls.Add(tvwGroupedMetadataFiles);
//			tvwGroupedMetadataFiles.BringToFront();
//			tvwGroupedMetadataFiles.Dock = DockStyle.Fill;

			SetupCommandBars();
		}

		public CodeGenerationTemplateEditor(CodeGeneratorCommand generatorCommand) : this() {
			SelectedObject = generatorCommand;
		}

		#endregion Constructors and member variables

		#region Windows Forms Designer generated code

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			panelMetadataFile = new Panel();
			txtOutput = new TextBox();
			tabControlMain = new TabControl();
			cboMetadataFile = new ComboBox();
			pageOutput = new TabPage();
			pageTemplate = new TabPage();
			tabControlMain.SuspendLayout();
			pageOutput.SuspendLayout();
			SuspendLayout();
			// 
			// panelMetadataFile
			// 
			panelMetadataFile.Dock = DockStyle.Fill;
			panelMetadataFile.Location = new Point(0, 21);
			panelMetadataFile.Name = "panelMetadataFile";
			panelMetadataFile.Size = new Size(640, 465);
			panelMetadataFile.TabIndex = 3;
			// 
			// txtOutput
			// 
			txtOutput.BackColor = SystemColors.Window;
			txtOutput.Dock = DockStyle.Fill;
			txtOutput.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			txtOutput.Location = new Point(0, 0);
			txtOutput.Multiline = true;
			txtOutput.Name = "txtOutput";
			txtOutput.ReadOnly = true;
			txtOutput.ScrollBars = ScrollBars.Both;
			txtOutput.Size = new Size(640, 486);
			txtOutput.TabIndex = 4;
			txtOutput.Text = "";
			txtOutput.WordWrap = false;
			// 
			// tabControlMain
			// 
			tabControlMain.Alignment = TabAlignment.Bottom;
			tabControlMain.Controls.Add(pageTemplate);
			tabControlMain.Controls.Add(pageOutput);
			tabControlMain.Dock = DockStyle.Fill;
			tabControlMain.Location = new Point(0, 0);
			tabControlMain.Name = "tabControlMain";
			tabControlMain.SelectedIndex = 0;
			tabControlMain.Size = new Size(648, 512);
			tabControlMain.TabIndex = 3;
			// 
			// cboMetadataFile
			// 
			cboMetadataFile.Dock = DockStyle.Top;
			cboMetadataFile.DropDownStyle = ComboBoxStyle.DropDownList;
			cboMetadataFile.Location = new Point(0, 0);
			cboMetadataFile.Name = "cboMetadataFile";
			cboMetadataFile.Size = new Size(640, 21);
			cboMetadataFile.TabIndex = 2;
			cboMetadataFile.SelectionChangeCommitted += new EventHandler(cboMetadataFile_SelectionChangeCommitted);
			// 
			// pageOutput
			// 
			pageOutput.Controls.Add(txtOutput);
			pageOutput.Location = new Point(4, 4);
			pageOutput.Name = "pageOutput";
			pageOutput.Size = new Size(640, 486);
			pageOutput.TabIndex = 0;
			pageOutput.Text = "Preview Output";
			// 
			// pageTemplate
			// 
			pageTemplate.Location = new Point(4, 4);
			pageTemplate.Name = "pageTemplate";
			pageTemplate.Size = new Size(640, 486);
			pageTemplate.TabIndex = 3;
			pageTemplate.Text = "Template";
			// 
			// CodeGenerationTemplateEditor
			// 
			Controls.Add(tabControlMain);
			Name = "CodeGenerationTemplateEditor";
			Size = new Size(648, 512);
			tabControlMain.ResumeLayout(false);
			pageOutput.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		#region InitializeMetadataFileCombo

		private void InitializeMetadataFileCombo() {
			cboMetadataFile.DataSource = _generatorCommand.IndividualMetadataFiles;
			try {
				cboMetadataFile.SelectedItem = _generatorCommand.IndividualMetadataFiles[0];
				metadataFileEditor.SelectedObject = cboMetadataFile.SelectedValue;
			}
			catch {}
		}

		#endregion InitializeMetadataFileCombo

		public event EventHandler SelectedObjectChanged;

		protected virtual void OnSelectedObjectChanged() {
			if (SelectedObjectChanged != null) {
				SelectedObjectChanged(SelectedObject, new EventArgs());
			}
		}

		#region DoBinding

		private void DoBinding() {
			templateEditor.SelectedObject = _generatorCommand.GetFullTemplatePath();
//			tvwGroupedMetadataFiles.Bind(_generatorCommand.Project.MetadataFiles, _generatorCommand.GroupedMetadataFiles);
			InitializeMetadataFileCombo();
		}

		#endregion DoBinding

		#region Properties

		#region SelectedObject

		public object SelectedObject {
			get { return _generatorCommand; }
			set {
				_generatorCommand = (CodeGeneratorCommand) value;
				DoBinding();
			}
		}

		#endregion SelectedObject

		#region IsDirty

		public bool IsDirty {
			get { return false; }
		}

		#endregion IsDirty

		#region Icon

		public Icon Icon {
			get { return Images.CreateIcon(Images.DocumentCh3Etah); }
		}

		#endregion Icon

		#endregion Properties

		#region ToString

		public override string ToString() {
			return _generatorCommand.Name + "::" + _generatorCommand.Template;
		}

		#endregion ToString

		#region CommitChanges

		public bool CommitChanges() {
			// TODO: CommitChanges
			return templateEditor.CommitChanges();
		}

		#endregion CommitChanges

		#region QueryUnload

		public void QueryUnload(out bool cancel) {
			templateEditor.QueryUnload(out cancel);
		}

		#endregion QueryUnload

		#region CommandBar code

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			// Handle keyboard shortcuts
			if (commandBarManager.PreProcessMessage(ref msg)) {
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void SetupCommandBars() {
			SetupCommandBarButtons();

			// Toolbar
			toolBar.Items.Add(cbiRunTemplate);

			// Add to user interface
			commandBarManager.CommandBars.Add(toolBar);
			Controls.Add(commandBarManager);
		}

		private void SetupCommandBarButtons() {
			cbiRunTemplate =
				new CommandBarButton(
					Images.DocumentArrowGreen, "&Preview Template Output", new EventHandler(RunTemplate_Click), Keys.F5);
		}

		#endregion CommandBar code

		#region Events

		private void cboMetadataFile_SelectionChangeCommitted(object sender, EventArgs e) {
			metadataFileEditor.SelectedObject = cboMetadataFile.SelectedValue;
		}

		private void RunTemplate_Click(object sender, EventArgs e) {
			// TODO: Allow user to preview template without saving the file
			try {
				txtOutput.Text = "";
				StringWriter outputWriter = new StringWriter();
				_generatorCommand.GenerateFile((MetadataFile) cboMetadataFile.SelectedValue, outputWriter);
				txtOutput.Text = outputWriter.ToString();
				//if (outputWriter.ToString().Trim() == "") {
				//	MessageBox.Show("Code generation did not return any results!");
				//}
				tabControlMain.SelectedTab = pageOutput;
			}
			catch (Exception ex) {
				MessageBox.Show("Error running command:\r\n\r\n" + ex.ToString(), "Error running command", MessageBoxButtons.OK,
				                MessageBoxIcon.Error);
			}
		}

		#endregion Events
	}
}