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

	public class ORLinkEditor : System.Windows.Forms.UserControl, IObjectEditor {
			
		private Link _link;

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
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabLinkFields;
		private System.Windows.Forms.Button btnRemoveLinkField;
		private System.Windows.Forms.Button btnAddLinkField;
		private System.Windows.Forms.DataGrid gridLinkFields;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkCascadeDelete;
		private System.Windows.Forms.CheckBox chkCascadeUpdate;
		private System.Windows.Forms.CheckBox chkIsCollection;
		private System.Windows.Forms.CheckBox chkIsProperty;
		private System.Windows.Forms.CheckBox chkHidden;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.ComboBox cboTargetEntity;
		private System.Windows.Forms.ComboBox cboTargetIndex;
		private System.Windows.Forms.ErrorProvider errorProvider;

		public ORLinkEditor(Link link) : this() {
			_link = link;
		}

		public ORLinkEditor() {
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
			this.chkHidden = new System.Windows.Forms.CheckBox();
			this.chkReadOnly = new System.Windows.Forms.CheckBox();
			this.chkIsProperty = new System.Windows.Forms.CheckBox();
			this.chkIsCollection = new System.Windows.Forms.CheckBox();
			this.chkCascadeDelete = new System.Windows.Forms.CheckBox();
			this.chkCascadeUpdate = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabLinkFields = new System.Windows.Forms.TabPage();
			this.btnRemoveLinkField = new System.Windows.Forms.Button();
			this.btnAddLinkField = new System.Windows.Forms.Button();
			this.gridLinkFields = new System.Windows.Forms.DataGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cboTargetEntity = new System.Windows.Forms.ComboBox();
			this.cboTargetIndex = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabLinkFields.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridLinkFields)).BeginInit();
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
			this.txtDBName.Location = new System.Drawing.Point(320, 8);
			this.txtDBName.Name = "txtDBName";
			this.txtDBName.Size = new System.Drawing.Size(196, 21);
			this.txtDBName.TabIndex = 14;
			this.txtDBName.Text = "textBox1";
			this.txtDBName.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.chkHidden);
			this.groupBox1.Controls.Add(this.chkReadOnly);
			this.groupBox1.Controls.Add(this.chkIsProperty);
			this.groupBox1.Controls.Add(this.chkIsCollection);
			this.groupBox1.Controls.Add(this.chkCascadeDelete);
			this.groupBox1.Controls.Add(this.chkCascadeUpdate);
			this.groupBox1.Location = new System.Drawing.Point(4, 60);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(512, 64);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Attributes";
			// 
			// chkHidden
			// 
			this.chkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkHidden.Location = new System.Drawing.Point(240, 16);
			this.chkHidden.Name = "chkHidden";
			this.chkHidden.TabIndex = 2;
			this.chkHidden.Text = "Hidden";
			// 
			// chkReadOnly
			// 
			this.chkReadOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkReadOnly.Location = new System.Drawing.Point(240, 36);
			this.chkReadOnly.Name = "chkReadOnly";
			this.chkReadOnly.TabIndex = 5;
			this.chkReadOnly.Text = "Read-Only";
			// 
			// chkIsProperty
			// 
			this.chkIsProperty.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsProperty.Location = new System.Drawing.Point(124, 36);
			this.chkIsProperty.Name = "chkIsProperty";
			this.chkIsProperty.TabIndex = 4;
			this.chkIsProperty.Text = "Is Property";
			// 
			// chkIsCollection
			// 
			this.chkIsCollection.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkIsCollection.Location = new System.Drawing.Point(12, 36);
			this.chkIsCollection.Name = "chkIsCollection";
			this.chkIsCollection.TabIndex = 3;
			this.chkIsCollection.Text = "Is Collection";
			// 
			// chkCascadeDelete
			// 
			this.chkCascadeDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkCascadeDelete.Location = new System.Drawing.Point(124, 16);
			this.chkCascadeDelete.Name = "chkCascadeDelete";
			this.chkCascadeDelete.TabIndex = 1;
			this.chkCascadeDelete.Text = "Cascade Delete";
			// 
			// chkCascadeUpdate
			// 
			this.chkCascadeUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkCascadeUpdate.Location = new System.Drawing.Point(12, 16);
			this.chkCascadeUpdate.Name = "chkCascadeUpdate";
			this.chkCascadeUpdate.TabIndex = 0;
			this.chkCascadeUpdate.Text = "Cascade Update";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.Location = new System.Drawing.Point(72, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(168, 21);
			this.txtName.TabIndex = 20;
			this.txtName.Text = "textBox6";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(4, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 19;
			this.label6.Text = "Link Name";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(248, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 13;
			this.label1.Text = "Link Name in DB ";
			this.label1.Visible = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabLinkFields);
			this.tabControl1.Location = new System.Drawing.Point(4, 128);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(512, 236);
			this.tabControl1.TabIndex = 26;
			// 
			// tabLinkFields
			// 
			this.tabLinkFields.Controls.Add(this.btnRemoveLinkField);
			this.tabLinkFields.Controls.Add(this.btnAddLinkField);
			this.tabLinkFields.Controls.Add(this.gridLinkFields);
			this.tabLinkFields.Location = new System.Drawing.Point(4, 22);
			this.tabLinkFields.Name = "tabLinkFields";
			this.tabLinkFields.Size = new System.Drawing.Size(504, 210);
			this.tabLinkFields.TabIndex = 4;
			this.tabLinkFields.Text = "Link Fields";
			// 
			// btnRemoveLinkField
			// 
			this.btnRemoveLinkField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemoveLinkField.Enabled = false;
			this.btnRemoveLinkField.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemoveLinkField.Location = new System.Drawing.Point(92, 180);
			this.btnRemoveLinkField.Name = "btnRemoveLinkField";
			this.btnRemoveLinkField.Size = new System.Drawing.Size(84, 24);
			this.btnRemoveLinkField.TabIndex = 4;
			this.btnRemoveLinkField.Text = "Remove Field";
			this.btnRemoveLinkField.Click += new System.EventHandler(this.btnRemoveLinkField_Click);
			// 
			// btnAddLinkField
			// 
			this.btnAddLinkField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddLinkField.Enabled = false;
			this.btnAddLinkField.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddLinkField.Location = new System.Drawing.Point(4, 180);
			this.btnAddLinkField.Name = "btnAddLinkField";
			this.btnAddLinkField.Size = new System.Drawing.Size(84, 24);
			this.btnAddLinkField.TabIndex = 3;
			this.btnAddLinkField.Text = "Add Field";
			this.btnAddLinkField.Click += new System.EventHandler(this.btnAddLinkField_Click);
			// 
			// gridLinkFields
			// 
			this.gridLinkFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridLinkFields.CaptionVisible = false;
			this.gridLinkFields.DataMember = "";
			this.gridLinkFields.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gridLinkFields.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridLinkFields.Location = new System.Drawing.Point(4, 4);
			this.gridLinkFields.Name = "gridLinkFields";
			this.gridLinkFields.Size = new System.Drawing.Size(496, 172);
			this.gridLinkFields.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 27;
			this.label2.Text = "Target Entity";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(248, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 29;
			this.label3.Text = "Target Index";
			// 
			// cboTargetEntity
			// 
			this.cboTargetEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetEntity.Location = new System.Drawing.Point(72, 36);
			this.cboTargetEntity.Name = "cboTargetEntity";
			this.cboTargetEntity.Size = new System.Drawing.Size(168, 21);
			this.cboTargetEntity.TabIndex = 31;
			this.cboTargetEntity.TextChanged += new System.EventHandler(this.cboTargetEntity_TextChanged);
			this.cboTargetEntity.Leave += new System.EventHandler(this.cboTargetEntity_Leave);
			this.cboTargetEntity.SelectionChangeCommitted += new System.EventHandler(this.cboTargetEntity_SelectionChangeCommitted);
			// 
			// cboTargetIndex
			// 
			this.cboTargetIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTargetIndex.Location = new System.Drawing.Point(320, 36);
			this.cboTargetIndex.Name = "cboTargetIndex";
			this.cboTargetIndex.Size = new System.Drawing.Size(196, 21);
			this.cboTargetIndex.TabIndex = 32;
			this.cboTargetIndex.SelectionChangeCommitted += new System.EventHandler(this.cboTargetIndex_SelectionChangeCommitted);
			// 
			// ORLinkEditor
			// 
			this.Controls.Add(this.cboTargetIndex);
			this.Controls.Add(this.cboTargetEntity);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.txtDBName);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ORLinkEditor";
			this.Size = new System.Drawing.Size(520, 368);
			this.Load += new System.EventHandler(this.ORIndexEditor_Load);
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabLinkFields.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridLinkFields)).EndInit();
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
			chkCascadeUpdate.DataBindings.Clear();
			chkCascadeDelete.DataBindings.Clear();
			chkHidden.DataBindings.Clear();
			chkIsCollection.DataBindings.Clear();
			chkIsProperty.DataBindings.Clear();
			chkReadOnly.DataBindings.Clear();
			cboTargetEntity.DataBindings.Clear();
			cboTargetIndex.DataBindings.Clear();

			txtName.DataBindings.Add("Text", _link, "Name");
			//txtDBName.DataBindings.Add("Text", _link, "DBName");
			cboTargetEntity.DataBindings.Add("Text", _link, "TargetEntityName");
			cboTargetIndex.DataBindings.Add("Text", _link, "TargetIndexName");
			chkCascadeUpdate.DataBindings.Add("Checked", _link, "CascadeUpdate");
			chkCascadeDelete.DataBindings.Add("Checked", _link, "CascadeDelete");
			chkHidden.DataBindings.Add("Checked", _link, "Hidden");
			chkIsCollection.DataBindings.Add("Checked", _link, "IsCollection");
			chkIsProperty.DataBindings.Add("Checked", _link, "IsProperty");
			chkReadOnly.DataBindings.Add("Checked", _link, "ReadOnly");
			
			RefreshLinkFieldsList();
			RefreshComboTargetEntities();

			this.Enabled = true;
		}
		#endregion DoBinding
		
		public void RefreshContent()
		{
			this.SelectedObject = this.SelectedObject;
		}
		
		#region SelectedObject
		public object SelectedObject 
		{
			get {
				return _link;
			}
			set {
				_link = (Link)value;
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
			if (_link == null) {
				return "Link: <no link selected>";
			}
			else {
				return "Entity: " + _link.Entity.Name + " | Link: " + _link.Name;
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
		private void RefreshLinkFieldsList() {
			ArrayList fields = new ArrayList();
			foreach (LinkField field in _link.Fields) {
				fields.Add(field);
			}
			DataGridHelper.SetGridDataSource(gridLinkFields, fields);

			DataGridComboBoxColumn source = (DataGridComboBoxColumn)DataGridHelper.AddDataGridColumn(gridLinkFields, "SourceFieldName", "Source Field (From Entity '" + _link.Entity.Name + "')", typeof(DataGridComboBoxColumn));
			source.ComboBox.DataSource = _link.Entity.Fields;
			source.ComboBox.DisplayMember = "Name";
			source.ComboBox.ValueMember = "Name";
			source.ComboBox.DropDownStyle = ComboBoxStyle.DropDown;
			source.Width = 200;
			DataGridColumnStyle target = DataGridHelper.AddDataGridColumn(gridLinkFields, "TargetFieldName", "Target Field (To Entity '" + cboTargetEntity.Text + "')");
            target.Width = 200;
			target.ReadOnly = true;
		}
		#endregion Refresh Grids
		
		#region Events
		private void ORIndexEditor_Load(object sender, System.EventArgs e) {
			if (_link != null) {
				DoBinding();
			}
		}

		private void cboTargetEntity_SelectionChangeCommitted(object sender, System.EventArgs e) {
			cboTargetEntity.Text = cboTargetEntity.SelectedItem.ToString();
			RefreshComboTargetIndexes();
		}

		private void cboTargetEntity_Leave(object sender, System.EventArgs e) {
			RefreshComboTargetIndexes();
		}

		private void btnAddLinkField_Click(object sender, System.EventArgs e) {
			_link.Fields.Add(new LinkField());
			RefreshLinkFieldsList();
		}

		private void btnRemoveLinkField_Click(object sender, System.EventArgs e) {
			LinkField field = GetSelectedField();
			if (field != null) {
				_link.Fields.Remove(field);
			}
			RefreshLinkFieldsList();
		}

		private void cboTargetIndex_SelectionChangeCommitted(object sender, System.EventArgs e) {
			cboTargetIndex.Text = cboTargetIndex.SelectedItem.ToString();
			Index index = GetSelectedIndex();
			if (index != null) {
				_link.Fields.Clear();
				foreach (IndexField indexField in index.Fields) {
					LinkField linkField = new LinkField();
					linkField.TargetFieldName = indexField.Name;
					_link.Fields.Add(linkField);
				}
				RefreshLinkFieldsList();
			}
		}

		private void cboTargetEntity_TextChanged(object sender, System.EventArgs e) {
			RefreshLinkFieldsList();
		}
		#endregion Events

		#region Refresh Combos
		private void RefreshComboTargetEntities() {
			cboTargetEntity.DataSource = null;
			cboTargetIndex.DataSource = null;
			Project project = null;
			try {
				project = _link.Entity.OwningMetadataFile.Project;
			}
			catch {
				return;
			}
			ArrayList list = new ArrayList();
			foreach (MetadataFile file in project.MetadataFiles) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if ((entity as Entity) != null) {
						list.Add(entity);
					}
				}
			}
			cboTargetEntity.DataSource = list;
			cboTargetEntity.Text = _link.TargetEntityName;
			RefreshComboTargetIndexes();
		}

		private void RefreshComboTargetIndexes() {
			cboTargetIndex.DataSource = null;
			Entity selectedEntity = GetSelectedTargetEntity();
			if (selectedEntity != null) {
				ArrayList list = new ArrayList();
				foreach (Index index in selectedEntity.Indexes) {
					list.Add(index);
				}
				cboTargetIndex.DataSource = list;
				cboTargetIndex.Text = _link.TargetIndexName;
			}
		}
		#endregion Refresh Combos

		private Entity GetSelectedTargetEntity() {
			Project project = null;
			try {
				project = _link.Entity.OwningMetadataFile.Project;
				if (project == null || cboTargetEntity.Text == string.Empty) {
					return null;
				}
			}
			catch {
				return null;
			}
			foreach (MetadataFile file in project.MetadataFiles) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if ((entity as Entity) != null && ((Entity)entity).Name == cboTargetEntity.Text) {
						return (Entity)entity;
					}
				}
			}
			return null;
		}
		
		private Index GetSelectedIndex() {
			Entity entity = GetSelectedTargetEntity();
			if (entity == null) {
				return null;
			}
			foreach (Index index in entity.Indexes) {
				if (index.Name == cboTargetIndex.Text) {
					return index;
				}
			}
			return null;
		}

		private LinkField GetSelectedField() {
			CurrencyManager cm = (CurrencyManager)gridLinkFields.BindingContext[gridLinkFields.DataSource];
			if (cm.Count > 0) {
				return (LinkField)cm.Current;
			}
			else {
				return null;
			}
		}

	}
}