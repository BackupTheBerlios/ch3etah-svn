using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;

namespace Ch3Etah.Gui.Widgets {
	public class ProjectExplorer : DockContent {
		
		private System.Windows.Forms.TreeView tvwProject;
		
		public ProjectExplorer() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectExplorer));
			this.tvwProject = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// tvwProject
			// 
			this.tvwProject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwProject.HideSelection = false;
			this.tvwProject.ImageIndex = -1;
			this.tvwProject.Location = new System.Drawing.Point(0, 0);
			this.tvwProject.Name = "tvwProject";
			this.tvwProject.SelectedImageIndex = -1;
			this.tvwProject.Size = new System.Drawing.Size(292, 266);
			this.tvwProject.TabIndex = 26;
			// 
			// ProjectExplorer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.tvwProject);
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProjectExplorer";
			this.Text = "Project Explorer";
			this.ResumeLayout(false);

		}

		public System.Windows.Forms.TreeView TvwProject {
			get { return tvwProject; }
		}
	}
}