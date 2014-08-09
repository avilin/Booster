﻿using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityBuilder.SimpleTileBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockRightCreator : EntityCreator
    {
        private SimpleTileDirector director = new SimpleTileDirector();

        public BlockRightCreator()
        {
            //this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockRightBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}