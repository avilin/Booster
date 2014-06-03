﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.Levels.Entities
{
    public class Spike : DamageBlock, IUpdateableObject
    {
        private Range range;
        private Boolean onRangeLimit;
        private Duration onRangeLimitDuration;

        public Spike(Vector2 position)
            : base(position)
        {
            Speed = Vector2.UnitY * 0.01f;
            range = new Range((int)position.Y, (int)position.Y + 32);
            onRangeLimit = true;
            onRangeLimitDuration = new Duration(2000);
        }

        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (Position.Y == range.Max)
            {
                return;
            }
            if (collisionableObject is IDamageable)
            {
                ((IDamageable)collisionableObject).LoseLife(Damage);
            }
        }

        public Vector2 Speed { get; set; }

        public void Update(GameTime gameTime)
        {
            if (onRangeLimit)
            {
                onRangeLimit = !onRangeLimitDuration.Update(gameTime);
                return;
            }

            Position = Position + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!range.IsInRange((int)Position.Y))
            {
                float y = MathHelper.Clamp(Position.Y, range.Min, range.Max);
                Position = Vector2.UnitX * Position + Vector2.UnitY * y;
                Speed *= -1;
                onRangeLimit = true;
            }
        }
    }
}