using Booster.Input;
using Booster.Levels;
using Booster.Levels.Entities;
using Booster.States;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.States
{
    public class StateLevelPlaying : IGameState
    {
        private IGameStateContext stateManager;

        public Map Map { get; set; }
        private Camera2D camera2D;

        public StateLevelPlaying(IGameStateContext stateManager)
        {
            this.stateManager = stateManager;

            Map = new Map(stateManager.Resources);
            camera2D = new Camera2D();
        }

        public void LoadMap(string file)
        {
            Map.LoadMap(file);
        }

        public void Initialize()
        {
            stateManager.Resources.Songs["level_music"].Play();
            camera2D.Initialize(stateManager.Game.GraphicsDevice.Viewport);
            Map.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Start) &&
                !inputSystem.PreviousActions.Contains(VirtualButtons.Start))
            {
                stateManager.CurrentState = GameStates.Pause;
                return;
            }

            Vector2 acceleration = Vector2.UnitX * inputSystem.LeftThumbSticks;

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Left))
            {
                acceleration += -Vector2.UnitX;
            }
            if (inputSystem.CurrentActions.Contains(VirtualButtons.Right))
            {
                acceleration += Vector2.UnitX;
            }

            if (inputSystem.CurrentActions.Contains(VirtualButtons.A)
                && !inputSystem.PreviousActions.Contains(VirtualButtons.A))
            {
                acceleration -= Vector2.UnitY;
            }

            if (inputSystem.CurrentActions.Contains(VirtualButtons.B)
                && !inputSystem.PreviousActions.Contains(VirtualButtons.B))
            {
                if (!Map.Player.CurrentEntityStates.Contains(EntityStates.Recharge))
                {
                    Map.Player.CurrentEntityStates.Add(EntityStates.Boost);
                    stateManager.Resources.SoundEffects["boost"].Play();
                }
            }

            Map.Update(gameTime);
            Map.MovePlayer(gameTime, acceleration);

            if (Map.Player.CurrentEntityStates.Contains(EntityStates.Win))
            {
                stateManager.CurrentState = GameStates.LevelCompleted;
                return;
            }

            if (!Map.Player.Active)
            {
                stateManager.CurrentState = GameStates.GameOver;
                //Initialize();
                return;
            }

            Vector2 cameraPosition = new Vector2((int)Map.Player.Position.X, (int)Map.Player.Position.Y);
            camera2D.Update(cameraPosition, Map.Tiles.GetLength(0) * Map.TileSide, Map.Tiles.GetLength(1) * Map.TileSide);
        }

        public void Draw(GameTime gameTime)
        {
            //Game.GraphicsDevice.Clear(Color.White);
            stateManager.Game.GraphicsDevice.Clear(Color.White);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));

            spriteBatch.Begin();
            spriteBatch.Draw(stateManager.Resources.Backgrounds["level_background"], stateManager.Game.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera2D.Transform);
            Map.Draw(gameTime, spriteBatch, camera2D);
            spriteBatch.End();

            spriteBatch.Begin();
            DrawLifes(spriteBatch);
            DrawScore(spriteBatch);
            DrawKeys(spriteBatch);
            DrawBoostBar(spriteBatch);

            spriteBatch.End();
        }

        public void DrawLifes(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectFull = stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_heartFull.png"];
            Rectangle sourceRectHalf = stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_heartHalf.png"];
            Rectangle sourceRectEmpty = stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_heartEmpty.png"];

            Rectangle sourceRect = sourceRectEmpty;
            int lifes = Map.Player.Health;
            Rectangle destinationRect = new Rectangle(5, 5, 53, 45);
            for (int i = 0; i < 2; i++)
            {
                if (i != 0)
                {
                    lifes -= 2;
                }
                if (lifes < 2)
                {
                    sourceRect = lifes <= 0 ? sourceRectEmpty : sourceRectHalf;
                }
                else
                {
                    sourceRect = sourceRectFull;
                }

                DrawHeart(spriteBatch, destinationRect, sourceRect);
                destinationRect.Offset(55, 0);
            }
        }

        public void DrawHeart(SpriteBatch spriteBatch, Rectangle destinationRect, Rectangle sourceRect)
        {
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, destinationRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void DrawScore(SpriteBatch spriteBatch)
        {
            Rectangle coinDestinationRect = new Rectangle(5, 55, 47, 47);
            Rectangle xDestinationRect = new Rectangle(55, 64, 30, 28);
            Rectangle tensDestinationRect = new Rectangle(90, 55, 32, 40);
            Rectangle unitsDestinationRect = new Rectangle(125, 55, 32, 40);
            int firstDigit = Map.Player.Score > 10 ? (Map.Player.Score / 10) % 10 : 0;
            int secondDigit = Map.Player.Score % 10;
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, coinDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_coins.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, xDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_x.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, tensDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_" + firstDigit + ".png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, unitsDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_" + secondDigit + ".png"], Color.White);
        }

        public void DrawKeys(SpriteBatch spriteBatch)
        {
            Rectangle keyDestinationRect = new Rectangle(5, 105, 44, 40);
            Rectangle xDestinationRect = new Rectangle(55, 114, 30, 28);
            Rectangle digitDestinationRect = new Rectangle(90, 105, 32, 40);
            int firstDigit = Map.Player.Keys % 10;
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, keyDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_keyYellow.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, xDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_x.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, digitDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_" + firstDigit + ".png"], Color.White);
        }

        public void DrawBoostBar(SpriteBatch spriteBatch)
        {
            Texture2D bar = new Texture2D(stateManager.Game.GraphicsDevice, 1, 1);
            bar.SetData<Color>(new Color[] { Color.White });

            double length = 100;
            Color color = Color.GreenYellow;
            if (Map.Player.CurrentEntityStates.Contains(EntityStates.Boost))
            {
                Duration duration = Map.Player.StatesTime[EntityStates.Boost];
                length = (double)(duration.Time - duration.ElapsedTime) / duration.Time * 100;
            }
            else if (Map.Player.CurrentEntityStates.Contains(EntityStates.Recharge))
            {
                Duration duration = Map.Player.StatesTime[EntityStates.Recharge];
                length = (double)duration.ElapsedTime / duration.Time * 100;
                color = Color.Red;
            }

            spriteBatch.Draw(bar, new Rectangle(118, 13, 104, 29), Color.Black);
            spriteBatch.Draw(bar, new Rectangle(120, 15, (int)length, 25), color);
        }
    }
}
