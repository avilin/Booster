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
        public static Player CreatePlayer(Texture2D texture, int tileSide, Vector2 position)
        {
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

        public static SimpleTile CreateBlock(Texture2D texture, int tileSide, Point mapCell)
        {
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2, mapCell.Y * tileSide + tileSide / 2);

            //SimpleTileCreator director = new SimpleTileCreator();
            //BlockBuilder builder = new BlockBuilder(position);
            //director.Construct(builder);
            //return builder.GetResult();

            Rectangle sourceRect = new Rectangle(720, 864, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);

            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            SimpleTile block = new SimpleTile(position, texture, sourceRect, box, boundingBox, CollisionTypes.Block);
            return block;
        }

        public static SimpleTile CreateOneWayPlatform(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(216, 648, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, 0);
            SimpleTile oneWayPlatform = new SimpleTile(position, texture, sourceRect, box, boundingBox, CollisionTypes.Top);
            return oneWayPlatform;
        }

        public static Spike CreateSpike(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(347, 35, 70, 35);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Spike spike = new Spike(position, texture, sourceRect, box, boundingBox, 1);
            return spike;
        }

        public static DamageBlock CreateDamageBlockLow(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(0, 216, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            DamageBlock damageBlock = new DamageBlock(position, texture, sourceRect, box, boundingBox, 1);
            return damageBlock;
        }

        public static DamageBlock CreateDamageBlockMid(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(0, 288, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            DamageBlock damageBlock = new DamageBlock(position, texture, sourceRect, box, boundingBox, 2);
            return damageBlock;
        }

        public static DamageBlock CreateDamageBlockHigh(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(0, 360, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            DamageBlock damageBlock = new DamageBlock(position, texture, sourceRect, box, boundingBox, 4);
            return damageBlock;
        }

        public static ScoreObject CreateScoreObjectLow(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(288, 432, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            ScoreObject scoreObject = new ScoreObject(position, texture, sourceRect, box, boundingBox, 1000);
            return scoreObject;
        }

        public static ScoreObject CreateScoreObjectMid(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(288, 288, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            ScoreObject scoreObject = new ScoreObject(position, texture, sourceRect, box, boundingBox, 5000);
            return scoreObject;
        }

        public static ScoreObject CreateScoreObjectHigh(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(288, 360, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            ScoreObject scoreObject = new ScoreObject(position, texture, sourceRect, box, boundingBox, 10000);
            return scoreObject;
        }

        public static ExitEntity CreateExit(Texture2D texture, int tileSide, Point mapCell)
        {
            Rectangle sourceRect = new Rectangle(288, 360, 70, 70);
            Box boundingBox = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Box box = new Box(tileSide / 2, tileSide / 2, tileSide / 2, tileSide / 2);
            Vector2 position = new Vector2(mapCell.X * tileSide + tileSide / 2,
                mapCell.Y * tileSide + tileSide / 2);
            ExitEntity exit = new ExitEntity(position, texture, sourceRect, box, boundingBox);
            return exit;
        }
    }
}
