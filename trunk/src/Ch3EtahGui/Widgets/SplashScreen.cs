using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Widgets
{
	/// <summary>
	/// Summary description for AboutWindow.
	/// </summary>
	public class SplashScreen : Form
	{

		#region Windows Form Designer generated code

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public SplashScreen()
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
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashScreen));
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblRevision = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.Location = new System.Drawing.Point(8, 192);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(117, 23);
			this.lblVersion.TabIndex = 0;
			this.lblVersion.Text = "Version {0}";
			// 
			// lblRevision
			// 
			this.lblRevision.AutoSize = true;
			this.lblRevision.BackColor = System.Drawing.Color.Transparent;
			this.lblRevision.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblRevision.Location = new System.Drawing.Point(8, 224);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(125, 23);
			this.lblRevision.TabIndex = 0;
			this.lblRevision.Text = "Revision {0}";
			// 
			// SplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(500, 300);
			this.ControlBox = false;
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblRevision);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.HelpButton = true;
			this.Name = "SplashScreen";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SplashScreen_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblRevision;

		private static SplashScreen defaultInstance = new SplashScreen();
		
		private static SplashScreen DefaultInstance
		{
			get
			{
				if (defaultInstance == null)
					defaultInstance = new SplashScreen();
				return defaultInstance;
			}
		}

		new public static void Show()
		{
			((Form) DefaultInstance).Show();
			Application.DoEvents();
		}

		new public static void Hide()
		{
			try
			{
				Application.DoEvents();
				Thread.Sleep(100);
				((Form) DefaultInstance).Close();
			}
			catch
			{
			}
		}

		private void SplashScreen_Load(object sender, System.EventArgs e)
		{
			lblVersion.Text = string.Format(lblVersion.Text, GetVersion());
			lblRevision.Text = string.Format(lblRevision.Text, GetRevision());
		}

		private string GetVersion()
		{
			Assembly assembly = GetType().Assembly;
			AssemblyName name = assembly.GetName();
			return string.Format("{0}.{1}", name.Version.Major, name.Version.Minor);
		}
		
		private string GetRevision()
		{
			Assembly assembly = GetType().Assembly;
			AssemblyName name = assembly.GetName();
			return string.Format("{0}{1}", name.Version.Build, name.Version.Revision);
		}
	}
}