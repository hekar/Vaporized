using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Vapor.State
{
    class Settings
    {
        private Properties.Settings settings;
        private Properties.Color colorSettings;

        public Properties.Settings Main
        {
            get
            {
                return settings;
            }
        }

        public Properties.Color Color
        {
            get
            {
                return colorSettings;
            }
        }

        public Settings()
        {
            settings = Properties.Settings.Default;
            colorSettings = Properties.Color.Default;
        }
    }
}
