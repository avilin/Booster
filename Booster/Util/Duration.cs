using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util
{
    public class Duration
    {
        public int Time { get; set; }
        public int ElapsedTime { get; set; }

        public Duration(int time)
        {
            Time = time;
            ElapsedTime = 0;
        }

        /// <summary>
        /// Update the time and return true if ElapsedTime > Time.
        /// </summary>
        public Boolean Update(GameTime gameTime)
        {
            ElapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ElapsedTime > Time)
            {
                ElapsedTime = 0;
                return true;
            }
            return false;
        }
    }
}
