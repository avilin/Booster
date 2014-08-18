using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Booster.States.Menus
{
    public class ChallengesMenu : ScrollableMenu
    {
        public ChallengesMenu(IStateManager stateManager)
            : base(stateManager)
        {
            
        }

        protected override void LoadMenuItems()
        {
            items = new List<MenuItem>();

            XDocument xdoc = XDocument.Load(@"Content\Levels\Levels.xml");
            IEnumerable<XElement> levels = xdoc.Descendants("Level").Where(level => level.Parent.Name == "Challenges");

            MenuItem item;
            foreach (XElement level in levels)
            {
                item = new MenuLevelItem(level);
                item.MenuItemAction = MissionActivated;
                items.Add(item);
            }


            item = new MenuItem();
            item.Name = "Back";
            item.MenuItemAction = BackActivated;
            items.Add(item);
        }

        public void MissionActivated()
        {
            if (!(SelectedItem is MenuLevelItem))
            {
                return;
            }
            ((StateManager)stateManager).LoadLevel(((MenuLevelItem)SelectedItem).Level);
            stateManager.CurrentState = GameStates.Loading;
        }

        public void BackActivated()
        {
            stateManager.CurrentState = GameStates.MainMenu;
        }
    }
}