using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public class ExitEntity : SimpleTile
    {
        public ExitEntity(Vector2 position)
            : base(position)
        {

        }

        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IStateable)
            {
                ((IStateable)collisionableObject).CurrentEntityStates.Add(EntityStates.Win);
            }
        }
    }
}