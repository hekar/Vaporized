using System;
using Gtk;
using Vapor.State;

namespace Vapor
{
	public partial class Login : Gtk.Window
	{
		private Label usernameLabel;
		private Entry username;
		private Label passwordLabel;
		private Entry password;
		private CheckButton savePassword;
		private Button ok;
		private Button cancel;
		private Vapor.State.Settings settings;
		
		public Login (Vapor.State.Settings settings) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			this.settings = settings;
			
			SetupDefaults ();
			SetupChildren ();
			AddListeners ();
		}

		public void SetupDefaults ()
		{
			Title = "Login";
		}

		public void SetupChildren ()
		{
			VBox l = new VBox (false, 7);
			Add (l);
			
			usernameLabel = new Label ();
			usernameLabel.Justify = Justification.Left;
			usernameLabel.Text = "Username";
			
			passwordLabel = new Label ();
			passwordLabel.Justify = Justification.Left;
			passwordLabel.Text = "Password";
			
			username = new Entry ();
			
			password = new Entry ();
			password.Visibility = false;
			
			savePassword = new CheckButton("Save Password");
			
			ok = new Button ();
			ok.Label = "OK";
			
			cancel = new Button ();
			cancel.Label = "Cancel";
			
			l.PackStart (usernameLabel, false, false, 0);
			l.PackStart (username, false, true, 2);
			l.PackStart (passwordLabel, false, false, 0);
			l.PackStart (password, false, true, 2);
			l.PackStart (savePassword, false, false, 2);
			l.PackStart (ok, false, false, 0);
			l.PackStart (cancel, false, false, 0);
			
			Default = ok;
			ActivateDefault();
		}
		
		public void AddListeners ()
		{
			ok.Clicked += HandleOkClicked;
			cancel.Clicked += HandleCancelClicked;
		}

		void HandleOkClicked (object sender, EventArgs e)
		{
            try
            {
                Steam3.UserName = username.Text;
                Steam3.Password = password.Text;

                Steam3.Initialize(settings);

                Steam3.Connect();
            }
            catch ( Steam3Exception ex )
            {
				Console.WriteLine(ex.StackTrace);
                //Util.MsgBox( this, "Unable to login to Steam3: " + ex.Message );
                return;
            }

            settings.Main.username = Steam3.UserName;
            var save = new SaveSettings();
            save.Save(settings);
			
			var dialog = new Friends();
			dialog.ShowAll();
		}
		
		void HandleCancelClicked (object sender, EventArgs e)
		{
			Dispose ();
		}
	}
}

