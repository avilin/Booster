using Booster.States.Menus;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Booster.States
{
    public class GameStateContext : IGameStateContext
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
                foreach (KeyValuePair<string, SoundEffectInstance> song in Resources.Songs.Where(song => song.Value.State == SoundState.Playing))
                {
                    song.Value.Stop();
                }
                InitializeCurrentGameState();
            }
        }

        public GameStateContext(Game game, Resources resources)
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