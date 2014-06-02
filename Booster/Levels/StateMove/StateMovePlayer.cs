using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Booster.Levels.Entities;

namespace Booster.Levels.StateMove
{
    public abstract class StateMovePlayer : IStateMove
    {
        public abstract void Move(ICollisionableObject player, Vector2 nextPosition, Map map);

        public void CheckPlayerTilesCollisions(Player player, Rectangle playerRectangle, Map map)
        {
            int firstXTileToCheck = playerRectangle.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerRectangle.X + playerRectangle.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = playerRectangle.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerRectangle.Y + playerRectangle.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (tile.CollisionType != CollisionTypes.Block)
                    {
                        continue;
                    }
                    if (playerRectangle.Intersects(tile.HitBox))
                    {
                        tile.OnCollision(player);
                    }
                }
            }
        }
    }
}