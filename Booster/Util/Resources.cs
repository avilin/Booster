using Booster.Levels;
using Booster.Levels.Entities.CreateEntityCommand;
using Booster.Levels.Entities.EntityBuilder;
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
        public Dictionary<EntityType, ICreateEntityCommand> EntityTypeCommand { get; set; }

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

            PlayerCreator playerCreator = new PlayerCreator();
            SimpleTileCreator simpleTileCreator = new SimpleTileCreator();
            DamageObjectCreator damageObjectCreator = new DamageObjectCreator();
            ScoreObjectCreator scoreObjectCreator = new ScoreObjectCreator();

            EntityTypeCommand = new Dictionary<EntityType, ICreateEntityCommand>();
            EntityTypeCommand.Add(EntityType.Player, new CreatePlayerCommand(playerCreator));
            EntityTypeCommand.Add(EntityType.Block, new CreateBlockCommand(simpleTileCreator));
            EntityTypeCommand.Add(EntityType.Platform, new CreatePlatformCommand(simpleTileCreator));
            EntityTypeCommand.Add(EntityType.Spike, new CreateSpikeCommand(damageObjectCreator));
            EntityTypeCommand.Add(EntityType.DamageObjectLow, new CreateDamageBlockLowCommand(damageObjectCreator));
            EntityTypeCommand.Add(EntityType.DamageObjectMid, new CreateDamageBlockMidCommand(damageObjectCreator));
            EntityTypeCommand.Add(EntityType.DamageObjectHigh, new CreateDamageBlockHighCommand(damageObjectCreator));
            EntityTypeCommand.Add(EntityType.ScoreObjectLow, new CreateScoreObjectLowCommand(scoreObjectCreator));
            EntityTypeCommand.Add(EntityType.ScoreObjectMid, new CreateScoreObjectMidCommand(scoreObjectCreator));
            EntityTypeCommand.Add(EntityType.ScoreObjectHigh, new CreateScoreObjectHighCommand(scoreObjectCreator));
            EntityTypeCommand.Add(EntityType.Exit, new CreateExitCommand(simpleTileCreator));
            EntityTypeCommand.Add(EntityType.Null, new CreateNullCommand());
        }

        public void LoadSpriteSheet(string name)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, name);
            SpriteSheets[name] = spriteSheetInfo;
        }
    }
}
