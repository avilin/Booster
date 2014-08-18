using Booster.Input;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States
{
    public class GameIntro : IGameState
    {
        private IStateManager stateManager;
        private float opacity;
        private float showningSpeed;
        private Range range;

        public GameIntro(IStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public void Initialize()
        {
            opacity = 0.25f;
            showningSpeed = 0.001f;
            range = new Range(0.25f, 1f);
        }

        public void Update(GameTime gameTime)
        {
            opacity += showningSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!range.IsInRange(opacity))
            {
                showningSpeed *= -1;
            }

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
            SpriteFont spriteFont = stateManager.Resources.SpriteFont;
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            string mensaje = "Press Start";
            spriteBatch.Begin();
            Vector2 size = spriteFont.MeasureString(mensaje);
            Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = position - size * 0.5f;
            Color color = Color.Green * opacity;
            spriteBatch.DrawString(spriteFont, mensaje, position, color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}