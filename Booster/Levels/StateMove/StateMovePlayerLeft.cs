﻿using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerLeft : StateMovePlayer
    {
        public override void Move(ICollisionable entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            bool xBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionX = nextPosition.X;
            int playerNextLeftX = playerHitBoxInNextPosition.X;

            List<ICollisionable>[,] collisions = new List<ICollisionable>[firstXTileToCheck + 1, lastYTileToCheck + 1];

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionable tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextLeftX >= (tile.HitBox.X + tile.HitBox.Width))
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
                        nextPlayerPositionX = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        lastXTileToCheck = i;
                        xBlocked = true;
                    }
                }
            }
            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsLeft(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck, lastYTileToCheck, xBlocked, map, collisions, player);
        }
    }
}