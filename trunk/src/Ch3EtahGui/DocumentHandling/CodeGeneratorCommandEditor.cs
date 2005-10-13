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
 *   Date: 29/9/2004
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Ch3Etah.Core.CodeGen.PackageLib;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Gui.Widgets;

namespace Ch3Etah.Gui.DocumentHandling {
	/// <summary>
	/// Description of CodeGeneratorCommandEditor.	
	/// </summary>
	public class CodeGeneratorCommandEditor : UserControl, IObjectEditor {
		private IContainer components;
		private Label lblInputParameters;
		private Panel panelTopRight;
		private Button btnEditTemplate;
		private Panel panelBottom;
		private Panel panelBottomRight;
		private TextBox txtOutputPath;
		private Label lblEngine;
		private Label lblTemplate;
		private Button btnSelectOutputPath;
		private Panel panelTop;
		private Splitter splitterBottom;
		private ToolTip toolTipProvider;
		private Label lblOutputPath;
		private Panel panelTopLeft;
		private Label lblPackage;
		private CheckBox chkOverwrite;
		private ComboBox cboEngine;
		private Splitter splitterTop;
		private Panel panelBottomLeft;

		#region Constructors and member variables

		private CodeGeneratorCommand _generatorCommand;

		private MetadataFileSelector tvwIndividualMetadataFiles;
		private GroupBox grpOutputGeneration;
		private RadioButton optSingleOutput;
		private RadioButton optMultiOutput;
		private GroupBox grpMultiOutput;
		private Panel panel1;
		private Button cmdSelectAll;
		private Button cmdSelectNone;
		private Button cmdInvertSelection;
		private ComboBox cboPackage;
		private ComboBox cboTemplate;
//		private MetadataFileSelector tvwGroupedMetadataFiles;
		private InputParameterCollectionEditor inputParametersEditor;

		public CodeGeneratorCommandEditor() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			tvwIndividualMetadataFiles = new MetadataFileSelector();
			grpMultiOutput.Controls.Add(tvwIndividualMetadataFiles);
			tvwIndividualMetadataFiles.BringToFront();
			tvwIndividualMetadataFiles.Dock = DockStyle.Fill;

//			tvwGroupedMetadataFiles = new MetadataFileSelector();
//			pageBatchMetadataFiles.Controls.Add(tvwGroupedMetadataFiles);
//			tvwGroupedMetadataFiles.BringToFront();
//			tvwGroupedMetadataFiles.Dock = DockStyle.Fill;

			inputParametersEditor = new InputParameterCollectionEditor();
			panelBottomLeft.Controls.Add(inputParametersEditor);
			inputParametersEditor.BringToFront();
			inputParametersEditor.Dock = DockStyle.Fill;

			toolTipProvider.SetToolTip(btnEditTemplate, "Edit Template");

			Enabled = false;
		}

		public CodeGeneratorCommandEditor(CodeGeneratorCommand generatorCommand) : this() {
			_generatorCommand = generatorCommand;
		}

		#endregion Constructors and member variables

