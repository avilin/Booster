using Booster.Input;
using Booster.Levels;
using Booster.Levels.Entities;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Booster.States
{
    public class LevelRunning : IGameState
    {
        private IStateManager stateManager;

        public Map Map { get; set; }
        private Camera2D camera2D;

        public LevelRunning(IStateManager stateManager)
        {
            this.stateManager = stateManager;

            Map = new Map(stateManager.Resources);
            camera2D = new Camera2D();
        }

        public void LoadMap(string file)
        {
            if (!File.Exists(file))
            {
                stateManager.CurrentState = GameStates.GameIntro;
                return;
            }
            Map.LoadMap(file);
        }

        public void Initialize()
        {
            //MediaPlayer.Play(stateManager.Resources.Songs["level_music"]);
            camera2D.Initialize(stateManager.Game.GraphicsDevice.Viewport);
            Map.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();

            if (CheckPause(inputSystem))
            {
                return;
            }

            Vector2 acceleration = GetPlayerActions(inputSystem);
            Map.Update(gameTime, acceleration);

            if (CheckWin())
            {
                return;
            }

            if (CheckGameOver())
            {
                return;
            }

            Vector2 cameraPosition = new Vector2((int)Map.Player.Position.X, (int)Map.Player.Position.Y);
            camera2D.Update(cameraPosition, Map.Tiles.GetLength(0) * Map.TileSide, Map.Tiles.GetLength(1) * Map.TileSide);
        }

        private Boolean CheckPause(InputSystem inputSystem)
        {
            if (inputSystem.CurrentActions.Contains(VirtualButtons.Start) &&
                !inputSystem.PreviousActions.Contains(VirtualButtons.Start))
            {
                stateManager.Resources.Songs["level_music"].Pause();
                stateManager.Resources.Songs["menu_music"].Play();
                stateManager.CurrentState = GameStates.Pause;
                return true;
            }
            return false;
        }

        private Vector2 GetPlayerActions(InputSystem inputSystem)
        {
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

            return acceleration;
        }

        private Boolean CheckWin()
        {
            if (Map.Player.CurrentEntityStates.Contains(EntityStates.Win))
            {
                stateManager.CurrentState = GameStates.LevelCompleted;
                return true;
            }
            return false;
        }

        private Boolean CheckGameOver()
        {
            if (!Map.Player.Active)
            {
                stateManager.CurrentState = GameStates.GameOver;
                return true;
            }
            return false;
        }

        public void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.White);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));

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

        private void DrawLifes(SpriteBatch spriteBatch)
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

        private void DrawHeart(SpriteBatch spriteBatch, Rectangle destinationRect, Rectangle sourceRect)
        {
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, destinationRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void DrawScore(SpriteBatch spriteBatch)
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

        private void DrawKeys(SpriteBatch spriteBatch)
        {
            Rectangle keyDestinationRect = new Rectangle(5, 105, 44, 40);
            Rectangle xDestinationRect = new Rectangle(55, 114, 30, 28);
            Rectangle digitDestinationRect = new Rectangle(90, 105, 32, 40);
            int firstDigit = Map.Player.Keys % 10;
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, keyDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_keyYellow.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, xDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_x.png"], Color.White);
            spriteBatch.Draw(stateManager.Resources.SpriteSheets["hud"].SpriteSheet, digitDestinationRect, stateManager.Resources.SpriteSheets["hud"].ObjectLocation["hud_" + firstDigit + ".png"], Color.White);
        }

        private void DrawBoostBar(SpriteBatch spriteBatch)
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