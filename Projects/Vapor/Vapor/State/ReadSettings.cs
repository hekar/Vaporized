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

            Properties.Settings.Default.Reload();

            Properties.Settings progSettings = Properties.Settings.Default;
            PropertyInfo[] props = progSettings.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string[] ignore = new string[] {
                    "Context",
                    "Properties",
                    "PropertyValues",
                    "Item",
                    "SettingsKey",
                    "IsSynchronized",
                    "Default",
                    "Providers"
                };

                if (!ignore.Contains(prop.Name))
                {
                    settings[prop.Name] = prop.GetValue(progSettings, null).ToString();
                }
            }

            return settings;
        }
    }
}
