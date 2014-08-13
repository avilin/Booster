using Microsoft.Xna.Framework.Input;

namespace Booster.Input
{
    public class VirtualButtonConfig
    {
        public VirtualButtons VirtualButton { get; set; }

        public Keys Key { get; set; }

        public Buttons Button { get; set; }

        public VirtualButtonConfig(VirtualButtons virtualButton, Keys key, Buttons button)
        {
            VirtualButton = virtualButton;
            Key = key;
            Button = button;
        }
    }
}