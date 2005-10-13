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
 *   User: Jacob Eggleston
 *   Date: 2005/7/29
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Gui.Widgets;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Gui.DocumentHandling {

	public class ORIndexEditor : System.Windows.Forms.UserControl, IObjectEditor {
			
		private Index _index;

		#region Windows Form Designer generated code


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDBName;
		private System.Windows.Forms.CheckBox chkPrimaryKey;
		private System.Windows.Forms.CheckBox chkDeleteBy;
		private System.Windows.Forms.CheckBox chkSelectBy;
		private System.Windows.Forms.CheckBox chkUnique;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabIndexFields;
		private System.Windows.Forms.TabPage tabOrderByFields;
		private System.Windows.Forms.TabPage tabSelectText;
		private System.Windows.Forms.TextBox txtSelectText;
		private System.Windows.Forms.TabPage tabDeleteText;
		private System.Windows.Forms.TextBox txtDeleteText;
		private System.Windows.Forms.DataGrid gridIndexFields;
		private System.Windows.Forms.Button btnAddIndexField;
		private System.Windows.Forms.Button btnRemoveIndexField;
		private System.Windows.Forms.DataGrid gridOrderFields;
		private System.Windows.Forms.Button btnRemoveOrderField;
		private System.Windows.Forms.Button btnAddOrderList;
		private System.Windows.Forms.ErrorProvider errorProvider;

		public ORIndexEditor(Index index) : this() {
			_index = index;
		}

		public ORIndexEditor() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Enabled = false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.txtDBName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkUnique = new System.Windows.Forms.CheckBox();
			this.chkPrimaryKey = new System.Windows.Forms.CheckBox();
			this.chkDeleteBy = new System.Windows.Forms.CheckBox();
			this.chkSelectBy = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabIndexFields = new System.Windows.Forms.TabPage();
			this.btnRemoveIndexField = new System.Windows.Forms.Button();
			this.btnAddIndexField = new System.Windows.Forms.Button();
			this.gridIndexFields = new System.Windows.Forms.DataGrid();
			this.tabOrderByFields = new System.Windows.Forms.TabPage();
			this.btnRemoveOrderField = new System.Windows.Forms.Button();
			this.btnAddOrderList = new System.Windows.Forms.Button();
			this.gridOrderFields = new System.Windows.Forms.DataGrid();
			this.tabSelectText = new System.Windows.Forms.TabPage();
			this.txtSelectText = new System.Windows.Forms.TextBox();
			this.tabDeleteText = new System.Windows.Forms.TabPage();
			this.txtDeleteText = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabIndexFields.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridIndexFields)).BeginInit();
			this.tabOrderByFields.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridOrderFields)).BeginInit();
			this.tabSelectText.SuspendLayout();
			this.tabDeleteText.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// txtDBName
			// 
			this.txtDBName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDBName.Enabled = false;
			this.txtDBName.Location = new System.Drawing.Point(384, 8);
			this.txtDBName.Name = "txtDBName";
			this.txtDBName.Size = new System.Drawing.Size(144, 21);
			this.txtDBName.TabIndex = 14;
			this.txtDBName.Text = "textBox1";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chkUnique);
			this.groupBox1.Controls.Add(this.chkPrimaryKey);
			this.groupBox1.Controls.Add(this.chkDeleteBy);
			this.groupBox1.Controls.Add(this.chkSelectBy);
			this.groupBox1.Location = new System.Drawing.Point(4, 36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(524, 64);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Attributes";
			// 
			// chkUnique
			// 
			this.chkUnique.Location = new System.Drawing.Point(100, 16);
			this.chkUnique.Name = "chkUnique";
			this.chkUnique.TabIndex = 1;
			this.chkUnique.Text = "Unique";
			// 
			// chkPrimaryKey
			// 
			this.chkPrimaryKey.Location = new System.Drawing.Point(12, 16);
			this.chkPrimaryKey.Name = "chkPrimaryKey";
			this.chkPrimaryKey.TabIndex = 0;
			this.chkPrimaryKey.Text = "Primary Key";
			// 
			// chkDeleteBy
			// 
			this.chkDeleteBy.Location = new System.Drawing.Point(100, 36);
			this.chkDeleteBy.Name = "chkDeleteBy";
			this.chkDeleteBy.TabIndex = 3;
			this.chkDeleteBy.Text = "Delete By";
			// 
			// chkSelectBy
			// 
			this.chkSelectBy.Location = new System.Drawing.Point(12, 36);
			this.chkSelectBy.Name = "chkSelectBy";
			this.chkSelectBy.TabIndex = 2;
			this.chkSelectBy.Text = "Select By";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(68, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(216, 21);
			this.txtName.TabIndex = 20;
			this.txtName.Text = "textBox6";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 19;
			this.label6.Text = "Index Name";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(292, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Index Name in DB ";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabIndexFields);
			this.tabControl1.Controls.Add(this.tabOrderByFields);
			this.tabControl1.Controls.Add(this.tabSelectText);
			this.tabControl1.Controls.Add(this.tabDeleteText);
			this.tabControl1.Location = new System.Drawing.Point(4, 104);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(524, 256);
			this.tabControl1.TabIndex = 26;
			// 
			// tabIndexFields
			// 
			this.tabIndexFields.Controls.Add(this.btnRemoveIndexField);
			this.tabIndexFields.Controls.Add(this.btnAddIndexField);
			this.tabIndexFields.Controls.Add(this.gridIndexFields);
			this.tabIndexFields.Location = new System.Drawing.Point(4, 22);
			this.tabIndexFields.Name = "tabIndexFields";
			this.tabIndexFields.Size = new System.Drawing.Size(516, 230);
			this.tabIndexFields.TabIndex = 4;
			this.tabIndexFields.Text = "Index Fields";
			// 
			// btnRemoveIndexField
			// 
			this.btnRemoveIndexField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemoveIndexField.Location = new System.Drawing.Point(92, 200);
			this.btnRemoveIndexField.Name = "btnRemoveIndexField";
			this.btnRemoveIndexField.Size = new System.Drawing.Size(84, 24);
			this.btnRemoveIndexField.TabIndex = 4;
			this.btnRemoveIndexField.Text = "Remove Field";
			this.btnRemoveIndexField.Click += new System.EventHandler(this.btnRemoveIndexField_Click);
			// 
			// btnAddIndexField
			// 
			this.btnAddIndexField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddIndexField.Location = new System.Drawing.Point(4, 200);
			this.btnAddIndexField.Name = "btnAddIndexField";
			this.btnAddIndexField.Size = new System.Drawing.Size(84, 24);
			this.btnAddIndexField.TabIndex = 3;
			this.btnAddIndexField.Text = "Add Field";
			this.btnAddIndexField.Click += new System.EventHandler(this.btnAddIndexField_Click);
			// 
			// gridIndexFields
			// 
			this.gridIndexFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridIndexFields.CaptionVisible = false;
			this.gridIndexFields.DataMember = "";
			this.gridIndexFields.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gridIndexFields.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridIndexFields.Location = new System.Drawing.Point(4, 4);
			this.gridIndexFields.Name = "gridIndexFields";
			this.gridIndexFields.Size = new System.Drawing.Size(508, 192);
			this.gridIndexFields.TabIndex = 1;
			// 
			// tabOrderByFields
			// 
			this.tabOrderByFields.Controls.Add(this.btnRemoveOrderField);
			this.tabOrderByFields.Controls.Add(this.btnAddOrderList);
			this.tabOrderByFields.Controls.Add(this.gridOrderFields);
			this.tabOrderByFields.Location = new System.Drawing.Point(4, 22);
			this.tabOrderByFields.Name = "tabOrderByFields";
			this.tabOrderByFields.Size = new System.Drawing.Size(516, 230);
			this.tabOrderByFields.TabIndex = 0;
			this.tabOrderByFields.Text = "Order By Fields";
			this.tabOrderByFields.Visible = false;
			// 
			// btnRemoveOrderField
			// 
			this.btnRemoveOrderField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemoveOrderField.Location = new System.Drawing.Point(92, 200);
			this.btnRemoveOrderField.Name = "btnRemoveOrderField";
			this.btnRemoveOrderField.Size = new System.Drawing.Size(84, 24);
			this.btnRemoveOrderField.TabIndex = 7;
			this.btnRemoveOrderField.Text = "Remove Field";
			this.btnRemoveOrderField.Click += new System.EventHandler(this.btnRemoveOrderField_Click);
			// 
			// btnAddOrderList
			// 
			this.btnAddOrderList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddOrderList.Location = new System.Drawing.Point(4, 200);
			this.btnAddOrderList.Name = "btnAddOrderList";
			this.btnAddOrderList.Size = new System.Drawing.Size(84, 24);
			this.btnAddOrderList.TabIndex = 6;
			this.btnAddOrderList.Text = "Add Field";
			this.btnAddOrderList.Click += new System.EventHandler(this.btnAddOrderList_Click);
			// 
			// gridOrderFields
			// 
			this.gridOrderFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridOrderFields.CaptionVisible = false;
			this.gridOrderFields.DataMember = "";
			this.gridOrderFields.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gridOrderFields.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridOrderFields.Location = new System.Drawing.Point(4, 4);
			this.gridOrderFields.Name = "gridOrderFields";
			this.gridOrderFields.Size = new System.Drawing.Size(508, 192);
			this.gridOrderFields.TabIndex = 5;
			// 
			// tabSelectText
			// 
			this.tabSelectText.Controls.Add(this.txtSelectText);
			this.tabSelectText.Location = new System.Drawing.Point(4, 22);
			this.tabSelectText.Name = "tabSelectText";
			this.tabSelectText.Size = new System.Drawing.Size(516, 230);
			this.tabSelectText.TabIndex = 2;
			this.tabSelectText.Text = "Select Text";
			this.tabSelectText.Visible = false;
			// 
			// txtSelectText
			// 
			this.txtSelectText.AcceptsReturn = true;
			this.txtSelectText.AcceptsTab = true;
			this.txtSelectText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSelectText.AutoSize = false;
			this.txtSelectText.HideSelection = false;
			this.txtSelectText.Location = new System.Drawing.Point(4, 4);
			this.txtSelectText.Multiline = true;
			this.txtSelectText.Name = "txtSelectText";
			this.txtSelectText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSelectText.Size = new System.Drawing.Size(508, 220);
			this.txtSelectText.TabIndex = 0;
			this.txtSelectText.Text = "";
			this.txtSelectText.WordWrap = false;
			// 
			// tabDeleteText
			// 
			this.tabDeleteText.Controls.Add(this.txtDeleteText);
			this.tabDeleteText.Location = new System.Drawing.Point(4, 22);
			this.tabDeleteText.Name = "tabDeleteText";
			this.tabDeleteText.Size = new System.Drawing.Size(516, 230);
			this.tabDeleteText.TabIndex = 3;
			this.tabDeleteText.Text = "Delete Text";
			this.tabDeleteText.Visible = false;
			// 
			// txtDeleteText
			// 
			this.txtDeleteText.AcceptsReturn = true;
			this.txtDeleteText.AcceptsTab = true;
			this.txtDeleteText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDeleteText.AutoSize = false;
			this.txtDeleteText.HideSelection = false;
			this.txtDeleteText.Location = new System.Drawing.Point(4, 4);
			this.txtDeleteText.Multiline = true;
			this.txtDeleteText.Name = "txtDeleteText";
			this.txtDeleteText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDeleteText.Size = new System.Drawing.Size(508, 220);
			this.txtDeleteText.TabIndex = 0;
			this.txtDeleteText.Text = "";
			this.txtDeleteText.WordWrap = false;
			// 
			// ORIndexEditor
			// 
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.txtDBName);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ORIndexEditor";
			this.Size = new System.Drawing.Size(532, 364);
			this.Load += new System.EventHandler(this.ORIndexEditor_Load);
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabIndexFields.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridIndexFields)).EndInit();
			this.tabOrderByFields.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridOrderFields)).EndInit();
			this.tabSelectText.ResumeLayout(false);
			this.tabDeleteText.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region IObjectEditor implementation
		
		public event EventHandler SelectedObjectChanged;
		protected virtual void OnSelectedObjectChanged() {
			if (this.SelectedObjectChanged != null) {
				SelectedObjectChanged(this.SelectedObject, new EventArgs());
			}
		}

		#region DoBinding
		private void DoBinding() {
			txtName.DataBindings.Clear();
			txtDBName.DataBindings.Clear();
			chkPrimaryKey.DataBindings.Clear();
			chkUnique.DataBindings.Clear();
			chkSelectBy.DataBindings.Clear();
			chkDeleteBy.DataBindings.Clear();
			txtSelectText.DataBindings.Clear();
			txtDeleteText.DataBindings.Clear();

			txtName.DataBindings.Add("Text", _index, "Name");
			txtDBName.DataBindings.Add("Text", _index, "DBName");
			chkPrimaryKey.DataBindings.Add("Checked", _index, "PrimaryKey");
			chkUnique.DataBindings.Add("Checked", _index, "Unique");
			chkSelectBy.DataBindings.Add("Checked", _index, "SelectBy");
			chkDeleteBy.DataBindings.Add("Checked", _index, "DeleteBy");
			txtSelectText.DataBindings.Add("Text", _index, "SelectText");
			txtDeleteText.DataBindings.Add("Text", _index, "DeleteText");
			
			RefreshIndexFieldsList();
			RefreshOrderFieldsList();
			
			if (_index.DBName != string.Empty) {
				this.btnAddIndexField.Enabled = false;
				this.btnRemoveIndexField.Enabled = false;
			}

			this.Enabled = true;
		}
		#endregion DoBinding
		
		#region SelectedObject
		public object SelectedObject {
			get {
				return _index;
			}
			set {
				_index = (Index)value;
				DoBinding();
			}
		}
		#endregion SelectedObject
		
		#region IsDirty
		public bool IsDirty {
			get {
				return false;
			}
		}
		#endregion IsDirty
		
		#region Icon
		public Icon Icon {
			get {
				return Images.CreateIcon(Images.Details);
			}
		}
		#endregion Icon
		
		#region ToString
		public override string ToString() {
			if (_index == null) {
				return "Index: <no index selected>";
			}
			else {
				return "Entity: " + _index.Entity.Name + " | Index: " + _index.Name;
			}
		}
		#endregion ToString
		
		#region CommitChanges
		public bool CommitChanges() {
			return true;
		}
		#endregion CommitChanges
		
		#region QueryUnload
		public void QueryUnload(out bool cancel) {
			if (!this.Validate()) {
				DialogResult res = MessageBox.Show("There are one or more errors with the data on this form. Would you like to close it anyway?", "Validation Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Yes) {
					cancel = false;
				}
				else {
					cancel = true;
				}
			}
			else {
				CommitChanges();
				cancel = false;
			}
		}
		#endregion QueryUnload

		#endregion IObjectEditor implementation

		#region Refresh Grids
		private void RefreshIndexFieldsList() {
			ArrayList fields = new ArrayList();
			foreach (IndexField field in _index.Fields) {
				fields.Add(field);
			}
			DataGridHelper.SetGridDataSource(gridIndexFields, fields);

			DataGridComboBoxColumn name = (DataGridComboBoxColumn)DataGridHelper.AddDataGridColumn(gridIndexFields, "Name", "Entity Field Name", typeof(DataGridComboBoxColumn));
			name.ComboBox.DataSource = _index.Entity.Fields;
			name.ComboBox.DisplayMember = "Name";
			name.ComboBox.ValueMember = "Name";
			name.Width = 150;
			name.ReadOnly = (_index.DBName != string.Empty);
			DataGridHelper.AddDataGridColumn(gridIndexFields, "ParameterName", "Parameter Name")
				.Width = 150;
			DataGridHelper.AddDataGridColumn(gridIndexFields, "PartialTextMatch", "Partial Text Match")
				.Width = 100;
		}

		private void RefreshOrderFieldsList() {
			ArrayList fields = new ArrayList();
			foreach (IndexOrderField field in _index.OrderByFields) {
				fields.Add(field);
			}
			DataGridHelper.SetGridDataSource(gridOrderFields, fields);

			DataGridComboBoxColumn name = (DataGridComboBoxColumn)DataGridHelper.AddDataGridColumn(gridOrderFields, "Name", "Entity Field Name", typeof(DataGridComboBoxColumn));
			name.ComboBox.DataSource = _index.Entity.Fields;
			name.ComboBox.DisplayMember = "Name";
			name.ComboBox.ValueMember = "Name";
			name.Width = 200;
		}
		#endregion Refresh Grids
		
		#region Events
		private void ORIndexEditor_Load(object sender, System.EventArgs e) {
			if (_index != null) {
				DoBinding();
			}
		}

		private void btnAddIndexField_Click(object sender, System.EventArgs e) {
			_index.Fields.Add(new IndexField());
			RefreshIndexFieldsList();
		}

		private void btnRemoveIndexField_Click(object sender, System.EventArgs e) {
			IndexField field = GetSelectedIndexField();
			if (field != null) {
				_index.Fields.Remove(field);
			}
			RefreshIndexFieldsList();
		}

		private void btnAddOrderList_Click(object sender, System.EventArgs e) {
			_index.OrderByFields.Add(new IndexOrderField());
			RefreshOrderFieldsList();
		}

		private void btnRemoveOrderField_Click(object sender, System.EventArgs e) {
			IndexOrderField field = GetSelectedOrderField();
			if (field != null) {
				_index.OrderByFields.Remove(field);
			}
			RefreshOrderFieldsList();
		}
		#endregion Events

		private IndexField GetSelectedIndexField() {
			CurrencyManager cm = (CurrencyManager)gridIndexFields.BindingContext[gridIndexFields.DataSource];
			if (cm.Count > 0) {
				return (IndexField)cm.Current;
			}
			else {
				return null;
			}
		}

		private IndexOrderField GetSelectedOrderField() {
			CurrencyManager cm = (CurrencyManager)gridOrderFields.BindingContext[gridOrderFields.DataSource];
			if (cm.Count > 0) {
				return (IndexOrderField)cm.Current;
			}
			else {
				return null;
			}
		}


	}
}