using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Booster.States;
using Booster.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Booster.States.Menus
{
    public abstract class Menu : IGameState
    {
        protected IGameStateContext stateManager;

        public List<MenuItem> Items { get; set; }
        protected int currentItem;

        public MenuItem SelectedItem
        {
            get
            {
                return Items.Count > 0 ? Items[currentItem] : null;
            }
        }

        public Menu(IGameStateContext stateManager)
        {
            this.stateManager = stateManager;
            Items = new List<MenuItem>();
        }

        public void SelectNext()
        {
            if (Items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem + 1) % Items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        public void SelectPrevious()
        {
            if (Items.Count > 0)
            {
                do
                {
                    currentItem = (currentItem - 1 + Items.Count) % Items.Count;
                } while (!SelectedItem.Enabled);
            }
        }

        public void Initialize()
        {
            LoadMenuItems();

            stateManager.Resources.Songs["menu_music"].Play();

            PositionMenuItems();
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                MenuItem item = Items[i];
                if (item == SelectedItem)
                {
                    if (item.Scale < 1.0f)
                    {
                        item.Color = Color.Yellow;
                        item.Scale += 0.001f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        item.Scale = 1.0f;
                    }
                }
                else if (item.Scale > 0.7f)
                {
                    item.Color = Color.Green;
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

        public abstract void LoadMenuItems();
        public abstract void PositionMenuItems();
    }
}
