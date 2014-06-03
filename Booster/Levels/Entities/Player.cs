using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Booster.Levels.StateMove;
using Booster.Util;
using Booster.Util.Animations;

namespace Booster.Levels.Entities
{
    public class Player : AnimatedEntity, IDamageable, ICollisionableObject, IMovable, IScoreable, IStateable, IUpdateableObject
    {
        private const float MaxSpeed = 0.5f;

        private IStateMove stateMovePlayer;

        public int Score { get; set; }
        public int Health { get; set; }
        public Vector2 Speed { get; set; }

        public HashSet<EntityStates> CurrentEntityStates { get; set; }

        public Dictionary<EntityStates, Duration> StatesTime { get; set; }

        public CollisionTypes CollisionType { get; set; }

        public Box BoundingBox { get; set; }

        public Rectangle HitBox
        {
            get { return BoundingBox.BoxInPosition(Position); }
        }

        public Player(Vector2 position)
            : base(position)
        {
            stateMovePlayer = null;

            Score = 0;
            Position = position;

            Speed = Vector2.Zero;

            Active = true;

            CurrentEntityStates = new HashSet<EntityStates>();
            CurrentEntityStates.Add(EntityStates.OnAir);
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentEntityStates.Contains(EntityStates.Dead))
            {
                Boolean dead = StatesTime[EntityStates.Dead].Update(gameTime);
                if (dead)
                {
                    Active = false;
                }
            }
            if (CurrentEntityStates.Contains(EntityStates.Hit))
            {
                Boolean hit = StatesTime[EntityStates.Hit].Update(gameTime);
                if (hit)
                {
                    CurrentEntityStates.Remove(EntityStates.Hit);
                }
            }
        }

        public void ApplyAcceleration(GameTime gameTime, Vector2 acceleration)
        {
            Speed += Vector2.UnitX * acceleration / 400 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (acceleration.Y != 0 && !CurrentEntityStates.Contains(EntityStates.OnAir))
            {
                Speed = Speed * Vector2.UnitX - Vector2.UnitY * 0.75f;
                CurrentEntityStates.Add(EntityStates.OnAir);
            }

            ApplyGravity(gameTime);
            ApplyFriction(gameTime);
        }

        public void ApplyGravity(GameTime gameTime)
        {
            Speed += Vector2.UnitY * 0.002f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Speed = Speed.Y > MaxSpeed ? Speed * Vector2.UnitX + MaxSpeed * Vector2.UnitY : Speed;
        }

