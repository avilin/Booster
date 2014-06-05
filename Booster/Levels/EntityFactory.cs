using Booster.Levels.Entities;
using Booster.Levels.Entities.CreateEntityCommand;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Booster.Levels
{
    public static class EntityFactory
    {
        public static Entity CreateEntity(EntityType entityType, Resources resources, Vector2 position)
        {
            Entity entity = null;

            //switch (entityType)
            //{
            //    case EntityType.Player:
            //        entity = EntityFactory.CreatePlayer(resources, position);
            //        break;
            //    case EntityType.Block:
            //        entity = EntityFactory.CreateBlock(resources, position);
            //        break;
            //    case EntityType.Platform:
            //        entity = EntityFactory.CreatePlatform(resources, position);
            //        break;
            //    case EntityType.Spike:
            //        entity = EntityFactory.CreateSpike(resources, position);
            //        break;
            //    case EntityType.DamageObjectLow:
            //        entity = EntityFactory.CreateDamageBlockLow(resources, position);
            //        break;
            //    case EntityType.DamageObjectMid:
            //        entity = EntityFactory.CreateDamageBlockMid(resources, position);
            //        break;
            //    case EntityType.DamageObjectHigh:
            //        entity = EntityFactory.CreateDamageBlockHigh(resources, position);
            //        break;
            //    case EntityType.ScoreObjectLow:
            //        entity = EntityFactory.CreateScoreObjectLow(resources, position);
            //        break;
            //    case EntityType.ScoreObjectMid:
            //        entity = EntityFactory.CreateScoreObjectMid(resources, position);
            //        break;
            //    case EntityType.ScoreObjectHigh:
            //        entity = EntityFactory.CreateScoreObjectHigh(resources, position);
            //        break;
            //    case EntityType.Exit:
            //        entity = EntityFactory.CreateExit(resources, position);
            //        break;
            //}

            ICreateEntityCommand command = resources.EntityTypeCommand[entityType];
            command.Execute(ref entity, position, resources);

            return entity;
        }
        private static Entity CreatePlayer(Resources resources, Vector2 position)
        {
            PlayerCreator director = new PlayerCreator();
            PlayerBuilder builder = new PlayerBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateBlock(Resources resources, Vector2 position)
        {
            SimpleTileCreator director = new SimpleTileCreator();
            BlockBuilder builder = new BlockBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreatePlatform(Resources resources, Vector2 position)
        {

            SimpleTileCreator director = new SimpleTileCreator();
            PlatformBuilder builder = new PlatformBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateSpike(Resources resources, Vector2 position)
        {
            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectSpikeBuilder builder = new DamageObjectSpikeBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateDamageBlockLow(Resources resources, Vector2 position)
        {
            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectLowBuilder builder = new DamageObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateDamageBlockMid(Resources resources, Vector2 position)
        {
            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectMidBuilder builder = new DamageObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateDamageBlockHigh(Resources resources, Vector2 position)
        {
            DamageObjectCreator director = new DamageObjectCreator();
            DamageObjectHighBuilder builder = new DamageObjectHighBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateScoreObjectLow(Resources resources, Vector2 position)
        {
            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectLowBuilder builder = new ScoreObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateScoreObjectMid(Resources resources, Vector2 position)
        {
            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectMidBuilder builder = new ScoreObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateScoreObjectHigh(Resources resources, Vector2 position)
        {
            ScoreObjectCreator director = new ScoreObjectCreator();
            ScoreObjectHighBuilder builder = new ScoreObjectHighBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }

        private static Entity CreateExit(Resources resources, Vector2 position)
        {
            SimpleTileCreator director = new SimpleTileCreator();
            ExitBuilder builder = new ExitBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
