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
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        InitializeElement("BLO", mapCell);
                        continue;
                    }
                    InitializeElement(i >= row.Count ? "BLO" : row[i], mapCell);
                }
            }
        }

        public void InitializeElement(string type, Point mapCell)
        {
            switch (type)
            {
                case "PLA":
                    int playerPositionX = mapCell.X * TileSide + TileSide / 2;
                    int playerPositionY = mapCell.Y * TileSide + TileSide;
                    Vector2 playerPosition = new Vector2(playerPositionX, playerPositionY);
                    Player = EntityFactory.CreatePlayer(resources, TileSide, playerPosition);
                    updateableElements.Add((Player));
                    drawableElements.Add((Player));
                    break;
                case "BLO":
                    SimpleTile block = EntityFactory.CreateBlock(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = block;
                    drawableElements.Add(block);
                    break;
                case "SPK":
                    Spike spike = EntityFactory.CreateSpike(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = spike;
                    updateableElements.Add(spike);
                    drawableElements.Add(spike);
                    break;
                case "DBL":
                    DamageBlock damageBlockLow = EntityFactory.CreateDamageBlockLow(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = damageBlockLow;
                    drawableElements.Add(damageBlockLow);
                    break;
                case "DBM":
                    DamageBlock damageBlockMid = EntityFactory.CreateDamageBlockMid(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = damageBlockMid;
                    drawableElements.Add(damageBlockMid);
                    break;
                case "DBH":
                    DamageBlock damageBlockHigh = EntityFactory.CreateDamageBlockHigh(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = damageBlockHigh;
                    drawableElements.Add(damageBlockHigh);
                    break;
                case "OWP":
                    SimpleTile oneWayPlatform = EntityFactory.CreateOneWayPlatform(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = oneWayPlatform;
                    drawableElements.Add(oneWayPlatform);
                    break;
                case "SOL":
                    ScoreObject scoreObjectLow = EntityFactory.CreateScoreObjectLow(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = scoreObjectLow;
                    drawableElements.Add((scoreObjectLow));
                    break;
                case "SOM":
                    ScoreObject scoreObjectMid = EntityFactory.CreateScoreObjectMid(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = scoreObjectMid;
                    drawableElements.Add(scoreObjectMid);
                    break;
                case "SOH":
                    ScoreObject scoreObjectHigh = EntityFactory.CreateScoreObjectHigh(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = scoreObjectHigh;
                    drawableElements.Add(scoreObjectHigh);
                    break;
                case "EXT":
                    SimpleTile exit = EntityFactory.CreateExit(resources, TileSide, mapCell);
                    Tiles[mapCell.X, mapCell.Y] = exit;
                    drawableElements.Add(exit);
                    break;
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
