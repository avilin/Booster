using Booster.Levels.Entities;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
                    Point mapCell = new Point(i, j);
                    Vector2 position = new Vector2(mapCell.X * TileSide + TileSide / 2, mapCell.Y * TileSide + TileSide / 2);
                    Entity entity;
                    EntityType entityType = EntityType.Block;
                    if (i != 0 && j != 0 && i != width - 1 && j != height - 1)
                    {
                        if (i < row.Count)
                        {
                            if (resources.StringType.ContainsKey(row[i]))
                            {
                                entityType = resources.StringType[row[i]];
                            }
                            else
                            {
                                entityType = EntityType.Null;
                            }
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
                    if (entity is Player)
                    {
                        Player = (Player)entity;
                        continue;
                    }
                    if (entity is ICollisionableObject)
                    {
                        Tiles[mapCell.X, mapCell.Y] = (ICollisionableObject)entity;
                    }
                }
            }
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

            Player.Update(gameTime);
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

            Player.Move(gameTime, this);

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

        public Boolean CheckPlayerTilesCollisions(Rectangle playerRectangle)
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
