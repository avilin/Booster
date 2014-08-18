using Booster.Levels.Entities;
using Microsoft.Xna.Framework;

namespace Booster.Levels.StateMove
{
    public interface IStateMove
    {
        void Move(ICollisionable entity, Vector2 nextPosition, Map map);
    }
}