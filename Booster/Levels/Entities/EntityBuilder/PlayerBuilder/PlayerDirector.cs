﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlayerDirector
    {
        public void Construct(IPlayerBuilder playerBuider)
        {
            playerBuider.BuildAnimations();
            playerBuider.BuildSounds();
            playerBuider.BuildBoundingBox();
            playerBuider.BuildCollisionType();
            playerBuider.BuildHealth();
            playerBuider.BuildStatesTime();
        }
    }
}
