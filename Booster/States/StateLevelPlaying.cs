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
        private Resources resources;

        private Map map;
        private Camera2D camera2D;

        public StateLevelPlaying(IGameStateContext stateManager, Resources resources)
        {
            this.stateManager = stateManager;
            this.resources = resources;

            map = new Map(resources);
            camera2D = new Camera2D();
        }

        public void LoadMap(string file)
        {
            map.LoadMap(file);
        }

        public void Initialize()
        {
            camera2D.Initialize(stateManager.Game.GraphicsDevice.Viewport);
            map.Initialize();
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

            map.Update(gameTime);
            map.MovePlayer(gameTime, acceleration);

            if (map.Player.CurrentEntityStates.Contains(EntityStates.Win))
            {
                stateManager.CurrentState = GameStates.StoryMenu;
                return;
            }

            if (!map.Player.Active)
            {
                Initialize();
                return;
            }

            Vector2 cameraPosition = new Vector2((int)map.Player.Position.X, (int)map.Player.Position.Y);
            camera2D.Update(cameraPosition, map.Tiles.GetLength(0) * map.TileSide, map.Tiles.GetLength(1) * map.TileSide);
        }

        public void Draw(GameTime gameTime)
        {
            //Game.GraphicsDevice.Clear(Color.White);
            stateManager.Game.GraphicsDevice.Clear(new Color(92, 129, 162));
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera2D.Transform);
            map.Draw(gameTime, spriteBatch, camera2D);
            spriteBatch.End();

            spriteBatch.Begin();
            map.Player.DrawLifes(spriteBatch, resources);
            spriteBatch.DrawString(spriteFont, "Score: " + map.Player.Score, new Vector2(10, 50), Color.Black);
            spriteBatch.End();
        }
    }
}
