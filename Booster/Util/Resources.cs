using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util
{
    public class Resources
    {
        private static Resources instance = null;

        public Dictionary<string, SpriteSheetInfo> SpriteSheets { get; set; }

        public static Resources GetInstance()
        {
            if (instance == null)
            {
                instance = new Resources();
            }

            return instance;
        }

        private Resources()
        {

        }
    }
}
