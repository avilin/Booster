using Microsoft.Xna.Framework;

namespace Booster.States
{
    public interface IGameState
    {
        void Initialize();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);
    }
}