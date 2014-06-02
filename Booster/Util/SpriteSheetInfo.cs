using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util
{
    public class SpriteSheetInfo
    {
        public Texture2D SpriteSheet { get; set; }
        public Dictionary<string, Rectangle> ObjectLocation { get; set; }

        public SpriteSheetInfo(string file)
        {

        }
    }
}
