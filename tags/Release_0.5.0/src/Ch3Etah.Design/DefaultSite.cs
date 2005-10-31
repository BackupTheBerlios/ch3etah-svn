using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Ch3Etah.Design
{
	/// <summary>
	/// Summary description for DefaultSite.
	/// </summary>
	public class DefaultSite: ISite
	{
		private readonly IContainer _container;
		private readonly IComponent _component;
		private IMenuCommandService _menuCommandService;
		private IDesigner _designer;
		public string _name;

		public DefaultSite(IComponent component, IContainer container)
		{
			this._container = container;
			this._component = component;
			this._component.Site = this;
		}

		public IComponent Component {
			get { return _component; }
		}

		public IContainer Container {
			get { return _container; }
		}

		public bool DesignMode {
			get { return false; }
		}

		public string Name {
			get { return this._name; }
			set { this._name = value; }
		}

		public IDesigner Designer
		{
			get
			{
				if (_designer == null)
				{
					AttributeCollection attrs = TypeDescriptor.GetAttributes(_component);
					foreach (Attribute attr in attrs) {
						if (attr is DesignerAttribute) {
							string designerTypeName = ((DesignerAttribute) attr).DesignerTypeName;
							try {
								Type type = Type.GetType(designerTypeName);
								IDesigner designer = (IDesigner) Activator.CreateInstance(type);
								designer.Initialize(_component);
								_designer = designer;
							}
							catch (Exception e) {
								Trace.WriteLine(string.Format("Error instantiating designer {0}: {1}", designerTypeName, e.Message), "WARN");
							}
							break;
						}
					}
				}
				return _designer;
			}
		}

		public object GetService(Type serviceType) {
			
			if (_component is IServiceProvider) {
				return ((IServiceProvider) _component).GetService(serviceType);
			}
			else if (_container != null && _container is IServiceProvider) {
				return ((IServiceProvider) _container).GetService(serviceType);
			}
			else if (serviceType == typeof(IMenuCommandService)) {
				return MenuCommandService;
			}
			
			return null;
		}

		public IMenuCommandService MenuCommandService {
			get
			{
				if (_menuCommandService == null)
				{
					_menuCommandService = new DefaultMenuCommandService(Designer);
				}
				return _menuCommandService;
			}
		}
	}
}
