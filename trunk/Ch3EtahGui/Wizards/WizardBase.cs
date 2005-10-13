using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ch3Etah.Gui.Wizards
{
	/// <summary>
	/// Summary description for WizardBase.
	/// </summary>
	public class WizardBase : System.Windows.Forms.Form
	{
        protected internal Ch3Etah.Gui.Widgets.Wizard.WizardControl wizardControl1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WizardBase()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.wizardControl1 = new Ch3Etah.Gui.Widgets.Wizard.WizardControl();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Size = new System.Drawing.Size(616, 414);
            this.wizardControl1.TabIndex = 0;
            // 
            // WizardBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(616, 414);
            this.Controls.Add(this.wizardControl1);
            this.Name = "WizardBase";
            this.Text = "WizardBase";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
