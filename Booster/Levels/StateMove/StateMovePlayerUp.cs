using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerUp : StateMovePlayer
    {
        public override void Move(ICollisionable entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            bool yBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerNextTopY = playerHitBoxInNextPosition.Y;

            List<ICollisionable>[,] collisions = new List<ICollisionable>[lastXTileToCheck + 1, firstYTileToCheck + 1];

            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
            {
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
                {
                    ICollisionable tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextTopY >= (tile.HitBox.Y + tile.HitBox.Height))
                    {
                        continue;
                    }

                    if (tile.CollisionType != CollisionTypes.Top)
                    {
                        if (collisions[i, j] == null)
                        {
                            collisions[i, j] = new List<ICollisionable>();
                        }
                        collisions[i, j].Add(tile);
                    }

                    if (tile.CollisionType == CollisionTypes.Block)
                    {
                        nextPlayerPositionY = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                        player.Speed *= Vector2.UnitX;
                        lastYTileToCheck = j;
                        yBlocked = true;
                    }
                }
            }

            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * player.Position + Vector2.UnitY * nextPlayerPositionY;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsUp(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck, lastYTileToCheck, yBlocked, map, collisions, player);
        }
    }
}