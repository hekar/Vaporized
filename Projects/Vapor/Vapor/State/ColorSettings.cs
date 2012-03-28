using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Vapor.State
{
    class ColorSettings : Dictionary<String, Color>
    {
        public static ColorSettings Defaults()
        {
            var c = new ColorSettings();

            c["status.ingame.fore"] = Color.FromArgb(177, 251, 80);
            c["status.online.fore"] = Color.FromArgb(111, 189, 255);
            c["status.offline.fore"] = Color.FromArgb(137, 137, 137);
            c["status.blocked.fore"] = Color.FromArgb(251, 80, 80);
            c["status.invited.fore"] = Color.FromArgb(250, 218, 94);
            c["status.requesting.fore"] = Color.FromArgb(135, 169, 107);

            c["friends.back"] = Color.FromArgb(5, 5, 5);
            c["friends.fore"] = Color.FromArgb(5, 5, 5);
            c["friends.high"] = Color.FromArgb(5, 5, 5);

            return c;
        }
    }
}
