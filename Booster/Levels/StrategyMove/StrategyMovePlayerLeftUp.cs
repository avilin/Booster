using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Booster.Levels.StrategyMove
{
    public class StrategyMovePlayerLeftUp : StrategyMovePlayer
    {
        public override void Move(ICollisionable entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            bool xBlocked = false;
            bool yBlocked = false;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            Vector2 nextPlayerPosition = player.Position;
            int playerLeftX = player.HitBox.X;
            int playerTopY = player.HitBox.Y;

            List<ICollisionable>[,] collisions = new List<ICollisionable>[firstXTileToCheck + 1, firstYTileToCheck + 1];

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
                {
                    ICollisionable tile = map.Tiles[i, j];
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
                        borderHitBox = new Rectangle(hitBoxInNextPositionToCheck.X - 1, hitBoxInNextPositionToCheck.Y - 1, hitBoxInNextPositionToCheck.Width + 1, 1);

                        if (tile.HitBox.Intersects(borderHitBox))
                        {
                            if (tile.CollisionType == CollisionTypes.None)
                            {
                                if (collisions[i, j] == null)
                                {
                                    collisions[i, j] = new List<ICollisionable>();
                                }
                                collisions[i, j].Add(tile);
                            }
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = hitBoxInNextPositionToCheck.X / map.TileSide;
                                lastYTileToCheck = hitBoxInNextPositionToCheck.Y / map.TileSide;
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

                        if (tile.HitBox.Intersects(borderHitBox))
                        {
                            if (tile.CollisionType == CollisionTypes.None)
                            {
                                if (collisions[i, j] == null)
                                {
                                    collisions[i, j] = new List<ICollisionable>();
                                }
                                collisions[i, j].Add(tile);
                            }
                            if (tile.CollisionType == CollisionTypes.Block)
                            {
                                nextPlayerPosition = playerIntermediatePosition;
                                lastXTileToCheck = hitBoxInNextPositionToCheck.X / map.TileSide;
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

                CheckCollisions(lastXTileToCheck, lastYTileToCheck, map, collisions, player);
            }
            else
            {
                Vector2 oldPosition = player.Position;
                player.Position = Vector2.UnitX * nextPlayerPosition + Vector2.UnitY * nextPlayerPosition;

                AddMovableElementsForCollision(map, player, oldPosition, ref collisions);
                if (xBlocked)
                {
                    Rectangle playerLeftSide = new Rectangle(player.HitBox.X - 1, player.HitBox.Y, 1, player.HitBox.Height);
                    Dictionary<CollisionTypes, List<ICollisionable>> borderCollisions = GetPlayerBorderCollisions(player, playerLeftSide, map);
                    CheckCollisions(lastXTileToCheck, lastYTileToCheck, map, collisions, player, borderCollisions);
                    player.Speed *= Vector2.UnitY;
                    MovePlayerUp(player, nextPosition, map);
                }
                else
                {
                    Rectangle playerTopSide = new Rectangle(player.HitBox.X, player.HitBox.Y - 1, player.HitBox.Width, 1);
                    Dictionary<CollisionTypes, List<ICollisionable>> borderCollisions = GetPlayerBorderCollisions(player, playerTopSide, map);
                    CheckCollisions(lastXTileToCheck, lastYTileToCheck, map, collisions, player, borderCollisions);
                    player.Speed *= Vector2.UnitX;
                    MovePlayerLeft(player, nextPosition, map);
                }
            }
        }

        private void CheckCollisions(int lastXTileToCheck, int lastYTileToCheck, Map map, List<ICollisionable>[,] collisions, Player player)
        {
            for (int i = collisions.GetLength(0) - 1; i >= lastXTileToCheck; i--)
            {
                for (int j = collisions.GetLength(1) - 1; j >= lastYTileToCheck; j--)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
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

        private void CheckCollisions(int lastXTileToCheck, int lastYTileToCheck, Map map, List<ICollisionable>[,] collisions, Player player, Dictionary<CollisionTypes, List<ICollisionable>> borderCollisions)
        {
            for (int i = collisions.GetLength(0) - 1; i >= lastXTileToCheck; i--)
            {
                for (int j = collisions.GetLength(1) - 1; j >= lastYTileToCheck; j--)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
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

            foreach (ICollisionable tile in borderCollisions[CollisionTypes.Block])
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

        private void MovePlayerLeft(Player player, Vector2 nextPosition, Map map)
        {
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
                        player.Speed *= Vector2.UnitY;
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

        private void MovePlayerUp(Player player, Vector2 nextPosition, Map map)
        {
            bool canMoveLeft = false;
            bool yBlocked = false;
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

            List<ICollisionable>[,] collisions = new List<ICollisionable>[firstXTileToCheck + 1, firstYTileToCheck + 1];

            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
            {
                ICollisionable tile;
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
                                canMoveLeft = true;
                            }
                        }
                    }
                }
            }
            Vector2 oldPosition = player.Position;
            player.Position = Vector2.UnitX * player.Position + Vector2.UnitY * nextPlayerPositionY;

            AddMovableElementsForCollision(map, player, oldPosition, ref collisions);

            CheckCollisionsUp(firstXTileToCheck, firstYTileToCheck, lastXTileToCheck + 1, lastYTileToCheck, yBlocked, map, collisions, player);
            if (canMoveLeft)
            {
                Move(player, nextPosition, map);
            }
        }
    }
}