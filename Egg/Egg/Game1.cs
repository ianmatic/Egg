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

        List<Texture2D> tileList;
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
        Enemy enemy;
        Enemy enemy2;
        Enemy enemy3;

        Player player;

        //animation fields
        Texture2D spriteSheet;


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
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            tileList = new List<Texture2D>();
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
            spriteSheet = Content.Load<Texture2D>("sprites");
                
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

            tileList.Add(LTopLeft);
            tileList.Add(LTopMid);
            tileList.Add(LTopRight);
            tileList.Add(LMidLeft);
            tileList.Add(LMidRight);
            tileList.Add(LBotLeft);
            tileList.Add(LBotRight);
            tileList.Add(LBotMid);
            tileList.Add(dBotLeft);
            tileList.Add(dBotMid);
            tileList.Add(dBotRight);
            tileList.Add(dMidLeft);
            tileList.Add(dMidRight);
            tileList.Add(dTopLeft);
            tileList.Add(dSolid);
            tileList.Add(dTopRight);
            tileList.Add(nLeftTop);
            tileList.Add(nLeftBot);
            tileList.Add(nRightBot);
            tileList.Add(nRightTop);

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

            if (SingleKeyPress(Keys.F8))
            {
                enemy.DebugCollision = !enemy.DebugCollision;
            }

            if (SingleKeyPress(Keys.F9))
            {
                player.Hitbox = new Rectangle(player.LastCheckpoint.X, player.LastCheckpoint.Y, 75, 75);
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
                    if (player.IsDebugging) //debugging text for player
                    {
                        spriteBatch.DrawString(menuText, "Horizontal Velocity: " + player.HorizontalVelocity, new Vector2(100, 25), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Vertical Velocity: " + player.VerticalVelocity, new Vector2(100, 60), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Player State: " + player.PlayerState, new Vector2(100, 95), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Facing right?: " + player.IsFacingRight, new Vector2(100, 130), Color.Cyan);
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

                        //This should work on any enemy (i.e. enemy list of a screen), fix this later!
                        n.CheckColliderAgainstEnemy(enemy);
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
                        n.CheckColliderAgainstEnemy(enemy);
                    }

                //}

            } // end foreach

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
            AddObjectToList(new CapturedChicken(5, testSprite, new Rectangle(0, 15, 30, 30), Color.Blue));
            AddObjectToList(new CapturedChicken(4, testSprite, new Rectangle(0, 30, 30, 30), Color.Green));
            AddObjectToList(new CapturedChicken(3, testSprite, new Rectangle(0, 45, 30, 30), Color.Yellow));
            AddObjectToList(new CapturedChicken(2, testSprite, new Rectangle(0, 60, 30, 30), Color.White));


            //PLATFORM CODE
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(700, 600, 700, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, bottomRectangle, new Rectangle(0, 600, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, bottomRectangle, new Rectangle(1300, 600, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(10, bottomRectangle, new Rectangle(1700, 200, 200, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(11, sideRectangle, new Rectangle(0, 0, 100, 900), Tile.TileType.Normal));
            AddObjectToList(new Tile(12, sideRectangle, new Rectangle(500, 600, 200, 400), Tile.TileType.Normal));
            AddObjectToList(new Tile(13, sideRectangle, new Rectangle(1500, 400, 100, 200), Tile.TileType.Normal));
            AddObjectToList(new Tile(14, sideRectangle, new Rectangle(1600, 200, 100, 400), Tile.TileType.Normal));
            AddObjectToList(new Tile(15, topRectangle, new Rectangle(0, 200, 400, 100), Tile.TileType.Normal));
            //AddObjectToList(new Tile(16, topRectangle, new Rectangle(1000, 200, 400, 100), Tile.TileType.Normal)); commented out to test bounce

            /* BOX CODE
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(200, 1600, 1300, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, sideRectangle, new Rectangle(800, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, sideRectangle, new Rectangle(100, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(9, topRectangle, new Rectangle(100,  1000, 1000, 100), Tile.TileType.Normal));
            */
            //enemy = new Enemy(new Rectangle(800, 400, 75, 75), collisionTest, 16, 60);
            //enemy = new Enemy(new Rectangle(800, 400, 75, 75), collisionTest, 16, 60, 5, 2, 100);
            enemy = new Enemy(new Rectangle(890,500, 75, 75), collisionTest, 16, 60);
            enemy2 = new Enemy(new Rectangle(275, 350, 75, 75), collisionTest, 17, 60);
            enemy3 = new Enemy(new Rectangle(200, 100, 75, 75), collisionTest, 18, 60);
            AddObjectToList(enemy);
            AddObjectToList(enemy2);
            AddObjectToList(enemy3);
            player = new Player(19, collisionTest, new Rectangle(300, 300, 75, 75), Color.White);
            AddObjectToList(player);

        }



        

    }
}
