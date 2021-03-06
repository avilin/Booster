﻿using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Booster.States.Menus
{
    public class PauseMenu : StaticMenu
    {
        public PauseMenu(IStateManager stateManager)
            : base(stateManager)
        {

        }

        protected override void LoadMenuItems()
        {
            items = new List<MenuItem>();

            MenuItem item;

            item = new MenuItem();
            item.Name = "Continue";
            item.MenuItemAction = ContinueActivated;
            items.Add(item);

            item = new MenuItem();
            item.Name = "Exit Level";
            item.MenuItemAction = QuitLevelActivated;
            items.Add(item);
        }

        public void ContinueActivated()
        {
            //MediaPlayer.Play(stateManager.Resources.Songs["level_music"]);
            stateManager.Resources.Songs["menu_music"].Stop();
            stateManager.Resources.Songs["level_music"].Resume();
            stateManager.CurrentState = GameStates.Playing;
        }

        public void QuitLevelActivated()
        {
            stateManager.Resources.Songs["level_music"].Stop();
            stateManager.Resources.Songs["menu_music"].Play();
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}