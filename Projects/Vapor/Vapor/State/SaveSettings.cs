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
            settings.Main.Save();
            settings.Color.Save();
        }
    }
}
