using Booster.Levels;
using Booster.States.Menus;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.States
{
    public class GameStateContext : IGameStateContext
    {
        public Game Game { get; set; }
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
                currentState = value;
                InitializeCurrentGameState();
            }
        }

        public GameStateContext(Game game)
        {
            this.Game = game;
        }

        public void Initialize()
        {
            States = new Dictionary<GameStates, IGameState>();
            States[GameStates.GameIntro] = new GameIntro(this);
            States[GameStates.MainMenu] = new MainMenu(this);
            States[GameStates.StoryMenu] = new StoryMenu(this);
            States[GameStates.Level] = new Level(this, Game);

            CurrentState = GameStates.GameIntro;
        }

        public void Update(GameTime gameTime)
        {
            States[CurrentState].Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            States[CurrentState].Draw(gameTime);
        }

        public void LoadLevel(string file)
        {
            (States[GameStates.Level] as Level).LoadMap(file);
        }

        public void InitializeCurrentGameState()
        {
            States[CurrentState].Initialize();
        }
    }
}
