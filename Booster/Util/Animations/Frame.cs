using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.Util.Animations
{
    public class Frame
    {
        public int FrameTime { get; set; }

        public Rectangle SourceRect { get; set; }

        public Frame(Rectangle sourceRect, int frameTime)
        {
            FrameTime = frameTime;
            SourceRect = sourceRect;
        }
    }
}