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
            PlayerCreator director = new PlayerCreator();
            PlayerBuilder builder = new PlayerBuilder(resources, position);
            director.Contruct(builder);
            return builder.GetResult();
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
            DamageObjectMidBuilder builder = new DamageObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        public static DamageBlock CreateDamageBlockHigh(Resources resources, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectHighBuilder builder = new DamageObjectHighBuilder(resources, position);
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
