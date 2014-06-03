using Booster.Levels;
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

        public Dictionary<string, EntityType> StringType { get; set; }

        public Resources(Game game)
        {
            this.game = game;
            SpriteSheets = new Dictionary<string, SpriteSheetInfo>();

            LoadSpriteSheet("p1");
            LoadSpriteSheet("tiles");
            LoadSpriteSheet("items");
            LoadSpriteSheet("hud");

            StringType = new Dictionary<string, EntityType>();
            StringType.Add("PLA", EntityType.Player);
            StringType.Add("BLO", EntityType.Block);
            StringType.Add("OWP", EntityType.Platform);
            StringType.Add("SPK", EntityType.Spike);
            StringType.Add("DBL", EntityType.DamageObjectLow);
            StringType.Add("DBM", EntityType.DamageObjectMid);
            StringType.Add("DBH", EntityType.DamageObjectHigh);
            StringType.Add("SOL", EntityType.ScoreObjectLow);
            StringType.Add("SOM", EntityType.ScoreObjectMid);
            StringType.Add("SOH", EntityType.ScoreObjectHigh);
            StringType.Add("EXT", EntityType.Exit);
        }

        public void LoadSpriteSheet(string name)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, name);
            SpriteSheets[name] = spriteSheetInfo;
        }
    }
}