        public void ApplyFriction(GameTime gameTime)
        {
            //Speed *= 0.05f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //int x = Speed.X < 0 ? -1 : Speed.X > 0 ? 1 : 0;
            //Speed -= Speed * 0.01f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Speed -= Vector2.UnitX * Speed * 0.01f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Vector2 GetNextPosition(GameTime gameTime)
        {
            Vector2 nextPosition = Position + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            return nextPosition;
        }

        public Vector2 GetIntermediatePositionWithX(Vector2 nextPosition, float intermediatePositionXToCheck)
        {
            Vector2 movement = nextPosition - Position;
            int intermediateMovementXToCheck = (int)intermediatePositionXToCheck - (int)Position.X;

            float intermediateMovementY = intermediateMovementXToCheck * movement.Y / movement.X;
            Vector2 intermediateMovement = new Vector2(intermediateMovementXToCheck, intermediateMovementY);
            Vector2 intermediatePosition = Position + intermediateMovement;
            return intermediatePosition;
        }

        public Vector2 GetIntermediatePositionWithY(Vector2 nextPosition, float intermediatePositionYToCheck)
        {
            Vector2 movement = nextPosition - Position;
            int intermediateMovementYToCheck = (int)intermediatePositionYToCheck - (int)Position.Y;

            float intermediateMovementX = intermediateMovementYToCheck * movement.X / movement.Y;
            Vector2 intermediateMovement = new Vector2(intermediateMovementX, intermediateMovementYToCheck);
            Vector2 intermediatePosition = Position + intermediateMovement;
            return intermediatePosition;
        }

        public void Move(GameTime gameTime, Map map)
        {
            Vector2 nextPosition = GetNextPosition(gameTime);
            //Position = new Vector2(208.02f, 544);
            //nextPosition = new Vector2(82f, 441);
            Vector2 playerMovement = nextPosition - Position;

            if (playerMovement.X > 0 && playerMovement.Y > 0)
            {
                stateMovePlayer = new StateMovePlayerRightDown();
            }
            else if (playerMovement.X > 0 && playerMovement.Y < 0)
            {
                stateMovePlayer = new StateMovePlayerRightUp();
            }
            else if (playerMovement.X > 0)
            {
                stateMovePlayer = new StateMovePlayerRight();
            }
            else if (playerMovement.X < 0 && playerMovement.Y > 0)
            {
                stateMovePlayer = new StateMovePlayerLeftDown();
            }
            else if (playerMovement.X < 0 && playerMovement.Y < 0)
            {
                stateMovePlayer = new StateMovePlayerLeftUp();
            }
            else if (playerMovement.X < 0)
            {
                stateMovePlayer = new StateMovePlayerLeft();
            }
            else if (playerMovement.Y > 0)
            {
                stateMovePlayer = new StateMovePlayerDown();
            }
            else if (playerMovement.Y < 0)
            {
                stateMovePlayer = new StateMovePlayerUp();
            }
            else
            {
                stateMovePlayer = null;
            }


            if (stateMovePlayer != null)
            {
                stateMovePlayer.Move(this, nextPosition, map);
            }
        }

        //public void MovePlayerLeft(Vector2 nextPosition, int firstXTileToCheck, int lastXTileToCheck, int firstYTileToCheck, int lastYTileToCheck, ICollisionable[,] map)
        //{
        //    float nextPlayerPositionX = nextPosition.X;
        //    int playerLeftX = HitBox.X;

        //    for (int i = lastXTileToCheck; i >= firstXTileToCheck; i--)
        //    {

        //        for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
        //        {
        //            ICollisionable tile = map[i, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            if (playerLeftX >= (tile.HitBox.X + tile.HitBox.Width))
        //            {
        //                float intermediatePlayerPositionXToCheck = tile.HitBox.X + tile.HitBox.Width + BoundingBox.OffSetLeft;
        //                Vector2 playerIntermediatePosition = GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

        //                Rectangle playerHitBoxInNextPositionToCheck = BoundingBox.HitBoxInPosition(playerIntermediatePosition);
        //                playerHitBoxInNextPositionToCheck.Offset(-1, 0);

        //                if (!tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
        //                {
        //                    continue;
        //                }


        //                if (tile.CollisionType == CollisionTypes.Block
        //                    && i < map.GetLength(0) - 1
        //                    && (map[i + 1, j] != null
        //                        && map[i + 1, j].CollisionType != CollisionTypes.Block
        //                        || map[i + 1, j] == null))
        //                {
        //                    nextPlayerPositionX = tile.HitBox.X + tile.HitBox.Width + BoundingBox.OffSetLeft;
        //                    //player.Speed *= Vector2.UnitY;
        //                    tile.OnCollision(this);
        //                }
        //            }
        //            else
        //            {
        //                float intermediatePlayerPositionXToCheck = tile.HitBox.X - BoundingBox.OffSetRight;
        //                Vector2 playerIntermediatePosition = GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

        //                Rectangle playerHitBoxInNextPositionToCheck = BoundingBox.HitBoxInPosition(playerIntermediatePosition);
        //                playerHitBoxInNextPositionToCheck.Offset(1, 0);

        //                if (tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
        //                {
        //                    tile.OnCollision(this);
        //                }
        //            }
        //        }
        //        if (nextPlayerPositionX != nextPosition.X)
        //        {
        //            break;
        //        }
        //    }
        //    Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * Position;
        //}

        //public void MovePlayerRight(Vector2 nextPosition, int firstXTileToCheck, int lastXTileToCheck, int firstYTileToCheck, int lastYTileToCheck, ICollisionable[,] map)
        //{
        //    float nextPlayerPositionX = nextPosition.X;
        //    int playerRightX = HitBox.X + HitBox.Width;

        //    for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
        //    {

        //        for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
        //        {
        //            ICollisionable tile = map[i, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            if (playerRightX <= tile.HitBox.X)
        //            {
        //                float intermediatePlayerPositionXToCheck = tile.HitBox.X - BoundingBox.OffSetRight;
        //                Vector2 playerIntermediatePosition = GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

        //                Rectangle playerHitBoxInNextPositionToCheck = BoundingBox.HitBoxInPosition(playerIntermediatePosition);
        //                playerHitBoxInNextPositionToCheck.Offset(1, 0);

        //                if (!tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
        //                {
        //                    continue;
        //                }

        //                if (tile.CollisionType == CollisionTypes.Block
        //                    && i > 0
        //                    && (map[i - 1, j] != null
        //                        && map[i - 1, j].CollisionType != CollisionTypes.Block
        //                        || map[i - 1, j] == null))
        //                {
        //                    nextPlayerPositionX = tile.HitBox.X - BoundingBox.OffSetRight;
        //                    //player.Speed *= Vector2.UnitY;
        //                    tile.OnCollision(this);
        //                }
        //            }
        //            else
        //            {
        //                float intermediatePlayerPositionXToCheck = tile.HitBox.X + tile.HitBox.Width + BoundingBox.OffSetLeft;
        //                Vector2 playerIntermediatePosition = GetIntermediatePositionWithX(nextPosition, intermediatePlayerPositionXToCheck);

        //                Rectangle playerHitBoxInNextPositionToCheck = BoundingBox.HitBoxInPosition(playerIntermediatePosition);
        //                playerHitBoxInNextPositionToCheck.Offset(-1, 0);

        //                if (tile.HitBox.Intersects(playerHitBoxInNextPositionToCheck))
        //                {
        //                    tile.OnCollision(this);
        //                }
        //            }
        //        }
        //        if (nextPlayerPositionX != nextPosition.X)
        //        {
        //            break;
        //        }
        //    }
        //    Position = Vector2.UnitX * nextPlayerPositionX + Vector2.UnitY * Position;
        //}

        //public void MovePlayerDown(Vector2 nextPosition, int firstYTileToCheck, int lastYTileToCheck, ICollisionable[,] map)
        //{
        //    float nextPlayerPositionY = nextPosition.Y;

        //    int firstXTileToCheck = HitBox.X / Level.TileSide;
        //    int lastXTileToCheck = (HitBox.X + HitBox.Width - 1) / Level.TileSide;

        //    int playerBottomY = HitBox.Y + HitBox.Height;

        //    Rectangle playerHitBoxInNextPosition = BoundingBox.HitBoxInPosition(nextPosition);
        //    int playerNextBottomY = playerHitBoxInNextPosition.Y + playerHitBoxInNextPosition.Height;

        //    for (int j = firstYTileToCheck; j <= lastYTileToCheck; j++)
        //    {
        //        for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
        //        {
        //            ICollisionable tile = map[i, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            if (playerNextBottomY <= tile.HitBox.Y || playerBottomY > tile.HitBox.Y)
        //            {
        //                continue;
        //            }

        //            if (tile.CollisionType == CollisionTypes.Block || tile.CollisionType == CollisionTypes.Top)
        //            {
        //                nextPlayerPositionY = tile.HitBox.Y - BoundingBox.OffSetBottom;
        //                //player.Speed *= Vector2.UnitX;
        //                tile.OnCollision(this);
        //            }
        //        }
        //        if (nextPlayerPositionY != nextPosition.Y)
        //        {
        //            break;
        //        }
        //        for (int k = firstXTileToCheck; k <= lastXTileToCheck; k++)
        //        {
        //            ICollisionable tile = map[k, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            tile.OnCollision(this);
        //            if (!tile.Active)
        //            {
        //                map[k, j] = null;
        //            }
        //        }
        //    }
        //    Position = Vector2.UnitX * Position + Vector2.UnitY * nextPlayerPositionY;
        //}

        //public void MovePlayerUp(Vector2 nextPosition, int firstYTileToCheck, int lastYTileToCheck, ICollisionable[,] map)
        //{
        //    float nextPlayerPositionY = nextPosition.Y;

        //    int firstXTileToCheck = HitBox.X / Level.TileSide;
        //    int lastXTileToCheck = (HitBox.X + HitBox.Width - 1) / Level.TileSide;

        //    Rectangle playerHitBoxInNextPosition = BoundingBox.HitBoxInPosition(nextPosition);

        //    int playerNextTopY = playerHitBoxInNextPosition.Y;

        //    for (int j = lastYTileToCheck; j >= firstYTileToCheck; j--)
        //    {
        //        for (int i = firstXTileToCheck; i <= lastXTileToCheck; i++)
        //        {
        //            ICollisionable tile = map[i, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            if (playerNextTopY >= (tile.HitBox.Y + tile.HitBox.Height))
        //            {
        //                continue;
        //            }

        //            if (tile.CollisionType == CollisionTypes.Block)
        //            {
        //                nextPlayerPositionY = tile.HitBox.Y + tile.HitBox.Height + BoundingBox.OffSetTop;
        //                Speed *= Vector2.UnitX;
        //                tile.OnCollision(this);
        //            }
        //        }
        //        if (nextPlayerPositionY != nextPosition.Y)
        //        {
        //            break;
        //        }
        //        for (int k = firstXTileToCheck; k <= lastXTileToCheck; k++)
        //        {
        //            ICollisionable tile = map[k, j];
        //            if (tile == null)
        //            {
        //                continue;
        //            }
        //            tile.OnCollision(this);
        //            if (!tile.Active)
        //            {
        //                map[k, j] = null;
        //            }
        //        }
        //    }
        //    Position = Vector2.UnitX * nextPosition + Vector2.UnitY * nextPlayerPositionY;
        //}

        public override void UpdateAnimation(GameTime gameTime)
        {
            ChangeAnimation();
            Animations[currentAnimation].Position = Position;
            Animations[currentAnimation].Update(gameTime);
        }

        public void ChangeAnimation()
        {
            if (!CurrentEntityStates.Contains(EntityStates.Move))
            {
                currentAnimation = "default";
            }
            if (CurrentEntityStates.Contains(EntityStates.Move))
            {
                currentAnimation = Animations.ContainsKey("move") ? "move" : currentAnimation;
            }
            if (CurrentEntityStates.Contains(EntityStates.OnAir))
            {
                currentAnimation = Animations.ContainsKey("onAir") ? "onAir" : currentAnimation;
            }
            if (CurrentEntityStates.Contains(EntityStates.Hit))
            {
                currentAnimation = Animations.ContainsKey("hurt") ? "hurt" : currentAnimation;
            }
            if (CurrentEntityStates.Contains(EntityStates.Dead))
            {
                currentAnimation = Animations.ContainsKey("dead") ? "dead" : currentAnimation;
            }
        }

        public void LoseLife(int damage)
        {
            if (CurrentEntityStates.Contains(EntityStates.Hit))
            {
                return;
            }
            CurrentEntityStates.Add(EntityStates.Hit);
            Health -= damage;
            if (Health <= 0)
            {
                CurrentEntityStates.Add(EntityStates.Dead);
                CurrentEntityStates.Remove(EntityStates.Hit);
                Health = 0;
            }
        }

        public void OnCollision(ICollisionableObject collisionableObject)
        {
        }

        public void IncrementScore(int score)
        {
            Score += score;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                SpriteEffects flip = CurrentEntityStates.Contains(EntityStates.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Animations[currentAnimation].Draw(spriteBatch, flip);
            }
        }

        public void DrawLifes(SpriteBatch spriteBatch, Resources resources)
        {
            Rectangle sourceRectFull = resources.SpriteSheets["hud"].ObjectLocation["hud_heartFull.png"];
            Rectangle sourceRectHalf = resources.SpriteSheets["hud"].ObjectLocation["hud_heartHalf.png"];
            Rectangle sourceRectEmpty = resources.SpriteSheets["hud"].ObjectLocation["hud_heartEmpty.png"];

            Rectangle sourceRect = sourceRectEmpty;
            int lifes = Health;
            Rectangle destinationRect = new Rectangle(2, 2, 53, 45);
            for (int i = 0; i < 5; i++)
            {
                if (i != 0)
                {
                    lifes -= 2;
                }
                if (lifes < 2)
                {
                    sourceRect = lifes <= 0 ? sourceRectEmpty : sourceRectHalf;
                }
                else
                {
                    sourceRect = sourceRectFull;
                }

                DrawHeart(spriteBatch, resources.SpriteSheets["hud"].SpriteSheet, destinationRect, sourceRect);
                destinationRect.Offset(55, 0);
            }
        }

        public void DrawHeart(SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle destinationRect, Rectangle sourceRect)
        {
            spriteBatch.Draw(spriteSheet, destinationRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}