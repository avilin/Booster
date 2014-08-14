using Booster.States.Menus;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Booster.States
{
    public class StateManager : IStateManager
    {
        public Resources Resources { get; set; }
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
                MediaPlayer.Stop();
                InitializeCurrentGameState();
            }
        }

        public StateManager(Game game, Resources resources)
        {
            Game = game;
            this.Resources = resources;
        }

        public void Initialize()
        {
            States = new Dictionary<GameStates, IGameState>();
            States[GameStates.GameIntro] = new GameIntro(this);
            States[GameStates.MainMenu] = new MainMenu(this);
            States[GameStates.StoryMenu] = new StoryMenu(this);
            States[GameStates.ChallengesMenu] = new ChallengesMenu(this);
            States[GameStates.Level] = new Level(this);
            States[GameStates.Loading] = new LoadingState(this);
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
            (States[GameStates.Loading] as LoadingState).LoadLevel(level);
        }

        public void InitializeCurrentGameState()
        {
            States[CurrentState].Initialize();
        }
    }
}