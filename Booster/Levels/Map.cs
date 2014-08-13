using Booster.Levels.Entities;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Booster.Levels
{
    public class Map
    {
        public int TileSide
        {
            get
            {
                return 32;
            }
        }
        private Resources resources;
        private List<string> levelFile;

        public Player Player { get; set; }
        public ICollisionableObject[,] Tiles { get; set; }

        public List<IMovable> MovableElements { get; set; }
        private List<IUpdateableObject> updateableElements;
        private List<IDrawableObject> drawableElements;

        public Map(Resources resources)
        {
            this.resources = resources;
        }

        public void LoadMap(string file)
        {
            levelFile = new List<string>(File.ReadAllLines(file));
        }

        public void Initialize()
        {
            if (levelFile == null)
            {
                return;
            }
            MovableElements = new List<IMovable>();
            updateableElements = new List<IUpdateableObject>();
            drawableElements = new List<IDrawableObject>();

            List<string> row = new List<string>(levelFile[0].Split(' '));
            int width = row.Count;
            int height = levelFile.Count;

            Tiles = new ICollisionableObject[width, height];

            for (int j = 0; j < height; j++)
            {
                row = new List<string>(levelFile[j].Split(' '));
                for (int i = 0; i < width; i++)
                {
                    Vector2 position = CalculatePositionWithCell(i, j);
                    Entity entity;
                    EntityType entityType = EntityType.BlockCenter;
                    if (i != 0 && j != 0 && i != width - 1 && j != height - 1)
                    {
                        if (i < row.Count)
                        {
                            entityType = resources.StringType.ContainsKey(row[i]) ? resources.StringType[row[i]] : EntityType.Null;
                        }
                    }

                    entity = resources.EntityTypeCreator[entityType].FactoryMethod(resources, position);
                    if (entity is IUpdateableObject)
                    {
                        updateableElements.Add((IUpdateableObject)entity);
                    }
                    if (entity is IDrawableObject)
                    {
                        drawableElements.Add((IDrawableObject)entity);
                    }
                    if (entity is IMovable)
                    {
                        MovableElements.Add((IMovable)entity);
                    }
                    if (!(entity is IMovable) && entity is ICollisionableObject)
                    {
                        Tiles[i, j] = (ICollisionableObject)entity;
                    }
                    if (entity is Player)
                    {
                        Player = (Player)entity;
                    }
                }
            }
        }

        public Vector2 CalculatePositionWithCell(int i, int j)
        {
            return new Vector2(i * TileSide + TileSide / 2, j * TileSide + TileSide / 2);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = updateableElements.Count - 1; i >= 0; i--)
            {
                if (!updateableElements[i].Active)
                {
                    updateableElements.RemoveAt(i);
                }
                else
                {
                    updateableElements[i].Update(gameTime);
                }
            }
        }

        public void MovePlayer(GameTime gameTime, Vector2 acceleration)
        {
            if (Player.CurrentEntityStates.Contains(EntityStates.Dead))
            {
                Player.UpdateAnimation(gameTime);
                return;
            }
            if (acceleration.X != 0)
            {
                Player.CurrentEntityStates.Add(EntityStates.Move);
                if (acceleration.X < 0)
                {
                    Player.CurrentEntityStates.Add(EntityStates.Left);
                    Player.CurrentEntityStates.Remove(EntityStates.Right);
                }
                else
                {
                    Player.CurrentEntityStates.Add(EntityStates.Right);
                    Player.CurrentEntityStates.Remove(EntityStates.Left);
                }
            }
            else
            {
                Player.CurrentEntityStates.Remove(EntityStates.Move);
            }

            Player.ApplyAcceleration(gameTime, acceleration);

            for (int i = MovableElements.Count - 1; i >= 0; i--)
            {
                if (!MovableElements[i].Active)
                {
                    MovableElements.RemoveAt(i);
                }
                else
                {
                    MovableElements[i].Move(gameTime, this);
                }
            }

            IsPlayerOnAir();

            Player.UpdateAnimation(gameTime);
        }

        public void IsPlayerOnAir()
        {
            Rectangle playerRectangle = Player.HitBox;
            playerRectangle.Offset(0, 1);
            if (CheckPlayerTilesCollisions(playerRectangle))
            {
                Player.CurrentEntityStates.Remove(EntityStates.OnAir);
            }
            else
            {
                Player.CurrentEntityStates.Add(EntityStates.OnAir);
            }
        }

        public bool CheckPlayerTilesCollisions(Rectangle playerRectangle)
        {
            int firstXTileToCheck = playerRectangle.X / TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (playerRectangle.X + playerRectangle.Width) / TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, Tiles.GetLength(0) - 1);
            int firstYTileToCheck = playerRectangle.Y / TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerRectangle.Y + playerRectangle.Height) / TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, Tiles.GetLength(1) - 1);

            for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    if (Tiles[i, j] == null)
                    {
                        continue;
                    }
                    ICollisionableObject tile = Tiles[i, j];
                    if (tile.CollisionType == CollisionTypes.Block)
                    {
                        if (playerRectangle.Intersects(tile.HitBox))
                        {
                            return true;
                        }
                    }
                    if (tile.CollisionType == CollisionTypes.Top)
                    {
                        int playerBottomY = Player.HitBox.Y + Player.HitBox.Height - 1;
                        if ((playerBottomY < tile.HitBox.Y)
                            && playerRectangle.Intersects(tile.HitBox))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<Point> GetEntityCoordinatesOnMap(ICollisionableObject entity)
        {
            int firstX = entity.HitBox.X / TileSide;
            int firstY = entity.HitBox.Y / TileSide;
            int lastX = (entity.HitBox.X + entity.HitBox.Width - 1) / TileSide;
            int lastY = (entity.HitBox.Y + entity.HitBox.Height - 1) / TileSide;

            List<Point> points = new List<Point>();
            for (int i = firstX; i <= lastX; i++)
            {
                for (int j = firstY; j <= lastY; j++)
                {
                    points.Add(new Point(i, j));
                }
            }
            return points;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera2D camera2D)
        {
            for (int i = drawableElements.Count - 1; i >= 0; i--)
            {
                if (!drawableElements[i].Active)
                {
                    drawableElements.RemoveAt(i);
                }
                else if (camera2D.isInView(drawableElements[i].DestinationRect))
                {
                    drawableElements[i].Draw(spriteBatch);
                }
            }
            Player.Draw(spriteBatch);
        }
    }
}