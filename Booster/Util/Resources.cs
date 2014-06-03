using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util
{
    public class Resources
    {
        private Game game;
        public Dictionary<string, SpriteSheetInfo> SpriteSheets { get; set; }

        public Resources(Game game)
        {
            this.game = game;
            SpriteSheets = new Dictionary<string, SpriteSheetInfo>();

            LoadSpriteSheet("p1");
            LoadSpriteSheet("tiles");
            LoadSpriteSheet("items");
            LoadSpriteSheet("hud");
        }

        public void LoadSpriteSheet(string name)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, name);
            SpriteSheets[name] = spriteSheetInfo;
        }
    }
}