		#region Windows Forms Designer generated code

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			components = new Container();
			panelBottomLeft = new Panel();
			lblInputParameters = new Label();
			splitterTop = new Splitter();
			cboEngine = new ComboBox();
			chkOverwrite = new CheckBox();
			lblPackage = new Label();
			panelTopLeft = new Panel();
			cboTemplate = new ComboBox();
			cboPackage = new ComboBox();
			btnEditTemplate = new Button();
			lblTemplate = new Label();
			lblOutputPath = new Label();
			toolTipProvider = new ToolTip(components);
			splitterBottom = new Splitter();
			panelTop = new Panel();
			panelTopRight = new Panel();
			btnSelectOutputPath = new Button();
			lblEngine = new Label();
			txtOutputPath = new TextBox();
			panelBottomRight = new Panel();
			grpMultiOutput = new GroupBox();
			panel1 = new Panel();
			cmdSelectAll = new Button();
			cmdSelectNone = new Button();
			cmdInvertSelection = new Button();
			grpOutputGeneration = new GroupBox();
			optMultiOutput = new RadioButton();
			optSingleOutput = new RadioButton();
			panelBottom = new Panel();
			panelBottomLeft.SuspendLayout();
			panelTopLeft.SuspendLayout();
			panelTop.SuspendLayout();
			panelTopRight.SuspendLayout();
			panelBottomRight.SuspendLayout();
			grpMultiOutput.SuspendLayout();
			panel1.SuspendLayout();
			grpOutputGeneration.SuspendLayout();
			panelBottom.SuspendLayout();
			SuspendLayout();
			// 
			// panelBottomLeft
			// 
			panelBottomLeft.Controls.Add(lblInputParameters);
			panelBottomLeft.Dock = DockStyle.Left;
			panelBottomLeft.Location = new Point(0, 0);
			panelBottomLeft.Name = "panelBottomLeft";
			panelBottomLeft.Size = new Size(296, 344);
			panelBottomLeft.TabIndex = 12;
			// 
			// lblInputParameters
			// 
			lblInputParameters.Dock = DockStyle.Top;
			lblInputParameters.Location = new Point(0, 0);
			lblInputParameters.Name = "lblInputParameters";
			lblInputParameters.Size = new Size(296, 16);
			lblInputParameters.TabIndex = 0;
			lblInputParameters.Text = "Input Parameters";
			// 
			// splitterTop
			// 
			splitterTop.Location = new Point(296, 0);
			splitterTop.Name = "splitterTop";
			splitterTop.Size = new Size(8, 96);
			splitterTop.TabIndex = 1;
			splitterTop.TabStop = false;
			// 
			// cboEngine
			// 
			cboEngine.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                     | AnchorStyles.Right)));
			cboEngine.DropDownStyle = ComboBoxStyle.DropDownList;
			cboEngine.Location = new Point(0, 64);
			cboEngine.Name = "cboEngine";
			cboEngine.Size = new Size(328, 21);
			cboEngine.TabIndex = 4;
			// 
			// chkOverwrite
			// 
			chkOverwrite.FlatStyle = FlatStyle.System;
			chkOverwrite.Location = new Point(16, 80);
			chkOverwrite.Name = "chkOverwrite";
			chkOverwrite.Size = new Size(152, 24);
			chkOverwrite.TabIndex = 8;
			chkOverwrite.Text = "Overwrite existing file(s)";
			// 
			// lblPackage
			// 
			lblPackage.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                      | AnchorStyles.Right)));
			lblPackage.Location = new Point(0, 8);
			lblPackage.Name = "lblPackage";
			lblPackage.Size = new Size(288, 16);
			lblPackage.TabIndex = 5;
			lblPackage.Text = "Package";
			// 
			// panelTopLeft
			// 
			panelTopLeft.Controls.Add(cboTemplate);
			panelTopLeft.Controls.Add(cboPackage);
			panelTopLeft.Controls.Add(btnEditTemplate);
			panelTopLeft.Controls.Add(lblTemplate);
			panelTopLeft.Controls.Add(lblPackage);
			panelTopLeft.Dock = DockStyle.Left;
			panelTopLeft.Location = new Point(0, 0);
			panelTopLeft.Name = "panelTopLeft";
			panelTopLeft.Size = new Size(296, 96);
			panelTopLeft.TabIndex = 0;
			// 
			// cboTemplate
			// 
			cboTemplate.DropDownStyle = ComboBoxStyle.DropDownList;
			cboTemplate.Location = new Point(8, 64);
			cboTemplate.Name = "cboTemplate";
			cboTemplate.Size = new Size(252, 21);
			cboTemplate.Sorted = true;
			cboTemplate.TabIndex = 1;
			cboTemplate.DropDown += new EventHandler(cboTemplate_DropDown);
			// 
			// cboPackage
			// 
			cboPackage.DropDownStyle = ComboBoxStyle.DropDownList;
			cboPackage.Location = new Point(8, 24);
			cboPackage.Name = "cboPackage";
			cboPackage.Size = new Size(280, 21);
			cboPackage.Sorted = true;
			cboPackage.TabIndex = 0;
			cboPackage.DropDown += new EventHandler(cboPackage_DropDown);
			// 
			// btnEditTemplate
			// 
			btnEditTemplate.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
			btnEditTemplate.FlatStyle = FlatStyle.System;
			btnEditTemplate.Location = new Point(264, 64);
			btnEditTemplate.Name = "btnEditTemplate";
			btnEditTemplate.Size = new Size(24, 21);
			btnEditTemplate.TabIndex = 2;
			btnEditTemplate.Text = "^";
			btnEditTemplate.Click += new EventHandler(btnEditTemplate_Click);
			// 
			// lblTemplate
			// 
			lblTemplate.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                       | AnchorStyles.Right)));
			lblTemplate.Location = new Point(0, 48);
			lblTemplate.Name = "lblTemplate";
			lblTemplate.Size = new Size(288, 16);
			lblTemplate.TabIndex = 7;
			lblTemplate.Text = "Template";
			// 
			// lblOutputPath
			// 
			lblOutputPath.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                         | AnchorStyles.Right)));
			lblOutputPath.Location = new Point(0, 8);
			lblOutputPath.Name = "lblOutputPath";
			lblOutputPath.Size = new Size(320, 16);
			lblOutputPath.TabIndex = 7;
			lblOutputPath.Text = "Output Path";
			// 
			// splitterBottom
			// 
			splitterBottom.Location = new Point(296, 0);
			splitterBottom.Name = "splitterBottom";
			splitterBottom.Size = new Size(8, 344);
			splitterBottom.TabIndex = 13;
			splitterBottom.TabStop = false;
			// 
			// panelTop
			// 
			panelTop.Controls.Add(panelTopRight);
			panelTop.Controls.Add(splitterTop);
			panelTop.Controls.Add(panelTopLeft);
			panelTop.Dock = DockStyle.Top;
			panelTop.Location = new Point(8, 8);
			panelTop.Name = "panelTop";
			panelTop.Size = new Size(632, 96);
			panelTop.TabIndex = 6;
			// 
			// panelTopRight
			// 
			panelTopRight.Controls.Add(btnSelectOutputPath);
			panelTopRight.Controls.Add(lblEngine);
			panelTopRight.Controls.Add(cboEngine);
			panelTopRight.Controls.Add(txtOutputPath);
			panelTopRight.Controls.Add(lblOutputPath);
			panelTopRight.Dock = DockStyle.Fill;
			panelTopRight.Location = new Point(304, 0);
			panelTopRight.Name = "panelTopRight";
			panelTopRight.Size = new Size(328, 96);
			panelTopRight.TabIndex = 2;
			// 
			// btnSelectOutputPath
			// 
			btnSelectOutputPath.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
			btnSelectOutputPath.FlatStyle = FlatStyle.System;
			btnSelectOutputPath.Location = new Point(300, 24);
			btnSelectOutputPath.Name = "btnSelectOutputPath";
			btnSelectOutputPath.Size = new Size(24, 21);
			btnSelectOutputPath.TabIndex = 7;
			btnSelectOutputPath.Text = "...";
			btnSelectOutputPath.Visible = false;
			// 
			// lblEngine
			// 
			lblEngine.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                     | AnchorStyles.Right)));
			lblEngine.Location = new Point(0, 48);
			lblEngine.Name = "lblEngine";
			lblEngine.Size = new Size(224, 16);
			lblEngine.TabIndex = 10;
			lblEngine.Text = "Engine";
			// 
			// txtOutputPath
			// 
			txtOutputPath.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                         | AnchorStyles.Right)));
			txtOutputPath.Location = new Point(0, 24);
			txtOutputPath.Name = "txtOutputPath";
			txtOutputPath.Size = new Size(296, 20);
			txtOutputPath.TabIndex = 3;
			txtOutputPath.Text = "";
			// 
			// panelBottomRight
			// 
			panelBottomRight.Controls.Add(grpMultiOutput);
			panelBottomRight.Controls.Add(grpOutputGeneration);
			panelBottomRight.Dock = DockStyle.Fill;
			panelBottomRight.Location = new Point(304, 0);
			panelBottomRight.Name = "panelBottomRight";
			panelBottomRight.Size = new Size(328, 344);
			panelBottomRight.TabIndex = 14;
			// 
			// grpMultiOutput
			// 
			grpMultiOutput.Controls.Add(panel1);
			grpMultiOutput.Dock = DockStyle.Fill;
			grpMultiOutput.Location = new Point(0, 112);
			grpMultiOutput.Name = "grpMultiOutput";
			grpMultiOutput.Size = new Size(328, 232);
			grpMultiOutput.TabIndex = 11;
			grpMultiOutput.TabStop = false;
			grpMultiOutput.Text = "Selected metadata files";
			// 
			// panel1
			// 
			panel1.Controls.Add(cmdSelectAll);
			panel1.Controls.Add(cmdSelectNone);
			panel1.Controls.Add(cmdInvertSelection);
			panel1.Dock = DockStyle.Right;
			panel1.Location = new Point(221, 16);
			panel1.Name = "panel1";
			panel1.Size = new Size(104, 213);
			panel1.TabIndex = 0;
			// 
			// cmdSelectAll
			// 
			cmdSelectAll.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                        | AnchorStyles.Right)));
			cmdSelectAll.FlatStyle = FlatStyle.System;
			cmdSelectAll.Location = new Point(8, 8);
			cmdSelectAll.Name = "cmdSelectAll";
			cmdSelectAll.Size = new Size(91, 23);
			cmdSelectAll.TabIndex = 0;
			cmdSelectAll.Text = "Select All";
			cmdSelectAll.Click += new EventHandler(cmdSelectAll_Click);
			// 
			// cmdSelectNone
			// 
			cmdSelectNone.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                         | AnchorStyles.Right)));
			cmdSelectNone.FlatStyle = FlatStyle.System;
			cmdSelectNone.Location = new Point(8, 40);
			cmdSelectNone.Name = "cmdSelectNone";
			cmdSelectNone.Size = new Size(91, 23);
			cmdSelectNone.TabIndex = 0;
			cmdSelectNone.Text = "Select None";
			cmdSelectNone.Click += new EventHandler(cmdSelectNone_Click);
			// 
			// cmdInvertSelection
			// 
			cmdInvertSelection.Anchor = ((AnchorStyles) (((AnchorStyles.Top | AnchorStyles.Left)
			                                              | AnchorStyles.Right)));
			cmdInvertSelection.FlatStyle = FlatStyle.System;
			cmdInvertSelection.Location = new Point(8, 72);
			cmdInvertSelection.Name = "cmdInvertSelection";
			cmdInvertSelection.Size = new Size(91, 23);
			cmdInvertSelection.TabIndex = 0;
			cmdInvertSelection.Text = "Invert Selection";
			cmdInvertSelection.Click += new EventHandler(cmdInvertSelection_Click);
			// 
			// grpOutputGeneration
			// 
			grpOutputGeneration.Controls.Add(optMultiOutput);
			grpOutputGeneration.Controls.Add(optSingleOutput);
			grpOutputGeneration.Controls.Add(chkOverwrite);
			grpOutputGeneration.Dock = DockStyle.Top;
			grpOutputGeneration.Location = new Point(0, 0);
			grpOutputGeneration.Name = "grpOutputGeneration";
			grpOutputGeneration.Size = new Size(328, 112);
			grpOutputGeneration.TabIndex = 0;
			grpOutputGeneration.TabStop = false;
			grpOutputGeneration.Text = "Ouput generation";
			// 
			// optMultiOutput
			// 
			optMultiOutput.FlatStyle = FlatStyle.System;
			optMultiOutput.Location = new Point(16, 48);
			optMultiOutput.Name = "optMultiOutput";
			optMultiOutput.Size = new Size(280, 24);
			optMultiOutput.TabIndex = 11;
			optMultiOutput.Text = "Multiple output files  (one output per metadata file)";
			optMultiOutput.CheckedChanged += new EventHandler(optMultiOutput_CheckedChanged);
			// 
			// optSingleOutput
			// 
			optSingleOutput.Checked = true;
			optSingleOutput.FlatStyle = FlatStyle.System;
			optSingleOutput.Location = new Point(16, 24);
			optSingleOutput.Name = "optSingleOutput";
			optSingleOutput.Size = new Size(112, 24);
			optSingleOutput.TabIndex = 9;
			optSingleOutput.TabStop = true;
			optSingleOutput.Text = "Single output file";
			optSingleOutput.CheckedChanged += new EventHandler(optSingleOutput_CheckedChanged);
			// 
			// panelBottom
			// 
			panelBottom.Controls.Add(panelBottomRight);
			panelBottom.Controls.Add(splitterBottom);
			panelBottom.Controls.Add(panelBottomLeft);
			panelBottom.Dock = DockStyle.Fill;
			panelBottom.Location = new Point(8, 104);
			panelBottom.Name = "panelBottom";
			panelBottom.Size = new Size(632, 344);
			panelBottom.TabIndex = 9;
			// 
			// CodeGeneratorCommandEditor
			// 
			Controls.Add(panelBottom);
			Controls.Add(panelTop);
			DockPadding.All = 8;
			Name = "CodeGeneratorCommandEditor";
			Size = new Size(648, 456);
			Load += new EventHandler(CodeGeneratorCommandEditorLoad);
			panelBottomLeft.ResumeLayout(false);
			panelTopLeft.ResumeLayout(false);
			panelTop.ResumeLayout(false);
			panelTopRight.ResumeLayout(false);
			panelBottomRight.ResumeLayout(false);
			grpMultiOutput.ResumeLayout(false);
			panel1.ResumeLayout(false);
			grpOutputGeneration.ResumeLayout(false);
			panelBottom.ResumeLayout(false);
			ResumeLayout(false);

		}

		#endregion

		public event EventHandler SelectedObjectChanged;

		protected virtual void OnSelectedObjectChanged() {
			if (SelectedObjectChanged != null) {
				SelectedObjectChanged(SelectedObject, new EventArgs());
			}
		}

		#region DoBinding

		private void DoBinding() {
			cboEngine.Items.Clear();
			cboEngine.Items.Add(CodeGeneratorEngine.NVelocity);
			cboEngine.Items.Add(CodeGeneratorEngine.Xslt);

			cboPackage.Items.Clear();
			cboPackage.Items.Add(_generatorCommand.Package);

			cboTemplate.Items.Clear();
			cboTemplate.Items.Add(_generatorCommand.Template);

			cboPackage.DataBindings.Clear();
			cboTemplate.DataBindings.Clear();
			txtOutputPath.DataBindings.Clear();
			chkOverwrite.DataBindings.Clear();
			cboEngine.DataBindings.Clear();

			cboPackage.DataBindings.Add("Text", _generatorCommand, "Package");
			cboTemplate.DataBindings.Add("Text", _generatorCommand, "Template");
			txtOutputPath.DataBindings.Add("Text", _generatorCommand, "OutputPath");
			chkOverwrite.DataBindings.Add("Checked", _generatorCommand, "Overwrite");
			cboEngine.DataBindings.Add("SelectedItem", _generatorCommand, "Engine");

			tvwIndividualMetadataFiles.Bind(_generatorCommand.Project.MetadataFiles, _generatorCommand.IndividualMetadataFiles);
			optMultiOutput.Checked = (_generatorCommand.CodeGenerationMode == CodeGenerationMode.MultipleOutput);
//			tvwGroupedMetadataFiles.Bind(_generatorCommand.Project.MetadataFiles, _generatorCommand.GroupedMetadataFiles);

			inputParametersEditor.SelectedObject = _generatorCommand.InputParameters;

			Enabled = true;
		}

		#endregion DoBinding

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
			get { return Images.CreateIcon(Images.DocumentArrowGreen); }
		}

		#endregion Icon

		#region ToString

		public override string ToString() {
			return _generatorCommand.Name;
		}

		#endregion ToString

		#region CommitChanges

		public bool CommitChanges() {
			inputParametersEditor.CommitChanges();
			return true;
		}

		#endregion CommitChanges

		#region QueryUnload

		public void QueryUnload(out bool cancel) {
			inputParametersEditor.QueryUnload(out cancel);
			if (cancel) {
				return;
			}
			cancel = (!CommitChanges());
		}

		#endregion QueryUnload

		#region Events

		private void CodeGeneratorCommandEditorLoad(object sender, EventArgs e) {
			if (_generatorCommand != null) {
				DoBinding();
			}
		}

		private void btnEditTemplate_Click(object sender, EventArgs e) {
			if (_generatorCommand.Template == "") {
				MessageBox.Show("You must specify a template first!", "Specify a template", MessageBoxButtons.OK,
				                MessageBoxIcon.Exclamation);
				return;
			}
			CodeGenerationTemplateEditor editor = new CodeGenerationTemplateEditor(_generatorCommand);
			ObjectEditorManager.OpenObjectEditor(editor);
		}

		private void cmdSelectAll_Click(object sender, EventArgs e) {
			Cursor.Current = Cursors.WaitCursor;
			tvwIndividualMetadataFiles.SuspendLayout();
			foreach (TreeNode node in tvwIndividualMetadataFiles.Nodes) {
				node.Checked = true;
			}
			tvwIndividualMetadataFiles.ResumeLayout();
			Cursor.Current = Cursors.Default;
		}

		private void cmdSelectNone_Click(object sender, EventArgs e) {
			Cursor.Current = Cursors.WaitCursor;
			tvwIndividualMetadataFiles.SuspendLayout();
			foreach (TreeNode node in tvwIndividualMetadataFiles.Nodes) {
				node.Checked = false;
			}
			tvwIndividualMetadataFiles.ResumeLayout();
			Cursor.Current = Cursors.Default;
		}

		private void cmdInvertSelection_Click(object sender, EventArgs e) {
			Cursor.Current = Cursors.WaitCursor;
			tvwIndividualMetadataFiles.SuspendLayout();
			foreach (TreeNode node in tvwIndividualMetadataFiles.Nodes) {
				node.Checked = !node.Checked;
			}
			tvwIndividualMetadataFiles.ResumeLayout();
			Cursor.Current = Cursors.Default;
		}

		private void optSingleOutput_CheckedChanged(object sender, EventArgs e) {
			_generatorCommand.CodeGenerationMode = CodeGenerationMode.SingleOutput;
		}

		private void optMultiOutput_CheckedChanged(object sender, EventArgs e) {
			_generatorCommand.CodeGenerationMode = CodeGenerationMode.MultipleOutput;
		}

		private void cboPackage_DropDown(object sender, EventArgs e) {
			
			Package[] packages = Package.ListPackages(
				_generatorCommand.Project.GetFullTemplatePath());

			if (packages != null && packages.Length > 0) {
				foreach (Package p in packages) {
					if (!cboPackage.Items.Contains(p.Name))
						cboPackage.Items.Add(p.Name);
				}
			}
		}

		private void cboTemplate_DropDown(object sender, EventArgs e) {
			if (cboPackage.Text.Trim() == "")
				return;

			Package package = Package.GetPackage(
				_generatorCommand.Project.GetFullTemplatePath(),
				cboPackage.Text);;
			if (package == null)
				return;
			
			foreach (Template t in package.Templates)
			{
				if (!cboTemplate.Items.Contains(t.Name))
					cboTemplate.Items.Add(t.Name);
			}
		}

		#endregion Events


	}
}