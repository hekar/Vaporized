using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Vapor.State
{
    class SaveSettings
    {
        public void Save(Settings settings)
        {
            foreach (var item in settings)
            {
                var prop = new SettingsPropertyValue(new SettingsProperty(item.Key));
                prop.PropertyValue = item.Value;
            }

            Properties.Settings progSettings = Properties.Settings.Default;
            PropertyInfo[] props = progSettings.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (settings.ContainsKey(prop.Name))
                {
                    prop.SetValue(progSettings, settings[prop.Name], null);
                }
            }

            Properties.Settings.Default.Save();
        }
    }
}
