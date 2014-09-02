using Microsoft.Xna.Framework;

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

        public MenuItem()
        {
            Name = "Menu Option";
            Enabled = true;
            Scale = 0.7f;
            Color = Color.Red;
            Position = Vector2.Zero;
        }

        public void DoAction()
        {
            if (Enabled == false)
            {
                return;
            }
            MenuItemAction();
        }
    }
}