using Booster.Levels.Entities;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels
{
    public class ScoreObjectHighBuilder : IScoreObjectBuilder
    {
        private ScoreObject scoreObject;
        private Resources resources;

        public ScoreObjectHighBuilder(Resources resources, Vector2 position)
        {
            scoreObject = new ScoreObject(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            scoreObject.Texture = resources.SpriteSheets["items"].SpriteSheet;
            scoreObject.CollisionSound = resources.SoundEffects["coin"];
        }

        public void BuildSourceRect()
        {
            scoreObject.SourceRect = resources.SpriteSheets["items"].ObjectLocation["coinGold.png"];
        }

        public void BuildDestinationRect()
        {
            scoreObject.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            scoreObject.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            scoreObject.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            scoreObject.CollisionType = CollisionTypes.None;
        }

        public void BuildScore()
        {
            scoreObject.Score = 3;
        }

        public ScoreObject GetResult()
        {
            return scoreObject;
        }
    }
}