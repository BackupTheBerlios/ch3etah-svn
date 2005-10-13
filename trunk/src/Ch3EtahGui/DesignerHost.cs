using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Ch3Etah.Gui {
	/// <summary>
	/// Summary description for DesignerHost.
	/// </summary>
	public class Site : ISite {
		#region Member variables ands constructors

		#region Member variables

		private IComponent component;
		private IContainer container;
		private string name;

		#endregion

		#region Constructors

		public Site(IComponent component, IContainer container) {
			this.component = component;
			this.container = container;
		}

		public Site(IComponent component, IContainer container, string name) {
			this.component = component;
			this.container = container;
			this.name = name;
		}

		#endregion

		#endregion

		#region ISite Members

		public IComponent Component {
			get { return component; }
		}

		public IContainer Container {
			get { return container; }
		}

		public bool DesignMode {
			get { return true; }
		}

		public string Name {
			get { return name; }
			set { name = value; }
		}

		#endregion

		#region IServiceProvider Members

		public object GetService(Type serviceType) {
			return null;
		}

		#endregion
	}

}