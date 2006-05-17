using System;
using System.Reflection;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Summary description for AboutDialog.
	/// </summary>
	public class AboutDialog : System.Windows.Forms.Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutDialog()
		{
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnTestExceptionHandling = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(500, 150);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(4, 154);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(492, 238);
			this.textBox1.TabIndex = 1;
			this.textBox1.TabStop = false;
			this.textBox1.Text = @"Product Version {0}

Website - http://ch3etah.sourceforge.net

CH3ETAH Project Contributors:
Jacob Eggleston
Igor Abade V. Leite
Guilherme Bacellar Moralez

A special thanks goes to Forum Access for believing in the idea and sponsoring a significant part of the development.

Other controls and libraries used by this project:

Portions Copyright (c) 2000-2005 icsharpcode.net (http://www.icsharpcode.net)
Portions Copyright (c) 2004 NVelocity (http://nvelocity.sourceforge.net)
Portions Copyright (c) 2004 Adapdev Technologies, LLC (http://www.adapdev.net)
Portions Copyright (c) 2002-2004 The Genghis Group (http://www.genghisgroup.com)
Portions Copyright (c) 2005 Weifen Luo 
Portions Copyright (c) 2001-2003 Lutz Roeder (http://www.aisto.com/roeder)


System Information:

Running instance: {1}
Command-line: {2}
Current directory: {3}
";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(418, 398);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnTestExceptionHandling
			// 
			this.btnTestExceptionHandling.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnTestExceptionHandling.Location = new System.Drawing.Point(4, 400);
			this.btnTestExceptionHandling.Name = "btnTestExceptionHandling";
			this.btnTestExceptionHandling.Size = new System.Drawing.Size(164, 23);
			this.btnTestExceptionHandling.TabIndex = 2;
			this.btnTestExceptionHandling.Text = "Test global exception handling";
			this.btnTestExceptionHandling.Click += new System.EventHandler(this.btnTestExceptionHandling_Click);
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(500, 428);
			this.ControlBox = false;
			this.Controls.Add(this.btnTestExceptionHandling);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AboutDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About CH3ETAH";
			this.Load += new System.EventHandler(this.AboutDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnTestExceptionHandling;
		private System.Windows.Forms.Button btnClose;

		private void btnClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void AboutDialog_Load(object sender, System.EventArgs e) {
			this.textBox1.Text = string.Format(this.textBox1.Text,
				Utility.GetCh3EtahVersion(), 
				Assembly.GetExecutingAssembly().GetName().CodeBase, 
				Environment.CommandLine,
				Environment.CurrentDirectory);
		}

		private void btnTestExceptionHandling_Click(object sender, System.EventArgs e)
		{
			throw new Ch3EtahTestException();
		}
		
	}

	public class Ch3EtahTestException : ApplicationException
	{
		public Ch3EtahTestException() 
			: base ("This is a test exception that is meant only for testing unhandled exception reporting.")
		{
		}
	}

}
