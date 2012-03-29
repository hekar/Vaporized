﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SteamKit2;
using Vapor.State;

namespace Vapor
{
    partial class MainForm : Form, ICallbackHandler
    {
        public bool Relog { get; private set; }

        Settings settings;

        bool shouldClose;

        bool suppressStateMsg;
        bool expectDisconnect;

        DateTime nextSort;
        Timer sortTimer;

        public MainForm(Settings settings)
        {
            this.settings = settings;
            InitializeComponent(settings);
            this.Enabled = false; // input is disabled until we login to steam3

            Steam3.AddHandler( this );

            selfControl.IsHighlighted = false;
            selfControl.BorderStyle = BorderStyle.None;
            selfControl.CanOpenProfile = true;

            selfControl.DisableDoubleClick();

            sortTimer = new Timer();
            sortTimer.Interval = 100;
            sortTimer.Tick += new EventHandler( sortTimer_Tick );
        }

        void sortTimer_Tick( object sender, EventArgs e )
        {
            if ( DateTime.Now < nextSort )
                return;

            this.Invoke( new MethodInvoker( () =>
            {
                this.UpdateFriends();
            } ) );

            sortTimer.Stop();
        }



        protected override void OnFormClosing( FormClosingEventArgs e )
        {
            Steam3.RemoveHandler( this );
            base.OnFormClosing( e );
        }

        public void HandleCallback( CallbackMsg msg )
        {
            if ( msg.IsType<SteamFriends.PersonaStateCallback>() )
            {
                var perState = ( SteamFriends.PersonaStateCallback )msg;

                if ( perState.FriendID == selfControl.Friend.SteamID )
                {
                    selfControl.UpdateFriend( selfControl.Friend );

                    suppressStateMsg = true;
                    stateComboBox.SelectedIndex = GetIndexFromState( perState.State );
                    suppressStateMsg = false;

                    return;
                }

                nextSort = DateTime.Now + TimeSpan.FromSeconds( 0.1 );
                sortTimer.Start();
            }

            if ( msg.IsType<SteamUser.LoggedOffCallback>() )
            {
                var callback = ( SteamUser.LoggedOffCallback )msg;

                Util.MsgBox( this, string.Format( "Logged off from Steam3: {0}", callback.Result ) );

                this.Relog = true;
                this.Close();

                return;
            }

            if ( msg.IsType<SteamFriends.FriendsListCallback>() )
            {
                selfControl.UpdateFriend( new Friend( Steam3.SteamUser.SteamID ) );
                this.UpdateFriends();
            }

            if ( msg.IsType<SteamUser.LoginKeyCallback>() )
            {
                Steam3.SteamFriends.SetPersonaState( EPersonaState.Online );
                this.Enabled = true;
            }

            if ( msg.IsType<SteamUser.LoggedOnCallback>() )
            {
                var logOnResp = ( SteamUser.LoggedOnCallback )msg;

                if ( logOnResp.Result == EResult.AccountLogonDenied )
                {
                    expectDisconnect = true;

                    SteamGuardDialog sgDialog = new SteamGuardDialog();

                    if ( sgDialog.ShowDialog( this ) != DialogResult.OK )
                    {
                        this.Relog = true;
                        this.Close();

                        return;
                    }

                    Steam3.AuthCode = sgDialog.AuthCode;

                    // if we got this logon response, we got disconnected, so lets reconnect
                    try
                    {
                        Steam3.Connect();
                    }
                    catch ( Steam3Exception ex )
                    {
                        Util.MsgBox( this, string.Format( "Unable to connect to Steam3: {0}", ex.Message ) );

                        this.Relog = true;
                        this.Close();

                        return;
                    }
                }
                else if ( logOnResp.Result != EResult.OK )
                {
                    Util.MsgBox( this, string.Format( "Unable to login to Steam3. Result code: {0}", logOnResp.Result ) );

                    this.Relog = true;
                    this.Close();

                    return;
                }
            }

            if ( msg.IsType<SteamFriends.FriendAddedCallback>() )
            {
                var friendAdded = ( SteamFriends.FriendAddedCallback )msg;

                if ( friendAdded.Result != EResult.OK )
                {
                    Util.MsgBox( this, "Unable to add friend! Result: " + friendAdded.Result );
                }
            }

            msg.Handle<SteamClient.DisconnectedCallback>( ( callback ) =>
                {
                    // if we expected this disconnection (cause of steamguard), we do nothing
                    if ( expectDisconnect )
                    {
                        expectDisconnect = false;
                        return;
                    }

                    Util.MsgBox( this, "Disconnected from Steam3!" );

                    this.Relog = true;
                    this.Close();

                    return;
                } );
        }

