﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Vapor.Properties;
using System.Linq;
using SteamKit2;

namespace Vapor
{
    partial class FriendControl : UserControl, ICallbackHandler
    {

        public Friend Friend { get; private set; }
        public byte[] AvatarHash { get; set; } // checking if update is necessary
        public bool IsHighlighted { get; set; }
        public bool CanOpenProfile { get; set; }

        bool highlighted;


        public FriendControl()
        {
            InitializeComponent();

            btnAccept.Visible = false;
            btnDeny.Visible = false;

            IsHighlighted = true;

            Steam3.AddHandler( this );

            this.MouseDoubleClick += FriendControl_MouseDoubleClick;
            this.MouseEnter += FriendControl_MouseEnter;
            this.MouseLeave += FriendControl_MouseLeave;

            foreach ( Control ctrl in this.Controls )
            {
                ctrl.MouseDoubleClick += FriendControl_MouseDoubleClick;
                ctrl.MouseEnter += FriendControl_MouseEnter;
                ctrl.MouseLeave += FriendControl_MouseLeave;
            }
        }

        ~FriendControl()
        {
            Steam3.RemoveHandler( this );
        }

        public FriendControl( Friend steamid )
            : this()
        {
            UpdateFriend( steamid );
        }

        public void DisableContextMenu()
        {
            this.ContextMenuStrip = null;
        }
        public void DisableDoubleClick()
        {
            this.MouseDoubleClick -= FriendControl_MouseDoubleClick;

            foreach ( Control ctrl in this.Controls )
                ctrl.MouseDoubleClick -= FriendControl_MouseDoubleClick;
        }


        public void HandleCallback( CallbackMsg msg )
        {
            if ( msg.IsType<SteamFriends.PersonaStateCallback>() )
            {
                var perState = ( SteamFriends.PersonaStateCallback )msg;

                if ( this.Friend == null )
                    return;

                if ( perState.FriendID != this.Friend.SteamID )
                    return;

                this.UpdateFriend( this.Friend );
            }
        }

        void AvatarDownloaded( AvatarDownloadDetails details )
        {
            try
            {
                if (avatarBox.InvokeRequired)
                {
                    avatarBox.Invoke(new MethodInvoker(() =>
                        {
                            AvatarDownloaded(details);
                        }
                    ));
                }
                else
                {
                    avatarBox.Image = ComposeAvatar(this.Friend, (details.Success ? details.Filename : null));
                }
            }
            catch ( Exception ex )
            {
                DebugLog.WriteLine( "FriendControl", "Unable to compose avatar: {0}", ex.Message );
            }
        }

        public void UpdateFriend( Friend steamid )
        {
            Friend = steamid;

            nameLbl.Text = steamid.GetName();
            statusLbl.Text = steamid.GetStatus();
            gameLbl.Text = steamid.GetGameName();

            if ( steamid.IsRequestingFriendship() )
            {
                btnAccept.Visible = true;
                btnDeny.Visible = true;
            }
            else
            {
                btnAccept.Visible = false;
                btnDeny.Visible = false;
            }

            nameLbl.ForeColor = statusLbl.ForeColor = gameLbl.ForeColor = Util.GetStatusColor( steamid );

            byte[] avatarHash = Steam3.SteamFriends.GetFriendAvatar( steamid.SteamID );
            bool validHash = avatarHash != null && !Util.IsZeros( avatarHash );

            if ((AvatarHash == null && !validHash && avatarBox.Image != null) || (AvatarHash != null && AvatarHash.SequenceEqual(avatarHash)))
            {
                // avatar is already up to date, no operations necessary
            }
            else if ( validHash )
            {
                AvatarHash = avatarHash;
                CDNCache.DownloadAvatar(steamid.SteamID, avatarHash, AvatarDownloaded);
            }
            else
            {
                AvatarHash = null;
                avatarBox.Image = ComposeAvatar( this.Friend, null );
            }
        }


        void Highlight()
        {
            if ( !this.IsHighlighted )
                return;

            if ( this.highlighted )
                return;

            this.BackColor = Color.FromArgb( 38, 38, 39 );

            highlighted = true;
        }
        void UnHighlight()
        {
            if ( !this.IsHighlighted )
                return;

            if ( !this.highlighted )
                return;

            this.BackColor = Color.FromArgb( 58, 58, 58 );

            highlighted = false;
        }


        Bitmap GetHolder( Friend steamid )
        {
            if ( steamid.IsInGame() )
                return Resources.IconIngame;

            if ( steamid.IsOnline() )
                return Resources.IconOnline;

            return Resources.IconOffline;
        }
        Bitmap GetAvatar( string path )
        {
            try
            {
                if (path == null)
                    return Resources.IconUnknown;
                return ( Bitmap )Bitmap.FromFile( path );
            }
            catch
            {
                return Resources.IconUnknown;
            }
        }

        Bitmap ComposeAvatar( Friend steamid, string path )
        {
            Bitmap holder = GetHolder( steamid );
            Bitmap avatar = GetAvatar( path );

            Graphics gfx = null;
            try
            {
                gfx = Graphics.FromImage( holder );
                gfx.DrawImage( avatar, new Rectangle( 4, 4, avatar.Width, avatar.Height ) );
            }
            finally
            {
                gfx.Dispose();
            }

            return holder;
        }

        void FriendControl_MouseEnter( object sender, EventArgs e )
        {
            this.Highlight();
        }
        void FriendControl_MouseLeave( object sender, EventArgs e )
        {
            this.UnHighlight();
        }

        void FriendControl_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            Steam3.ChatManager.GetChat( this.Friend.SteamID );
        }

        private void btnAccept_Click( object sender, EventArgs e )
        {
            Steam3.SteamFriends.AddFriend( this.Friend.SteamID );
        }

        private void btnDeny_Click( object sender, EventArgs e )
        {
            Steam3.SteamFriends.RemoveFriend( this.Friend.SteamID );
        }

        private void removeFriendToolStripMenuItem_Click( object sender, EventArgs e )
        {
            DialogResult result = Util.MsgBox(
                this,
                string.Format( "Are you sure you wish to remove {0} from your friends list?", this.Friend.GetName() ),
                MessageBoxButtons.YesNo
            );

            if ( result != DialogResult.Yes )
                return;

            Steam3.SteamFriends.RemoveFriend( this.Friend.SteamID );
        }

        private void addFriendToolStripMenuItem_Click( object sender, EventArgs e )
        {
            MainForm mf = this.ParentForm as MainForm;
            mf.AddFriend();
        }

        private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
        {
            MainForm mf = this.ParentForm as MainForm;
            //mf.ReloadFriends();
        }

        private void avatarBox_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            if ( this.CanOpenProfile )
            {
                Util.OpenProfile( this.Friend.SteamID );
            }
        }

        private void viewProfileToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Util.OpenProfile( this.Friend.SteamID );
        }

        private void vaporContextMenu1_Opening( object sender, CancelEventArgs e )
        {
            viewProfileToolStripMenuItem.Text = string.Format( "View {0}'s Profile", this.Friend.GetName() );
        }
        
    }
}