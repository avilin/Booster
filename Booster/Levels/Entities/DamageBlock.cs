using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public class DamageBlock : SimpleTile
    {
        public int Damage { get; set; }

        public DamageBlock(Vector2 position)
            : base(position)
        {
            Damage = 0;
        }

        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (collisionableObject is IDamageable)
            {
                ((IDamageable)collisionableObject).LoseLife(Damage);
            }
        }
    }
}