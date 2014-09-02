using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Booster.States.Menus
{
    public class MainMenu : StaticMenu
    {
        public MainMenu(IStateManager stateManager)
            : base(stateManager)
        {
            
        }

        protected override void LoadMenuItems()
        {
            items = new List<MenuItem>();

            MenuItem item;

            item = new MenuItem();
            item.Name = "Story Mode";
            item.MenuItemAction = StoryModeActivated;
            items.Add(item);

            item = new MenuItem();
            item.Name = "Challenges";
            item.MenuItemAction = ChallengesActivated;
            items.Add(item);

            item = new MenuItem();
            item.Name = "Change to Fullscreen";
            item.MenuItemAction = ChangeResolutionActivated;
            items.Add(item);

            item = new MenuItem();
            item.Name = "Reset Progress";
            item.MenuItemAction = ResetProgressActivated;
            items.Add(item);

            item = new MenuItem();
            item.Name = "Exit";
            item.MenuItemAction = QuitActivated;
            items.Add(item);
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
                return;
                //graphics.PreferredBackBufferWidth = 800;
                //graphics.PreferredBackBufferHeight = 600;
                //graphics.IsFullScreen = false;
            }
            else
            {
                graphics.PreferredBackBufferWidth = (int)(graphics.GraphicsDevice.DisplayMode.Width * 0.8);
                graphics.PreferredBackBufferHeight = (int)(graphics.GraphicsDevice.DisplayMode.Height * 0.8);
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