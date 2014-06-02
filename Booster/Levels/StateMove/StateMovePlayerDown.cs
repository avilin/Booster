using Booster.Levels.Entities;
using Microsoft.Xna.Framework;

namespace Booster.Levels.StateMove
{
    public class StateMovePlayerDown : StateMovePlayer
    {
        public override void Move(ICollisionableObject entity, Vector2 nextPosition, Map map)
        {
            Player player = entity as Player;

            Rectangle playerHitBoxInNextPosition = player.BoundingBox.BoxInPosition(nextPosition);

            int firstXTileToCheck = player.HitBox.X / map.TileSide;
            firstXTileToCheck = (int)MathHelper.Clamp(firstXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int lastXTileToCheck = (player.HitBox.X + player.HitBox.Width - 1) / map.TileSide;
            lastXTileToCheck = (int)MathHelper.Clamp(lastXTileToCheck, 0, map.Tiles.GetLength(0) - 1);
            int firstYTileToCheck = player.HitBox.Y / map.TileSide;
            firstYTileToCheck = (int)MathHelper.Clamp(firstYTileToCheck, 0, map.Tiles.GetLength(1) - 1);
            int lastYTileToCheck = (playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height - 1) / map.TileSide;
            lastYTileToCheck = (int)MathHelper.Clamp(lastYTileToCheck, 0, map.Tiles.GetLength(1) - 1);

            float nextPlayerPositionY = nextPosition.Y;
            int playerBottomY = player.HitBox.Y + player.HitBox.Height;
            int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

            for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
            {
                for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
                {
                    ICollisionableObject tile = map.Tiles[i, j];
                    if (tile == null)
                    {
                        continue;
                    }
                    if (playerNextBottomY <= tile.HitBox.Y || playerBottomY > tile.HitBox.Y)
                    {
                        continue;
                    }
                    if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
                    {
                        nextPlayerPositionY = tile.HitBox.Y - player.BoundingBox.OffSetBottom;
                        //player.Speed *= Vector2.UnitX;
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
