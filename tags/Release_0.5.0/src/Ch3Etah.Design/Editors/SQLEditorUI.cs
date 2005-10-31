using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace Ch3Etah.Design.Editors {
	/// <summary>
	/// Summary description for SQLEditor.
	/// </summary>
	public class SQLEditorUI : Form {
		#region Windows Form Designer generated code

		private Panel panel1;
		private Button buttonOK;
		private Button buttonCancel;
		private TextEditorControl textEditorControlSQL;

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SQLEditorUI));
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.textEditorControlSQL = new ICSharpCode.TextEditor.TextEditorControl();
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
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(328, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(248, 4);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// textEditorControlSQL
			// 
			this.textEditorControlSQL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditorControlSQL.Encoding = ((System.Text.Encoding)(resources.GetObject("textEditorControlSQL.Encoding")));
			this.textEditorControlSQL.Location = new System.Drawing.Point(0, 0);
			this.textEditorControlSQL.Name = "textEditorControlSQL";
			this.textEditorControlSQL.ShowEOLMarkers = true;
			this.textEditorControlSQL.ShowInvalidLines = false;
			this.textEditorControlSQL.ShowSpaces = true;
			this.textEditorControlSQL.ShowTabs = true;
			this.textEditorControlSQL.ShowVRuler = true;
			this.textEditorControlSQL.Size = new System.Drawing.Size(408, 286);
			this.textEditorControlSQL.TabIndex = 1;
			// 
			// SQLEditorUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 318);
			this.Controls.Add(this.textEditorControlSQL);
			this.Controls.Add(this.panel1);
			this.MinimizeBox = false;
			this.Name = "SQLEditorUI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SQLEditor";
			this.Load += new System.EventHandler(this.SQLEditorUI_Load);
			this.Activated += new System.EventHandler(this.SQLEditorUI_Activated);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Constructor

		public SQLEditorUI() {
			InitializeComponent();
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

		public String SqlQuery {
			get { return textEditorControlSQL.Text; }
			set { textEditorControlSQL.Text = value; }
		}

		#endregion

		#region Events

		private void SQLEditorUI_Activated(object sender, EventArgs e) {
			textEditorControlSQL.Focus();
		}

		private void SQLEditorUI_Load(object sender, EventArgs e) {
			HighlightingManager.Manager.AddSyntaxModeFileProvider(
				new FileSyntaxModeProvider(AppDomain.CurrentDomain.BaseDirectory));
			textEditorControlSQL.Document.HighlightingStrategy =
				HighlightingStrategyFactory.CreateHighlightingStrategy("Velocity - TSQL Output");
			textEditorControlSQL.Refresh();
		}

		private void buttonOK_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
		}

		#endregion

		#region Methods

		#endregion
	}
}