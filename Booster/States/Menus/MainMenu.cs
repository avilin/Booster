﻿using System;
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

            item = new MenuItem();
            item.Name = "Story Mode";
            item.MenuItemAction = StoryModeActivated;
            Items.Add(item);

            item = new MenuItem();
            item.Name = "Challenges";
            item.MenuItemAction = ChallengesActivated;
            Items.Add(item);

            item = new MenuItem();
            item.Name = "Change resolution";
            item.MenuItemAction = ChangeResolutionActivated;
            Items.Add(item);

            item = new MenuItem();
            item.Name = "Quit";
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

        public void ChangeResolutionActivated()
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)stateManager.Game.Services.GetService(typeof(GraphicsDeviceManager));
            if (graphics.IsFullScreen)
            {

            }
            else
            {
                graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
                graphics.IsFullScreen = true;
            }

            graphics.GraphicsDevice.PresentationParameters.BackBufferWidth = graphics.PreferredBackBufferWidth;
            graphics.GraphicsDevice.PresentationParameters.BackBufferHeight = graphics.PreferredBackBufferHeight;
            graphics.GraphicsDevice.Viewport = new Viewport(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            graphics.ApplyChanges();

            Initialize();
        }

        public void QuitActivated()
        {
            stateManager.Game.Exit();
        }
    }
}
