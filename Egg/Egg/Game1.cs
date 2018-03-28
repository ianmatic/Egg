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
        //test textures
        Texture2D bottomRectangle;
        Texture2D topRectangle;
        Texture2D sideRectangle;
        Texture2D collisionTest;

        GameState currentState;
        //GameState previousState;
        KeyboardState kb;
        KeyboardState oldKB;
        Player player;

        //animation fields
        int currentFrame;
        double frameRate;
        double secondsPerFrame;
        double timeCounter;


        //Tile Fields
        public Texture2D LTopLeft;
        public Texture2D LTopMid;
        public Texture2D LTopRight;
        public Texture2D LMidRight;
        public Texture2D LMidLeft;
        public Texture2D LBotLeft;
        public Texture2D LBotMid;
        public Texture2D LBotRight;
        public Texture2D dTopLeft;
        public Texture2D dTopMid;
        public Texture2D dTopRight;
        public Texture2D dMidLeft;
        public Texture2D dSolid;
        public Texture2D dMidRight;
        public Texture2D dBotLeft;
        public Texture2D dBotMid;
        public Texture2D dBotRight;
        public Texture2D nLeftBot;
        public Texture2D nLeftTop;
        public Texture2D nRightBot;
        public Texture2D nRightTop;

        //DO NOT ADD DIRECTLY TO THIS LIST
        List<GameObject> objectList;
        Stack<GameObject> sortHolder;

        List<Texture2D> tileSpriteList;


        //Map Builder Tool
        Mappy Builder = new Mappy();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 3800;
            graphics.PreferredBackBufferHeight = 2000;
            graphics.ApplyChanges();
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
            currentState = GameState.Menu;

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

            tileSpriteList = new List<Texture2D>();
            //Put tile loop here

            PotatoDebugging();

            //animation Stuff
            currentFrame = 1;
            frameRate = 60.0; //assuming we are doing 60fps here, change if not
            secondsPerFrame = 1.0f / frameRate;
            timeCounter = 0;

            //loading Tiles
            
            LTopLeft = Content.Load<Texture2D>(@"tiles\LTopLeft");
            LTopMid = Content.Load<Texture2D>(@"tiles\LTopMid");
            LTopRight = Content.Load<Texture2D>(@"tiles\LTopRight");
            LMidLeft = Content.Load<Texture2D>(@"tiles\LMidLeft");
            LMidRight = Content.Load<Texture2D>(@"tiles\LMidRight");
            LBotLeft = Content.Load<Texture2D>(@"tiles\LBotLeft");
            LBotRight = Content.Load<Texture2D>(@"tiles\LBotRight");
            LBotMid = Content.Load<Texture2D>(@"tiles\LBotMid");
            dBotLeft = Content.Load<Texture2D>(@"tiles\dBotLeft");
            dBotMid = Content.Load<Texture2D>(@"tiles\dBotMid");
            dBotRight = Content.Load<Texture2D>(@"tiles\dBotRight");
            dMidLeft = Content.Load<Texture2D>(@"tiles\dMidLeft");
            dMidRight = Content.Load<Texture2D>(@"tiles\dMidRight");
            dTopLeft = Content.Load<Texture2D>(@"tiles\dTopLeft");
            dSolid = Content.Load<Texture2D>(@"tiles\dSolid");
            dTopMid = Content.Load<Texture2D>(@"tiles\dTopMid");
            dTopRight = Content.Load<Texture2D>(@"tiles\dTopRight");
            nLeftTop = Content.Load<Texture2D>(@"tiles\nLeftTop");
            nLeftBot = Content.Load<Texture2D>(@"tiles\nLeftbot");
            nRightBot = Content.Load<Texture2D>(@"tiles\nRightBot");
            nRightTop = Content.Load<Texture2D>(@"tiles\nRightTop");
            
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

            oldKB = kb;
            kb = Keyboard.GetState();

            //FSM for switching between main menu, game, and level transition screens
            switch (currentState)
            {
                case GameState.Menu:
                    if (kb.IsKeyDown(Keys.Enter))
                    {
                        currentState = GameState.Game;
                    }                   
                    break;

                case GameState.Game:
                    GameUpdateLoop();
                    //Transition to level end not yet implemented
                    break;

                    //Not yet implemented due to being a polish feature
                case GameState.GameOver:
                    break;
            }            

            //Must hold down P, O, and G at the same time to activate level editor
            if (kb.IsKeyDown(Keys.P) && kb.IsKeyDown(Keys.O) && kb.IsKeyDown(Keys.G))
            {
                //Show dialog goes here.
                Builder.ShowDialog();
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

            //Draws sprites & text based on FSM
            switch (currentState)
            {
                case GameState.Menu:
                    spriteBatch.DrawString(menuText, "Egg", new Vector2(350, 200), Color.White);
                    spriteBatch.DrawString(menuText, "Press Enter", new Vector2(300, 300), Color.White);
                    break;

                case GameState.Game:
                    //Draws potatos to test DrawLevel
                    foreach (GameObject g in objectList)
                    {
                        g.Draw(spriteBatch);
                    }
                    break;

                case GameState.GameOver:
                    spriteBatch.DrawString(menuText, "You beat a level, but you shouldn't see this yet.", new Vector2(350, 200), Color.White);
                    break;
            }
                       
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Returns true if a key was held for a single frame
        public bool SingleKeyPress(Keys n)
        {
            if (kb.IsKeyDown(n) && !oldKB.IsKeyDown(n))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        //Any logic during the game loop (minus drawing) goes here, as the Update loop is intended to hold logic involving the FSM between menus
        private void GameUpdateLoop()
        {
            foreach (GameObject n in objectList)
            {
                //if (n.IsActive)
                //{
                    if (n is Player)
                    {
                        n.FiniteState();
                    }
                    else if (n is Enemy)
                    {
                        Enemy e = (Enemy)n;
                        e.FiniteState();
                        e.UpdateEnemyData();
                        e.CheckColliderAgainstPlayer(player);
                    }
                    else
                    {
                        n.CheckColliderAgainstPlayer(player);
                    }

                //}

            } // end foreach

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

        #region Sorting Logic
        //Adds object g to the list of game objects, sorted by draw level. DO NOT directly add to objectList, or the sorting will be off!
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
            bottomRectangle = Content.Load<Texture2D>("red");
            sideRectangle = Content.Load<Texture2D>("blue");
            topRectangle = Content.Load<Texture2D>("green");
            collisionTest = Content.Load<Texture2D>("white");

            AddObjectToList(new CapturedChicken(1, testSprite, new Rectangle(0, 0, 30, 30), Color.Red));
            AddObjectToList(new CapturedChicken(4, testSprite, new Rectangle(0, 30, 30, 30), Color.Green));
            AddObjectToList(new CapturedChicken(3, testSprite, new Rectangle(0, 45, 30, 30), Color.Yellow));
            AddObjectToList(new CapturedChicken(2, testSprite, new Rectangle(0, 60, 30, 30), Color.White));

            //PLATFORM CODE
            AddObjectToList(new Tile(11, bottomRectangle, new Rectangle(1900, 600, 300, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(1000, 600, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, bottomRectangle, new Rectangle(2300, 600, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, sideRectangle, new Rectangle(0, 0, 100, 900), Tile.TileType.Normal));
            AddObjectToList(new Tile(9, bottomRectangle, new Rectangle(1700, 600, 300, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(10, sideRectangle, new Rectangle(1500, 400, 200, 400), Tile.TileType.Normal));
            
            /* BOX CODE
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(200, 1600, 1300, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, sideRectangle, new Rectangle(800, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, sideRectangle, new Rectangle(100, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(9, topRectangle, new Rectangle(100,  1000, 1000, 100), Tile.TileType.Normal));
            */
            

            player = new Player(5, collisionTest, new Rectangle(1300, 300, 100, 100), Color.White, 50, 50);
            AddObjectToList(player);

        }


        //Gets a texture based on the string (s) passed in
        public Texture2D GetTexture(string s)
        {
            switch (s)
            {
                case "LTopLeft":
                    return LTopLeft;
                case "LTopMid":
                    return LTopMid;
                case "LTopRight":
                    return LTopRight;
                case "LMidLeft":
                    return LMidLeft;
                case "LMidRight":
                    return LMidRight;
                case "LBotLeft":
                    return LBotLeft;
                case "LBotMid":
                    return LBotMid;
                case "LBotRight":
                    return LBotRight;

                case "dTopLeft":
                    return dTopLeft;
                case "dTopMid":
                    return dTopMid;
                case "dTopRight":
                    return dTopRight;
                case "dMidLeft":
                    return dMidLeft;
                case "dSolid":
                    return dSolid;
                case "dMidRight":
                    return dMidRight;
                case "dBotLeft":
                    return dBotLeft;
                case "dBotMid":
                    return dBotMid;
                case "dBotRight":
                    return dBotRight;

                case "nLeftTop":
                    return nLeftTop;
                case "nLeftBot":
                    return nLeftBot;
                case "nRightTop":
                    return nRightTop;
                case "nRightBot":
                    return nRightBot;

                default:    //failsafe case
                    return dSolid;
            }
        }

    }
}
