using Booster.Util;
using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlayerBuilder: IPlayerBuilder
    {
        private Player player;
        private Resources resources;

        public PlayerBuilder(Resources resources, Vector2 position)
        {
            player = new Player(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            BuildAnimations();
            BuildSounds();
        }

        private void BuildAnimations()
        {
            Dictionary<String, Animation> playerAnimations = new Dictionary<string, Animation>();

            AnimationCreator director = new AnimationCreator();
            IAnimationBuilder builder;

            builder = new PlayerAnimationStandBuilder(resources, player.Position);
            director.Construct(builder);
            playerAnimations.Add("default", builder.GetProduct());

            builder = new PlayerAnimationWalkBuilder(resources, player.Position);
            director.Construct(builder);
            playerAnimations.Add("move", builder.GetProduct());

            builder = new PlayerAnimationHurtBuilder(resources, player.Position);
            director.Construct(builder);
            playerAnimations.Add("hurt", builder.GetProduct());

            builder = new PlayerAnimationDeadBuilder(resources, player.Position);
            director.Construct(builder);
            playerAnimations.Add("dead", builder.GetProduct());

            builder = new PlayerAnimationJumpBuilder(resources, player.Position);
            director.Construct(builder);
            playerAnimations.Add("onAir", builder.GetProduct());

            player.Animations = playerAnimations;
        }

        private void BuildSounds()
        {
            player.JumpSound = resources.SoundEffects["jump"];
            player.HitSound = resources.SoundEffects["hit"];
        }

        public void BuildBoundingBox()
        {
            player.BoundingBox = new Box(16, 32, 16, 32);
        }

        public void BuildCollisionType()
        {
            player.CollisionType = CollisionTypes.None;
        }

        public void BuildHealth()
        {
            player.Health = 4;
        }

        public void BuildStatesTime()
        {
            Dictionary<EntityStates, Duration> statesTime = new Dictionary<EntityStates, Duration>();
            statesTime[EntityStates.Hit] = new Duration(1000);
            statesTime[EntityStates.Dead] = new Duration(1000);
            player.StatesTime = statesTime;
        }

        public Player GetResult()
        {
            return player;
        }
    }
}
