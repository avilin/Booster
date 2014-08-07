using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Booster.Levels;
using Booster.States;

namespace Booster.States.Menus
{
    public class StoryMenu : Menu
    {
        public StoryMenu(GameStateContext stateManager)
            : base(stateManager)
        {
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

            item = new MenuItem("Back");
            item.MenuItemAction = BackActivated;
            Items.Add(item);
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