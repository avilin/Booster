using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerRightDown : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            bool xBlocked = false;
            bool yBlocked = false;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            Vector2 nextPlayerPosition = player.Position;
            int playerRightX = player.HitBox.X + player.HitBox.Width;
            int playerBottomY = player.HitBox.Y + player.HitBox.Height;
            int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

            List<ICollisionableObject>[,] collisions = new List<ICollisionableObject>[lastXTileToCheck + 1, lastYTileToCheck + 1];

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
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
                        if (tile.HitBox.Intersects(borderHitBox))
                        {
                            if (tile.CollisionType == CollisionTypes.None)
                            {
                                if (collisions[i, j] == null)
                                {
                                    collisions[i, j] = new List<ICollisionableObject>();
                                }
                                collisions[i, j].Add(tile);
                            }
                            if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = (hitBoxInNextPositionToCheck.X + hitBoxInNextPositionToCheck.Width - 1) / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;
                                yBlocked = true;
                            }
                        }
                    }
                    if (playerRightX <= tile.HitBox.X)
                    {
                        float intermediatePlayerPositionXToCheck = tile.HitBox.X - player.BoundingBox.OffSetRight;
                        playerIntermediatePosition = player.GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X + hitBoxInNextPositionToCheck.Width, hitBoxInNextPositionToCheck.Y, 1, hitBoxInNextPositionToCheck.Height);
                        if (tile.HitBox.Intersects(borderHitBox))
                        {
                            if (tile.CollisionType == CollisionTypes.None)
                            {
                                if (collisions[i, j] == null)
                                {
                                    collisions[i, j] = new List<ICollisionableObject>();
                                }
                                collisions[i, j].Add(tile);
                            }
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = (hitBoxInNextPositionToCheck.X + hitBoxInNextPositionToCheck.Width - 1) / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;
                                xBlocked = true;
                            }
                        }
                    }
                    if (!xBlocked && !yBlocked)
                    {
                        float intermediatePlayerPositionXToCheck = tile.HitBox.X - player.BoundingBox.OffSetRight;
                        playerIntermediatePosition = player.GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X + hitBoxInNextPositionToCheck.Width, hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height, 1, 1);
                        if (tile.HitBox.Intersects(borderHitBox))
                        {
                            if (tile.CollisionType == CollisionTypes.None)
                            {
                                if (collisions[i, j] == null)
                                {
                                    collisions[i, j] = new List<ICollisionableObject>();
                                }
                                collisions[i, j].Add(tile);
                            }
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = (hitBoxInNextPositionToCheck.X + hitBoxInNextPositionToCheck.Width - 1) / map.TileSide;
                                lastYTileToCheck = (hitBoxInNextPositionToCheck.Y + hitBoxInNextPositionToCheck.Height - 1) / map.TileSide;

                                xBlocked = true;
                            }
                        }
                    }
                }
            }

            if (!xBlocked && !yBlocked)
            {
                Vector2 oldPosition = player.Position;
                player.Position = nextPosition;

                AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

                CheckCollisions(firstXTileToCheck, firstYTileToCheck, map, collisions, player);
            }
            else
            {
                Vector2 oldPosition = player.Position;
                player.Position = Vector2.UnitX * nextPlayerPosition + Vector2.UnitY * nextPlayerPosition;

                AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

                if (xBlocked)
                {
                    Rectangle playerRightSide = new Rectangle(player.HitBox.X + player.HitBox.Width, player.HitBox.Y, 1, player.HitBox.Height);
                    Dictionary<CollisionTypes, List<ICollisionableObject>> borderCollisions = GetPlayerBorderCollisions(player, playerRightSide, map);
                    CheckCollisions(firstXTileToCheck, firstYTileToCheck, map, collisions, player, borderCollisions);
                    player.Speed *= Vector2.UnitY;
                    MovePlayerDown(player, nextPosition, map);
                }
                else
                {
                    Rectangle playerBottomSide = new Rectangle(player.HitBox.X, player.HitBox.Y + player.HitBox.Height, player.HitBox.Width, 1);
                    Dictionary<CollisionTypes, List<ICollisionableObject>> borderCollisions = GetPlayerBorderCollisions(player, playerBottomSide, map);
                    CheckCollisions(firstXTileToCheck, firstYTileToCheck, map, collisions, player, borderCollisions);
                    //player.Speed *= Vector2.UnitX;
                    MovePlayerRight(player, nextPosition, map);
                }
            }
        }

        public void CheckCollisions(int firstXTileToCheck, int firstYTileToCheck, Map map, List<ICollisionableObject>[,] collisions, Player player)
        {
            for (int i = firstXTileToCheck; i < collisions.GetLength(0); i++)
            {
                for (int j = firstYTileToCheck; j < collisions.GetLength(1); j++)
                {
                    List<ICollisionableObject> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionableObject tile in list)
                    {
                        tile.OnCollision(player);
                        if (!tile.Active && map.Tiles[i, j] == tile)
                        {
                            map.Tiles[i, j] = null;
                        }
                    }
                }
            }
        }

        public void CheckCollisions(int firstXTileToCheck, int firstYTileToCheck, Map map, List<ICollisionableObject>[,] collisions, Player player, Dictionary<CollisionTypes, List<ICollisionableObject>> borderCollisions)
        {
            for (int i = firstXTileToCheck; i < collisions.GetLength(0); i++)
            {
                for (int j = firstYTileToCheck; j < collisions.GetLength(1); j++)
                {
                    List<ICollisionableObject> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionableObject tile in list)
                    {
                        if (borderCollisions.ContainsKey(CollisionTypes.None) && borderCollisions[CollisionTypes.None].Contains(tile))
                        {
                            continue;
                        }
                        tile.OnCollision(player);
                        if (!tile.Active && map.Tiles[i, j] == tile)
                        {
                            map.Tiles[i, j] = null;
                        }
                    }
                }
            }

            if (!borderCollisions.ContainsKey(CollisionTypes.Block))
            {
                return;
            }

            foreach (ICollisionableObject tile in borderCollisions[CollisionTypes.Block])
            {
                tile.OnCollision(player);
                if (!tile.Active)
                {
                    int i = tile.HitBox.Center.X / map.TileSide;
                    int j = tile.HitBox.Center.Y / map.TileSide;
                    map.Tiles[i, j] = null;
                }
            }
        }

        public void MovePlayerRight(Player player, Vector2 nextPosition, Map map)
        {
            bool canMoveDown = false;
            bool xBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide + 1;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionX = nextPosition.X;
            int playerNextRightX = playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width;

            List<ICollisionableObject>[,] collisions = new List<ICollisionableObject>[lastXTileToCheck + 1, lastYTileToCheck];

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
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
                        if (playerNextRightX <= tile.HitBox.X)
                        {
                            continue;
                        }
                        if (tile.CollisionType != CollisionTypes.Top)
                        {
                            if (collisions[i, j] == null)
                            {
                                collisions[i, j] = new List<ICollisionableObject>();
                            }
                            collisions[i, j].Add(tile);
                        }
                        if (tile.CollisionType == CollisionTypes.Block)
                        {
                            nextPlayerPositionX = tile.HitBox.X - player.BoundingBox.OffSetRight;
                            player.Speed *= Vector2.UnitY;
                            lastXTileToCheck = i;
                            xBlocked = true;
                        }
                    }
                    else
                    {
                        if (tile != null && (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top))
                        {
                            continue;
                        }
                        if (nextPlayerPositionX >= i * map.TileSide + player.BoundingBox.OffSetLeft)
                        {
                            nextPlayerPositionX = i * map.TileSide + player.BoundingBox.OffSetLeft;
                            player.Speed *= Vector2.UnitY;
                            canMoveDown = true;
                        }
                    }
                }
                if (canMoveDown)
                {
                    break;
                }
            }
            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsRight(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck, lastYTileToCheck - 1, xBlocked, map, collisions, player);
            if (canMoveDown)
            {
                Move(player, nextPosition, map);
            }
        }

        public void MovePlayerDown(Player player, Vector2 nextPosition, Map map)
        {
            bool canMoveRight = false;
            bool yBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide + 1;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide + 1;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerBottomY = player.HitBox.Y + player.HitBox.Height;
            int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

            List<ICollisionableObject>[,] collisions = new List<ICollisionableObject>[lastXTileToCheck, lastYTileToCheck + 1];

            for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
            {
                ICollisionableObject tile;
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
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

                        if (collisions[i, j] == null)
                        {
                            collisions[i, j] = new List<ICollisionableObject>();
                        }
                        collisions[i, j].Add(tile);

                        if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                        {
                            nextPlayerPositionY = tile.HitBox.Y - player.BoundingBox.OffSetBottom;
                            //player.Speed *= Vector2.UnitX;
                            lastYTileToCheck = j;
                            yBlocked = true;
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
                                nextPlayerPositionY = j * map.TileSide > nextPlayerPositionY ? j * map.TileSide : nextPlayerPositionY;
                                //player.Speed *= Vector2.UnitX;
                                canMoveRight = true;
                            }
                        }
                    }
                }
                if (canMoveRight)
                {
                    break;
                }
            }
            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * player.Position + Vector2.UnitY * nextPlayerPositionY;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsDown(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck - 1, lastYTileToCheck, yBlocked, map, collisions, player);
            if (canMoveRight)
            {
                Move(player, nextPosition, map);
            }
        }
    }
}
