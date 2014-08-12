using Booster.Input;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Booster.States
{
    public class LoadingState : IGameState
    {
        private IGameStateContext stateManager;
        private string mensaje;
        private XElement level;
        private Duration waitTime;

        public LoadingState(IGameStateContext stateManager)
        {
            this.stateManager = stateManager;
        }

        public void Initialize()
        {
            waitTime = new Duration(2000);
            string levelName = level.Attribute("name").Value;
            mensaje = "Loading " + levelName;
        }

        public void Update(GameTime gameTime)
        {
            bool change = waitTime.Update(gameTime);
            if (change)
            {
                stateManager.CurrentState = GameStates.Level;
                ((Level)stateManager.States[GameStates.Level]).LoadMap(level);
            }
        }

        public void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            spriteBatch.Begin();
            Vector2 size = spriteFont.MeasureString(mensaje);
            Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = position - size * 0.5f;
            spriteBatch.DrawString(spriteFont, mensaje, position, Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void LoadLevel(XElement level)
        {
            this.level = level;
        }
    }
}