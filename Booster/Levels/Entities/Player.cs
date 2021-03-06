﻿using Booster.Levels.StrategyMove;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Booster.Levels.Entities
{
    public class Player : AnimatedEntity, IDamageable, ICollisionable, IMoveable, IScore, IUpdateableObject, IKeyOwner
    {
        private const float MaxSpeed = 0.5f;

        private IStrategyMove strategyMovePlayer;

        public int Keys { get; set; }
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

        public SoundEffect JumpSound { get; set; }
        public SoundEffect HitSound { get; set; }

        public Player(Vector2 position)
            : base(position)
        {
            strategyMovePlayer = null;

            Keys = 0;
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
                bool dead = StatesTime[EntityStates.Dead].Update(gameTime);
                if (dead)
                {
                    Active = false;
                }
            }
            if (CurrentEntityStates.Contains(EntityStates.Hit))
            {
                bool hit = StatesTime[EntityStates.Hit].Update(gameTime);
                if (hit)
                {
                    CurrentEntityStates.Remove(EntityStates.Hit);
                }
            }
            if (CurrentEntityStates.Contains(EntityStates.Recharge))
            {
                bool recharge = StatesTime[EntityStates.Recharge].Update(gameTime);
                if (recharge)
                {
                    CurrentEntityStates.Remove(EntityStates.Recharge);
                }
            }
            if (CurrentEntityStates.Contains(EntityStates.Boost))
            {
                bool boost = StatesTime[EntityStates.Boost].Update(gameTime);
                if (boost)
                {
                    CurrentEntityStates.Remove(EntityStates.Boost);
                    CurrentEntityStates.Add(EntityStates.Recharge);
                }
            }
        }

        public void ApplyAcceleration(GameTime gameTime, Vector2 acceleration)
        {
            if (CurrentEntityStates.Contains(EntityStates.Boost))
            {
                Speed += Vector2.UnitX * acceleration / 300 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                Speed += Vector2.UnitX * acceleration / 400 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (acceleration.Y != 0 && !CurrentEntityStates.Contains(EntityStates.OnAir))
            {
                Speed = Speed * Vector2.UnitX - Vector2.UnitY * 0.65f;
                CurrentEntityStates.Add(EntityStates.OnAir);
                JumpSound.Play();
            }
            if (!CurrentEntityStates.Contains(EntityStates.Boost))
            {
                ApplyGravity(gameTime);
            }
            ApplyFriction(gameTime);
        }

        private void ApplyGravity(GameTime gameTime)
        {
            Speed += Vector2.UnitY * 0.002f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Speed = Speed.Y > MaxSpeed ? Speed * Vector2.UnitX + MaxSpeed * Vector2.UnitY : Speed;
        }

        private void ApplyFriction(GameTime gameTime)
        {
            Speed -= Vector2.UnitX * Speed * 0.01f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        private Vector2 GetNextPosition(GameTime gameTime)
        {
            if (CurrentEntityStates.Contains(EntityStates.Boost))
            {
                Speed *= Vector2.UnitX;
            }
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
            Vector2 playerMovement = nextPosition - Position;

            if (playerMovement.X > 0 && playerMovement.Y > 0)
            {
                strategyMovePlayer = new StrategyMovePlayerRightDown();
            }
            else if (playerMovement.X > 0 && playerMovement.Y < 0)
            {
                strategyMovePlayer = new StrategyMovePlayerRightUp();
            }
            else if (playerMovement.X > 0)
            {
                strategyMovePlayer = new StrategyMovePlayerRight();
            }
            else if (playerMovement.X < 0 && playerMovement.Y > 0)
            {
                strategyMovePlayer = new StrategyMovePlayerLeftDown();
            }
            else if (playerMovement.X < 0 && playerMovement.Y < 0)
            {
                strategyMovePlayer = new StrategyMovePlayerLeftUp();
            }
            else if (playerMovement.X < 0)
            {
                strategyMovePlayer = new StrategyMovePlayerLeft();
            }
            else if (playerMovement.Y > 0)
            {
                strategyMovePlayer = new StrategyMovePlayerDown();
            }
            else if (playerMovement.Y < 0)
            {
                strategyMovePlayer = new StrategyMovePlayerUp();
            }
            else
            {
                strategyMovePlayer = null;
            }

            if (strategyMovePlayer != null)
            {
                strategyMovePlayer.Move(this, nextPosition, map);
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

        private void ChangeAnimation()
        {
            if (!CurrentEntityStates.Contains(EntityStates.Move))
            {
                currentAnimation = "default";
            }
            if (CurrentEntityStates.Contains(EntityStates.Move))
            {
                currentAnimation = Animations.ContainsKey("move") ? "move" : currentAnimation;
            }
            if (CurrentEntityStates.Contains(EntityStates.OnAir) || CurrentEntityStates.Contains(EntityStates.Boost))
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
            HitSound.Play();
            Health -= damage;
            if (Health <= 0)
            {
                CurrentEntityStates.Add(EntityStates.Dead);
                CurrentEntityStates.Remove(EntityStates.Hit);
                Health = 0;
            }
        }

        public void OnCollision(ICollisionable collisionableObject)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                SpriteEffects flip = CurrentEntityStates.Contains(EntityStates.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                Animations[currentAnimation].Draw(spriteBatch, flip);
            }
        }
    }
}