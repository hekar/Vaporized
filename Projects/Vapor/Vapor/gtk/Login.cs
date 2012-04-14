using System;

namespace Vapor
{
	public partial class Login : Gtk.Window
	{
		public Login () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

