using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public class ScoreObject : StaticEntity, ICollisionableObject
    {
        public int Score { get; set; }

        public CollisionTypes CollisionType
        {
            get
            {
                return CollisionTypes.None;
            }
        }

        public Box BoundingBox { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return BoundingBox.BoxInPosition(Position);
            }
        }

        public ScoreObject(Vector2 position, Texture2D texture, Rectangle sourceRect, Box box, Box boundingBox, int score)
            : base(position, texture, sourceRect, box)
        {
            BoundingBox = boundingBox;
            Score = score;
        }

        public void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IScoreable)
            {
                ((IScoreable)collisionableObject).IncrementScore(Score);
            }
            Active = false;
        }
    }
}
