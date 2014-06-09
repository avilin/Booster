﻿using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class ScoreObjectHighCreator : EntityCreator
    {
        private ScoreObjectDirector director = new ScoreObjectDirector();

        public ScoreObjectHighCreator()
        {
            //this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IScoreObjectBuilder builder = new ScoreObjectHighBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}