using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerLeftDown : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            Boolean xBlocked = false;
            Boolean yBlocked = false;
            //nextPosition = new Vector2(600, 500);
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            Vector2 nextPlayerPosition = player.Position;
            int playerLeftX = player.HitBox.X;
            int playerBottomY = player.HitBox.Y + player.HitBox.Height;
            int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }

                    Vector2 playerIntermediatePosition;
                    Rectangle hitBoxInNextPositionToCheck;
                    Rectangle borderHitBox;

                    if (playerNextBottomY >= tile.HitBox.Y && playerBottomY <= tile.HitBox.Y)
                    {
                        float intermediatePlayerPositionYToCheck = tile.HitBox.Y - player.BoundingBox.OffSetBottom;
                        playerIntermediatePosition = player.GetIntermediatePositionWithY(nextPosition, intermediatePlayerPositionYToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X, hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height, hitBoxInNextPositionToCheck.Width, 1);

                        if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                        {
                            if (tile.HitBox.Intersects(borderHitBox))
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = hitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;
                                yBlocked = true;
                            }
                        }
                    }
                    if (playerLeftX >= (tile.HitBox.X + tile.HitBox.Width))
                    {
                        float intermediatePlayerPositionXToCheck = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        playerIntermediatePosition = player.GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X - 1, hitBoxInNextPositionToCheck.Y, 1, hitBoxInNextPositionToCheck.Height);

                        if (tile.CollisionType == CollisionTypes.Block)
                        {
                            if (tile.HitBox.Intersects(borderHitBox))
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = hitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;
                                xBlocked = true;
                            }
                        }
                    }
                    if (!xBlocked && !yBlocked)
                    {
                        float intermediatePlayerPositionXToCheck = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        playerIntermediatePosition = player.GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X - 1, hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height, 1, 1);

                        if (tile.CollisionType == CollisionTypes.Block)
                        {
                            if (tile.HitBox.Intersects(borderHitBox))
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = hitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;
                                
                                xBlocked = true;
                            }
                        }
                    }
                }
            }
            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
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
                player.Position = Vector2.UnitX * nextPlayerPosition + Vector2.UnitY * nextPlayerPosition;
                if (xBlocked)
                {
                    Rectangle playerLeftSide = new Rectangle(player.HitBox.X, player.HitBox.Y, 1, player.HitBox.Height);
                    CheckPlayerTilesCollisions(player, playerLeftSide, map);
                    player.Speed *= Vector2.UnitY;
                    MovePlayerDown(player, nextPosition, map);
                }
                else
                {
                    Rectangle playerBottomSide = new Rectangle(player.HitBox.X, player.HitBox.Y - 1, player.HitBox.Width, 1);
                    CheckPlayerTilesCollisions(player, playerBottomSide, map);
                    //player.Speed *= Vector2.UnitX;
                    MovePlayerLeft(player, nextPosition, map);
                }
            }
        }

        public void MovePlayerLeft(Player player, Vector2 nextPosition, Map map)
        {
            Boolean canMoveDown = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide + 1;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionX = nextPosition.X;
            int playerNextLeftX = playerHitBoxInNextPosition.X;

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                ICollisionableObject tile;
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    tile = map.Tiles[i, j];
                    if (j != lastYTileToCheck)
                    {
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
                    else
                    {
                        if (tile != null && (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top))
                        {
                            continue;
                        }
                        if (nextPlayerPositionX <= i * map.TileSide + player.BoundingBox.OffSetLeft)
                        {
                            nextPlayerPositionX = i * map.TileSide + player.BoundingBox.OffSetLeft;
                            player.Speed *= Vector2.UnitY;
                            canMoveDown = true;
                        }
                    }
                }
                if (nextPlayerPositionX != nextPosition.X)
                {
                    break;
                }
                for (int k = firstYTileToCheck; k <= lastYTileToCheck; k++)
                {
                    tile = map.Tiles[i, k];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (k == lastYTileToCheck)
                    {
                        if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                        {
                            tile.OnCollision(player);
                        }
                    }
                    else
                    {
                        tile.OnCollision(player);
                    }
                    if (!tile.Active)
                    {
                        map.Tiles[i, k] = null;
                    }
                }
                if (canMoveDown)
                {
                    break;
                }
            }
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;
            if (canMoveDown)
            {
                Move(player, nextPosition, map);
            }
        }

        public void MovePlayerDown(Player player, Vector2 nextPosition, Map map)
        {
            Boolean canMoveLeft = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = player.HitBox.X / map.TileSide - 1;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide + 1;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerBottomY = player.HitBox.Y + player.HitBox.Height;
            int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

            for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
            {
                ICollisionableObject tile;
                for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
                {
                    tile = map.Tiles[i, j];
                    if (i != lastXTileToCheck)
                    {
                        if (tile == null)
                        {
                            continue;
                        }
                        if (playerNextBottomY <= tile.HitBox.Y || playerBottomY > tile.HitBox.Y)
                        {
                            continue;
                        }
                        if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                        {
                            nextPlayerPositionY = tile.HitBox.Y - player.BoundingBox.OffSetBottom;
                            //player.Speed *= Vector2.UnitX;
                            tile.OnCollision(player);
                        }
                    }
                    else
                    {
                        if (tile != null && tile.CollisionType == CollisionTypes.Block)
                        {
                            continue;
                        }
                        if (j > 0 && (map.Tiles[i, j - 1] == null || map.Tiles[i, j - 1].CollisionType != CollisionTypes.Block))
                        {
                            if (nextPosition.Y >= j * map.TileSide)
                            {
                                nextPlayerPositionY = j * map.TileSide;
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
