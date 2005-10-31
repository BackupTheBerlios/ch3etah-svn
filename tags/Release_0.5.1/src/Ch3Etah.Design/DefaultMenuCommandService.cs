using System.ComponentModel.Design;

namespace Ch3Etah.Design
{
	public class DefaultMenuCommandService: IMenuCommandService
	{
		private readonly IDesigner _designer;

		public DefaultMenuCommandService(IDesigner designer)
		{
			_designer = designer;
		}
		
		#region IMenuCommandService Members

		public void AddCommand(MenuCommand command) {
			// TODO:  Add DefaultMenuCommandService.AddCommand implementation
		}

		public void RemoveVerb(DesignerVerb verb) {
			// TODO:  Add DefaultMenuCommandService.RemoveVerb implementation
		}

		public void RemoveCommand(MenuCommand command) {
			// TODO:  Add DefaultMenuCommandService.RemoveCommand implementation
		}

		public MenuCommand FindCommand(CommandID commandID) {
			// TODO:  Add DefaultMenuCommandService.FindCommand implementation
			return null;
		}

		public bool GlobalInvoke(CommandID commandID) {
			// TODO:  Add DefaultMenuCommandService.GlobalInvoke implementation
			return false;
		}

		public void ShowContextMenu(CommandID menuID, int x, int y) {
			// TODO:  Add DefaultMenuCommandService.ShowContextMenu implementation
		}

		public void AddVerb(DesignerVerb verb) {
			// TODO:  Add DefaultMenuCommandService.AddVerb implementation
		}

		public DesignerVerbCollection Verbs {
			get {
				return _designer.Verbs;
			}
		}

		#endregion
	}
}