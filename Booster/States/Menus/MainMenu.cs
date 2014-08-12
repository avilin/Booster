using System;
using Microsoft.Xna.Framework;
using Booster.States;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Linq;

namespace Booster.States.Menus
{
    public class MainMenu : StaticMenu
    {
        public MainMenu(GameStateContext stateManager)
            : base(stateManager)
        {
            
        }

        public override void LoadMenuItems()
        {
            Items = new List<MenuItem>();

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
            item.Name = "Reset progress";
            item.MenuItemAction = ResetProgressActivated;
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
                //graphics.PreferredBackBufferWidth = 800;
                //graphics.PreferredBackBufferHeight = 600;
                //graphics.IsFullScreen = false;
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

        public void ResetProgressActivated()
        {
            XDocument xdoc = XDocument.Load(@"Content\Levels\Levels.xml");
            foreach (XElement level in xdoc.Descendants("Level"))
            {
                level.Elements("Score").Remove();
            }

            foreach (XElement level in xdoc.Descendants("Level").Where(l => l.Parent.Name == "StoryLevels"))
            {
                if (xdoc.Descendants("StoryLevels").First().Descendants("Level").First() == level)
                {
                    continue;
                }
                level.Attribute("enabled").Value = "false";
            }
            xdoc.Save(@"Content\Levels\Levels.xml");
        }

        public void QuitActivated()
        {
            stateManager.Game.Exit();
        }
    }
}
