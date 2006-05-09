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
using System.Windows.Forms;

using Ch3Etah.Core.CodeGen;
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
		private Button cmdSelectAll;
		private Button cmdSelectNone;
		private Button cmdInvertSelection;
		private ComboBox cboPackage;
		private ComboBox cboTemplate;
		private System.Windows.Forms.Panel panelMetadataFileSelectionButtons;
		private System.Windows.Forms.CheckBox chkAutoSelectMetadataFiles;
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
			toolTipProvider.SetToolTip(chkAutoSelectMetadataFiles, "Auto-select metadata files that are compatible with the current template when they are added to the project.\r\nIt is generally recommended that you leave this checked, unless you know that you only want to execute the template for specific files.");

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
			this.components = new System.ComponentModel.Container();
			this.panelBottomLeft = new System.Windows.Forms.Panel();
			this.lblInputParameters = new System.Windows.Forms.Label();
			this.splitterTop = new System.Windows.Forms.Splitter();
			this.cboEngine = new System.Windows.Forms.ComboBox();
			this.chkOverwrite = new System.Windows.Forms.CheckBox();
			this.lblPackage = new System.Windows.Forms.Label();
			this.panelTopLeft = new System.Windows.Forms.Panel();
			this.cboTemplate = new System.Windows.Forms.ComboBox();
			this.cboPackage = new System.Windows.Forms.ComboBox();
			this.btnEditTemplate = new System.Windows.Forms.Button();
			this.lblTemplate = new System.Windows.Forms.Label();
			this.lblOutputPath = new System.Windows.Forms.Label();
			this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
			this.splitterBottom = new System.Windows.Forms.Splitter();
			this.panelTop = new System.Windows.Forms.Panel();
			this.panelTopRight = new System.Windows.Forms.Panel();
			this.btnSelectOutputPath = new System.Windows.Forms.Button();
			this.lblEngine = new System.Windows.Forms.Label();
			this.txtOutputPath = new System.Windows.Forms.TextBox();
			this.panelBottomRight = new System.Windows.Forms.Panel();
			this.grpMultiOutput = new System.Windows.Forms.GroupBox();
			this.panelMetadataFileSelectionButtons = new System.Windows.Forms.Panel();
			this.chkAutoSelectMetadataFiles = new System.Windows.Forms.CheckBox();
			this.cmdSelectAll = new System.Windows.Forms.Button();
			this.cmdSelectNone = new System.Windows.Forms.Button();
			this.cmdInvertSelection = new System.Windows.Forms.Button();
			this.grpOutputGeneration = new System.Windows.Forms.GroupBox();
			this.optMultiOutput = new System.Windows.Forms.RadioButton();
			this.optSingleOutput = new System.Windows.Forms.RadioButton();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.panelBottomLeft.SuspendLayout();
			this.panelTopLeft.SuspendLayout();
			this.panelTop.SuspendLayout();
			this.panelTopRight.SuspendLayout();
			this.panelBottomRight.SuspendLayout();
			this.grpMultiOutput.SuspendLayout();
			this.panelMetadataFileSelectionButtons.SuspendLayout();
			this.grpOutputGeneration.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelBottomLeft
			// 
			this.panelBottomLeft.Controls.Add(this.lblInputParameters);
			this.panelBottomLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelBottomLeft.Location = new System.Drawing.Point(0, 0);
			this.panelBottomLeft.Name = "panelBottomLeft";
			this.panelBottomLeft.Size = new System.Drawing.Size(296, 344);
			this.panelBottomLeft.TabIndex = 12;
			// 
			// lblInputParameters
			// 
			this.lblInputParameters.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInputParameters.Location = new System.Drawing.Point(0, 0);
			this.lblInputParameters.Name = "lblInputParameters";
			this.lblInputParameters.Size = new System.Drawing.Size(296, 16);
			this.lblInputParameters.TabIndex = 0;
			this.lblInputParameters.Text = "Input Parameters";
			// 
			// splitterTop
			// 
			this.splitterTop.Location = new System.Drawing.Point(296, 0);
			this.splitterTop.Name = "splitterTop";
			this.splitterTop.Size = new System.Drawing.Size(8, 96);
			this.splitterTop.TabIndex = 1;
			this.splitterTop.TabStop = false;
			// 
			// cboEngine
			// 
			this.cboEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboEngine.Location = new System.Drawing.Point(0, 64);
			this.cboEngine.Name = "cboEngine";
			this.cboEngine.Size = new System.Drawing.Size(328, 21);
			this.cboEngine.TabIndex = 4;
			// 
			// chkOverwrite
			// 
			this.chkOverwrite.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkOverwrite.Location = new System.Drawing.Point(16, 80);
			this.chkOverwrite.Name = "chkOverwrite";
			this.chkOverwrite.Size = new System.Drawing.Size(152, 24);
			this.chkOverwrite.TabIndex = 8;
			this.chkOverwrite.Text = "Overwrite existing file(s)";
			// 
			// lblPackage
			// 
			this.lblPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPackage.Location = new System.Drawing.Point(0, 8);
			this.lblPackage.Name = "lblPackage";
			this.lblPackage.Size = new System.Drawing.Size(288, 16);
			this.lblPackage.TabIndex = 5;
			this.lblPackage.Text = "Package";
			// 
			// panelTopLeft
			// 
			this.panelTopLeft.Controls.Add(this.cboTemplate);
			this.panelTopLeft.Controls.Add(this.cboPackage);
			this.panelTopLeft.Controls.Add(this.btnEditTemplate);
			this.panelTopLeft.Controls.Add(this.lblTemplate);
			this.panelTopLeft.Controls.Add(this.lblPackage);
			this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelTopLeft.Location = new System.Drawing.Point(0, 0);
			this.panelTopLeft.Name = "panelTopLeft";
			this.panelTopLeft.Size = new System.Drawing.Size(296, 96);
			this.panelTopLeft.TabIndex = 0;
			// 
			// cboTemplate
			// 
			this.cboTemplate.Location = new System.Drawing.Point(8, 64);
			this.cboTemplate.Name = "cboTemplate";
			this.cboTemplate.Size = new System.Drawing.Size(244, 21);
			this.cboTemplate.Sorted = true;
			this.cboTemplate.TabIndex = 1;
			this.cboTemplate.DropDown += new System.EventHandler(this.cboTemplate_DropDown);
			// 
			// cboPackage
			// 
			this.cboPackage.Location = new System.Drawing.Point(8, 24);
			this.cboPackage.Name = "cboPackage";
			this.cboPackage.Size = new System.Drawing.Size(280, 21);
			this.cboPackage.Sorted = true;
			this.cboPackage.TabIndex = 0;
			this.cboPackage.DropDown += new System.EventHandler(this.cboPackage_DropDown);
			// 
			// btnEditTemplate
			// 
			this.btnEditTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEditTemplate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEditTemplate.Location = new System.Drawing.Point(256, 64);
			this.btnEditTemplate.Name = "btnEditTemplate";
			this.btnEditTemplate.Size = new System.Drawing.Size(32, 21);
			this.btnEditTemplate.TabIndex = 2;
			this.btnEditTemplate.Text = "Edit";
			this.btnEditTemplate.Click += new System.EventHandler(this.btnEditTemplate_Click);
			// 
			// lblTemplate
			// 
			this.lblTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblTemplate.Location = new System.Drawing.Point(0, 48);
			this.lblTemplate.Name = "lblTemplate";
			this.lblTemplate.Size = new System.Drawing.Size(288, 16);
			this.lblTemplate.TabIndex = 7;
			this.lblTemplate.Text = "Template";
			// 
			// lblOutputPath
			// 
			this.lblOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblOutputPath.Location = new System.Drawing.Point(0, 8);
			this.lblOutputPath.Name = "lblOutputPath";
			this.lblOutputPath.Size = new System.Drawing.Size(320, 16);
			this.lblOutputPath.TabIndex = 7;
			this.lblOutputPath.Text = "Output Path";
			// 
			// splitterBottom
			// 
			this.splitterBottom.Location = new System.Drawing.Point(296, 0);
			this.splitterBottom.Name = "splitterBottom";
			this.splitterBottom.Size = new System.Drawing.Size(8, 344);
			this.splitterBottom.TabIndex = 13;
			this.splitterBottom.TabStop = false;
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.panelTopRight);
			this.panelTop.Controls.Add(this.splitterTop);
			this.panelTop.Controls.Add(this.panelTopLeft);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(8, 8);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(632, 96);
			this.panelTop.TabIndex = 6;
			// 
			// panelTopRight
			// 
			this.panelTopRight.Controls.Add(this.btnSelectOutputPath);
			this.panelTopRight.Controls.Add(this.lblEngine);
			this.panelTopRight.Controls.Add(this.cboEngine);
			this.panelTopRight.Controls.Add(this.txtOutputPath);
			this.panelTopRight.Controls.Add(this.lblOutputPath);
			this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelTopRight.Location = new System.Drawing.Point(304, 0);
			this.panelTopRight.Name = "panelTopRight";
			this.panelTopRight.Size = new System.Drawing.Size(328, 96);
			this.panelTopRight.TabIndex = 2;
			// 
			// btnSelectOutputPath
			// 
			this.btnSelectOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectOutputPath.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectOutputPath.Location = new System.Drawing.Point(300, 24);
			this.btnSelectOutputPath.Name = "btnSelectOutputPath";
			this.btnSelectOutputPath.Size = new System.Drawing.Size(24, 21);
			this.btnSelectOutputPath.TabIndex = 7;
			this.btnSelectOutputPath.Text = "...";
			this.btnSelectOutputPath.Visible = false;
			// 
			// lblEngine
			// 
			this.lblEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblEngine.Location = new System.Drawing.Point(0, 48);
			this.lblEngine.Name = "lblEngine";
			this.lblEngine.Size = new System.Drawing.Size(224, 16);
			this.lblEngine.TabIndex = 10;
			this.lblEngine.Text = "Engine";
			// 
			// txtOutputPath
			// 
			this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputPath.Location = new System.Drawing.Point(0, 24);
			this.txtOutputPath.Name = "txtOutputPath";
			this.txtOutputPath.Size = new System.Drawing.Size(300, 20);
			this.txtOutputPath.TabIndex = 3;
			this.txtOutputPath.Text = "";
			// 
			// panelBottomRight
			// 
			this.panelBottomRight.Controls.Add(this.grpMultiOutput);
			this.panelBottomRight.Controls.Add(this.grpOutputGeneration);
			this.panelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBottomRight.Location = new System.Drawing.Point(304, 0);
			this.panelBottomRight.Name = "panelBottomRight";
			this.panelBottomRight.Size = new System.Drawing.Size(328, 344);
			this.panelBottomRight.TabIndex = 14;
			// 
			// grpMultiOutput
			// 
			this.grpMultiOutput.Controls.Add(this.panelMetadataFileSelectionButtons);
			this.grpMultiOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpMultiOutput.Location = new System.Drawing.Point(0, 112);
			this.grpMultiOutput.Name = "grpMultiOutput";
			this.grpMultiOutput.Size = new System.Drawing.Size(328, 232);
			this.grpMultiOutput.TabIndex = 11;
			this.grpMultiOutput.TabStop = false;
			this.grpMultiOutput.Text = "Selected metadata files";
			// 
			// panelMetadataFileSelectionButtons
			// 
			this.panelMetadataFileSelectionButtons.Controls.Add(this.chkAutoSelectMetadataFiles);
			this.panelMetadataFileSelectionButtons.Controls.Add(this.cmdSelectAll);
			this.panelMetadataFileSelectionButtons.Controls.Add(this.cmdSelectNone);
			this.panelMetadataFileSelectionButtons.Controls.Add(this.cmdInvertSelection);
			this.panelMetadataFileSelectionButtons.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelMetadataFileSelectionButtons.Location = new System.Drawing.Point(221, 16);
			this.panelMetadataFileSelectionButtons.Name = "panelMetadataFileSelectionButtons";
			this.panelMetadataFileSelectionButtons.Size = new System.Drawing.Size(104, 213);
			this.panelMetadataFileSelectionButtons.TabIndex = 0;
			// 
			// chkAutoSelectMetadataFiles
			// 
			this.chkAutoSelectMetadataFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkAutoSelectMetadataFiles.Location = new System.Drawing.Point(16, 104);
			this.chkAutoSelectMetadataFiles.Name = "chkAutoSelectMetadataFiles";
			this.chkAutoSelectMetadataFiles.Size = new System.Drawing.Size(80, 32);
			this.chkAutoSelectMetadataFiles.TabIndex = 1;
			this.chkAutoSelectMetadataFiles.Text = "Auto-select new files";
			// 
			// cmdSelectAll
			// 
			this.cmdSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSelectAll.Location = new System.Drawing.Point(8, 8);
			this.cmdSelectAll.Name = "cmdSelectAll";
			this.cmdSelectAll.Size = new System.Drawing.Size(91, 23);
			this.cmdSelectAll.TabIndex = 0;
			this.cmdSelectAll.Text = "Select All";
			this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
			// 
			// cmdSelectNone
			// 
			this.cmdSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSelectNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdSelectNone.Location = new System.Drawing.Point(8, 40);
			this.cmdSelectNone.Name = "cmdSelectNone";
			this.cmdSelectNone.Size = new System.Drawing.Size(91, 23);
			this.cmdSelectNone.TabIndex = 0;
			this.cmdSelectNone.Text = "Select None";
			this.cmdSelectNone.Click += new System.EventHandler(this.cmdSelectNone_Click);
			// 
			// cmdInvertSelection
			// 
			this.cmdInvertSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmdInvertSelection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdInvertSelection.Location = new System.Drawing.Point(8, 72);
			this.cmdInvertSelection.Name = "cmdInvertSelection";
			this.cmdInvertSelection.Size = new System.Drawing.Size(91, 23);
			this.cmdInvertSelection.TabIndex = 0;
			this.cmdInvertSelection.Text = "Invert Selection";
			this.cmdInvertSelection.Click += new System.EventHandler(this.cmdInvertSelection_Click);
			// 
			// grpOutputGeneration
			// 
			this.grpOutputGeneration.Controls.Add(this.optMultiOutput);
			this.grpOutputGeneration.Controls.Add(this.optSingleOutput);
			this.grpOutputGeneration.Controls.Add(this.chkOverwrite);
			this.grpOutputGeneration.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpOutputGeneration.Location = new System.Drawing.Point(0, 0);
			this.grpOutputGeneration.Name = "grpOutputGeneration";
			this.grpOutputGeneration.Size = new System.Drawing.Size(328, 112);
			this.grpOutputGeneration.TabIndex = 0;
			this.grpOutputGeneration.TabStop = false;
			this.grpOutputGeneration.Text = "Ouput generation";
			// 
			// optMultiOutput
			// 
			this.optMultiOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optMultiOutput.Location = new System.Drawing.Point(16, 48);
			this.optMultiOutput.Name = "optMultiOutput";
			this.optMultiOutput.Size = new System.Drawing.Size(280, 24);
			this.optMultiOutput.TabIndex = 11;
			this.optMultiOutput.Text = "Multiple output files  (one output per metadata file)";
			this.optMultiOutput.CheckedChanged += new System.EventHandler(this.optMultiOutput_CheckedChanged);
			// 
			// optSingleOutput
			// 
			this.optSingleOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.optSingleOutput.Location = new System.Drawing.Point(16, 24);
			this.optSingleOutput.Name = "optSingleOutput";
			this.optSingleOutput.Size = new System.Drawing.Size(112, 24);
			this.optSingleOutput.TabIndex = 9;
			this.optSingleOutput.Text = "Single output file";
			this.optSingleOutput.CheckedChanged += new System.EventHandler(this.optSingleOutput_CheckedChanged);
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.panelBottomRight);
			this.panelBottom.Controls.Add(this.splitterBottom);
			this.panelBottom.Controls.Add(this.panelBottomLeft);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBottom.Location = new System.Drawing.Point(8, 104);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(632, 344);
			this.panelBottom.TabIndex = 9;
			// 
			// CodeGeneratorCommandEditor
			// 
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelTop);
			this.DockPadding.All = 8;
			this.Name = "CodeGeneratorCommandEditor";
			this.Size = new System.Drawing.Size(648, 456);
			this.Load += new System.EventHandler(this.CodeGeneratorCommandEditorLoad);
			this.panelBottomLeft.ResumeLayout(false);
			this.panelTopLeft.ResumeLayout(false);
			this.panelTop.ResumeLayout(false);
			this.panelTopRight.ResumeLayout(false);
			this.panelBottomRight.ResumeLayout(false);
			this.grpMultiOutput.ResumeLayout(false);
			this.panelMetadataFileSelectionButtons.ResumeLayout(false);
			this.grpOutputGeneration.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);

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
			foreach(string engine in TransformationEngineFactory.GetConfiguredEngineNames())
			{
				cboEngine.Items.Add(engine);
			}

			cboPackage.Items.Clear();
			cboPackage.Items.Add(_generatorCommand.Package);

			cboTemplate.Items.Clear();
			cboTemplate.Items.Add(_generatorCommand.Template);

			cboPackage.DataBindings.Clear();
			cboTemplate.DataBindings.Clear();
			txtOutputPath.DataBindings.Clear();
			chkOverwrite.DataBindings.Clear();
			cboEngine.DataBindings.Clear();
			chkAutoSelectMetadataFiles.DataBindings.Clear();

			cboPackage.DataBindings.Add("Text", _generatorCommand, "Package");
			cboTemplate.DataBindings.Add("Text", _generatorCommand, "Template");
			txtOutputPath.DataBindings.Add("Text", _generatorCommand, "OutputPath");
			chkOverwrite.DataBindings.Add("Checked", _generatorCommand, "Overwrite");
			cboEngine.DataBindings.Add("Text", _generatorCommand, "Engine");

			tvwIndividualMetadataFiles.Bind(_generatorCommand.Project.MetadataFiles, _generatorCommand.SelectedMetadataFiles);
			optSingleOutput.Checked = (_generatorCommand.CodeGenerationMode == CodeGenerationMode.SingleOutput);
			optMultiOutput.Checked = (_generatorCommand.CodeGenerationMode == CodeGenerationMode.MultipleOutput);
//			tvwGroupedMetadataFiles.Bind(_generatorCommand.Project.MetadataFiles, _generatorCommand.GroupedMetadataFiles);
			chkAutoSelectMetadataFiles.DataBindings.Add("Checked", _generatorCommand, "AutoSelectMetadataFiles");

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
//			lblSingleOutputAdvice.Visible = true;
//			panelMetadataFileSelectionButtons.Visible = false;
//			tvwIndividualMetadataFiles.Visible = false;
		}

		private void optMultiOutput_CheckedChanged(object sender, EventArgs e) {
			_generatorCommand.CodeGenerationMode = CodeGenerationMode.MultipleOutput;
//			lblSingleOutputAdvice.Visible = false;
//			panelMetadataFileSelectionButtons.Visible = true;
//			tvwIndividualMetadataFiles.Visible = true;
		}

		private void cboPackage_DropDown(object sender, EventArgs e) {
			
			ArrayList packages = Package.ListPackages(
				_generatorCommand.Project.GetFullTemplatePath());

			if (packages != null && packages.Count > 0) {
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