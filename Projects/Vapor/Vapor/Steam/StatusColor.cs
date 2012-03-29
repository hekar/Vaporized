using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Vapor.State
{
    class StatusColor
    {
        private Settings settings;

        public StatusColor(Settings settings)
        {
            this.settings = settings;
        }

        public Color GetStatusColor(Friend steamid)
        {
            Color inGame = this.settings.Color.StatusIngameFore;
            Color online = this.settings.Color.StatusOnlineFore;
            Color offline = this.settings.Color.StatusOfflineFore;
            Color blocked = this.settings.Color.StatusBlockedFore;
            Color invited = this.settings.Color.StatusInvitedFore;
            Color requesting = this.settings.Color.StatusInvitedBack;

            if (steamid.IsAcceptingFriendship())
            {
                return invited;
            }
            else if (steamid.IsRequestingFriendship())
            {
                return requesting;
            }
            else if (steamid.IsBlocked())
            {
                return blocked;
            }
            else if (steamid.IsInGame())
            {
                return inGame;
            }
            else if (!steamid.IsOnline())
            {
                return offline;
            }
            
            return online;
        }
    }
}
