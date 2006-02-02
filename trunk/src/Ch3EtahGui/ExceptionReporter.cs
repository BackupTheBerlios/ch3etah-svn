using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch3Etah.Gui
{
	public class ExceptionReporter : System.Windows.Forms.Form
	{
		#region Fields and Constructors
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnReportError;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtError;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.Container components = null;

		public ExceptionReporter()
		{
			InitializeComponent();
		}

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
		#endregion Fields and Constructors

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionReporter));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtError = new System.Windows.Forms.TextBox();
			this.btnReportError = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(176, 360);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(184, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(488, 48);
			this.label1.TabIndex = 1;
			this.label1.Text = @"An unexpected error has occurred in the application. Often these errors are not fatal and you can continue working without any problems. We would like to ask you to help us make CH3ETAH better by reporting this error to us through the forums on the project website.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(184, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(368, 24);
			this.label2.TabIndex = 2;
			this.label2.Text = "Thank you for helping us to make this program better!";
			// 
			// txtError
			// 
			this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtError.BackColor = System.Drawing.SystemColors.Menu;
			this.txtError.Location = new System.Drawing.Point(184, 128);
			this.txtError.Multiline = true;
			this.txtError.Name = "txtError";
			this.txtError.ReadOnly = true;
			this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtError.Size = new System.Drawing.Size(480, 200);
			this.txtError.TabIndex = 3;
			this.txtError.Text = "";
			// 
			// btnReportError
			// 
			this.btnReportError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.btnReportError.BackColor = System.Drawing.SystemColors.Control;
			this.btnReportError.Location = new System.Drawing.Point(184, 328);
			this.btnReportError.Name = "btnReportError";
			this.btnReportError.Size = new System.Drawing.Size(400, 23);
			this.btnReportError.TabIndex = 4;
			this.btnReportError.Text = "Report this error";
			this.btnReportError.Click += new System.EventHandler(this.btnReportError_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(586, 328);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(184, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(488, 57);
			this.label3.TabIndex = 6;
			this.label3.Text = @"All you need to do is click the button labeled ""Report this error."" When you do this, the contents of the text box below will be copied to the clipboard and a new browser window will be opened to the web site. Simply paste the text in the message, along with any information (or suggestions) you may wish to add.";
			// 
			// ExceptionReporter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(674, 360);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnReportError);
			this.Controls.Add(this.txtError);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionReporter";
			this.Text = "Unhandled Exception";
			this.ResumeLayout(false);

		}
		#endregion

		private static bool _registered = false;
		public static void Register()
		{
			if (_registered) return;
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			_registered = true;
		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			try
			{
				if (IsFilteredException(e.Exception))
					return;
				string msg = "CH3ETAH Version: " + Utility.GetCh3EtahVersion() + "\r\n";
				msg += e.Exception.ToString();
				ExceptionReporter dlg = new ExceptionReporter();
				dlg.txtError.Text = msg;
				dlg.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR OPENING GLOBAL EXCEPTION HANDLER: " + ex.ToString());
			}
		}

		private static bool IsFilteredException(Exception ex)
		{
			// This is a very ugly hack, but there's a veeeery 
			// intermittent exception related to the DockPane. This
			// error is nearly impossible to debug and doesn't seem
			// to have any real impact on usability. So we'll eat it
			// until we figure out how to fix it.
			if (ex is ObjectDisposedException
				&& ex.Message.IndexOf("\"DockPane\"") > 0)
			{
				return true;
			}
			return false;
		}

		private void btnReportError_Click(object sender, System.EventArgs e)
		{
			try
			{
				Clipboard.SetDataObject(
					new DataObject(DataFormats.Text, txtError.Text), 
					true);
				Utility.OpenUrl(@"http://sourceforge.net/tracker/?func=add&group_id=118003&atid=679758");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
