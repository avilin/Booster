using Booster.Levels.Entities;
using Microsoft.Xna.Framework;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerUp : IStateMove
    {
        public void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = (player.HitBox.Y + player.HitBox.Height - 1) / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = playerHitBoxInNextPosition.Y / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerNextTopY = playerHitBoxInNextPosition.Y;

            for (int j = firstYTileToCheck; j >= lastYTileToCheck; j--)
            {
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextTopY >= (tile.HitBox.Y + tile.HitBox.Height))
                    {
                        continue;
                    }

                    if (tile.CollisionType == CollisionTypes.Block)
                    {
                        nextPlayerPositionY = tile.HitBox.Y + tile.HitBox.Height + player.BoundingBox.OffSetTop;
                        player.Speed *= Vector2.UnitX;
                        tile.OnCollision(player);
                    }
                }
                if (nextPlayerPositionY != nextPosition.Y)
                {
                    break;
                }
                for (int k = firstXTileToCheck; k <= lastXTileToCheck; k++)
                {
                    ICollisionableObject tile = map.Tiles[k, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    tile.OnCollision(player);
                    if (!tile.Active)
                    {
                        map.Tiles[k, j] = null;
                    }
                }
            }
            player.Position = Vector2.UnitX * player.Position + Vector2.UnitY * nextPlayerPositionY;
        }
    }
}
