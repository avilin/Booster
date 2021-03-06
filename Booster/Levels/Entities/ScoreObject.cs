﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Booster.Levels.Entities
{
    public class ScoreObject : SimpleTile
    {
        public int Score { get; set; }
        public SoundEffect CollisionSound { get; set; }

        public ScoreObject(Vector2 position)
            : base(position)
        {
            Score = 0;
        }

        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (!Active)
            {
                return;
            }
            if (collisionableObject is IScore)
            {
                CollisionSound.Play();
                ((IScore)collisionableObject).Score += Score;
                Active = false;
            }
        }
    }
}