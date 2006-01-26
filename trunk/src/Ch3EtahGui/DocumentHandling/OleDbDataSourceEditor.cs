using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using Adapdev.Data;
using Adapdev.Data.Schema;

using Ch3Etah.Core.Config;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Design.CustomUI;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Gui.DocumentHandling {
	/// <summary>
	/// Summary description for OleDbDataSourceEditor.
	/// </summary>
	public class OleDbDataSourceEditor : UserControl, IObjectEditor {

		#region Windows Form Designer generated code

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

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
			this.cmdBuild = new System.Windows.Forms.Button();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.grpConnectionString = new System.Windows.Forms.GroupBox();
			this.cmdTest = new System.Windows.Forms.Button();
			this.btnAddEntities = new System.Windows.Forms.Button();
			this.btnRefreshEntityList = new System.Windows.Forms.Button();
			this.tvwEntities = new System.Windows.Forms.TreeView();
			this.label7 = new System.Windows.Forms.Label();
			this.grpConnectionString.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdBuild
			// 
			this.cmdBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBuild.Location = new System.Drawing.Point(376, 88);
			this.cmdBuild.Name = "cmdBuild";
			this.cmdBuild.Size = new System.Drawing.Size(88, 24);
			this.cmdBuild.TabIndex = 3;
			this.cmdBuild.Text = "&Build...";
			this.cmdBuild.Click += new System.EventHandler(this.cmdBuild_Click);
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionString.Location = new System.Drawing.Point(8, 27);
			this.txtConnectionString.Multiline = true;
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(456, 53);
			this.txtConnectionString.TabIndex = 1;
			this.txtConnectionString.Text = "";
			// 
			// grpConnectionString
			// 
			this.grpConnectionString.Controls.Add(this.cmdTest);
			this.grpConnectionString.Controls.Add(this.txtConnectionString);
			this.grpConnectionString.Controls.Add(this.cmdBuild);
			this.grpConnectionString.Location = new System.Drawing.Point(8, 8);
			this.grpConnectionString.Name = "grpConnectionString";
			this.grpConnectionString.Size = new System.Drawing.Size(472, 120);
			this.grpConnectionString.TabIndex = 0;
			this.grpConnectionString.TabStop = false;
			this.grpConnectionString.Text = "Connection String";
			// 
			// cmdTest
			// 
			this.cmdTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdTest.Location = new System.Drawing.Point(240, 88);
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Size = new System.Drawing.Size(128, 24);
			this.cmdTest.TabIndex = 2;
			this.cmdTest.Text = "&Test connection...";
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			// 
			// btnAddEntities
			// 
			this.btnAddEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddEntities.Enabled = false;
			this.btnAddEntities.Location = new System.Drawing.Point(488, 184);
			this.btnAddEntities.Name = "btnAddEntities";
			this.btnAddEntities.Size = new System.Drawing.Size(128, 23);
			this.btnAddEntities.TabIndex = 6;
			this.btnAddEntities.Text = "&Add / Update Selected";
			this.btnAddEntities.Click += new System.EventHandler(this.btnAddEntities_Click);
			// 
			// btnRefreshEntityList
			// 
			this.btnRefreshEntityList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefreshEntityList.Location = new System.Drawing.Point(488, 152);
			this.btnRefreshEntityList.Name = "btnRefreshEntityList";
			this.btnRefreshEntityList.Size = new System.Drawing.Size(128, 23);
			this.btnRefreshEntityList.TabIndex = 5;
			this.btnRefreshEntityList.Text = "&Refresh List";
			this.btnRefreshEntityList.Click += new System.EventHandler(this.btnRefreshEntityList_Click);
			// 
			// tvwEntities
			// 
			this.tvwEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwEntities.CheckBoxes = true;
			this.tvwEntities.ImageIndex = -1;
			this.tvwEntities.Location = new System.Drawing.Point(8, 152);
			this.tvwEntities.Name = "tvwEntities";
			this.tvwEntities.SelectedImageIndex = -1;
			this.tvwEntities.Size = new System.Drawing.Size(472, 240);
			this.tvwEntities.TabIndex = 4;
			this.tvwEntities.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwEntities_AfterCheck);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(308, 16);
			this.label7.TabIndex = 7;
			this.label7.Text = "Select the database entities to add to this project";
			// 
			// OleDbDataSourceEditor
			// 
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnAddEntities);
			this.Controls.Add(this.btnRefreshEntityList);
			this.Controls.Add(this.tvwEntities);
			this.Controls.Add(this.grpConnectionString);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "OleDbDataSourceEditor";
			this.Size = new System.Drawing.Size(624, 400);
			this.Load += new System.EventHandler(this.OleDbDataSourceEditor_Load);
			this.grpConnectionString.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Member variables and constructors

		#region Member variables

		private Button cmdBuild;
		private TextBox txtConnectionString;
		private GroupBox grpConnectionString;
		private Button btnAddEntities;
		private Button btnRefreshEntityList;
		private TreeView tvwEntities;
		private Button cmdTest;
		private Label label7;

		private DataSource _dataSource;

		#endregion

		#region Constructors

		public OleDbDataSourceEditor(DataSource ds) : this() {
			_dataSource = ds;
		}

		public OleDbDataSourceEditor() {
			InitializeComponent();
		}

		#endregion

		#endregion

		#region IObjectEditor implementation

		public event EventHandler SelectedObjectChanged;

		protected virtual void OnSelectedObjectChanged() {
			if (SelectedObjectChanged != null) {
				SelectedObjectChanged(SelectedObject, new EventArgs());
			}
		}

		#region DoBinding

		private void DoBinding() {
			txtConnectionString.DataBindings.Clear();
			txtConnectionString.DataBindings.Add("Text", _dataSource, "ConnectionString");
			Enabled = true;
		}

		#endregion DoBinding

		#region SelectedObject

		public object SelectedObject {
			get { return _dataSource; }
			set {
				_dataSource = (DataSource) value;
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
			get { return Images.CreateIcon(Images.DataSource); }
		}

		#endregion Icon

		#region ToString

		public override string ToString() {
			return _dataSource.Name;
		}

		#endregion ToString

		#region CommitChanges

		public bool CommitChanges() {
			return true;
		}

		#endregion CommitChanges

		#region QueryUnload

		public void QueryUnload(out bool cancel) {
			//			txtName_Validated(null, null);
			//			if (_errCount > 0) {
			//				DialogResult result = MessageBox.Show("There are errors which should be corrected before proceding!", 
			//					"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//				cancel = true;
			//				return;
			//			}
			if (!Validate()) {
				DialogResult res =
					MessageBox.Show("There are one or more errors with the data on this form. Would you like to close it anyway?",
					                "Validation Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
					                MessageBoxDefaultButton.Button2);
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

		#region Events

		private void OleDbDataSourceEditor_Load(object sender, EventArgs e) {
			if (_dataSource != null) {
				DoBinding();
			}
		}

		private void btnAddEntities_Click(object sender, EventArgs e) {
			try 
			{
				this.Cursor = Cursors.WaitCursor;
				this.btnAddEntities.Enabled = false;
				this.btnRefreshEntityList.Enabled = false;
				this.cmdBuild.Enabled = false;
				this.cmdTest.Enabled = false;
				this.tvwEntities.Enabled = false;
				UpdateProjectEntities();
			}
			catch (Exception ex) 
			{
				MessageBox.Show("The following error occurred while trying to update the entities in this project: \r\n\r\n" +
					ex.ToString());
			}
			finally
			{
				this.Cursor = Cursors.Default;
				this.btnRefreshEntityList.Enabled = true;
				this.cmdBuild.Enabled = true;
				this.cmdTest.Enabled = true;
				this.tvwEntities.Enabled = true;
				DoEnableButtons();
			}
		}

		private void btnRefreshEntityList_Click(object sender, EventArgs e) {
			UpdateEntityList();
		}

		private void cmdTest_Click(object sender, EventArgs e) {
			try {
				_dataSource.TestConnection();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message + "|" + ex.StackTrace,
					"Error - " + ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			MessageBox.Show("Connection tested successfully!",
				"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			UpdateEntityList();
		}

		//		private void optSqlProvider_CheckedChanged(object sender, EventArgs e) {
		//			pnlSqlProvider.Visible = true;
		//			pnlOleDbProvider.Visible = false;
		//		}

		//		private void optOleDbProvider_CheckedChanged(object sender, EventArgs e) {
		//			pnlSqlProvider.Visible = false;
		//			pnlOleDbProvider.Visible = true;
		//		}

		private void cmdBuild_Click(object sender, EventArgs e) {

			OleDbConnectionStringDialog dialog = new OleDbConnectionStringDialog();
			string connectionString = dialog.EditValue(null, txtConnectionString.Text) as string;
			
			if (connectionString != null) {
				_dataSource.ConnectionString = connectionString;
				txtConnectionString.Text = connectionString;
				UpdateEntityList();
			}

		}

		#endregion Events

		#region Private methods

		private void UpdateEntityList() {
			try {
				this.Cursor = Cursors.WaitCursor;
				this.btnRefreshEntityList.Enabled = false;
				this.btnAddEntities.Enabled = false;
				tvwEntities.Nodes.Clear();
				tvwEntities.ImageList = Images.GetImageList();
				TreeNode tables = new TreeNode("Tables", Images.Indexes.FolderClosed, Images.Indexes.FolderOpen);
				TreeNode views = new TreeNode("Views", Images.Indexes.FolderClosed, Images.Indexes.FolderOpen);

				DatabaseSchema schema = SchemaBuilder.CreateDatabaseSchema(_dataSource.ConnectionString, DbType.SQLSERVER, DbProviderType.OLEDB);
				tvwEntities.Tag = schema;
				foreach (TableSchema entity in schema.SortedTables.Values) {
					if (entity.TableType == TableType.TABLE) {
						TreeNode node = new TreeNode(entity.Name, Images.Indexes.Details, Images.Indexes.Details);
						node.Tag = entity;
						tables.Nodes.Add(node);
						if (DataSourcedEntityExistsInProject(entity))
						{
							node.Checked = true;
						}
					}
					else if (entity.TableType == TableType.VIEW) {
						TreeNode node = new TreeNode(entity.Name, Images.Indexes.Edit, Images.Indexes.Edit);
						node.Tag = entity;
						views.Nodes.Add(node);
					}
				}

				if (tables.Nodes.Count > 0) {
					tvwEntities.Nodes.Add(tables);
					tables.Expand();
					tvwEntities.SelectedNode = tables;
				}
				if (views.Nodes.Count > 0) {
					tvwEntities.Nodes.Add(views);
				}
				DoEnableButtons();
			}
			catch (Exception ex) {
				btnAddEntities.Enabled = false;
				MessageBox.Show("The following error occurred refreshing the entity list: \r\n\r\n" + ex.ToString());
			}
			finally {
				this.Cursor = Cursors.Default;
				this.btnRefreshEntityList.Enabled = true;
			}

		}
		
		private bool DataSourcedEntityExistsInProject(TableSchema table)
		{
			Entity entity = GetMetadataEntity(table, false);
			if (entity != null && entity.DataSourceName == _dataSource.Name)
				return true;
			else
				return false;
		}

		private void UpdateProjectEntities() {
			ArrayList updatedEntities = new ArrayList();
			//loop through selected entities and add / update each one
			DatabaseSchema db = (DatabaseSchema) tvwEntities.Tag;
			foreach (TableSchema table in GetSelectedTables()) {
				Entity entity = GetMetadataEntity(table, true);
				entity.RefreshDBInfo(_dataSource, db, table);
				updatedEntities.Add(entity);
				if (_dataSource.OrmConfiguration.CustomEntityAttributes.Count > 0)
				{
					foreach (NameValuePair att in _dataSource.OrmConfiguration.CustomEntityAttributes)
					{
						((IMetadataNode)entity).SetAttributeValue(att.Name, att.Value);
//						XmlAttribute xmlAtt = ((IMetadataNode)entity).LoadedXmlNode.Attributes[att.Name];
//						if (xmlAtt == null)
//						{
//							xmlAtt = ((IMetadataNode)entity).LoadedXmlNode.OwnerDocument.CreateAttribute(att.Name);
//							((IMetadataNode)entity).LoadedXmlNode.Attributes.Append(xmlAtt);
//						}
//						xmlAtt.Value = att.Value;
					}
				}
				entity.OwningMetadataFile.Save();
			}
			if (_dataSource.OrmConfiguration.AutoMapLinks)
			{
				foreach (Entity entity in updatedEntities)
				{
					entity.RefreshDBLinks(_dataSource);
					entity.OwningMetadataFile.Save();
				}
			}
			OnSelectedObjectChanged();
		}

		private TableSchemaCollection GetSelectedTables() {
			TableSchemaCollection tables = new TableSchemaCollection();
			foreach (TreeNode groupNode in tvwEntities.Nodes) {
				foreach (TreeNode node in groupNode.Nodes) {
					if (node.Checked || groupNode.Checked) {
						tables.Add((TableSchema) node.Tag);
					}
				}
			}
			return tables;
		}

		private Entity GetMetadataEntity(TableSchema table, bool create) {
			foreach (MetadataFile file in _dataSource.Project.MetadataFiles) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if ((entity as Entity) != null &&
					    (((Entity) entity).DataSourceName == _dataSource.Name || file.GetFullPath() == GetEntityFileName(table)) &&
					    ((Entity) entity).DBEntityName == table.Name) {
						return (Entity) entity;
					}
				}
			}
			if (create)
				return CreateMetadataEntity(table);
			else
				return null;
		}

		private Entity CreateMetadataEntity(TableSchema table) {
			//string fileName = GetEntityFileName(table);
			MetadataFile file = new MetadataFile(_dataSource.Project);
			Entity entity = new Entity();
			file.MetadataEntities.Add(entity);
			file.Save(GetEntityFileName(table));
			_dataSource.Project.MetadataFiles.Add(file);
			return entity;
		}

		private string GetEntityFileName(TableSchema table) {
			return Path.Combine(_dataSource.Project.GetFullMetadataPath(), table.Name + ".xml");
		}

		#endregion Private methods

		private void tvwEntities_AfterCheck(object sender, TreeViewEventArgs e) {
			foreach (TreeNode node in e.Node.Nodes) {
				node.Checked = e.Node.Checked;
			}
			DoEnableButtons();
		}
		
		private void DoEnableButtons()
		{
			bool enabled = false;
			foreach (TreeNode groupNode in tvwEntities.Nodes) {
				foreach (TreeNode node in groupNode.Nodes) {
					if (node.Checked || groupNode.Checked) {
						enabled = true;
						break;
					}
					if (enabled)
						break;
				}
				btnAddEntities.Enabled = enabled;
			}
		}

	}
}