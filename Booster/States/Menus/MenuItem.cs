using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.States.Menus
{
    public delegate void MenuItemDelegate();

    public class MenuItem
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        public MenuItemDelegate MenuItemAction;

        public MenuItem(string name)
        {
            Name = name;
            Enabled = true;
            Scale = 0.7f;
            Color = Color.Green;
            Position = Vector2.Zero;
        }

        public virtual void DoAction()
        {
            MenuItemAction();
        }
    }
}
