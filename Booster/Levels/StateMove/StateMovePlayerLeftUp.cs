using System;
using Microsoft.Xna.Framework;
using Booster.Levels.Entities;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerLeftUp : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            Boolean xBlocked = false;
            Boolean yBlocked = false;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionX = player.Position.X;
            float nextPlayerPositionY = player.Position.Y;
            int playerLeftX = player.HitBox.X;
            int playerTopY = player.HitBox.Y;

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerTopY >= tile.HitBox.Y + tile.HitBox.Height)
                    {
                        float intermediatePlayerPositionYToCheck = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                        Vector2 playerIntermediatePosition = player.GetIntermediatePositionWithY(nextPosition, intermediatePlayerPositionYToCheck);

                        Rectangle playerHitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        playerHitBoxInNextPositionToCheck.Offset(-1, -1);

                        if (tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
                        {
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPositionY = playerIntermediatePosition.Y;
                                nextPlayerPositionX = playerIntermediatePosition.X;
                                playerHitBoxInNextPositionToCheck.Offset(1, 1);
                                lastXTileToCheck = playerHitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = playerHitBoxInNextPositionToCheck.Y / map.TileSide;
                                yBlocked = true;
                            }
                        }
                    }
                    if (playerLeftX >= (tile.HitBox.X + tile.HitBox.Width))
                    {
                        float intermediatePlayerPositionXToCheck = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        Vector2 playerIntermediatePosition = player.GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

                        Rectangle playerHitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        playerHitBoxInNextPositionToCheck.Offset(-1, 0);

                        if (tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
                        {
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPositionX = playerIntermediatePosition.X;
                                nextPlayerPositionY = playerIntermediatePosition.Y;
                                playerHitBoxInNextPositionToCheck.Offset(1, 0);
                                lastXTileToCheck = playerHitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = playerHitBoxInNextPositionToCheck.Y / map.TileSide;
                                xBlocked = true;
                            }
                        }
                    }
                }
            }
            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    tile.OnCollision(player);
                    if (!tile.Active)
                    {
                        map.Tiles[i, j] = null;
                    }
                }
            }
            if (!xBlocked && !yBlocked)
            {
                player.Position = nextPosition;
            }
            else
            {
                player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * nextPlayerPositionY;
                if (xBlocked)
                {
                    Rectangle playerLeftSide = new Rectangle(player.HitBox.X - 1, player.HitBox.Y, 1, player.HitBox.Height);
                    CheckPlayerTilesCollisions(player, playerLeftSide, map);
                    player.Speed *= Vector2.UnitY;
                    MovePlayerUp(player, nextPosition, map);
                }
                else
                {
                    Rectangle playerTopSide = new Rectangle(player.HitBox.X, player.HitBox.Y - 1, player.HitBox.Width, 1);
                    CheckPlayerTilesCollisions(player, playerTopSide, map);
                    player.Speed *= Vector2.UnitX;
                    MovePlayerLeft(player, nextPosition, map);
                }
            }
        }

        public void MovePlayerLeft(Player player, Vector2 nextPosition, Map map)
        {
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

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextLeftX >= (tile.HitBox.X + tile.HitBox.Width))
                    {
                        continue;
                    }
                    if (tile.CollisionType == CollisionTypes.Block)
                    {
                        nextPlayerPositionX = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        player.Speed *= Vector2.UnitY;
                        tile.OnCollision(player);
                    }
                }
                if (nextPlayerPositionX != nextPosition.X)
                {
                    break;
                }
                for (int k = firstYTileToCheck; k <= lastYTileToCheck; k++)
                {
                    ICollisionableObject tile = map.Tiles[i, k];
                    if (tile == null)
                    {
                        continue;
                    }
                    tile.OnCollision(player);
                    if (!tile.Active)
                    {
                        map.Tiles[i, k] = null;
                    }
                }
            }
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;
        }

        public void MovePlayerUp(Player player, Vector2 nextPosition, Map map)
        {
            Boolean canMoveLeft = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = player.HitBox.X / map.TileSide - 1;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerNextTopY = playerHitBoxInNextPosition.Y;

            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
            {
                ICollisionableObject tile;
                for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
                {
                    tile = map.Tiles[i, j];
                    if (i == firstXTileToCheck)
                    {
                        if (tile == null)
                        {
                            continue;
                        }
                        if (playerNextTopY >= (tile.HitBox.Y + tile.HitBox.Height))
                        {
                            continue;
                        }

                        if (tile.CollisionType == CollisionTypes.Block)
                        {
                            nextPlayerPositionY = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                            player.Speed *= Vector2.UnitX;
                            tile.OnCollision(player);
                        }
                    }
                    else
                    {
                        if (tile != null && tile.CollisionType == CollisionTypes.Block)
                        {
                            continue;
                        }
                        if (j < map.Tiles.GetLength(1) - 1 && (map.Tiles[i, j + 1] == null || map.Tiles[i, j + 1].CollisionType != CollisionTypes.Block))
                        {
                            if (nextPosition.Y <= j * map.TileSide + map.TileSide)
                            {
                                nextPlayerPositionY = j * map.TileSide + map.TileSide;
                                //player.Speed *= Vector2.UnitX;
                                canMoveLeft = true;
                            }
                        }
                    }
                }
                if (nextPlayerPositionY != nextPosition.Y && !canMoveLeft)
                {
                    break;
                }
                for (int k = firstXTileToCheck; k > lastXTileToCheck; k--)
                {
                    tile = map.Tiles[k, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    tile.OnCollision(player);
                    if (!tile.Active)
                    {
                        map.Tiles[k, j] = null;
                    }
                }
                if (canMoveLeft)
                {
                    break;
                }
            }
            player.Position = Vector2.UnitX * player.Position + Vector2.UnitY * nextPlayerPositionY;
            if (canMoveLeft)
            {
                Move(player, nextPosition, map);
            }
        }
    }
}
