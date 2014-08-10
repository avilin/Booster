using Booster.Levels;
using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityFactoryMethod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
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

        public Dictionary<string, SoundEffect> SoundEffects { get; set; }
        public Dictionary<string, SoundEffectInstance> Songs { get; set; }

        public Dictionary<string, EntityType> StringType { get; set; }
        public Dictionary<EntityType, EntityCreator> EntityTypeCreator { get; set; }

        public Dictionary<string, Texture2D> Backgrounds { get; set; }

        public Resources(Game game)
        {
            this.game = game;

            LoadGraphics();
            LoadSounds();
            LoadFactortMethod();
        }

        public void LoadGraphics()
        {
            SpriteSheets = new Dictionary<string, SpriteSheetInfo>();

            LoadSpriteSheet("p1");
            LoadSpriteSheet("tiles");
            LoadSpriteSheet("items");
            LoadSpriteSheet("hud");

            Backgrounds = new Dictionary<string, Texture2D>();
            Backgrounds.Add("level_background", game.Content.Load<Texture2D>(@"Graphics\level_background"));
        }

        public void LoadSpriteSheet(string spriteSheetName)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, spriteSheetName);
            SpriteSheets[spriteSheetName] = spriteSheetInfo;
        }

        public void LoadSounds()
        {
            SoundEffects = new Dictionary<string, SoundEffect>();
            SoundEffects["coin"] = game.Content.Load<SoundEffect>(@"Sounds\coin.wav");
            SoundEffects["hit"] = game.Content.Load<SoundEffect>(@"Sounds\hit.wav");
            SoundEffects["jump"] = game.Content.Load<SoundEffect>(@"Sounds\jump.wav");
            SoundEffects["menu"] = game.Content.Load<SoundEffect>(@"Sounds\menu.wav");

            Songs = new Dictionary<string, SoundEffectInstance>();
            Songs["menu_music"] = game.Content.Load<SoundEffect>(@"Sounds\menu_music.wav").CreateInstance();
            Songs["menu_music"].IsLooped = true;
            Songs["level_music"] = game.Content.Load<SoundEffect>(@"Sounds\level_music.wav").CreateInstance();
            Songs["level_music"].IsLooped = true;
        }

        public void LoadFactortMethod()
        {
            StringType = new Dictionary<string, EntityType>();
            StringType.Add("PLA", EntityType.Player);
            StringType.Add("BLO", EntityType.Block);
            StringType.Add("BLC", EntityType.BlockCenter);
            StringType.Add("BLM", EntityType.BlockMid);
            StringType.Add("BLL", EntityType.BlockLeft);
            StringType.Add("BLR", EntityType.BlockRight);
            StringType.Add("OWP", EntityType.Platform);
            StringType.Add("OWM", EntityType.PlatformMid);
            StringType.Add("OWL", EntityType.PlatformLeft);
            StringType.Add("OWR", EntityType.PlatformRight);
            StringType.Add("SPK", EntityType.Spike);
            StringType.Add("DBL", EntityType.DamageObjectLow);
            StringType.Add("DBM", EntityType.DamageObjectMid);
            StringType.Add("DBH", EntityType.DamageObjectHigh);
            StringType.Add("SOL", EntityType.ScoreObjectLow);
            StringType.Add("SOM", EntityType.ScoreObjectMid);
            StringType.Add("SOH", EntityType.ScoreObjectHigh);
            StringType.Add("DOR", EntityType.Door);
            StringType.Add("KEY", EntityType.Key);
            StringType.Add("EXT", EntityType.Exit);

            EntityTypeCreator = new Dictionary<EntityType, EntityCreator>();
            EntityTypeCreator.Add(EntityType.Player, new PlayerCreator());
            EntityTypeCreator.Add(EntityType.Block, new BlockCreator());
            EntityTypeCreator.Add(EntityType.BlockCenter, new BlockCenterCreator());
            EntityTypeCreator.Add(EntityType.BlockMid, new BlockMidCreator());
            EntityTypeCreator.Add(EntityType.BlockLeft, new BlockLeftCreator());
            EntityTypeCreator.Add(EntityType.BlockRight, new BlockRightCreator());
            EntityTypeCreator.Add(EntityType.Platform, new PlatformCreator());
            EntityTypeCreator.Add(EntityType.PlatformMid, new PlatformMidCreator());
            EntityTypeCreator.Add(EntityType.PlatformLeft, new PlatformLeftCreator());
            EntityTypeCreator.Add(EntityType.PlatformRight, new PlatformRightCreator());
            EntityTypeCreator.Add(EntityType.Spike, new SpikeCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectLow, new DamageBlockLowCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectMid, new DamageBlockMidCreator());
            EntityTypeCreator.Add(EntityType.DamageObjectHigh, new DamageBlockHighCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectLow, new ScoreObjectLowCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectMid, new ScoreObjectMidCreator());
            EntityTypeCreator.Add(EntityType.ScoreObjectHigh, new ScoreObjectHighCreator());
            EntityTypeCreator.Add(EntityType.Door, new DoorCreator());
            EntityTypeCreator.Add(EntityType.Key, new KeyCreator());
            EntityTypeCreator.Add(EntityType.Exit, new ExitCreator());
            EntityTypeCreator.Add(EntityType.Null, new NullEntityCreator());
        }
    }
}
