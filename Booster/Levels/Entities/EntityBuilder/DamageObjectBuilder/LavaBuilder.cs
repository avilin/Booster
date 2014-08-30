﻿using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class LavaBuilder : IDamageObjectBuilder
    {
        private DamageBlock damageObject;
        private Resources resources;

        public LavaBuilder(Resources resources, Vector2 position)
        {
            damageObject = new DamageBlock(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            damageObject.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            damageObject.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["liquidLava.png"];
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
            damageObject.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            damageObject.CollisionType = CollisionTypes.Block;
        }

        public void BuildDamage()
        {
            damageObject.Damage = 4;
        }

        public DamageBlock GetResult()
        {
            return damageObject;
        }
    }
}