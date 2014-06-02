using Booster.Levels.Entities;
using Microsoft.Xna.Framework;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerLeft : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = playerHitBoxInNextPosition.X / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionX = nextPosition.X;
            int playerNextLeftX = playerHitBoxInNextPosition.X;

            for (int i = firstXTileToCheck; i >= lastXTileToCheck; i--)
            {
                for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextLeftX >= (tile.HitBox.X + tile.HitBox.Width))
                    {
                        continue;
                    }
                    if (tile.CollisionType == CollisionTypes.Block)
                    {
                        nextPlayerPositionX = tile.HitBox.X + tile.HitBox.Width + player.BoundingBox.OffSetLeft;
                        //player.Speed *= Vector2.UnitY;
                        tile.OnCollision(player);
                    }
                }
                if (nextPlayerPositionX != nextPosition.X)
                {
                    break;
                }
                for (int k = firstYTileToCheck; k <= lastYTileToCheck; k++)
                {
                    ICollisionableObject tile = map.Tiles[i, k];
                    if (tile == null)
                    {
                        continue;
                    }
                    tile.OnCollision(player);
                    if (!tile.Active)
                    {
                        map.Tiles[i, k] = null;
                    }
                }
            }
            player.Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * player.Position;
        }
    }
}
