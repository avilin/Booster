using Booster.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Xml.Linq;

namespace Booster.States
{
    public class LevelCompleted : IGameState
    {
        private IStateManager stateManager;
        private string mensaje;
        private XElement levelCompleted;

        public LevelCompleted(IStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public void Initialize()
        {
            string score;
            score = levelCompleted.Elements("Score").OrderByDescending(element => element.Attribute("score").Value).First().Attribute("score").Value;
            mensaje = "Best score: " + score;
        }

        public void Update(GameTime gameTime)
        {
            InputSystem inputSystem = InputSystem.GetInstance();
            inputSystem.GetActions();
            if (inputSystem.CurrentActions.Contains(VirtualButtons.A))
            {
                if (levelCompleted.Parent.Name == "StoryLevels" && levelCompleted.NextNode != null)
                {
                    ((StateManager)stateManager).LoadLevel(levelCompleted.NodesAfterSelf().OfType<XElement>().First());
                    stateManager.CurrentState = GameStates.Loading;
                }
                else
                {
                    stateManager.Resources.Songs["level_music"].Stop();
                    stateManager.Resources.Songs["menu_music"].Play();
                    stateManager.CurrentState = GameStates.MainMenu;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            stateManager.Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = (SpriteBatch)stateManager.Game.Services.GetService(typeof(SpriteBatch));
            SpriteFont spriteFont = stateManager.Resources.SpriteFont;
            Viewport viewport = stateManager.Game.GraphicsDevice.Viewport;
            spriteBatch.Begin();
            spriteBatch.Draw(stateManager.Resources.Backgrounds["intro_background"], viewport.Bounds, Color.White);
            Vector2 size = spriteFont.MeasureString(mensaje);
            Vector2 position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            position = position - size * 0.5f;
            spriteBatch.DrawString(spriteFont, mensaje, position, Color.Yellow, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void ChangeLevelCompleted(XElement level)
        {
            this.levelCompleted = level;
        }
    }
}