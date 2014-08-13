using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States.Menus
{
    public abstract class ScrolleableMenu : Menu
    {
        protected Camera2D camera2D;
        protected int menuHeight;

        public ScrolleableMenu(IGameStateContext stateManager)
            : base(stateManager)
        {
            camera2D = new Camera2D();
            menuHeight = stateManager.Game.GraphicsDevice.Viewport.Height;
        }

        protected abstract override void LoadMenuItems();

        protected override void PositionMenuItems()
        {
            currentItem = 0;
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            MenuItem item;
            for (int i = 0; i < items.Count; i++)
            {
                float scale = 0.7f;
                item = items[i];
                item.Scale = scale;
                item.Color = Color.Green;
                Vector2 size = spriteFont.MeasureString(item.Name);
                Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
                position.Y -= viewport.Height / 4;
                position.Y += i * (size.Y * 1.2f) * scale;

                item.Position = position;

                if (!item.Enabled)
                {
                    item.Color = Color.DarkGray;
                }
            }

            menuHeight = (int)items[items.Count - 1].Position.Y + viewport.Height / 4;
            camera2D.Initialize(stateManager.Game.GraphicsDevice.Viewport);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Vector2 cameraPosition = new Vector2((int)SelectedItem.Position.X, (int)SelectedItem.Position.Y);
            camera2D.Update(cameraPosition, stateManager.Game.GraphicsDevice.Viewport.Width, menuHeight);
        }

        public override void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera2D.Transform);
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