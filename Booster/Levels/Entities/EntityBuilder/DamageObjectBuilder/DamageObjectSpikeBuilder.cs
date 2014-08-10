using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class DamageObjectSpikeBuilder : IDamageObjectBuilder
    {
        private DamageBlock damageObject;
        private Resources resources;

        public DamageObjectSpikeBuilder(Resources resources, Vector2 position)
        {
            damageObject = new Spike(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            damageObject.Texture = resources.SpriteSheets["items"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            damageObject.SourceRect = resources.SpriteSheets["items"].ObjectLocation["spikes.png"];
        }

        public void BuildDestinationRect()
        {
            damageObject.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            damageObject.LayerDepth = 0.2f;
        }

        public void BuildBoundingBox()
        {
            damageObject.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            damageObject.CollisionType = CollisionTypes.None;
        }

        public void BuildDamage()
        {
            damageObject.Damage = 1;
        }

        public DamageBlock GetResult()
        {
            return damageObject;
        }
    }
}
