using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        Texture2D testSprite;
        GameState currentState;
        GameState previousState;
        KeyboardState kb;
        Player player;

        //animation fields
        int currentFrame;
        double frameRate;
        double secondsPerFrame;
        double timeCounter;

        List<GameObject> objectList;
        Stack<GameObject> sortHolder;

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

            objectList = new List<GameObject>();
            sortHolder = new Stack<GameObject>();
            
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

            PotatoDebugging();

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

            foreach (GameObject n in objectList)
            {
                if (n is Player)
                {
                    n.FiniteState();
                }
            }

            //Must hold down P, O, and G at the same time to activate level editor
            if (kb.IsKeyDown(Keys.P) && kb.IsKeyDown(Keys.O) && kb.IsKeyDown(Keys.G))
            {
                //Show dialog goes here.
            }

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

            //Draws potatos to test DrawLevel
            foreach (GameObject g in objectList)
            {
                g.Draw(spriteBatch);
            }

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

        #region Sorting Logic
        //Adds object g to the list of game objects, sorted by draw level
        private void AddObjectToList(GameObject g)
        {
            if (objectList.Count == 0)
            {
                objectList.Add(g);
            }
            else
            {
                if (g.DrawLevel == objectList[(objectList.Count - 1)].DrawLevel)
                {
                    objectList.Add(g);
                }
                else
                {
                    for (int i = (objectList.Count - 1); i >= 0; i--)
                    {
                        if (objectList[i].DrawLevel == g.DrawLevel)
                        {
                            break;
                        }
                        sortHolder.Push(objectList[i]);
                        objectList.Remove(objectList[i]);
                    }

                    objectList.Add(g);

                    while (sortHolder.Count > 0)
                    {
                        objectList.Add(sortHolder.Pop());
                    }

                } //End of sorting logic


            }

        }
        #endregion

        //Run this in LoadContent if you need to test drawing objects to the screen
        private void PotatoDebugging()
        {
            testSprite = Content.Load<Texture2D>("potato");

            AddObjectToList(new CapturedChicken(1, testSprite, new Rectangle(0, 0, 30, 30), Color.Red));
            AddObjectToList(new CapturedChicken(5, testSprite, new Rectangle(0, 15, 30, 30), Color.Blue));
            AddObjectToList(new CapturedChicken(4, testSprite, new Rectangle(0, 30, 30, 30), Color.Green));
            AddObjectToList(new CapturedChicken(3, testSprite, new Rectangle(0, 45, 30, 30), Color.Yellow));
            AddObjectToList(new CapturedChicken(2, testSprite, new Rectangle(0, 60, 30, 30), Color.White));
            AddObjectToList(new Player(6, testSprite, new Rectangle(50, 50, 100, 100), Color.Wheat, 50, 50));

        }


    }

   



}
