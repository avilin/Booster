using Booster.Levels.Entities;
using Microsoft.Xna.Framework;

namespace Booster.Levels.StrategyMove
{
    public interface IStrategyMove
    {
        void Move(ICollisionable entity, Vector2 nextPosition, Map map);
    }
}