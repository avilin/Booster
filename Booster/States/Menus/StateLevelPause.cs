using Booster.Input;
using Booster.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.States.Menus
{
    public class StateLevelPause : Menu
    {

        public StateLevelPause(IGameStateContext stateManager)
            : base(stateManager)
        {
            MenuItem item;

            item = new MenuItem();
            item.Name = "Continue";
            item.MenuItemAction = ContinueActivated;
            Items.Add(item);

            item = new MenuItem();
            item.Name = "Exit Level";
            item.MenuItemAction = QuitLevelActivated;
            Items.Add(item);
        }

        public void ContinueActivated()
        {
            stateManager.CurrentState = GameStates.Playing;
        }

        public void QuitLevelActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}
