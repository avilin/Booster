using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Booster.Levels
{
    public class EntityFactory
    {
        public static Player CreatePlayer(Resources resources, int tileSide, Vector2 position)
        {
            Texture2D texture = resources.SpriteSheets["p1"].SpriteSheet;
            List<Frame> frames = new List<Frame>();
            frames.Add(new Frame(new Rectangle(67, 196, 66, 92), 1000));
            Box box = new Box(20, tileSide, 20, tileSide);
            IAnimationBuilder animationBuilder = new AnimationBuilder(texture, frames, box);
            animationBuilder.BuildPosition(position);
            Dictionary<String, Animation> playerAnimations = new Dictionary<string, Animation>();
            playerAnimations.Add("default", animationBuilder.GetProduct());

            frames = new List<Frame>();
            frames.Add(new Frame(new Rectangle(0, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(73, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(146, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(0, 98, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(73, 98, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(146, 98, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(219, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(292, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(219, 98, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(365, 0, 72, 97), 50));
            frames.Add(new Frame(new Rectangle(292, 98, 72, 97), 50));
            animationBuilder = new AnimationBuilder(texture, frames, box);
            animationBuilder.BuildPosition(position);
            playerAnimations.Add("move", animationBuilder.GetProduct());

            frames = new List<Frame>();
            frames.Add(new Frame(new Rectangle(438, 0, 69, 92), 1000));
            animationBuilder = new AnimationBuilder(texture, frames, box);
            animationBuilder.BuildPosition(position);
            playerAnimations.Add("hurt", animationBuilder.GetProduct());

            frames = new List<Frame>();
            frames.Add(new Frame(new Rectangle(365, 98, 69, 71), 1000));
            animationBuilder = new AnimationBuilder(texture, frames, box);
            animationBuilder.BuildPosition(position);
            playerAnimations.Add("dead", animationBuilder.GetProduct());

            frames = new List<Frame>();
            frames.Add(new Frame(new Rectangle(438, 93, 67, 94), 1000));
            animationBuilder = new AnimationBuilder(texture, frames, box);
            animationBuilder.BuildPosition(position);
            playerAnimations.Add("onAir", animationBuilder.GetProduct());

            Vector2 playerPosition = position;
            Box hitBoxOffSetFromPosition = new Box(tileSide / 2, tileSide, tileSide / 2, tileSide);
            Player player = new Player(playerPosition, playerAnimations, hitBoxOffSetFromPosition);
            return player;
        }

        public static SimpleTile CreateBlock(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            SimpleTileCreator director = new SimpleTileCreator();
            BlockBuilder builder = new BlockBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static SimpleTile CreateOneWayPlatform(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            SimpleTileCreator director = new SimpleTileCreator();
            PlatformBuilder builder = new PlatformBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static Spike CreateSpike(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectSpikeBuilder builder = new DamageObjectSpikeBuilder(resources, position);
            director.Construct(builder);
            return (Spike)builder.GetResult();
        }

        public static DamageBlock CreateDamageBlockLow(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectLowBuilder builder = new DamageObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static DamageBlock CreateDamageBlockMid(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectLowBuilder builder = new DamageObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static DamageBlock CreateDamageBlockHigh(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectLowBuilder builder = new DamageObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static ScoreObject CreateScoreObjectLow(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);

            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectLowBuilder builder = new ScoreObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static ScoreObject CreateScoreObjectMid(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);

            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectMidBuilder builder = new ScoreObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static ScoreObject CreateScoreObjectHigh(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);

            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectHighBuilder builder = new ScoreObjectHighBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static SimpleTile CreateExit(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);

            SimpleTileCreator director = new SimpleTileCreator();
            ExitBuilder builder = new ExitBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
