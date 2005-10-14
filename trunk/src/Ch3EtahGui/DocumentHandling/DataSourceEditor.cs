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
 *   Date: 2005/7/23
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Adapdev.Data;
using Adapdev.Data.Schema;
using ADODB;
using Ch3Etah.Core.Interop;
using Ch3Etah.Core.Metadata;
using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Gui.DocumentHandling {

	public class DataSourceEditor : UserControl, IObjectEditor {
	
		//private const string SQL_CONNECTIONSTRING = "Data source={0};Initial Catalog={1};Integrated Security={2};Uid={3};Password={4}";
		
		private DataSource _dataSource;
		private int _errCount;

		#region Windows Form Designer generated code

		private Label label1;
		private ComboBox cboServer;
		private Label label2;
		private Label label3;
		private TextBox txtUid;
		private TextBox txtPassword;
		private Label label4;
		private ComboBox cboDatabase;
		private TextBox txtConnectionString;
		private Label label5;
		private Button cmdTest;
		private Button cmdBuild;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private Label label6;
		private ErrorProvider errorProvider;
		private TextBox txtName;
		private TabControl tabControl1;
		private TabPage tabConnectionProperties;
		private TabPage tabTables;
		private Button btnRefreshEntityList;
		private TreeView tvwEntities;
		private Label label7;
		private Button btnAddEntities;
		private ListBox lstConnectionType;

		public DataSourceEditor(DataSource ds) : this() {
			_dataSource = ds;
		}

		public DataSourceEditor() {
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
			this.txtPassword = new TextBox();
			this.label4 = new Label();
			this.txtUid = new TextBox();
			this.label3 = new Label();
			this.cboDatabase = new ComboBox();
			this.label2 = new Label();
			this.cboServer = new ComboBox();
			this.label1 = new Label();
			this.cmdBuild = new Button();
			this.label5 = new Label();
			this.txtConnectionString = new TextBox();
			this.cmdTest = new Button();
			this.label6 = new Label();
			this.txtName = new TextBox();
			this.errorProvider = new ErrorProvider();
			this.lstConnectionType = new ListBox();
			this.tabControl1 = new TabControl();
			this.tabConnectionProperties = new TabPage();
			this.tabTables = new TabPage();
			this.btnAddEntities = new Button();
			this.btnRefreshEntityList = new Button();
			this.tvwEntities = new TreeView();
			this.label7 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabConnectionProperties.SuspendLayout();
			this.tabTables.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.txtPassword, ErrorIconAlignment.MiddleLeft);
			this.txtPassword.Location = new Point(296, 128);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new Size(184, 21);
			this.txtPassword.TabIndex = 9;
			this.txtPassword.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new Point(192, 132);
			this.label4.Name = "label4";
			this.label4.Size = new Size(55, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Password:";
			// 
			// txtUid
			// 
			this.txtUid.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.txtUid, ErrorIconAlignment.MiddleLeft);
			this.txtUid.Location = new Point(296, 100);
			this.txtUid.Name = "txtUid";
			this.txtUid.Size = new Size(184, 21);
			this.txtUid.TabIndex = 7;
			this.txtUid.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new Point(192, 104);
			this.label3.Name = "label3";
			this.label3.Size = new Size(62, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "User name:";
			// 
			// cboDatabase
			// 
			this.cboDatabase.AllowDrop = true;
			this.cboDatabase.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.cboDatabase, ErrorIconAlignment.MiddleLeft);
			this.cboDatabase.Location = new Point(296, 72);
			this.cboDatabase.Name = "cboDatabase";
			this.cboDatabase.Size = new Size(184, 21);
			this.cboDatabase.TabIndex = 12;
			this.cboDatabase.DropDown += new EventHandler(this.cboDatabase_DropDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new Point(192, 76);
			this.label2.Name = "label2";
			this.label2.Size = new Size(55, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Database:";
			// 
			// cboServer
			// 
			this.cboServer.AllowDrop = true;
			this.cboServer.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.cboServer, ErrorIconAlignment.MiddleLeft);
			this.cboServer.Location = new Point(296, 44);
			this.cboServer.Name = "cboServer";
			this.cboServer.Size = new Size(184, 21);
			this.cboServer.TabIndex = 5;
			this.cboServer.DropDown += new EventHandler(this.cboServer_DropDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new Point(192, 48);
			this.label1.Name = "label1";
			this.label1.Size = new Size(40, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Server:";
			// 
			// cmdBuild
			// 
			this.cmdBuild.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
			this.cmdBuild.Location = new Point(392, 320);
			this.cmdBuild.Name = "cmdBuild";
			this.cmdBuild.Size = new Size(88, 24);
			this.cmdBuild.TabIndex = 2;
			this.cmdBuild.Text = "&Build...";
			this.cmdBuild.Click += new EventHandler(this.cmdBuild_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new Point(192, 168);
			this.label5.Name = "label5";
			this.label5.Size = new Size(95, 17);
			this.label5.TabIndex = 1;
			this.label5.Text = "&Connection string:";
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
				| AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.txtConnectionString, ErrorIconAlignment.TopRight);
			this.txtConnectionString.Location = new Point(192, 184);
			this.txtConnectionString.Multiline = true;
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.ReadOnly = true;
			this.txtConnectionString.Size = new Size(288, 132);
			this.txtConnectionString.TabIndex = 0;
			this.txtConnectionString.Text = "";
			// 
			// cmdTest
			// 
			this.cmdTest.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
			this.cmdTest.Location = new Point(192, 320);
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Size = new Size(132, 24);
			this.cmdTest.TabIndex = 17;
			this.cmdTest.Text = "&Test connection...";
			this.cmdTest.Click += new EventHandler(this.cmdTest_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new Point(192, 20);
			this.label6.Name = "label6";
			this.label6.Size = new Size(97, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "Datasource &Name:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.errorProvider.SetIconAlignment(this.txtName, ErrorIconAlignment.MiddleLeft);
			this.txtName.Location = new Point(296, 16);
			this.txtName.Name = "txtName";
			this.txtName.Size = new Size(184, 21);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			this.txtName.Validating += new CancelEventHandler(this.txtName_Validating);
			this.txtName.Validated += new EventHandler(this.txtName_Validated);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// lstConnectionType
			// 
			this.lstConnectionType.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) 
				| AnchorStyles.Left)));
			this.lstConnectionType.IntegralHeight = false;
			this.lstConnectionType.Location = new Point(8, 8);
			this.lstConnectionType.Name = "lstConnectionType";
			this.lstConnectionType.Size = new Size(176, 336);
			this.lstConnectionType.TabIndex = 18;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
				| AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabConnectionProperties);
			this.tabControl1.Controls.Add(this.tabTables);
			this.tabControl1.Location = new Point(4, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(496, 376);
			this.tabControl1.TabIndex = 19;
			// 
			// tabConnectionProperties
			// 
			this.tabConnectionProperties.Controls.Add(this.txtPassword);
			this.tabConnectionProperties.Controls.Add(this.label6);
			this.tabConnectionProperties.Controls.Add(this.txtName);
			this.tabConnectionProperties.Controls.Add(this.label4);
			this.tabConnectionProperties.Controls.Add(this.txtUid);
			this.tabConnectionProperties.Controls.Add(this.label3);
			this.tabConnectionProperties.Controls.Add(this.cboDatabase);
			this.tabConnectionProperties.Controls.Add(this.label2);
			this.tabConnectionProperties.Controls.Add(this.label1);
			this.tabConnectionProperties.Controls.Add(this.cboServer);
			this.tabConnectionProperties.Controls.Add(this.lstConnectionType);
			this.tabConnectionProperties.Controls.Add(this.cmdBuild);
			this.tabConnectionProperties.Controls.Add(this.cmdTest);
			this.tabConnectionProperties.Controls.Add(this.txtConnectionString);
			this.tabConnectionProperties.Controls.Add(this.label5);
			this.tabConnectionProperties.Location = new Point(4, 22);
			this.tabConnectionProperties.Name = "tabConnectionProperties";
			this.tabConnectionProperties.Size = new Size(488, 350);
			this.tabConnectionProperties.TabIndex = 0;
			this.tabConnectionProperties.Text = "Connection Properties";
			// 
			// tabTables
			// 
			this.tabTables.Controls.Add(this.btnAddEntities);
			this.tabTables.Controls.Add(this.btnRefreshEntityList);
			this.tabTables.Controls.Add(this.tvwEntities);
			this.tabTables.Controls.Add(this.label7);
			this.tabTables.Location = new Point(4, 22);
			this.tabTables.Name = "tabTables";
			this.tabTables.Size = new Size(488, 350);
			this.tabTables.TabIndex = 1;
			this.tabTables.Text = "Select Tables";
			// 
			// btnAddEntities
			// 
			this.btnAddEntities.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
			this.btnAddEntities.Location = new Point(300, 320);
			this.btnAddEntities.Name = "btnAddEntities";
			this.btnAddEntities.Size = new Size(180, 23);
			this.btnAddEntities.TabIndex = 6;
			this.btnAddEntities.Text = "&Add Selected Entities to Project";
			this.btnAddEntities.Click += new EventHandler(this.btnAddEntities_Click);
			// 
			// btnRefreshEntityList
			// 
			this.btnRefreshEntityList.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
			this.btnRefreshEntityList.Location = new Point(8, 320);
			this.btnRefreshEntityList.Name = "btnRefreshEntityList";
			this.btnRefreshEntityList.Size = new Size(96, 23);
			this.btnRefreshEntityList.TabIndex = 5;
			this.btnRefreshEntityList.Text = "&Refresh List";
			this.btnRefreshEntityList.Click += new EventHandler(this.btnRefreshEntityList_Click);
			// 
			// tvwEntities
			// 
			this.tvwEntities.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
				| AnchorStyles.Left) 
				| AnchorStyles.Right)));
			this.tvwEntities.CheckBoxes = true;
			this.tvwEntities.ImageIndex = -1;
			this.tvwEntities.Location = new Point(8, 24);
			this.tvwEntities.Name = "tvwEntities";
			this.tvwEntities.SelectedImageIndex = -1;
			this.tvwEntities.Size = new Size(472, 292);
			this.tvwEntities.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.Location = new Point(8, 8);
			this.label7.Name = "label7";
			this.label7.Size = new Size(308, 16);
			this.label7.TabIndex = 4;
			this.label7.Text = "Select the database entities to add to this project";
			// 
			// DataSourceEditor
			// 
			this.Controls.Add(this.tabControl1);
			this.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((Byte)(0)));
			this.Name = "DataSourceEditor";
			this.Size = new Size(504, 384);
			this.Load += new EventHandler(this.DataSourceEditor_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabConnectionProperties.ResumeLayout(false);
			this.tabTables.ResumeLayout(false);
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
			lstConnectionType.Items.Clear();
			foreach (ConnectionType type in ConnectionTypeFactory.ListConnectionTypes()) {
				lstConnectionType.Items.Add(type);
			}
			
			lstConnectionType.DataBindings.Clear();
			txtName.DataBindings.Clear();
			cboServer.DataBindings.Clear();
			cboDatabase.DataBindings.Clear();
			txtUid.DataBindings.Clear();
			txtPassword.DataBindings.Clear();
			txtConnectionString.DataBindings.Clear();
			
			lstConnectionType.DataBindings.Add("SelectedItem", _dataSource, "ConnectionType");
			txtName.DataBindings.Add("Text", _dataSource, "Name");
			cboServer.DataBindings.Add("Text", _dataSource, "Server");
			cboDatabase.DataBindings.Add("Text", _dataSource, "Database");
			txtUid.DataBindings.Add("Text", _dataSource, "User");
			txtPassword.DataBindings.Add("Text", _dataSource, "Password");
			txtConnectionString.DataBindings.Add("Text", _dataSource, "ConnectionString");
			
			this.Enabled = true;
		}
		#endregion DoBinding
		
		#region SelectedObject
		public object SelectedObject {
			get {
				return _dataSource;
			}
			set {
				_dataSource = (DataSource)value;
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
				return Images.CreateIcon(Images.DataSource);
			}
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

		#region Events
		private void btnAddEntities_Click(object sender, EventArgs e) {
			try {
				UpdateProjectEntities();
			}
			catch (Exception ex) {
				MessageBox.Show("The following error occurred while trying to update the entities in this project: \r\n\r\n" + ex.ToString());
			}
		}

		private void btnRefreshEntityList_Click(object sender, EventArgs e) {
			try {
				UpdateEntityList();
			}
			catch (Exception ex) {
				MessageBox.Show("The following error occurred refreshing the entity list: \r\n\r\n" + ex.ToString());
			}
		}
		
		private void cboServer_DropDown(object sender, EventArgs e) {
			
			this.cboServer.Items.Clear();
			
//			ApplicationClass app = new ApplicationClass();
//			foreach (object item in app.ListAvailableSQLServers()) 
//			{
//				this.cboServer.Items.Add(item);
//			}

			
//			try 
//			{
//				this.Cursor = Cursors.WaitCursor;
//				
//				try {
//					foreach (object item in app.ListAvailableSQLServers()) {
//						this.cboServer.Items.Add(item);
//					}
//				}
//				catch (Exception e1) {
//					MessageBox.Show("Could not enumerate the SQL Servers on your network. Check the version " +
//						"of the SQL Server client installed on your computer. Versions prior to SQL Server 2000 " +
//						"SP2 have a bug which prevents the enumeration of servers by .NET applications.",
//						"Error - " + e1.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//				}
//			}
//			finally {
//				this.Cursor = Cursors.Default;
//			}
		}

//		private DataSource NewDataSource() {
//			ShowDialog();
//			return dataSource;
//		}

//		private void chkIntegrated_CheckedChanged(object sender, EventArgs e) {
//			txtUid.Enabled = ! chkIntegrated.Checked;
//			txtPassword.Enabled = ! chkIntegrated.Checked;
//		}

		private void cboDatabase_DropDown(object sender, EventArgs e) {
			if (cboServer.Text.Equals("")) {
				return;
			}
			this.cboDatabase.Items.Clear();
			
			
//			SQLServer server = new SQLServerClass();
//			if (_dataSource.ConnectionType != null) 
//			{
//				server.LoginSecure = _dataSource.ConnectionType.UsesIntegratedSecurity;
//			}
//			server.Connect(cboServer.Text, txtUid.Text, txtPassword.Text);
//			foreach (Database item in server.Databases) 
//			{
//				this.cboDatabase.Items.Add(item.Name);
//			}

			
//			try 
//			{
//				this.Cursor = Cursors.WaitCursor;
//				SQLServer server = new SQLServerClass();
//				if (_dataSource.ConnectionType != null) {
//					server.LoginSecure = _dataSource.ConnectionType.UsesIntegratedSecurity;
//				}
//				server.Connect(cboServer.Text, txtUid.Text, txtPassword.Text);
//				foreach (Database item in server.Databases) {
//					this.cboDatabase.Items.Add(item.Name);
//				}
//			}
//			catch (Exception e1) {
//				MessageBox.Show("Could not enumerate the SQL Servers on your network. Check the version " +
//					"of the SQL Server client installed on your computer. Versions prior to SQL Server 2000 " +
//					"SP2 have a bug which prevents the enumeration of servers by .NET applications.",
//					"Error - " + e1.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//			}
//			finally {
//				this.Cursor = Cursors.Default;
//			}
		}

		private void cmdTest_Click(object sender, EventArgs e) {
			try 
			{
				_dataSource.TestConnection();
			}
			catch (Exception ex) 
			{
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
			
			DataLinks dl = new DataLinks();
			object cn;

			if (!txtConnectionString.Equals("")) {
				cn = new Connection();
				((Connection) cn).ConnectionString = txtConnectionString.Text;
				dl.PromptEdit(ref cn);
			}
			else {
				cn = (Connection) dl.PromptNew();
			}

			txtConnectionString.Text = ((Connection) cn).ConnectionString;
		}


		private void txtName_Validating(object sender, CancelEventArgs e) {
			_errCount++;
			errorProvider.SetError(txtName, "There is an error with this source name. Data source names cannot be left blank and must be unique within the project.");
		}
		
		private void txtName_Validated(object sender, EventArgs e) {
			if (txtName.Text.Equals("")) {
				errorProvider.SetError(txtName, "You need to provide a name for this connection.");
				_errCount++;
			}
			else {
				if (!errorProvider.GetError(txtName).Equals("")) {
					_errCount--;
				}
				errorProvider.SetError(txtName, "");
			}
		}

		private void DataSourceEditor_Load(object sender, EventArgs e) {
			if (_dataSource != null) {
				DoBinding();
			}
		}
		#endregion Events

		#region Private methods
		private void UpdateEntityList() {
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

		private void UpdateProjectEntities() {
			//loop through selected entities and add / update each one
			DatabaseSchema db = (DatabaseSchema)tvwEntities.Tag;
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
						tables.Add((TableSchema)node.Tag);
					}
				}
			}
			return tables;
		}

		private Entity GetMetadataEntity(TableSchema table) {
			foreach (MetadataFile file in _dataSource.Project.MetadataFiles) {
				foreach (IMetadataEntity entity in file.MetadataEntities) {
					if ((entity as Entity) != null && 
						(((Entity)entity).DataSourceName == _dataSource.Name || file.GetFullPath() == GetEntityFileName(table))&& 
						((Entity)entity).DBEntityName == table.Name) {
						return (Entity)entity;
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

	}
}