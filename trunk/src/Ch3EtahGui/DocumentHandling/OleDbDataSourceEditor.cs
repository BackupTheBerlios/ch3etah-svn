using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Adapdev.Data;
using Adapdev.Data.Schema;
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
			cmdBuild = new Button();
			txtConnectionString = new TextBox();
			grpConnectionString = new GroupBox();
			cmdTest = new Button();
			btnAddEntities = new Button();
			btnRefreshEntityList = new Button();
			tvwEntities = new TreeView();
			label7 = new Label();
			grpConnectionString.SuspendLayout();
			SuspendLayout();
			// 
			// cmdBuild
			// 
			cmdBuild.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
			cmdBuild.Location = new Point(376, 88);
			cmdBuild.Name = "cmdBuild";
			cmdBuild.Size = new Size(88, 24);
			cmdBuild.TabIndex = 3;
			cmdBuild.Text = "&Build...";
			cmdBuild.Click += new EventHandler(cmdBuild_Click);
			// 
			// txtConnectionString
			// 
			txtConnectionString.Anchor = ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom)
			                                                | AnchorStyles.Left)
			                                               | AnchorStyles.Right)));
			txtConnectionString.Location = new Point(8, 26);
			txtConnectionString.Multiline = true;
			txtConnectionString.Name = "txtConnectionString";
			txtConnectionString.Size = new Size(456, 54);
			txtConnectionString.TabIndex = 1;
			txtConnectionString.Text = "";
			// 
			// grpConnectionString
			// 
			grpConnectionString.Controls.Add(cmdTest);
			grpConnectionString.Controls.Add(txtConnectionString);
			grpConnectionString.Controls.Add(cmdBuild);
			grpConnectionString.Location = new Point(8, 8);
			grpConnectionString.Name = "grpConnectionString";
			grpConnectionString.Size = new Size(472, 120);
			grpConnectionString.TabIndex = 0;
			grpConnectionString.TabStop = false;
			grpConnectionString.Text = "Connection String";
			// 
			// cmdTest
			// 
			cmdTest.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Left)));
			cmdTest.Location = new Point(240, 88);
			cmdTest.Name = "cmdTest";
			cmdTest.Size = new Size(128, 24);
			cmdTest.TabIndex = 2;
			cmdTest.Text = "&Test connection...";
			cmdTest.Click += new EventHandler(cmdTest_Click);
			// 
			// btnAddEntities
			// 
			btnAddEntities.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
			btnAddEntities.Enabled = false;
			btnAddEntities.Location = new Point(400, 184);
			btnAddEntities.Name = "btnAddEntities";
			btnAddEntities.Size = new Size(88, 23);
			btnAddEntities.TabIndex = 6;
			btnAddEntities.Text = "&Add to Project";
			btnAddEntities.Click += new EventHandler(btnAddEntities_Click);
			// 
			// btnRefreshEntityList
			// 
			btnRefreshEntityList.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
			btnRefreshEntityList.Location = new Point(400, 152);
			btnRefreshEntityList.Name = "btnRefreshEntityList";
			btnRefreshEntityList.Size = new Size(88, 23);
			btnRefreshEntityList.TabIndex = 5;
			btnRefreshEntityList.Text = "&Refresh List";
			btnRefreshEntityList.Click += new EventHandler(btnRefreshEntityList_Click);
			// 
			// tvwEntities
			// 
			tvwEntities.Anchor = ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom)
			                                        | AnchorStyles.Left)
			                                       | AnchorStyles.Right)));
			tvwEntities.CheckBoxes = true;
			tvwEntities.ImageIndex = -1;
			tvwEntities.Location = new Point(8, 152);
			tvwEntities.Name = "tvwEntities";
			tvwEntities.SelectedImageIndex = -1;
			tvwEntities.Size = new Size(384, 240);
			tvwEntities.TabIndex = 4;
			tvwEntities.AfterCheck += new TreeViewEventHandler(tvwEntities_AfterCheck);
			// 
			// label7
			// 
			label7.Location = new Point(8, 136);
			label7.Name = "label7";
			label7.Size = new Size(308, 16);
			label7.TabIndex = 7;
			label7.Text = "Select the database entities to add to this project";
			// 
			// OleDbDataSourceEditor
			// 
			Controls.Add(label7);
			Controls.Add(btnAddEntities);
			Controls.Add(btnRefreshEntityList);
			Controls.Add(tvwEntities);
			Controls.Add(grpConnectionString);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((Byte) (0)));
			Name = "OleDbDataSourceEditor";
			Size = new Size(496, 400);
			Load += new EventHandler(OleDbDataSourceEditor_Load);
			grpConnectionString.ResumeLayout(false);
			ResumeLayout(false);

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
			}
		}

		private void btnRefreshEntityList_Click(object sender, EventArgs e) {
			try
			{
				this.Cursor = Cursors.WaitCursor;
				UpdateEntityList();
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
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
			}
			catch (Exception ex) {
				MessageBox.Show("The following error occurred refreshing the entity list: \r\n\r\n" + ex.ToString());
			}

		}

		private void UpdateProjectEntities() {
			//loop through selected entities and add / update each one
			DatabaseSchema db = (DatabaseSchema) tvwEntities.Tag;
			foreach (TableSchema table in GetSelectedTables()) {
				Entity entity = GetMetadataEntity(table);
				entity.RefreshDBInfo(_dataSource, db, table);
				entity.OwningMetadataFile.Save();
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

		private Entity GetMetadataEntity(TableSchema table) {
			foreach (MetadataFile file in _dataSource.Project.MetadataFiles) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if ((entity as Entity) != null &&
					    (((Entity) entity).DataSourceName == _dataSource.Name || file.GetFullPath() == GetEntityFileName(table)) &&
					    ((Entity) entity).DBEntityName == table.Name) {
						return (Entity) entity;
					}
				}
			}
			return CreateMetadataEntity(table);
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