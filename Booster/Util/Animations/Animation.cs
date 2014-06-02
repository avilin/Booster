﻿using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Booster.Util.Animations
{
    public class Animation
    {
        public Texture2D SpriteSheet { get; set; }
        public float Scale { get; set; }
        public bool Looping { get; set; }
        private int elapsedTime;
        private int currentFrame;
        public Color Color { get; set; }
        public List<Frame> Frames { get; set; }
        public float LayerDepth { get; set; }
        public Box Box { get; set; }
        public Rectangle DestinationRect
        {
            get
            {
                return Box.BoxInPosition(Position);
            }
        }
        public bool Active { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRect
        {
            get
            {
                return Frames[currentFrame].SourceRect;
            }
        }

        public void Initialize(Texture2D spriteSheet, List<Frame> frames, Box box)
        {
            this.SpriteSheet = spriteSheet;
            this.Frames = frames;
            Position = Vector2.Zero;
            this.Box = box;
            this.Color = Color.White;
            this.Scale = 1f;
            this.Looping = true;

            elapsedTime = 0;
            currentFrame = 0;
            LayerDepth = 1f;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (Active == false)
                return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > Frames[currentFrame].FrameTime)
            {
                currentFrame++;

                if (currentFrame == Frames.Count)
                {
                    currentFrame = 0;
                    if (Looping == false)
                        Active = false;
                }

                elapsedTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects flip)
        {
            if (Active)
            {
                spriteBatch.Draw(SpriteSheet, DestinationRect, SourceRect, Color, 0.0f, Vector2.Zero, flip, LayerDepth);
            }
        }
    }
}
