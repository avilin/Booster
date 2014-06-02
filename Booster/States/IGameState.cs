using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Booster.Input;

namespace Booster.States
{
    public interface IGameState
    {
        void Initialize();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);
    }
}