        private static int rankFriend(Friend x)
        {
            if (x.IsRequestingFriendship())
                return 0;
            else if (x.IsAcceptingFriendship())
                return 99;
            else if (x.IsInGame())
                return 1;
            else if (x.IsOnline())
                return 2;
            else if (x.GetState() == EPersonaState.Busy)
                return 3;
            else if (x.GetState() == EPersonaState.Away)
                return 4;
            else if (x.GetState() == EPersonaState.Snooze)
                return 5;

            return 10;
        }
        private static int compareFriends(Friend a, Friend b)
        {
            if (a == b)
                return 0;

            int rankA = rankFriend(a);
            int rankB = rankFriend(b);

            if (rankA < rankB)
                return -1;
            else if (rankA > rankB)
                return 1;
            else
                return a.GetName().CompareTo(b.GetName());
        }

        // sort and reflow controls
        public void UpdateFriends()
        {
            List<Friend> friendsList = GetFriends();
            friendsList.Sort( compareFriends );

            int scroll = friendsFlow.VerticalScroll.Value;

            friendsFlow.SuspendLayout();

            int clientIndex = 0;

            foreach ( Friend friend in friendsList )
            {
                FriendControl friendControl = null;
                
                foreach( FriendControl fc in friendsFlow.Controls )
                {
                    if ( fc.Friend.Equals( friend ) )
                    {
                        friendControl = fc;
                        break;
                    }
                }

                if ( friendControl == null )
                {
                    friendControl = new FriendControl(settings, friend);
                    friendsFlow.Controls.Add( friendControl );
                }
                else
                {
                    friendControl.UpdateFriend( friend );
                }

                friendsFlow.Controls.SetChildIndex( friendControl, clientIndex );
                clientIndex++;
            }

            List<FriendControl> controlsToRemove = new List<FriendControl>();
            foreach (FriendControl fc in friendsFlow.Controls)
            {
                if (!friendsList.Contains(fc.Friend))
                {
                    controlsToRemove.Add(fc);
                }
            }

            controlsToRemove.ForEach( fc => friendsFlow.Controls.Remove( fc ) );

            ResizeFriends();

            friendsFlow.ResumeLayout();
            friendsFlow.PerformLayout();
            friendsFlow.Refresh();

            friendsFlow.VerticalScroll.Value = scroll;
        }

        void ResizeFriends()
        {
            foreach ( FriendControl fc in friendsFlow.Controls )
            {
                fc.Width = this.ClientSize.Width - 6;

                if ( friendsFlow.VerticalScroll.Visible )
                    fc.Width -= 18;
            }
        }

        List<Friend> GetFriends()
        {
            List<Friend> friends = new List<Friend>();

            int friendCount = Steam3.SteamFriends.GetFriendCount();
            for ( int x = 0 ; x < friendCount ; ++x )
            {
                SteamID friendId = Steam3.SteamFriends.GetFriendByIndex( x );

                Friend friend = new Friend( friendId );
                friends.Add( friend );
            }

            return friends;
        }

        public void AddFriend()
        {
            var friendDialog = new AddFriendDialog();

            friendDialog.ShowDialog( this );
        }


        private void refreshListToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.UpdateFriends();
        }
        private void btnAddFriend_Click( object sender, EventArgs e )
        {
            this.AddFriend();
        }
        private void addFriendToolStripMenuItem_Click( object sender, EventArgs e )
        {
            this.AddFriend();
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
#if TRAY_BUILD
            if ( shouldClose || Relog )
            {
                return;
            }

            e.Cancel = true;
            this.Hide();
#endif
        }

        private void showHideToolStripMenuItem_Click( object sender, EventArgs e )
        {
            ToggleFormVisibility();
        }

        private void UpdateContextState()
        {
            showHideToolStripMenuItem.Text = (this.Visible ? "Hide" : "Show");
        }

        private void ToggleFormVisibility()
        {
            if ( this.Visible )
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.Focus();
            }
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            shouldClose = true;
            this.Close();
        }


        // translates a state combo box selection index to persona state
        EPersonaState GetStateFromIndex( int idx )
        {
            // note: this _MUST_ match the UI items
            EPersonaState[] states =
            {
                EPersonaState.Online,
                EPersonaState.Away,
                EPersonaState.Busy,
                EPersonaState.Snooze,
                EPersonaState.Offline,
            };

            if ( idx < 0 || idx >= states.Length )
            {
                return EPersonaState.Online;
            }

            return states[ idx ];
        }

        // translates persona state to state combo box selection index
        int GetIndexFromState( EPersonaState state )
        {
            switch ( state )
            {
                case EPersonaState.Online:
                    return 0;

                case EPersonaState.Away:
                    return 1;

                case EPersonaState.Busy:
                    return 2;

                case EPersonaState.Snooze:
                    return 3;

                case EPersonaState.Offline:
                    return 4;
            }

            return 0;
        }

        private void stateComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( suppressStateMsg )
            {
                return;
            }

            Steam3.SteamFriends.SetPersonaState( GetStateFromIndex( stateComboBox.SelectedIndex ) );
        }

        private void MainForm_ResizeEnd( object sender, EventArgs e )
        {
            friendsFlow.SuspendLayout();

            ResizeFriends();

            friendsFlow.ResumeLayout();
        }

        private void notifyIcon1_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            ToggleFormVisibility();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            UpdateContextState();
        }
    }
}
