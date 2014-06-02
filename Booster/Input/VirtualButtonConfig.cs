using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Booster.Input
{
    public class VirtualButtonConfig
    {
        public VirtualButtons VirtualButton { get; set; }

        public string Description { get; set; }

        public Keys Key { get; set; }

        public Buttons Button { get; set; }

        public VirtualButtonConfig(VirtualButtons virtualButton, Keys key, Buttons button)
        {
            VirtualButton = virtualButton;
            Key = key;
            Button = button;
        }

        public VirtualButtonConfig(VirtualButtons virtualButton, string description, Keys key, Buttons button)
        {
            VirtualButton = virtualButton;
            Description = description;
            Key = key;
            Button = button;
        }

        public void ChangeKeyConfig(Keys key)
        {
            Key = key;
        }

        public void ChangeButtonConfig(Buttons button)
        {
            Button = button;
        }
    }
}
