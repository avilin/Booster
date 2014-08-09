using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Booster.Levels;
using Booster.States;
using System.Xml.Linq;
using System.Linq;

namespace Booster.States.Menus
{
    public class StoryMenu : Menu
    {
        public StoryMenu(GameStateContext stateManager)
            : base(stateManager)
        {

        }

        public override void Initialize()
        {
            Items = new List<MenuItem>();

            LoadLevels();

            MenuItem item = new MenuItem();
            item.Name = "Back";
            item.MenuItemAction = BackActivated;
            Items.Add(item);

            base.Initialize();
        }

        public void LoadLevels()
        {
            XDocument xdoc = XDocument.Load(@"Content\Levels\Levels.xml");
            IEnumerable<XElement> levels = xdoc.Descendants("Level").Where(level => level.Parent.Name == "StoryLevels");

            MenuItem item;
            foreach (XElement level in levels)
            {
                item = new MenuLevelItem(level);
                item.MenuItemAction = MissionActivated;
                //item.File = @"Content\Levels\" + level.Attribute("file").Value;
                Items.Add(item);
            }
        }

        public void MissionActivated()
        {
            if (!(SelectedItem is MenuLevelItem))
            {
                return;
            }
            stateManager.CurrentState = GameStates.Level;
            ((GameStateContext)stateManager).LoadLevel(((MenuLevelItem)SelectedItem).Level);
        }

        public void BackActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}