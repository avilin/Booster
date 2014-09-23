using Booster.Levels.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Booster.Levels.StrategyMove
{
    public abstract class StrategyMovePlayer : IStrategyMove
    {
        public abstract void Move(ICollisionable player, Vector2 nextPosition, Map map);

        protected Dictionary<CollisionTypes, List<ICollisionable>> GetPlayerBorderCollisions(Player player, Rectangle playerBorderRectangle, Map map)
        {
            Dictionary<CollisionTypes, List<ICollisionable>> collisions = new Dictionary<CollisionTypes, List<ICollisionable>>();

            int firstXTileToCheck = playerBorderRectangle.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerBorderRectangle.X + playerBorderRectangle.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = playerBorderRectangle.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerBorderRectangle.Y + playerBorderRectangle.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionable tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerBorderRectangle.Intersects(tile.HitBox))
                    {
                        if (tile.CollisionType == CollisionTypes.Block)
                        {
                            if (!collisions.ContainsKey(CollisionTypes.Block))
                            {
                                collisions.Add(CollisionTypes.Block, new List<ICollisionable>());
                            }
                            collisions[CollisionTypes.Block].Add(tile);
                        }
                        if (tile.CollisionType == CollisionTypes.None)
                        {
                            if (!collisions.ContainsKey(CollisionTypes.None))
                            {
                                collisions.Add(CollisionTypes.None, new List<ICollisionable>());
                            }
                            collisions[CollisionTypes.None].Add(tile);
                        }
                    }
                }
            }
            return collisions;
        }

        protected void CheckCollisionsRight(int firstXTileToCheck, int firstYTileToCheck, int lastXTileToCheck, int lastYTileToCheck, bool xBlocked,
            Map map, List<ICollisionable>[,] collisions, Player player)
        {
            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
                    {
                        if (map.Tiles[i, j] == tile)
                        {
                            if (!xBlocked)
                            {
                                tile.OnCollision(player);
                                if (!tile.Active)
                                {
                                    map.Tiles[i, j] = null;
                                }
                            }
                            else
                            {
                                if (i != lastXTileToCheck || tile.CollisionType != CollisionTypes.None)
                                {
                                    tile.OnCollision(player);
                                    if (!tile.Active)
                                    {
                                        map.Tiles[i, j] = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            tile.OnCollision(player);
                        }
                    }
                }
            }
        }

        protected void CheckCollisionsLeft(int firstXTileToCheck, int firstYTileToCheck, int lastXTileToCheck, int lastYTileToCheck, bool xBlocked,
            Map map, List<ICollisionable>[,] collisions, Player player)
        {
            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
                    {
                        if (map.Tiles[i, j] == tile)
                        {
                            if (!xBlocked)
                            {
                                tile.OnCollision(player);
                                if (!tile.Active)
                                {
                                    map.Tiles[i, j] = null;
                                }
                            }
                            else
                            {
                                if (i != lastXTileToCheck || tile.CollisionType != CollisionTypes.None)
                                {
                                    tile.OnCollision(player);
                                    if (!tile.Active)
                                    {
                                        map.Tiles[i, j] = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            tile.OnCollision(player);
                        }
                    }
                }
            }
        }

        protected void CheckCollisionsDown(int firstXTileToCheck, int firstYTileToCheck, int lastXTileToCheck, int lastYTileToCheck, bool yBlocked,
            Map map, List<ICollisionable>[,] collisions, Player player)
        {
            for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
            {
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
                    {
                        if (map.Tiles[i, j] == tile)
                        {
                            if (!yBlocked)
                            {
                                tile.OnCollision(player);
                                if (!tile.Active)
                                {
                                    map.Tiles[i, j] = null;
                                }
                            }
                            else
                            {
                                if (j != lastYTileToCheck || tile.CollisionType != CollisionTypes.None)
                                {
                                    tile.OnCollision(player);
                                    if (!tile.Active)
                                    {
                                        map.Tiles[i, j] = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            tile.OnCollision(player);
                        }
                    }
                }
            }
        }

        protected void CheckCollisionsUp(int firstXTileToCheck, int firstYTileToCheck, int lastXTileToCheck, int lastYTileToCheck, bool yBlocked,
            Map map, List<ICollisionable>[,] collisions, Player player)
        {
            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
            {
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
                {
                    List<ICollisionable> list = collisions[i, j];
                    if (list == null)
                    {
                        continue;
                    }
                    foreach (ICollisionable tile in list)
                    {
                        if (map.Tiles[i, j] == tile)
                        {
                            if (!yBlocked)
                            {
                                tile.OnCollision(player);
                                if (!tile.Active)
                                {
                                    map.Tiles[i, j] = null;
                                }
                            }
                            else
                            {
                                if (j != lastYTileToCheck || tile.CollisionType != CollisionTypes.None)
                                {
                                    tile.OnCollision(player);
                                    if (!tile.Active)
                                    {
                                        map.Tiles[i, j] = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            tile.OnCollision(player);
                        }
                    }
                }
            }
        }

        protected void AddMovableElementsForCollision(Map map, Player player, Vector2 oldPosition, ref List<ICollisionable>[,] collisions)
        {
            Rectangle rectangle = Rectangle.Union(player.BoundingBox.BoxInPosition(oldPosition), player.BoundingBox.BoxInPosition(player.Position));
            for (int k = 0; k < map.MovableElements.Count; k++)
            {
                IMoveable movable = map.MovableElements[k];
                if (!(movable is ICollisionable))
                {
                    continue;
                }
                if (movable == player)
                {
                    continue;
                }
                ICollisionable collisionableObject = movable as ICollisionable;
                if (collisionableObject.HitBox.Intersects(rectangle))
                {
                    List<Point> coordinates = map.GetEntityCoordinatesOnMap(collisionableObject);
                    for (int l = 0; l < coordinates.Count; l++)
                    {
                        int i = coordinates[l].X;
                        int j = coordinates[l].Y;
                        if (i > collisions.GetLength(0) - 1 || j > collisions.GetLength(1) - 1)
                        {
                            continue;
                        }
                        if (collisions[i, j] == null)
                        {
                            collisions[i, j] = new List<ICollisionable>();
                        }
                        collisions[i, j].Add(collisionableObject);
                    }
                }
            }
        }
    }
}