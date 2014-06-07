using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class PlayerCreator : EntityCreator
    {
        private PlayerDirector director = new PlayerDirector();

        public PlayerCreator()
        {
            //this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IPlayerBuilder builder = new PlayerBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
