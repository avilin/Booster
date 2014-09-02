using Booster.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Booster.States.Menus
{
    public abstract class Menu : IGameState
    {
        protected IStateManager stateManager;

        protected List<MenuItem> items;
        protected int currentItem;

        protected MenuItem SelectedItem
        {
            get
            {
                return items.Count > 0 ? items[currentItem] : null;
            }
        }

        public Menu(IStateManager stateManager)
        {
            this.stateManager = stateManager;
            items = new List<MenuItem>();
        }

        private void SelectNext()
        {
            if (items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem + 1) % items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        private void SelectPrevious()
        {
            if (items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem - 1 + items.Count) % items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        public void Initialize()
        {
            LoadMenuItems();

            MediaPlayer.Play(stateManager.Resources.Songs["menu_music"]);

            PositionMenuItems();
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < items.Count; i++)
            {
                MenuItem item = items[i];
                if (item == SelectedItem)
                {
                    if (item.Scale < 1.0f)
                    {
                        item.Color = Color.White;
                        item.Scale += 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        item.Scale = 1.0f;
                    }
                }
                else if (item.Scale > 0.7f)
                {
                    item.Color = Color.Red;
                    item.Scale -= 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    item.Scale = 0.7f;
                }
            }

            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();

            if (inputSystem.CurrentActions.Contains(VirtualButtons.Down) && !inputSystem.PreviousActions.Contains(VirtualButtons.Down))
            {
                SelectNext();
            }
            if (inputSystem.CurrentActions.Contains(VirtualButtons.Up) && !inputSystem.PreviousActions.Contains(VirtualButtons.Up))
            {
                SelectPrevious();
            }
            if (inputSystem.CurrentActions.Contains(VirtualButtons.A) && !inputSystem.PreviousActions.Contains(VirtualButtons.A))
            {
                stateManager.Resources.SoundEffects["menu"].Play();
                SelectedItem.DoAction();
            }
        }

        public abstract void Draw(GameTime gameTime);

        protected abstract void LoadMenuItems();
        protected abstract void PositionMenuItems();
    }
}