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
            Map.Player.DrawLifes(spriteBatch, stateManager.Resources);
            spriteBatch.DrawString(spriteFont, "Score: " + Map.Player.Score, new Vector2(10, 50), Color.Black);
            spriteBatch.End();
        }
    }
}
