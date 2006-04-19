using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace Ch3Etah.Design {
	
	public class ExtendedVerb : DesignerVerb {
		
		private readonly Image icon;
		private readonly bool beginMenuGroup;

		public ExtendedVerb(string VerbText, Image Icon, EventHandler Handler) : this(VerbText, Icon, false, Handler) {}
			
		public ExtendedVerb(string VerbText, Image Icon, bool BeginGroup, EventHandler Handler) : base(VerbText, Handler) {
			this.icon = Icon;
			this.beginMenuGroup = BeginGroup;
		}

		public Image Icon {
			get { return icon; }
		}

		public bool BeginMenuGroup {
			get { return beginMenuGroup; }
		}
	}
}