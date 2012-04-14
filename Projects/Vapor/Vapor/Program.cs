using System;
using Gtk;
using SteamKit2;
using Vapor.State;
using System.Threading;


namespace Vapor
{

    class Program
    {
        public static void Main( string[] args )
        {
            try
            {
                var settings = new ReadSettings().Read();
                Start( settings, args );
            }
            catch ( Exception ex )
            {
                new ErrorDialog( ex ).ShowDialog();
            }
        }

        static void Start(Vapor.State.Settings settings, string[] args)
        {
			Application.Init();
			
            bool useUdp = FindArg(args, "-udp");
            settings.Main.steam3_useUdp = useUdp;

			var login = new Login(settings);
			login.ShowAll();

            CDNCache.Initialize();

            while ( login.Visible )
            {
                //Steam3.Update();
				Application.RunIteration();

                Thread.Sleep( 1 ); // sue me, AzuiSleet.
            }

            Steam3.Shutdown();

            CDNCache.Shutdown();
        }

        static bool FindArg( string[] args, string arg )
        {
            foreach ( string potentialArg in args )
            {
                if ( potentialArg.IndexOf( arg, StringComparison.OrdinalIgnoreCase ) != -1 )
                    return true;
            }
            return false;
        }
    }
}
