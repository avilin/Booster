using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public class ExitEntity : SimpleTile
    {
        public ExitEntity(Vector2 position)
            : base(position)
        {

        }

        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (collisionableObject is Player)
            {
                ((Player)collisionableObject).CurrentEntityStates.Add(EntityStates.Win);
            }
        }
    }
}