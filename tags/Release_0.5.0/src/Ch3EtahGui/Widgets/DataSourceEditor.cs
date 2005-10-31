using System;
using System.ComponentModel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using ADODB;
using Ch3Etah.Core.ProjectLib;
using SQLDMO;

namespace Ch3Etah.Gui.Widgets {

	public class DataSourceEditor : Form {
	
		private const string SQL_CONNECTIONSTRING = "Data source={0};Initial Catalog={1};Integrated Security={2};Uid={3};Password={4}";
		private int errCount;

		#region Windows Form Designer generated code

		private GroupBox groupBox1;
		private RadioButton optSqlProvider;
		private RadioButton optOleDbProvider;
		private Panel pnlSqlProvider;
		private Label label1;
		private ComboBox cmbSqlServer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtUid;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbDatabase;
		private System.Windows.Forms.CheckBox chkIntegrated;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancelar;
		private System.Windows.Forms.Panel pnlOleDbProvider;
		private System.Windows.Forms.TextBox txtConnectionString;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdTest;
		private System.Windows.Forms.Button cmdBuild;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtNome;
		private System.Windows.Forms.ErrorProvider errorProvider;

		private DataSource dataSource;

		public DataSourceEditor() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.optOleDbProvider = new System.Windows.Forms.RadioButton();
			this.optSqlProvider = new System.Windows.Forms.RadioButton();
			this.pnlSqlProvider = new System.Windows.Forms.Panel();
			this.chkIntegrated = new System.Windows.Forms.CheckBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtUid = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbDatabase = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbSqlServer = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancelar = new System.Windows.Forms.Button();
			this.pnlOleDbProvider = new System.Windows.Forms.Panel();
			this.cmdBuild = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.cmdTest = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.pnlSqlProvider.SuspendLayout();
			this.pnlOleDbProvider.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.optOleDbProvider);
			this.groupBox1.Controls.Add(this.optSqlProvider);
			this.groupBox1.Location = new System.Drawing.Point(152, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ADO.NET Data Provider";
			// 
			// optOleDbProvider
			// 
			this.optOleDbProvider.Checked = true;
			this.optOleDbProvider.Enabled = false;
			this.optOleDbProvider.Location = new System.Drawing.Point(112, 16);
			this.optOleDbProvider.Name = "optOleDbProvider";
			this.optOleDbProvider.Size = new System.Drawing.Size(136, 24);
			this.optOleDbProvider.TabIndex = 2;
			this.optOleDbProvider.TabStop = true;
			this.optOleDbProvider.Text = "OLEDB (Access etc.)";
			this.optOleDbProvider.CheckedChanged += new System.EventHandler(this.optOleDbProvider_CheckedChanged);
			// 
			// optSqlProvider
			// 
			this.optSqlProvider.Enabled = false;
			this.optSqlProvider.Location = new System.Drawing.Point(8, 16);
			this.optSqlProvider.Name = "optSqlProvider";
			this.optSqlProvider.TabIndex = 1;
			this.optSqlProvider.Text = "&SQL Server";
			this.optSqlProvider.CheckedChanged += new System.EventHandler(this.optSqlProvider_CheckedChanged);
			// 
			// pnlSqlProvider
			// 
			this.pnlSqlProvider.Controls.Add(this.chkIntegrated);
			this.pnlSqlProvider.Controls.Add(this.txtPassword);
			this.pnlSqlProvider.Controls.Add(this.label4);
			this.pnlSqlProvider.Controls.Add(this.txtUid);
			this.pnlSqlProvider.Controls.Add(this.label3);
			this.pnlSqlProvider.Controls.Add(this.cmbDatabase);
			this.pnlSqlProvider.Controls.Add(this.label2);
			this.pnlSqlProvider.Controls.Add(this.cmbSqlServer);
			this.pnlSqlProvider.Controls.Add(this.label1);
			this.pnlSqlProvider.Location = new System.Drawing.Point(8, 284);
			this.pnlSqlProvider.Name = "pnlSqlProvider";
			this.pnlSqlProvider.Size = new System.Drawing.Size(400, 160);
			this.pnlSqlProvider.TabIndex = 3;
			this.pnlSqlProvider.Visible = false;
			// 
			// chkIntegrated
			// 
			this.chkIntegrated.Location = new System.Drawing.Point(104, 64);
			this.chkIntegrated.Name = "chkIntegrated";
			this.chkIntegrated.Size = new System.Drawing.Size(284, 20);
			this.chkIntegrated.TabIndex = 10;
			this.chkIntegrated.Text = "Windows Integrated Security (Trusted connection)";
			this.chkIntegrated.CheckedChanged += new System.EventHandler(this.chkIntegrated_CheckedChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(284, 40);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.TabIndex = 9;
			this.txtPassword.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(220, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Password:";
			// 
			// txtUid
			// 
			this.txtUid.Location = new System.Drawing.Point(104, 40);
			this.txtUid.Name = "txtUid";
			this.txtUid.Size = new System.Drawing.Size(112, 21);
			this.txtUid.TabIndex = 7;
			this.txtUid.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 17);
			this.label3.TabIndex = 6;
			this.label3.Text = "User name:";
			// 
			// cmbDatabase
			// 
			this.cmbDatabase.AllowDrop = true;
			this.cmbDatabase.Location = new System.Drawing.Point(104, 92);
			this.cmbDatabase.Name = "cmbDatabase";
			this.cmbDatabase.Size = new System.Drawing.Size(284, 21);
			this.cmbDatabase.TabIndex = 12;
			this.cmbDatabase.DropDown += new System.EventHandler(this.cmbDatabase_DropDown);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Database:";
			// 
			// cmbSqlServer
			// 
			this.cmbSqlServer.AllowDrop = true;
			this.cmbSqlServer.Location = new System.Drawing.Point(104, 16);
			this.cmbSqlServer.Name = "cmbSqlServer";
			this.cmbSqlServer.Size = new System.Drawing.Size(284, 21);
			this.cmbSqlServer.TabIndex = 5;
			this.cmbSqlServer.DropDown += new System.EventHandler(this.cmbSqlServer_DropDown);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Server:";
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(244, 236);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(76, 24);
			this.cmdOk.TabIndex = 14;
			this.cmdOk.Text = "OK";
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancelar
			// 
			this.cmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancelar.Location = new System.Drawing.Point(328, 236);
			this.cmdCancelar.Name = "cmdCancelar";
			this.cmdCancelar.Size = new System.Drawing.Size(76, 24);
			this.cmdCancelar.TabIndex = 15;
			this.cmdCancelar.Text = "Cancel";
			// 
			// pnlOleDbProvider
			// 
			this.pnlOleDbProvider.Controls.Add(this.cmdBuild);
			this.pnlOleDbProvider.Controls.Add(this.label5);
			this.pnlOleDbProvider.Controls.Add(this.txtConnectionString);
			this.pnlOleDbProvider.Location = new System.Drawing.Point(8, 60);
			this.pnlOleDbProvider.Name = "pnlOleDbProvider";
			this.pnlOleDbProvider.Size = new System.Drawing.Size(400, 160);
			this.pnlOleDbProvider.TabIndex = 16;
			// 
			// cmdBuild
			// 
			this.cmdBuild.Location = new System.Drawing.Point(304, 124);
			this.cmdBuild.Name = "cmdBuild";
			this.cmdBuild.Size = new System.Drawing.Size(88, 24);
			this.cmdBuild.TabIndex = 2;
			this.cmdBuild.Text = "&Build...";
			this.cmdBuild.Click += new System.EventHandler(this.cmdBuild_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(95, 17);
			this.label5.TabIndex = 1;
			this.label5.Text = "&Connection string:";
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Location = new System.Drawing.Point(8, 28);
			this.txtConnectionString.Multiline = true;
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(384, 92);
			this.txtConnectionString.TabIndex = 0;
			this.txtConnectionString.Text = "";
			// 
			// cmdTest
			// 
			this.cmdTest.Location = new System.Drawing.Point(12, 236);
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Size = new System.Drawing.Size(132, 24);
			this.cmdTest.TabIndex = 17;
			this.cmdTest.Text = "&Test connection...";
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "&Name:";
			// 
			// txtNome
			// 
			this.txtNome.Location = new System.Drawing.Point(8, 28);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(124, 21);
			this.txtNome.TabIndex = 1;
			this.txtNome.Text = "";
			this.txtNome.Validated += new System.EventHandler(this.txtNome_Validated);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// DataSourceEditor
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.cmdCancelar;
			this.ClientSize = new System.Drawing.Size(416, 272);
			this.Controls.Add(this.txtNome);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmdTest);
			this.Controls.Add(this.pnlOleDbProvider);
			this.Controls.Add(this.cmdCancelar);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.pnlSqlProvider);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataSourceEditor";
			this.ShowInTaskbar = false;
			this.Text = "New Data Source";
			this.groupBox1.ResumeLayout(false);
			this.pnlSqlProvider.ResumeLayout(false);
			this.pnlOleDbProvider.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void cmbSqlServer_DropDown(object sender, EventArgs e) {
			ApplicationClass app = new ApplicationClass();
			this.cmbSqlServer.Items.Clear();

			try {
				foreach (object item in app.ListAvailableSQLServers()) {
					this.cmbSqlServer.Items.Add(item);
				}
			}
			catch (Exception e1) {
				MessageBox.Show("Impossível enumerar os servidores da rede. Cheque a versão " +
					"do cliente do SQL Server 2000 instalado na sua máquina. Versões anteriores " +
					"ao SP2 contêm um bug que impede a enumeração dos servidores por aplicações .NET",
				                "Erro - " + e1.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public DataSource newDataSource() {
			ShowDialog();
			return dataSource;
		}

		private void chkIntegrated_CheckedChanged(object sender, EventArgs e) {
			txtUid.Enabled = ! chkIntegrated.Checked;
			txtPassword.Enabled = ! chkIntegrated.Checked;
		}

		private void cmbDatabase_DropDown(object sender, EventArgs e) {
			if (cmbSqlServer.Text.Equals("")) {
				return;
			}

			this.cmbDatabase.Items.Clear();

			try {
				SQLServer server = new SQLServerClass();
				server.LoginSecure = chkIntegrated.Checked;
				server.Connect(cmbSqlServer.Text, txtUid.Text, txtPassword.Text);
				foreach (Database item in server.Databases) {
					this.cmbDatabase.Items.Add(item.Name);
				}
			}
			catch (Exception e1) {
				MessageBox.Show("Impossível enumerar os servidores da rede. Cheque a versão " +
					"do cliente do SQL Server 2000 instalado na sua máquina. Versões anteriores " +
					"ao SP2 contêm um bug que impede a enumeração dos servidores por aplicações .NET",
				                "Erro - " + e1.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void cmdTest_Click(object sender, EventArgs e) {
			object cn;

			if (optSqlProvider.Checked) {
				cn = new SqlConnection(string.Format(
					SQL_CONNECTIONSTRING,
					new string[] {
						cmbSqlServer.Text, cmbDatabase.Text, (chkIntegrated.Checked ? "SSPI" : ""),
						txtUid.Text, txtPassword.Text
					}));
			}
			else {
				cn = new OleDbConnection(txtConnectionString.Text);
			}

			try {
				if (optSqlProvider.Checked) {
					((SqlConnection) cn).Open();
					((SqlConnection) cn).Close();
				}
				else {
					((OleDbConnection) cn).Open();
					((OleDbConnection) cn).Close();
				}
				MessageBox.Show("Connection successfully tested!",
				                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception e1) {
				MessageBox.Show(e1.Message + "|" + e1.StackTrace,
				                "Error - " + e1.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void optSqlProvider_CheckedChanged(object sender, EventArgs e) {
			pnlSqlProvider.Visible = true;
			pnlOleDbProvider.Visible = false;
		}

		private void optOleDbProvider_CheckedChanged(object sender, EventArgs e) {
			pnlSqlProvider.Visible = false;
			pnlOleDbProvider.Visible = true;
		}

		private void cmdBuild_Click(object sender, EventArgs e) {
//			DataLinks dl = new DataLinks();
//			object cn;
//
//			if (!txtConnectionString.Equals("")) {
//				cn = new Connection();
//				((Connection) cn).ConnectionString = txtConnectionString.Text;
//				dl.PromptEdit(ref cn);
//			}
//			else {
//				cn = (Connection) dl.PromptNew();
//			}
//
//			txtConnectionString.Text = ((Connection) cn).ConnectionString;
		}

		private void cmdOk_Click(object sender, EventArgs e) {
			txtNome_Validated(null, null);
			if (errCount == 0) {
				string connectionString = txtConnectionString.Text;
//				string connectionString = string.Format(
//					SQL_CONNECTIONSTRING,
//					new string[] {
//						cmbSqlServer.Text, cmbDatabase.Text, (chkIntegrated.Checked ? "SSPI" : ""),
//						txtUid.Text, txtPassword.Text});
				dataSource = new DataSource(txtNome.Text, connectionString);
				this.Close();
			}
			else {
				MessageBox.Show("Há erros que devem ser corrigidos antes de prosseguir.", 
					"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void txtNome_Validated(object sender, EventArgs e) {
			if (txtNome.Text.Equals("")) {
				errorProvider.SetError(txtNome, "Você deve informar o nome desta origem de dados");
				errCount++;
			}
			else {
				if (!errorProvider.GetError(txtNome).Equals(""))
					errCount--;
				errorProvider.SetError(txtNome, "");
			}
		}

	}
}