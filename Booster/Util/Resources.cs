using Booster.Levels;
using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityFactoryMethod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Booster.Util
{
    public class Resources
    {
        private Game game;
        public SpriteFont SpriteFont { get; set; }
        public Dictionary<string, SpriteSheetInfo> SpriteSheets { get; set; }

        public Dictionary<string, SoundEffect> SoundEffects { get; set; }
        public Dictionary<string, Song> Songs { get; set; }

        public Dictionary<string, EntityType> StringType { get; set; }
        public Dictionary<EntityType, EntityCreator> EntityTypeCreator { get; set; }

        public Dictionary<string, Texture2D> Backgrounds { get; set; }

        public Resources(Game game)
        {
            this.game = game;
            SpriteFont = game.Content.Load<SpriteFont>(@"Fonts\menu");
            LoadGraphics();
            LoadSounds();
            LoadFactoryMethod();
        }

        private void LoadGraphics()
        {
            SpriteSheets = new Dictionary<string, SpriteSheetInfo>();

            LoadSpriteSheet("p1");
            LoadSpriteSheet("tiles");
            LoadSpriteSheet("items");
            LoadSpriteSheet("hud");
            LoadSpriteSheet("enemies");

            Backgrounds = new Dictionary<string, Texture2D>();
            Backgrounds.Add("level_background", game.Content.Load<Texture2D>(@"Graphics\level_background"));
            Backgrounds.Add("intro_background", game.Content.Load<Texture2D>(@"Graphics\intro_background"));
            Backgrounds.Add("menu_background", game.Content.Load<Texture2D>(@"Graphics\menu_background"));
            Backgrounds.Add("gameover_background", game.Content.Load<Texture2D>(@"Graphics\gameover_background"));
        }

        private void LoadSpriteSheet(string spriteSheetName)
        {
            SpriteSheetInfo spriteSheetInfo = new SpriteSheetInfo(game, spriteSheetName);
            SpriteSheets[spriteSheetName] = spriteSheetInfo;
        }

        private void LoadSounds()
        {
            SoundEffects = new Dictionary<string, SoundEffect>();
            SoundEffects["coin"] = game.Content.Load<SoundEffect>(@"Sounds\coin");
            SoundEffects["hit"] = game.Content.Load<SoundEffect>(@"Sounds\hit");
            SoundEffects["jump"] = game.Content.Load<SoundEffect>(@"Sounds\jump");
            SoundEffects["boost"] = game.Content.Load<SoundEffect>(@"Sounds\boost");
            SoundEffects["menu"] = game.Content.Load<SoundEffect>(@"Sounds\menu");
            SoundEffects["door"] = game.Content.Load<SoundEffect>(@"Sounds\door");
            SoundEffects["key"] = game.Content.Load<SoundEffect>(@"Sounds\key");

            Songs = new Dictionary<string, Song>();
            Songs["menu_music"] = game.Content.Load<Song>(@"Sounds\menu_music.wav");
            //Songs["menu_music"].IsLooped = true;
            Songs["level_music"] = game.Content.Load<Song>(@"Sounds\level_music.wav");
            //Songs["level_music"].IsLooped = true;
        }

        private void LoadFactoryMethod()
        {
            StringType = new Dictionary<string, EntityType>();
            StringType.Add("221", EntityType.Player);
            StringType.Add("29", EntityType.Block);
            StringType.Add("155", EntityType.BlockCenter);
            StringType.Add("135", EntityType.BlockMid);
            StringType.Add("147", EntityType.BlockLeft);
            StringType.Add("123", EntityType.BlockRight);
            StringType.Add("112", EntityType.Platform);
            StringType.Add("88", EntityType.PlatformMid);
            StringType.Add("100", EntityType.PlatformLeft);
            StringType.Add("76", EntityType.PlatformRight);
            StringType.Add("162", EntityType.Spike);
            StringType.Add("61", EntityType.DamageObjectLow);
            StringType.Add("8", EntityType.Lava);
            StringType.Add("139", EntityType.LavaTop);
            StringType.Add("44", EntityType.DamageObjectHigh);
            StringType.Add("209", EntityType.ScoreObjectLow);
            StringType.Add("193", EntityType.ScoreObjectMid);
            StringType.Add("201", EntityType.ScoreObjectHigh);
            StringType.Add("55", EntityType.Door);
            StringType.Add("198", EntityType.Key);
            StringType.Add("65", EntityType.Exit);

            PlayerDirector playerDirector = new PlayerDirector();
            SimpleTileDirector simpleTileDirector = new SimpleTileDirector();
            ScoreObjectDirector scoreObjectDirector = new ScoreObjectDirector();
            DamageObjectDirector damageObjectDirector = new DamageObjectDirector();

            EntityTypeCreator = new Dictionary<EntityType, EntityCreator>();
            EntityTypeCreator.Add(EntityType.Player, new PlayerCreator(playerDirector));
            EntityTypeCreator.Add(EntityType.Block, new BlockCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.BlockCenter, new BlockCenterCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.BlockMid, new BlockMidCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.BlockLeft, new BlockLeftCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.BlockRight, new BlockRightCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.Platform, new PlatformCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.PlatformMid, new PlatformMidCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.PlatformLeft, new PlatformLeftCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.PlatformRight, new PlatformRightCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.Spike, new SpikeCreator(damageObjectDirector));
            EntityTypeCreator.Add(EntityType.DamageObjectLow, new DamageBlockLowCreator(damageObjectDirector));
            EntityTypeCreator.Add(EntityType.Lava, new LavaCreator(damageObjectDirector));
            EntityTypeCreator.Add(EntityType.LavaTop, new LavaTopCreator(damageObjectDirector));
            EntityTypeCreator.Add(EntityType.DamageObjectHigh, new DamageBlockHighCreator(damageObjectDirector));
            EntityTypeCreator.Add(EntityType.ScoreObjectLow, new ScoreObjectLowCreator(scoreObjectDirector));
            EntityTypeCreator.Add(EntityType.ScoreObjectMid, new ScoreObjectMidCreator(scoreObjectDirector));
            EntityTypeCreator.Add(EntityType.ScoreObjectHigh, new ScoreObjectHighCreator(scoreObjectDirector));
            EntityTypeCreator.Add(EntityType.Door, new DoorCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.Key, new KeyCreator(simpleTileDirector));
            EntityTypeCreator.Add(EntityType.Exit, new ExitCreator(simpleTileDirector));
        }
    }
}