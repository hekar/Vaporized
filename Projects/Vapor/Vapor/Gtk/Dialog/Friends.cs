using System;
using Gtk;

namespace Vapor
{
	public partial class Friends : Gtk.Window
	{
		private MenuBar menubar;
		private Toolbar toolbar;
		private Statusbar statusbar;
		private TreeView friends;
		
		public Friends () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			SetupDefaults ();
			SetupChildren ();
			AddListeners ();
		}

		public void SetupDefaults ()
		{
			Title = "Friends";
		}

		public void SetupChildren ()
		{
			VBox l = new VBox (false, 0);
			Add (l);
			
			menubar = CreateMenu ();
			toolbar = CreateToolbar ();
			statusbar = CreateStatusBar ();
			
			friends = CreateFriends ();
			
			l.PackStart (menubar, false, false, 0);
			l.PackStart (toolbar);
			l.PackStart (statusbar);
		}

		public void AddListeners ()
		{
		}

		private Gtk.MenuBar CreateMenu ()
		{
			MenuBar menu = new MenuBar ();
			
			// File
			Menu fileSub = new Menu ();
			MenuItem file = new MenuItem ("File");
			file.Submenu = fileSub;
			
			MenuItem login = new MenuItem ("Login");
			MenuItem exit = new MenuItem ("Exit");
			
			fileSub.Append (login);
			fileSub.Append (exit);
			
			// Options
			Menu optionSub = new Menu ();
			MenuItem option = new MenuItem ("Options");
			option.Submenu = optionSub;
			
			MenuItem preferences = new MenuItem ("Preferences");
			
			optionSub.Append (preferences);
			
			// Help
			Menu helpSub = new Menu ();
			MenuItem help = new MenuItem ("Help");
			help.Submenu = helpSub;
			
			MenuItem about = new MenuItem ("About");
			
			helpSub.Append (about);
			
			// Menubar
			menu.Append (file);
			menu.Append (option);
			menu.Append (help);
			
			return menu;
		}

		private Gtk.Toolbar CreateToolbar ()
		{
			
			Toolbar toolbar = new Toolbar ();
			
			return toolbar;
		}
		
		private Statusbar CreateStatusBar ()
		{
			Statusbar statusbar = new Statusbar ();
			
			
			return statusbar;
		}
		
		private	TreeView CreateFriends ()
		{
			TreeView tree = new TreeView();
			
			return tree;
		}
	}
}

