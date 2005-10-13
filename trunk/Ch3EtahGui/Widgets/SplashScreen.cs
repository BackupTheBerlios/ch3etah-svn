using System.ComponentModel;
using System.Drawing;
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
			ResourceManager resources = new ResourceManager(typeof (SplashScreen));
			// 
			// AboutWindow
			// 
			AutoScaleBaseSize = new Size(5, 13);
			BackgroundImage = ((Image) (resources.GetObject("$this.BackgroundImage")));
			ClientSize = new Size(500, 300);
			ControlBox = false;
			FormBorderStyle = FormBorderStyle.None;
			HelpButton = true;
			Name = "AboutWindow";
			StartPosition = FormStartPosition.CenterScreen;
			TopMost = true;

		}

		#endregion

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
	}
}