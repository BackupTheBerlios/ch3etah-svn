using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Summary description for SQLEditor.
	/// </summary>
	public class SQLEditor : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBoxSQL;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxSQL = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.buttonOK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 286);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(408, 32);
			this.panel1.TabIndex = 0;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(248, 4);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "&OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(328, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxSQL
			// 
			this.textBoxSQL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxSQL.Location = new System.Drawing.Point(0, 0);
			this.textBoxSQL.MaxLength = 327670;
			this.textBoxSQL.Multiline = true;
			this.textBoxSQL.Name = "textBoxSQL";
			this.textBoxSQL.Size = new System.Drawing.Size(408, 318);
			this.textBoxSQL.TabIndex = 3;
			this.textBoxSQL.Text = "";
			// 
			// SQLEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 318);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textBoxSQL);
			this.Name = "SQLEditor";
			this.Text = "SQLEditor";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	
		#endregion

		#region Constructor

		public SQLEditor()
		{
			InitializeComponent();
		}


		public SQLEditor(String query)
		{
			this.textBoxSQL.Text = query;
		}


		#endregion

		#region Destructor

		#endregion

		#region Constantes

		#endregion

		#region Enums

		#endregion

		#region Fields

		#endregion

		#region Properties

		public String SqlQuery
		{
			get { return textBoxSQL.Text; }
			set { textBoxSQL.Text = value; }
		}


		#endregion

		#region Events

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		#endregion

		#region Methods

		#endregion
	}
}