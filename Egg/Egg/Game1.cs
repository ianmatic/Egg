using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Egg
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        enum GameState
        {
            Menu,
            Game,
            GameOver
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont menuText;
        GameState currentState;
        GameState previousState;
        KeyboardState kb;
        Player player;

        //animation fields
        int currentFrame;
        double frameRate;
        double secondsPerFrame;
        double timeCounter;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //biscui
        //egg
        //I wrote over someone's comment

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            player = new Player(100,100);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           menuText= Content.Load<SpriteFont>("menutext");



            //animation Stuff
            currentFrame = 1;
            frameRate = 60.0; //assuming we are doing 60fps here, change if not
            secondsPerFrame = 1.0f / frameRate;
            timeCounter = 0;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            kb = Keyboard.GetState();

            //Must hold down P, O, and G at the same time to activate level editor
            if (kb.IsKeyDown(Keys.P) && kb.IsKeyDown(Keys.O) && kb.IsKeyDown(Keys.G))
            {
                //Show dialog goes here.
            }

            player.FiniteState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(menuText, "Egg", new Vector2(350, 200), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public bool SingleKeyPress(Keys n)
        {
            return false;
        }


        private void UpdateAnimation(GameTime time)
        {
            timeCounter += time.ElapsedGameTime.TotalSeconds;

            if(timeCounter >= secondsPerFrame)
            {
                currentFrame++;
                if(currentFrame >= 4)// 4 is a placeholder for how many frames of walk there are
                {
                    currentFrame = 1;
                }
            }

            timeCounter -= secondsPerFrame;
        }
        private void DrawWalking(SpriteEffects flip)
        {
            //this is what Chris had, feel free to use or remove as needed.
            //spriteBatch.Draw(
				//marioTexture,
				//marioPosition,
				//new Rectangle(widthOfSingleSprite * currentFrame, 0, 
                //widthOfSingleSprite, marioTexture.Height),
				//Color.White,
				//0.0f,
				//Vector2.Zero,
				//1.0f,
				//flip,
				//0.0f);
        }

        private void DrawIdle(SpriteEffects flip)
        {
            //spriteBatch.Draw(
                //marioTexture,
                //marioPosition,
                //new Rectangle(0, 0, widthOfSingleSprite, marioTexture.Height),
                //Color.White,
                //0.0f,
                //Vector2.Zero,
                //1.0f,
                //flip,
                //0.0f);
        }



    }

   



}
