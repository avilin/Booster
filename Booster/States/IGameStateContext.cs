using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.States
{
    public interface IGameStateContext
    {
        Resources Resources { get; set; }
        Game Game { get; set; }
        Dictionary<GameStates, IGameState> States { get; set; }
        GameStates CurrentState { get; set; }

        void Initialize();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        void InitializeCurrentGameState();
    }
}
