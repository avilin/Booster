using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States.Menus
{
    public abstract class StaticMenu : Menu
    {
        public StaticMenu(IGameStateContext stateManager)
            : base(stateManager)
        {

        }

        protected abstract override void LoadMenuItems();

        protected override void PositionMenuItems()
        {
            currentItem = 0;
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            for (int i = 0; i < items.Count; i++)
            {
                float scale = 0.7f;
                MenuItem item = items[i];
                item.Scale = scale;
                item.Color = Color.Green;
                Vector2 size = spriteFont.MeasureString(item.Name);
                Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
                position.Y -= (items.Count - 1) * (size.Y * 1.2f) / 2 * scale;
                position.Y += i * (size.Y * 1.2f) * scale;

                item.Position = position;

                if (!item.Enabled)
                {
                    item.Color = Color.DarkGray;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            spriteBatch.Begin();
            for (int i = 0; i < items.Count; i++)
            {
                MenuItem item = items[i];
                Vector2 size = spriteFont.MeasureString(item.Name);
                Vector2 position = item.Position - size * 0.5f * item.Scale;
                spriteBatch.DrawString(spriteFont, item.Name, position, item.Color, 0, Vector2.Zero, item.Scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }
    }
}