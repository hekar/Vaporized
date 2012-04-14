using System;
using Gtk;

namespace Vapor
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class FriendsView : Gtk.Bin
	{
		private TreeView tree;
		
		public FriendsView ()
		{
			this.Build ();
			
			SetupDefaults();
			SetupChildren();
			AddListeners();
		}

		public void SetupDefaults ()
		{
		}

		public void SetupChildren ()
		{
			tree = new TreeView();
			
			TreeViewColumn column = new TreeViewColumn();
			column.Title = "Friends";
			tree.AppendColumn(column);
		}

		public void AddListeners ()
		{
		}
	}
}

