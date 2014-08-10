﻿using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (!Active)
            {
                return;
            }
            if (collisionableObject is IScoreable)
            {
                CollisionSound.Play();
                ((IScoreable)collisionableObject).IncrementScore(Score);
            }
            Active = false;
        }
    }
}
