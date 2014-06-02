using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Booster.States;
using Booster.Input;
using System.IO;
using Booster.States.Menus;

namespace Booster.Levels
{
    public class Level : IGameState, IGameStateContext
    {
        public Game Game { get; set; }

        private IGameStateContext stateManager;

        public Dictionary<GameStates, IGameState> States { get; set; }

        private GameStates currentState;
        public GameStates CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                if (States.ContainsKey(value))
                {
                    currentState = value;
                    if (currentState != GameStates.Playing)
                    {
                        InitializeCurrentGameState();
                    }
                }
                else
                {
                    stateManager.CurrentState = value;
                }
            }
        }

        public Level(IGameStateContext stateManager, Game game)
        {
            this.Game = game;
            this.stateManager = stateManager;
        }

        public void LoadMap(string file)
        {
            ((StateLevelPlaying)States[GameStates.Playing]).LoadMap(file);
            InitializeCurrentGameState();
        }

        public void InitializeCurrentGameState()
        {
            States[CurrentState].Initialize();
        }

        public void Initialize()
        {
            States = new Dictionary<GameStates, IGameState>();
            States[GameStates.Playing] = new StateLevelPlaying(this);
            States[GameStates.Pause] = new StateLevelPause(this);

            CurrentState = GameStates.Playing;
        }

        public void Update(GameTime gameTime)
        {
            States[CurrentState].Update(gameTime);
        }

        //public void MovePlayer(Vector2 nextPosition)
        //{
        //    Vector2 playerMovement = nextPosition - player.Position;
        //    int numberOfChecks = (int)playerMovement.Length() + 1;
        //    Vector2 nextPositionToTry = player.Position + playerMovement / numberOfChecks;

        //    for (int i = 0; i < numberOfChecks; i++)
        //    {
        //        Rectangle playerRectangle = player.BoundingBox.HitBoxInPosition(nextPositionToTry);
        //        if (!CheckPlayerTilesCollisions(playerRectangle))
        //        {
        //            player.Position = nextPositionToTry;
        //            nextPositionToTry += playerMovement / numberOfChecks;
        //        }
        //        else
        //        {
        //            if (playerMovement.X != 0 && playerMovement.Y != 0)
        //            {
        //                MovePlayer(nextPosition * Vector2.UnitX + player.Position * Vector2.UnitY);
        //                MovePlayer(nextPosition * Vector2.UnitY + player.Position * Vector2.UnitX);
        //            }
        //            break;
        //        }
        //    }
        //}

        public void Draw(GameTime gameTime)
        {
            States[CurrentState].Draw(gameTime);
        }
    }
}