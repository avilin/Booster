﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Booster.Levels;
using Booster.States;
using System.Xml.Linq;
using System.Linq;

namespace Booster.States.Menus
{
    public class StoryMenu : StaticMenu
    {
        public StoryMenu(GameStateContext stateManager)
            : base(stateManager)
        {

        }

        public override void LoadMenuItems()
        {
            Items = new List<MenuItem>();

            XDocument xdoc = XDocument.Load(@"Content\Levels\Levels.xml");
            IEnumerable<XElement> levels = xdoc.Descendants("Level").Where(level => level.Parent.Name == "StoryLevels");

            MenuItem item;
            foreach (XElement level in levels)
            {
                item = new MenuLevelItem(level);
                item.MenuItemAction = MissionActivated;
                Items.Add(item);
            }

            item = new MenuItem();
            item.Name = "Back";
            item.MenuItemAction = BackActivated;
            Items.Add(item);
        }

        public void MissionActivated()
        {
            if (!(SelectedItem is MenuLevelItem))
            {
                return;
            }
            ((GameStateContext)stateManager).LoadLevel(((MenuLevelItem)SelectedItem).Level);
            stateManager.CurrentState = GameStates.Loading;
        }

        public void BackActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}