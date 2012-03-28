using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vapor.State
{
    class Settings : Dictionary<String, String>
    {
        // TODO: Load color settings
        public ColorSettings ColorSettings = ColorSettings.Defaults();
    }
}
