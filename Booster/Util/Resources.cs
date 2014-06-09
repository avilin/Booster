using Booster.Levels;
using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityFactoryMethod;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Booster.Util
{
    public class Resources
    {
        private Game game;
        public Dictionary<string, SpriteSheetInfo> SpriteSheets { get; set; }

        public Dictionary<string, EntityType> StringType { get; set; }
        public Dictionary<EntityType, EntityCreator> EntityTypeCreator { get; set; }

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

            EntityTypeCreator = new Dictionary<EntityType, EntityCreator>();
            EntityTypeCreator.Add(EntityType.Player, new PlayerCreator());
            EntityTypeCreator.Add(EntityType.Block, new BlockCreator());
            EntityTypeCreator.Add(EntityType.Platform, new PlatformCreator());
            EntityTypeCreator.Add(EntityType.Spike, new SpikeCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectLow, new DamageBlockLowCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectMid, new DamageBlockMidCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectHigh, new DamageBlockHighCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectLow, new ScoreObjectLowCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectMid, new ScoreObjectMidCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectHigh, new ScoreObjectHighCreator());
            EntityTypeCreator.Add(EntityType.Exit, new ExitCreator());
            EntityTypeCreator.Add(EntityType.Null, new NullEntityCreator());
        }

        public void LoadSpriteSheet(string spriteSheetName)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, spriteSheetName);
            SpriteSheets[spriteSheetName] = spriteSheetInfo;
        }
    }
}
