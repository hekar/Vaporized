﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SteamKit2;
using Vapor.State;


namespace Vapor
{

    class Program
    {
        public static void Main( string[] args )
        {
            if ( FindArg( args, "-debug" ) )
            {
                DebugLog.AddListener( new ConsoleDebugListener() );

                TraceDialog td = new TraceDialog();
                td.Show();

                FileTrace ft = new FileTrace();
            }

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

        static void Start( Settings settings, string[] args )
        {
            LoginDialog ld = new LoginDialog( settings, FindArg( args, "-udp" ) );

            if ( ld.ShowDialog() != DialogResult.OK )
                return;

            CDNCache.Initialize();

            MainForm mf = new MainForm();
            mf.Show();

            while ( mf.Created )
            {
                Steam3.Update();
                Application.DoEvents();

                Thread.Sleep( 1 ); // sue me, AzuiSleet.
            }

            Steam3.Shutdown();

            CDNCache.Shutdown();

            if ( mf.Relog )
            {
                try
                {
                    Start( settings, args );
                }
                catch ( Exception ex )
                {
                    new ErrorDialog( ex ).ShowDialog();
                }
            }
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
