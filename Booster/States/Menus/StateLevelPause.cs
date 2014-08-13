using System.Collections.Generic;

namespace Booster.States.Menus
{
    public class StateLevelPause : StaticMenu
    {
        public StateLevelPause(IGameStateContext stateManager)
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
            stateManager.CurrentState = GameStates.Playing;
            stateManager.Resources.Songs["level_music"].Play();
        }

        public void QuitLevelActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}