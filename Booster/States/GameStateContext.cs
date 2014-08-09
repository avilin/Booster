using Booster.Levels;
using Booster.States.Menus;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Booster.States
{
    public class GameStateContext : IGameStateContext
    {
        private Resources resources;
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

        public GameStateContext(Game game, Resources resources)
        {
            Game = game;
            this.resources = resources;
        }

        public void Initialize()
        {
            States = new Dictionary<GameStates, IGameState>();
            States[GameStates.GameIntro] = new GameIntro(this);
            States[GameStates.MainMenu] = new MainMenu(this);
            States[GameStates.StoryMenu] = new StoryMenu(this);
            States[GameStates.ChallengesMenu] = new ChallengesMenu(this);
            States[GameStates.Level] = new Level(this, resources);
            States[GameStates.GameOver] = new GameOver(this);
            States[GameStates.LevelCompleted] = new LevelCompleted(this);

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

        public void LoadLevel(XElement level)
        {
            //if (!File.Exists(file))
            //{
            //    CurrentState = GameStates.MainMenu;
            //    return;
            //}
            (States[GameStates.Level] as Level).LoadMap(level);
        }

        public void InitializeCurrentGameState()
        {
            States[CurrentState].Initialize();
        }
    }
}
