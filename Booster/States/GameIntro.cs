using Booster.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States
{
    public class GameIntro : IGameState
    {
        private IGameStateContext stateManager;

        public GameIntro(IGameStateContext stateManager)
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
            if (inputSystem.CurrentActions.Contains(VirtualButtons.Start))
            {
                stateManager.CurrentState = GameStates.MainMenu;
            }
        }

        public void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            string mensaje = "Pulsa Start";
            spriteBatch.Begin();
            Vector2 size = spriteFont.MeasureString(mensaje);
            Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = position - size * 0.5f;
            spriteBatch.DrawString(spriteFont, mensaje, position, Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}