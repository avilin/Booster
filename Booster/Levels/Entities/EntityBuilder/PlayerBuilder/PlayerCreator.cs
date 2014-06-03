using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlayerCreator
    {
        public void Contruct(IPlayerBuilder playerBuider)
        {
            playerBuider.BuildAnimations();
            playerBuider.BuildBoundingBox();
            playerBuider.BuildCollisionType();
            playerBuider.BuildHealth();
            playerBuider.BuildStatesTime();
        }
    }
}
