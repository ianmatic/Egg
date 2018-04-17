using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

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
            Options,
            Game,
            GameOver
        }

        List<Texture2D> tileList;
        List<Enemy> tempEnemyList;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont menuText;
        Texture2D testSprite;
        //test textures
        Texture2D bottomRectangle;
        Texture2D topRectangle;
        Texture2D sideRectangle;
        Texture2D collisionTest;
        Screen mainScreen = new Screen("variableSizeDemo");

        GameState currentState;
        //GameState previousState;
        KeyboardState kb;
        KeyboardState oldKB;
        Enemy enemy;
        Enemy enemy2;
        Enemy enemy3;

        bool paused = false;

        Player player;

        //animation fields
        public Texture2D spriteSheet;
        int currentFrame = 1;
        double fps = 60.0;
        double secondsPerFrame;
        double timeCounter = 0;
        int numSpritesPerSheet = 4;
        int widthOfASingleSprite = 795 / 4;
        
        
        GameTime gameTime = new GameTime();


        //Tile Fields
        public Texture2D blankTile;
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

            tempEnemyList = new List<Enemy>();

            PotatoDebugging();

            //animation Stuff
            spriteSheet = Content.Load<Texture2D>("sprites");
           
            



            #region loading Tiles
            blankTile = Content.Load<Texture2D>(@"clearTile");
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

            tileList.Add(blankTile);

            tileList.Add(LTopLeft);
            tileList.Add(LTopMid);
            tileList.Add(LTopRight);
            tileList.Add(LMidLeft);
            tileList.Add(blankTile);
            tileList.Add(LMidRight);
            tileList.Add(LBotLeft);
            tileList.Add(LBotMid);
            tileList.Add(LBotRight);

            tileList.Add(dTopLeft);
            tileList.Add(dTopMid);
            tileList.Add(dTopRight);
            tileList.Add(dMidLeft);
            tileList.Add(dSolid);
            tileList.Add(dMidRight);
            tileList.Add(dBotLeft);
            tileList.Add(dBotMid);
            tileList.Add(dBotRight);

            tileList.Add(nLeftTop);
            tileList.Add(nLeftBot);
            tileList.Add(nRightBot);
            tileList.Add(nRightTop);
            #endregion 
            
            mainScreen.UpdateTiles(tileList);
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
            if (!paused)
            {
                if (SingleKeyPress(Keys.P) && currentState != GameState.Options)
                {
                    paused = true;
                }
                //FSM for switching between main menu, game, and level transition screens
                switch (currentState)
                {
                    case GameState.Menu:
                        if (SingleKeyPress(Keys.Enter))
                        {
                            currentState = GameState.Game;
                        }
                        else if (SingleKeyPress(Keys.Tab))
                        {
                            currentState = GameState.Options;
                        }
                        break;
                    case GameState.Options:
                        if (SingleKeyPress(Keys.Tab))
                        {
                            if (paused)
                            {
                                currentState = GameState.Game;
                            }
                            else
                            {
                                currentState = GameState.Menu;
                            }
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

                if (SingleKeyPress(Keys.F9) || player.Hitpoints <= 0)
                {
                    player.Hitbox = new Rectangle(player.LastCheckpoint.X, player.LastCheckpoint.Y, 75, 75);
                    player.PlayerState = PlayerState.IdleRight;
                    player.HorizontalVelocity = 0;
                    player.VerticalVelocity = 0;
                    player.Hitpoints = 5;
                    player.InHitStun = false;
                }
            }
            else
            {
                if (SingleKeyPress(Keys.Tab))
                {
                    if (currentState == GameState.Game)
                    {
                        currentState = GameState.Options;
                    }
                    else if (currentState == GameState.Options)
                    {
                        currentState = GameState.Game;
                    }

                }
                if (SingleKeyPress(Keys.P) && currentState != GameState.Options)
                {
                    paused = false;
                }
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
            UpdateAnimation(gameTime);
            //modified spriteBatch begin so the images are scaled by nearest neighbor instead of getting antialiased
            //this makes it so the pixel art keeps crisp lines
            spriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            //Draws sprites & text based on FSM
            switch (currentState)
            {
                case GameState.Menu:
                    spriteBatch.DrawString(menuText, "Egg", new Vector2(350, 200), Color.White);
                    spriteBatch.DrawString(menuText, "Press Enter", new Vector2(300, 300), Color.White);
                    spriteBatch.DrawString(menuText, "Or", new Vector2(365, 400), Color.White);
                    spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(240, 500), Color.White);
                    break;
                case GameState.Options:
                    spriteBatch.DrawString(menuText, "Options (Stretch Goal)", new Vector2(350, 200), Color.White);
                    spriteBatch.DrawString(menuText, "Rebind keys: ", new Vector2(150, 400), Color.White);
                    spriteBatch.DrawString(menuText, "Toggle fullscreen: ", new Vector2(650, 400), Color.White);
                    if (paused)
                    {
                        spriteBatch.DrawString(menuText, "Press tab to return to game", new Vector2(50, 50), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press tab to return to menu", new Vector2(50, 50), Color.White);
                    }
                    break;
                case GameState.Game:
                    mainScreen.DrawTilesFromMap(spriteBatch, @"..\..\..\..\Resources\levelExports\platformDemo", tileList);
                    //Draws potatos to test DrawLevel
                    
                    foreach (GameObject g in objectList)
                    {
                       
                        g.Draw(spriteBatch);

                        if(g is Player)
                        {
                            Player p = (Player)g;

                            if(p.PlayerState == PlayerState.IdleLeft)
                            {
                                DrawIdle(SpriteEffects.FlipHorizontally);
                            }
                            else if(p.PlayerState == PlayerState.IdleRight)
                            {
                                DrawIdle(SpriteEffects.None);
                            }
                            else if (p.PlayerState == PlayerState.WalkLeft)
                            {
                                DrawWalking(SpriteEffects.FlipHorizontally); 
                            }
                            else if (p.PlayerState == PlayerState.WalkRight)
                            {
                                DrawWalking(SpriteEffects.None);
                            }
                            else if (p.PlayerState == PlayerState.JumpLeft)
                            {
                                DrawIdle(SpriteEffects.FlipHorizontally);
                            }
                            else if (p.PlayerState == PlayerState.JumpRight)
                            {
                                DrawIdle(SpriteEffects.None);
                            }
                            else if(p.PlayerState == PlayerState.Fall)
                            {
                                DrawIdle(SpriteEffects.None);
                            }
                        }
                       
                    }
                    if (player.IsDebugging) //debugging text for player
                    {
                        spriteBatch.DrawString(menuText, "Horizontal Velocity: " + player.HorizontalVelocity, new Vector2(100, 25), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Vertical Velocity: " + player.VerticalVelocity, new Vector2(100, 60), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Player State: " + player.PlayerState, new Vector2(100, 95), Color.Cyan);
                        spriteBatch.DrawString(menuText, "Facing right?: " + player.IsFacingRight, new Vector2(100, 130), Color.Cyan);
                        spriteBatch.DrawString(menuText, "hitpoints: " + player.Hitpoints, new Vector2(100, 165), Color.Cyan);
                    }
                    break;

                case GameState.GameOver:
                    spriteBatch.DrawString(menuText, "You beat a level, but you shouldn't see this yet.", new Vector2(350, 200), Color.White);
                    break;
            }
            if (paused && currentState == GameState.Game)
            {
                spriteBatch.DrawString(menuText, "Paused", new Vector2(900, 400), Color.White);
                spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(800, 500), Color.White);
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
            Tile[,] tileSet = mainScreen.UpdateTiles(tileList);
                      
            foreach (GameObject n in objectList)
            {
                //if (n.IsActive)
                //{
                    if (n is Player)
                    {
                        Player p = (Player)n;
                        p.FiniteState();
                    

                    //This should work on any enemy (i.e. enemy list of a screen), fix this later!
                    foreach (Enemy e in tempEnemyList)
                        {
                            if (!p.InBounceLockout)
                            {
                                p.CheckColliderAgainstEnemy(e);
                            }
                        }
                        
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
                        foreach (Enemy e in tempEnemyList)
                        {
                            n.CheckColliderAgainstEnemy(enemy);
                        }

                    }

                //}

            } // end foreach

            foreach (Tile t in tileSet)
            {
                if (t.DefaultSprite != null)
                {
                    t.CheckColliderAgainstPlayer(player);

                    foreach (Enemy e in tempEnemyList)
                    {
                        t.CheckColliderAgainstEnemy(enemy);
                    }
                }
            }
        }

            

        #region Sorting Logic
        //Adds object g to the list of game objects, sorted by draw level. DO NOT directly add to objectList, or the sorting will be off!
        private void AddObjectToList(GameObject g)
        {
            if (g is Enemy)
            {
                Enemy e = (Enemy)g;
                tempEnemyList.Add(e);
            }

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
                        if (objectList[i].DrawLevel <= g.DrawLevel)
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


            #region CapturedChickens

            AddObjectToList(new CapturedChicken(115, testSprite, new Rectangle(0, 0, 30, 30), Color.Red));
            AddObjectToList(new CapturedChicken(111, testSprite, new Rectangle(0, 15, 30, 30), Color.Aqua));
            AddObjectToList(new CapturedChicken(114, testSprite, new Rectangle(0, 30, 30, 30), Color.Green));
            AddObjectToList(new CapturedChicken(113, testSprite, new Rectangle(0, 45, 30, 30), Color.Yellow));
            AddObjectToList(new CapturedChicken(112, testSprite, new Rectangle(0, 60, 30, 30), Color.White));

            #endregion

            #region Checkpoints
            AddObjectToList(new Checkpoint(3, collisionTest, new Rectangle(400, 350, 75, 75)));
            AddObjectToList(new Checkpoint(3, collisionTest, new Rectangle(1500, 250, 75, 75)));
            #endregion

            #region Platform Code            
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(700, 500, 700, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, bottomRectangle, new Rectangle(0, 500, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, bottomRectangle, new Rectangle(1300, 500, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(10, bottomRectangle, new Rectangle(1700, 200, 200, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(11, sideRectangle, new Rectangle(0, 0, 100, 900), Tile.TileType.Normal));
            AddObjectToList(new Tile(12, sideRectangle, new Rectangle(500, 500, 200, 400), Tile.TileType.Normal));
            AddObjectToList(new Tile(13, sideRectangle, new Rectangle(1100, 400, 100, 200), Tile.TileType.Normal));
            AddObjectToList(new Tile(14, sideRectangle, new Rectangle(1600, 200, 100, 400), Tile.TileType.Normal));
            AddObjectToList(new Tile(15, topRectangle, new Rectangle(0, 300, 400, 100), Tile.TileType.Normal));
            //AddObjectToList(new Tile(16, topRectangle, new Rectangle(1000, 200, 400, 100), Tile.TileType.Normal)); commented out to test bounce
            #endregion

            /* BOX CODE
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(200, 1600, 1300, 100), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, sideRectangle, new Rectangle(800, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, sideRectangle, new Rectangle(100, 1000, 100, 800), Tile.TileType.Normal));
            AddObjectToList(new Tile(9, topRectangle, new Rectangle(100,  1000, 1000, 100), Tile.TileType.Normal));
            */
            //enemy = new Enemy(new Rectangle(800, 400, 75, 75), collisionTest, 16, 60);
            //enemy = new Enemy(new Rectangle(800, 400, 75, 75), collisionTest, 4, 60, 5, 2, 100); //moving enemy
            enemy = new Enemy(new Rectangle(890, 500, 75, 75), collisionTest, 16, 60);
            enemy2 = new Enemy(new Rectangle(225, 150, 75, 75), collisionTest, 4, 60);
            enemy3 = new Enemy(new Rectangle(500, 150, 75, 75), collisionTest, 4, 60, 5, 2 , 100);
            AddObjectToList(enemy);
            AddObjectToList(enemy2);
            AddObjectToList(enemy3);
            player = new Player(5, collisionTest, new Rectangle(450, 350, 75, 75), Color.White);
            AddObjectToList(player);

            foreach (GameObject g in objectList)
            {
                Debug.WriteLine("Test");
            }
        }
        private void UpdateAnimation(GameTime time)
        {
           
            secondsPerFrame = 1.0f / fps;
            timeCounter += time.ElapsedGameTime.TotalSeconds;

            if (timeCounter >= secondsPerFrame)
            {
                currentFrame++;
                if (currentFrame >= 4) currentFrame = 1;
               
            }

            timeCounter -= secondsPerFrame;
        }
        public void DrawWalking( SpriteEffects flip)
        {
            
            spriteBatch.Draw(
                spriteSheet,
                new Vector2(player.Hitbox.X ,player.Hitbox.Y-200),
                new Rectangle(widthOfASingleSprite * currentFrame, 0, widthOfASingleSprite, spriteSheet.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                flip,
                0.0f);

        }
        public void DrawIdle( SpriteEffects flip)
        {
                spriteBatch.Draw(
                spriteSheet,
                 new Vector2(player.Hitbox.X , player.Hitbox.Y- 200),
                new Rectangle(0, 0, widthOfASingleSprite, spriteSheet.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                flip,
                0.0f);

        }


    }
}
