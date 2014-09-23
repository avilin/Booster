using Microsoft.Xna.Framework;

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
        public bool Update(GameTime gameTime)
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