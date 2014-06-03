using Booster.Levels.Entities;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels
{
    public class ScoreObjectMidBuilder : IScoreObjectBuilder
    {
        private ScoreObject scoreObject;
        private Resources resources;

        public ScoreObjectMidBuilder(Resources resources, Vector2 position)
        {
            scoreObject = new ScoreObject(position);
            this.resources = resources;
        }

        public void BuildTexture()
        {
            scoreObject.Texture = resources.SpriteSheets["items"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            scoreObject.SourceRect = resources.SpriteSheets["items"].ObjectLocation["coinSilver.png"];
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
            scoreObject.Score = 5000;
        }

        public ScoreObject GetResult()
        {
            return scoreObject;
        }
    }
}