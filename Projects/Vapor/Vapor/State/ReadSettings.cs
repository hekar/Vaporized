using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Vapor.State
{
    class ReadSettings
    {
        public Settings Read()
        {
            Settings settings = new Settings();
            settings.Main.Reload();
            settings.Color.Reload();

            return settings;
        }
    }
}
