using System;
using System.Windows.Forms;

using Ch3Etah.Core.ProjectLib;
using Ch3Etah.Gui.Widgets;
//using Ch3tah.DatabaseWizard.Persistence;

namespace Ch3tah.DatabaseWizard.UI.Forms {
	/// <summary>
	/// Summary description for DataSourceSelector.
	/// </summary>
	public class DataSourceSelector : Form {

		#region Windows Form Designer generated code

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancelar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblProvider;
		private System.Windows.Forms.Label lblConnectionString;
		private System.Windows.Forms.ListBox lstDataSources;
		private System.Windows.Forms.Button cmdNova;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataSourceSelector() {
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
			this.lstDataSources = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancelar = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblConnectionString = new System.Windows.Forms.Label();
			this.lblProvider = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmdNova = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstDataSources
			// 
			this.lstDataSources.Location = new System.Drawing.Point(4, 24);
			this.lstDataSources.Name = "lstDataSources";
			this.lstDataSources.Size = new System.Drawing.Size(292, 95);
			this.lstDataSources.TabIndex = 1;
			this.lstDataSources.SelectedIndexChanged += new System.EventHandler(this.lstDataSources_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Origem de dados:";
			// 
			// cmdOK
			// 
			this.cmdOK.Location = new System.Drawing.Point(304, 24);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancelar
			// 
			this.cmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancelar.Location = new System.Drawing.Point(304, 52);
			this.cmdCancelar.Name = "cmdCancelar";
			this.cmdCancelar.TabIndex = 6;
			this.cmdCancelar.Text = "Cancelar";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblConnectionString);
			this.groupBox1.Controls.Add(this.lblProvider);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(4, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 100);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Detalhes sobre a origem de dados";
			// 
			// lblConnectionString
			// 
			this.lblConnectionString.Location = new System.Drawing.Point(108, 44);
			this.lblConnectionString.Name = "lblConnectionString";
			this.lblConnectionString.Size = new System.Drawing.Size(260, 52);
			this.lblConnectionString.TabIndex = 3;
			this.lblConnectionString.Text = "(ConnectionString)";
			// 
			// lblProvider
			// 
			this.lblProvider.AutoSize = true;
			this.lblProvider.Location = new System.Drawing.Point(108, 20);
			this.lblProvider.Name = "lblProvider";
			this.lblProvider.Size = new System.Drawing.Size(54, 16);
			this.lblProvider.TabIndex = 2;
			this.lblProvider.Text = "(Provider)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(98, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "String de conexão:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Provedor:";
			// 
			// cmdNova
			// 
			this.cmdNova.Location = new System.Drawing.Point(52, 124);
			this.cmdNova.Name = "cmdNova";
			this.cmdNova.TabIndex = 2;
			this.cmdNova.Text = "&Nova...";
			this.cmdNova.Click += new System.EventHandler(this.cmdNova_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(136, 124);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "&Editar...";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(220, 124);
			this.button2.Name = "button2";
			this.button2.TabIndex = 4;
			this.button2.Text = "E&xcluir...";
			// 
			// DataSourceSelector
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancelar;
			this.ClientSize = new System.Drawing.Size(386, 260);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdNova);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cmdCancelar);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstDataSources);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DataSourceSelector";
			this.Text = "Selecione a origem de dados";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DataSource ds;
		private Project project;

		public DataSource SelectDataSource(Project project) {

			this.lblConnectionString.Text = "";
			this.lblProvider.Text = "";
			this.project = project;
			this.lstDataSources.DataSource = project.DataSources;

			ShowDialog();

			return ds;
		}

		private void lstDataSources_SelectedIndexChanged(object sender, EventArgs e) {
		
			if (lstDataSources.SelectedItem != null) {
				ds = (DataSource) lstDataSources.SelectedItem;
				lblProvider.Text = ds.Provider;
				lblConnectionString.Text = ds.ConnectionString;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e) {

			if (lstDataSources.SelectedItem == null) {
				MessageBox.Show("Selecione uma origem de dados.", "Erro", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
			}
			else {
				this.Close();
			}

		}

		private void cmdNova_Click(object sender, EventArgs e) {

			DataSourceEditor form = new DataSourceEditor();
			DataSource ds = form.newDataSource();

			if (ds != null) {
				project.DataSources.Add(ds);
				lstDataSources.DataSource = null;
				lstDataSources.DataSource = project.DataSources;
				lstDataSources.SelectedIndex = lstDataSources.FindString(ds.Name);
			}
		}
	}
}