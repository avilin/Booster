using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder.DamageObjectBuilder
{
    public class LavaTopBuilder : IDamageObjectBuilder
    {
        private DamageObject damageObject;
        private Resources resources;

        public LavaTopBuilder(Resources resources, Vector2 position)
        {
            damageObject = new DamageObject(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            damageObject.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            damageObject.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["liquidLavaTop_mid.png"];
        }

        public void BuildDestinationRect()
        {
            damageObject.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            damageObject.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            damageObject.BoundingBox = new Box(16, 0, 16, 16);
        }

        public void BuildCollisionType()
        {
            damageObject.CollisionType = CollisionTypes.None;
        }

        public void BuildDamage()
        {
            damageObject.Damage = 4;
        }

        public DamageObject GetResult()
        {
            return damageObject;
        }
    }
}
