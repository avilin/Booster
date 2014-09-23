using Booster.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States
{
    public class GameOver : IGameState
    {
        private IStateManager stateManager;

        public GameOver(IStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();
            if (inputSystem.CurrentActions.Contains(VirtualButtons.A))
            {
                stateManager.Resources.Songs["level_music"].Stop();
                stateManager.Resources.Songs["menu_music"].Play();
                stateManager.CurrentState = GameStates.MainMenu;
            }
        }

        public void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            //SpriteFont spriteFont = stateManager.Resources.SpriteFont;
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            //string mensaje = "Game Over";
            spriteBatch.Begin();
            spriteBatch.Draw(stateManager.Resources.Backgrounds["gameover_background"], viewport.Bounds, Color.White);
            //Vector2 size = spriteFont.MeasureString(mensaje);
            //Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            //position = position - size * 0.5f;
            //spriteBatch.DrawString(spriteFont, mensaje, position, Color.Red, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}