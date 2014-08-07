using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Booster.Levels;
using Booster.States;
using Booster.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States.Menus
{
    public class ChallengesMenu : Menu
    {
        private Camera2D camera2D;
        private int menuHeight;

        public ChallengesMenu(GameStateContext stateManager)
            : base(stateManager)
        {
            camera2D = new Camera2D();
            menuHeight = stateManager.Game.GraphicsDevice.Viewport.Height;

            MenuItem item;

            item = new MenuItem("Level 1");
            item.MenuItemAction = MissionActivated;
            item.File = @"Content\Levels\Level 1.txt";
            Items.Add(item);

            item = new MenuItem("Level 2");
            item.MenuItemAction = MissionActivated;
            item.File = @"Content\Levels\Level 2.txt";
            Items.Add(item);

            item = new MenuItem("Level 3");
            item.MenuItemAction = MissionActivated;
            item.File = @"Content\Levels\Level 3.txt";
            Items.Add(item);

            item = new MenuItem("Level 4");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 5");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 6");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 7");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 8");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 9");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 10");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 11");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Level 12");
            item.MenuItemAction = MissionActivated;
            Items.Add(item);

            item = new MenuItem("Back");
            item.MenuItemAction = BackActivated;
            Items.Add(item);
        }

        public override void Initialize()
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
                position.Y -= viewport.Height / 4;
                position.Y += i * (size.Y * 1.2f) * scale;

                item.Position = position;

                if (!item.Enabled)
                {
                    item.Color = Color.DarkGray;
                }
            }

            menuHeight = (int)Items[Items.Count - 1].Position.Y + viewport.Height / 4;
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

        public void MissionActivated()
        {
            stateManager.CurrentState = GameStates.Level;
            ((GameStateContext)stateManager).LoadLevel(SelectedItem.File);
        }

        public void BackActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}