using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Booster.States.Menus
{
    public class MenuLevelItem : MenuItem
    {
        public XElement Level { get; set; }
        public MenuLevelItem(XElement level)
            : base()
        {
            Level = level;
            Name = Level.Attribute("name").Value;
            Enabled = Boolean.Parse(Level.Attribute("enabled").Value);
        }
    }
}
