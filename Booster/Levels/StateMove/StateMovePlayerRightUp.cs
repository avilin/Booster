using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerRightUp : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            bool xBlocked = false;
            bool yBlocked = false;

            if (((int)nextPosition.X - map.TileSide / 2) % map.TileSide == 0 && nextPosition.X != (int)nextPosition.X)
            {
                nextPosition = Vector2.UnitX * ((int)nextPosition.X + 1) + Vector2.UnitY * nextPosition;
            }
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            Vector2 nextPlayerPosition = player.Position;
            int playerRightX = player.HitBox.X + player.HitBox.Width;
            int playerTopY = player.HitBox.Y;

            List<ICollisionableObject>[,] collisions = new List<ICollisionableObject>[lastXTileToCheck + 1, firstYTileToCheck + 1];

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
            {
                for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }

                    Vector2 playerIntermediatePosition;
                    Rectangle hitBoxInNextPositionToCheck;
                    Rectangle borderHitBox;

                    if (playerTopY >= tile.HitBox.Y + tile.HitBox.Height)
                    {
                        float intermediatePlayerPositionYToCheck = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                        playerIntermediatePosition = player.GetIntermediatePositionWithY(nextPosition, intermediatePlayerPositionYToCheck);

                        hitBoxInNextPositionToCheck = player.BoundingBox.BoxInPosition(playerIntermediatePosition);
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X, hitBoxInNextPositionToCheck.Y - 1, hitBoxInNextPositionToCheck.Width + 1, 1);

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
                                lastYTileToCheck = hitBoxInNextPositionToCheck.Y / map.TileSide;
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
                                lastYTileToCheck = hitBoxInNextPositionToCheck.Y / map.TileSide;
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

                CheckCollisions(firstXTileToCheck, lastYTileToCheck, map, collisions, player);
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
                    CheckCollisions(firstXTileToCheck, lastYTileToCheck, map, collisions, player, borderCollisions);
                    player.Speed *= Vector2.UnitY;
                    MovePlayerUp(player, nextPosition, map);
                }
                else
                {
                    Rectangle playerTopSide = new Rectangle(player.HitBox.X, player.HitBox.Y - 1, player.HitBox.Width, 1);
                    Dictionary<CollisionTypes, List<ICollisionableObject>> borderCollisions = GetPlayerBorderCollisions(player, playerTopSide, map);
                    CheckCollisions(firstXTileToCheck, lastYTileToCheck, map, collisions, player, borderCollisions);
                    player.Speed *= Vector2.UnitX;
                    MovePlayerRight(player, nextPosition, map);
                }
            }
        }

        public void CheckCollisions(int firstXTileToCheck, int lastYTileToCheck, Map map, List<ICollisionableObject>[,] collisions, Player player)
        {
            for (int i = firstXTileToCheck; i < collisions.GetLength(0); i++)
            {
                for (int j = collisions.GetLength(1) - 1; j >= lastYTileToCheck; j--)
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

        public void CheckCollisions(int firstXTileToCheck, int lastYTileToCheck, Map map, List<ICollisionableObject>[,] collisions, Player player, Dictionary<CollisionTypes, List<ICollisionableObject>> borderCollisions)
        {
            for (int i = firstXTileToCheck; i < collisions.GetLength(0); i++)
            {
                for (int j = collisions.GetLength(1) - 1; j >= lastYTileToCheck; j--)
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
            bool xBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);


            float nextPlayerPositionX = nextPosition.X;
            int playerNextRightX = playerHitBoxInNextPosition.X + playerHitBoxInNextPosition.Width;

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
                        lastXTileToCheck = i;
                        xBlocked = true;
                    }
                }
            }
            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsRight(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck, lastYTileToCheck, xBlocked, map, collisions, player);

        }

        public void MovePlayerUp(Player player, Vector2 nextPosition, Map map)
        {
            bool canMoveRight = false;
            bool yBlocked = false;
            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide + 1;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerNextTopY = playerHitBoxInNextPosition.Y;

            List<ICollisionableObject>[,] collisions = new List<ICollisionableObject>[lastXTileToCheck, firstYTileToCheck + 1];

            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
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
                        if (playerNextTopY >= (tile.HitBox.Y + tile.HitBox.Height))
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
                            nextPlayerPositionY = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                            player.Speed *= Vector2.UnitX;
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
                        if (j < map.Tiles.GetLength(1) - 1 && (map.Tiles[i, j + 1] == null || map.Tiles[i, j + 1].CollisionType != CollisionTypes.Block))
                        {
                            if (nextPosition.Y <= j * map.TileSide + map.TileSide)
                            {
                                nextPlayerPositionY = j * map.TileSide + map.TileSide;
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

            CheckCollisionsUp(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck - 1, lastYTileToCheck, yBlocked, map, collisions, player);
            if (canMoveRight)
            {
                Move(player, nextPosition, map);
            }
        }
    }
}