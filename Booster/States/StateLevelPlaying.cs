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

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Back) &&
                !inputSystem.PreviousActions.Contains(VirtualButtons.Back))
            {
                stateManager.CurrentState = GameStates.StoryMenu;
                return;
            }

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Guide) &&
                !inputSystem.PreviousActions.Contains(VirtualButtons.Guide))
            {
                stateManager.CurrentState = GameStates.MainMenu;
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
            //spriteBatch.DrawString(spriteFont, "Score: " + Map.Player.Score, new Vector2(10, 50), Color.Black);
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
    }
}
