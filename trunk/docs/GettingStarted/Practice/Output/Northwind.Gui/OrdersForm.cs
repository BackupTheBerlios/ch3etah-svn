using System;
using System.Windows.Forms;

using Northwind.Domain;

namespace Northwind.Gui
{
	public class OrdersForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

		public OrdersForm()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			// 
			// OrdersForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 270);
			this.Name = "OrdersForm";
			this.Text = "Orders";
			this.Load += new System.EventHandler(this.OrdersForm_Load);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new OrdersForm());
		}

		private void OrdersForm_Load(object sender, System.EventArgs e)
		{
			Order o = new Order();
			o.ShipName = "John Doe";
			this.Text = o.ShipName;
		}


	}
}
