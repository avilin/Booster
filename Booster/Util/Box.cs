using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Booster.Util
{
    public class Box
    {
        public int OffSetTop { get; set; }
        public int OffSetBottom { get; set; }
        public int OffSetLeft { get; set; }
        public int OffSetRight { get; set; }

        public Rectangle BoxInPosition(Vector2 position)
        {
            int x = (int)position.X - OffSetLeft;
            int y = (int)position.Y - OffSetTop;
            int width = OffSetLeft + OffSetRight;
            int height = OffSetTop + OffSetBottom;
            return new Rectangle(x, y, width, height);
        }

        public Box(int offSetLeft, int offSetTop, int offSetRight, int offSetBottom)
        {
            OffSetLeft = offSetLeft;
            OffSetTop = offSetTop;
            OffSetRight = offSetRight;
            OffSetBottom = offSetBottom;
        }
    }
}
