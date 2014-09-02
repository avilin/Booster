using Booster.States;
using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Booster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Booster : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private IStateManager stateManager;
        private Resources resources;

        public Booster()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            stateManager = new StateManager(this, resources);

            stateManager.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            resources = new Resources(this);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            // TODO: use this.Content to load your game content here

            Services.AddService(typeof(GraphicsDeviceManager), graphics);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            stateManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            stateManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void OnDeactivated(Object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);
            if (stateManager != null 
                && stateManager.CurrentState == GameStates.Level 
                && ((Level)stateManager.States[GameStates.Level]).CurrentState == GameStates.Playing)
            {
                ((Level)stateManager.States[GameStates.Level]).CurrentState = GameStates.Pause;
            }
        }
    }
}