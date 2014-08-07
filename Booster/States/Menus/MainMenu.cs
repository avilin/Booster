using System;
using Microsoft.Xna.Framework;
using Booster.States;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States.Menus
{
    public class MainMenu : Menu
    {
        public MainMenu(GameStateContext stateManager)
            : base(stateManager)
        {
            MenuItem item;

            item = new MenuItem("Story Mode");
            item.MenuItemAction = StoryModeActivated;
            Items.Add(item);

            item = new MenuItem("Challenges");
            item.MenuItemAction = ChallengesActivated;
            Items.Add(item);

            item = new MenuItem("Options");
            item.MenuItemAction = OptionsActivated;
            Items.Add(item);

            item = new MenuItem("Quit");
            item.MenuItemAction = QuitActivated;
            Items.Add(item);
        }

        public void StoryModeActivated()
        {
            stateManager.CurrentState = GameStates.StoryMenu;
        }

        public void ChallengesActivated()
        {
            stateManager.CurrentState = GameStates.ChallengesMenu;
        }

        public void OptionsActivated()
        {
            //GraphicsDeviceManager graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));
            //graphics.PreferredBackBufferWidth = 800;
            //graphics.PreferredBackBufferHeight = 600;

            //graphics.GraphicsDevice.PresentationParameters.BackBufferWidth = graphics.PreferredBackBufferWidth;
            //graphics.GraphicsDevice.PresentationParameters.BackBufferHeight = graphics.PreferredBackBufferHeight;
            //graphics.GraphicsDevice.Viewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            //graphics.ApplyChanges();

            //Initialize();
        }

        public void QuitActivated()
        {
            stateManager.Game.Exit();
        }
    }
}
