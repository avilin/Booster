using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Booster.Util
{
    public class SpriteSheetInfo
    {
        public Texture2D SpriteSheet { get; set; }
        public Dictionary<string, Rectangle> ObjectLocation { get; set; }

        public SpriteSheetInfo(Game game, string spriteSheetName)
        {
            ObjectLocation = new Dictionary<string, Rectangle>();

            XDocument xdoc = XDocument.Load(@"Content\Graphics\" + spriteSheetName + "_spritesheet.xml");
            SpriteSheet = game.Content.Load<Texture2D>(@"Graphics\" + xdoc.Root.Attribute("imagePath").Value);
            foreach (XElement element in xdoc.Descendants("SubTexture"))
            {
                int x = Int32.Parse(element.Attribute("x").Value);
                int y = Int32.Parse(element.Attribute("y").Value);
                int width = Int32.Parse(element.Attribute("width").Value);
                int height = Int32.Parse(element.Attribute("height").Value);
                ObjectLocation.Add(element.Attribute("name").Value, new Rectangle(x, y, width, height));
            }
        }
    }
}
