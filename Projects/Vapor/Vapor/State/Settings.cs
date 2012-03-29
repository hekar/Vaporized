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
        

        public string this[string index]
        {
            get
            {
                return settings.Properties[index].ToString();
            }

            set
            {
                PropertyInfo[] props = settings.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == index)
                    {
                        prop.SetValue(settings, value, null);
                    }
                }
            }
        }
    }
}
