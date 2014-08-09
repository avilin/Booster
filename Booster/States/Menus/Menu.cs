using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Booster.States;
using Booster.Input;
using System;

namespace Booster.States.Menus
{
    public class Menu : IGameState
    {
        protected IGameStateContext stateManager;

        public List<MenuItem> Items { get; set; }
        protected int currentItem;

        public MenuItem SelectedItem
        {
            get {
                return Items.Count > 0 ? Items[currentItem] : null;
            }
        }

        public Menu(IGameStateContext stateManager)
        {
            this.stateManager = stateManager;
            Items = new List<MenuItem>();
        }

        public void SelectNext()
        {
            if (Items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem + 1) % Items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        public void SelectPrevious()
        {
            if (Items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem - 1 + Items.Count) % Items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        public virtual void Initialize()
        {
            currentItem = 0;
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            for (int i = 0; i < Items.Count; i++)
            {
                float scale = 0.7f;
                MenuItem item = Items[i];
                item.Scale = scale;
                item.Color = Color.Green;
                Vector2 size = spriteFont.MeasureString(item.Name);
                Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
                position.Y -= (Items.Count - 1) * (size.Y * 1.2f) / 2 * scale;
                position.Y += i * (size.Y * 1.2f) * scale;

                item.Position = position;

                if (!item.Enabled)
                {
                    item.Color = Color.DarkGray;
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                MenuItem item = Items[i];
                if (item == SelectedItem)
                {
                    if (item.Scale < 1.0f)
                    {
                        item.Color = Color.Yellow;
                        item.Scale += 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        item.Scale = 1.0f;
                    }
                }
                else if (item.Scale > 0.7f)
                {
                    item.Color = Color.Green;
                    item.Scale -= 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    item.Scale = 0.7f;
                }
            }

            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Down) && !inputSystem.PreviousActions.Contains(VirtualButtons.Down))
            {
                SelectNext();
            }
            if (inputSystem.CurrentActions.Contains(VirtualButtons.Up) && !inputSystem.PreviousActions.Contains(VirtualButtons.Up))
            {
                SelectPrevious();
            }
            if (inputSystem.CurrentActions.Contains(VirtualButtons.A) && !inputSystem.PreviousActions.Contains(VirtualButtons.A))
            {
                SelectedItem.DoAction();
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = (SpriteFont)stateManager.Game.Services.GetService(typeof(SpriteFont));
            spriteBatch.Begin();
            for (int i = 0; i < Items.Count; i++)
            {
                MenuItem item = Items[i];
                Vector2 size = spriteFont.MeasureString(item.Name);
                Vector2 position = item.Position - size * 0.5f * item.Scale;
                spriteBatch.DrawString(spriteFont, item.Name, position, item.Color, 0, Vector2.Zero, item.Scale, SpriteEffects.None, 0);
                //if (item == SelectedItem)
                //{
                //    Vector2 vector = new Vector2(25, 0);
                //    spriteBatch.DrawString(spriteFont, ">", position - vector, item.Color, 0, Vector2.Zero, item.Scale, SpriteEffects.None, 0.0f);
                //}
            }
            spriteBatch.End();
        }
    }
}
