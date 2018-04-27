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
        SpriteFont titleText;
        SpriteFont optionsText;
        Texture2D testSprite;
        Texture2D menu;
        Texture2D options;
        //test textures
        Texture2D bottomRectangle;
        Texture2D topRectangle;
        Texture2D sideRectangle;
        Texture2D collisionTest;
        Screen mainScreen = new Screen("mapDemo");

        GameState currentState;
        //GameState previousState;
        KeyboardState kb;
        KeyboardState oldKB;
        Enemy enemy;
        Enemy enemy2;
        Enemy enemy3;
       

        bool paused = false;
        bool fullscreen = false;
        MouseState ms;
        MouseState previousMs;
        Rectangle mouseRect;
        Rectangle startRect;
        Rectangle optionsRect;
        Rectangle optionsReturnRect;
        Rectangle fullscreenRect;
        Rectangle leftRect;
        Rectangle rightRect;
        Rectangle jumpRect;
        Rectangle downRect;
        Rectangle rollRect;
        Rectangle pauseRect;
        bool rebindingLeft = false;
        bool rebindingRight = false;
        bool rebindingJump = false;
        bool rebindingDown = false;
        bool rebindingRoll = false;
        bool rebindingPause = false;
        Keys[] keys;

        Player player;

        //animation fields
        public Texture2D spriteSheet;
        int currentFrame = 1;
        double fps = 60.0;
        double secondsPerFrame;
        double timeCounter = 0;
        int numSpritesPerSheet = 4;
        int widthOfASingleSprite = 56 / 4;
        bool animationOn= false;
        Dictionary<int, Texture2D> walkFrameDictionary;

       
        GameTime gameTime = new GameTime();

        //enemy texture
        Texture2D jellyBoi;
      

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
            titleText = Content.Load<SpriteFont>("titletext");
            optionsText = Content.Load<SpriteFont>("optionsText");
            menu = Content.Load<Texture2D>("menuTexture");
            options = Content.Load<Texture2D>("optionsTexture");
            jellyBoi = Content.Load<Texture2D>("jellyboi");

            tileSpriteList = new List<Texture2D>();
            //Put tile loop here

            tempEnemyList = new List<Enemy>();
            //animation Stuff
            spriteSheet = Content.Load<Texture2D>("sprites");
            PotatoDebugging();
            walkFrameDictionary = new Dictionary<int, Texture2D>();
            
           
           
            
            



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

            previousMs = ms;
            ms = Mouse.GetState();

            mouseRect = new Rectangle(ms.X, ms.Y, 1, 1);
            if (!paused)
            {
                if (SingleKeyPress(player.BindableKb["pause"]) && currentState != GameState.Options)
                {
                    paused = true;
                }
                //FSM for switching between main menu, game, and level transition screens
                switch (currentState)
                {
                    case GameState.Menu:
                        if (SingleKeyPress(Keys.Enter) || (startRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            currentState = GameState.Game;
                        }
                        else if (SingleKeyPress(Keys.Tab) || (optionsRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            currentState = GameState.Options;
                        }
                        break;
                    case GameState.Options:
                        if (SingleKeyPress(Keys.Tab) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingRoll = false;
                            rebindingPause = false;
                            if (paused)
                            {
                                currentState = GameState.Game;
                            }
                            else
                            {
                                currentState = GameState.Menu;
                            }
                        }
                        if ((leftRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingLeft)
                        {
                            rebindingLeft = true;

                            rebindingRight = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingRoll = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                player.BindableKb["left"] = keys[0];
                                rebindingLeft = false;
                            }
                        }
                        if ((rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRight)
                        {
                            rebindingRight = true;

                            rebindingLeft = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingRoll = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                player.BindableKb["right"] = keys[0];
                                rebindingRight = false;
                            }
                        }
                        if ((rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRoll)
                        {
                            rebindingRoll = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                player.BindableKb["roll"] = keys[0];
                                rebindingRoll = false;
                            }
                        }
                        if ((jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingJump)
                        {
                            rebindingJump = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingDown = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                player.BindableKb["jump"] = keys[0];
                                rebindingJump = false;
                            }
                        }
                        if ((downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingDown)
                        {
                            rebindingDown = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingJump = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                player.BindableKb["downDash"] = keys[0];
                                rebindingDown = false;
                            }
                        }
                        if ((pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingPause)
                        {
                            rebindingPause = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingJump = false;
                            rebindingDown = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0 && keys[0] != Keys.Tab)
                            {
                                player.BindableKb["pause"] = keys[0];
                                rebindingPause = false;
                            }
                        }
                        if (fullscreenRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            fullscreen = !fullscreen;
                            graphics.ToggleFullScreen();
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

                DebugKeyboardInputs();
            }
            else
            {
                if (currentState == GameState.Options)
                {
                    if (fullscreenRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        fullscreen = !fullscreen;
                        graphics.ToggleFullScreen();
                    }
                }
                if (SingleKeyPress(Keys.Tab) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                {
                    if (currentState == GameState.Game)
                    {
                        currentState = GameState.Options;
                    }
                    else if (currentState == GameState.Options)
                    {
                        currentState = GameState.Game;
                        rebindingLeft = false;
                        rebindingRight = false;
                        rebindingJump = false;
                        rebindingDown = false;
                        rebindingRoll = false;
                        rebindingPause = false;
                    }

                }
                if ((leftRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingLeft)
                {
                    rebindingLeft = true;

                    rebindingRight = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingRoll = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        player.BindableKb["left"] = keys[0];
                        rebindingLeft = false;
                    }
                }
                if ((rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRight)
                {
                    rebindingRight = true;

                    rebindingLeft = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingRoll = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 &&  keys[0] != Keys.Tab)
                    {
                        player.BindableKb["right"] = keys[0];
                        rebindingRight = false;
                    }
                }
                if ((rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRoll)
                {
                    rebindingRoll = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 &&  keys[0] != Keys.Tab)
                    {
                        player.BindableKb["roll"] = keys[0];
                        rebindingRoll = false;
                    }
                }
                if ((jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingJump)
                {
                    rebindingJump = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingDown = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 &&  keys[0] != Keys.Tab)
                    {
                        player.BindableKb["jump"] = keys[0];
                        rebindingJump = false;
                    }
                }
                if ((downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingDown)
                {
                    rebindingDown = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingJump = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 &&  keys[0] != Keys.Tab)
                    {
                        player.BindableKb["downDash"] = keys[0];
                        rebindingDown = false;
                    }
                }
                if ((pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingPause)
                {
                    rebindingPause = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingJump = false;
                    rebindingDown = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        player.BindableKb["pause"] = keys[0];
                        rebindingPause = false;
                    }
                }
                if (SingleKeyPress(player.BindableKb["pause"]) && currentState != GameState.Options)
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


           //update the animation
            UpdateAnimation(gameTime);


            //modified spriteBatch begin so the images are scaled by nearest neighbor instead of getting antialiased
            //this makes it so the pixel art keeps crisp lines
            spriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            //Draws sprites & text based on FSM
            switch (currentState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(topRectangle, mouseRect, Color.Red); //for testing
                    spriteBatch.DrawString(titleText, "Egg", new Vector2(880, 320), Color.White);
                    startRect = new Rectangle(690, 470, 515, 32);
                    spriteBatch.DrawString(menuText, "Press Enter to Start", new Vector2(690, 470), Color.White);

                    if (startRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Enter to Start", new Vector2(690, 470), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Enter to Start", new Vector2(690, 470), Color.White);
                    }


                    spriteBatch.DrawString(menuText, "- Or -", new Vector2(860, 570), Color.White);
                    optionsRect = new Rectangle(680, 670, 545, 32);
                    if (optionsRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(680, 670), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(680, 670), Color.White);
                    }
                    break;
                case GameState.Options:
                    spriteBatch.Draw(options, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(topRectangle, mouseRect, Color.Red); //for testing
                    spriteBatch.DrawString(menuText, "Options", new Vector2(850, 300), Color.White);
                    spriteBatch.DrawString(menuText, "Rebind keys ", new Vector2(450, 450), Color.White);
                    leftRect = new Rectangle(450, 520, 170, 24);
                    rightRect = new Rectangle(450, 550, 190, 24);
                    rollRect = new Rectangle(450, 580, 80, 24);
                    jumpRect = new Rectangle(450, 610, 80, 24);
                    downRect = new Rectangle(450, 640, 170, 24);
                    pauseRect = new Rectangle(450, 670, 100, 24);
                    if (leftRect.Intersects(mouseRect) || rebindingLeft)
                    {
                        spriteBatch.DrawString(optionsText, "Move Left: " + player.BindableKb["left"].ToString(), new Vector2(450, 520), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Move Left: " + player.BindableKb["left"].ToString(), new Vector2(450, 520), Color.White);
                    }
                    if (rightRect.Intersects(mouseRect) || rebindingRight)
                    {
                        spriteBatch.DrawString(optionsText, "Move Right: " + player.BindableKb["right"].ToString(), new Vector2(450, 550), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Move Right: " + player.BindableKb["right"].ToString(), new Vector2(450, 550), Color.White);
                    }
                    if (rollRect.Intersects(mouseRect) || rebindingRoll)
                    {
                        spriteBatch.DrawString(optionsText, "Roll: " + player.BindableKb["roll"].ToString(), new Vector2(450, 580), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Roll: " + player.BindableKb["roll"].ToString(), new Vector2(450, 580), Color.White);
                    }
                    if (jumpRect.Intersects(mouseRect) || rebindingJump)
                    {
                        spriteBatch.DrawString(optionsText, "Jump: " + player.BindableKb["jump"].ToString(), new Vector2(450, 610), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Jump: " + player.BindableKb["jump"].ToString(), new Vector2(450, 610), Color.White);
                    }
                    if (downRect.Intersects(mouseRect) || rebindingDown)
                    {
                        spriteBatch.DrawString(optionsText, "Down-Dash: " + player.BindableKb["downDash"].ToString(), new Vector2(450, 640), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Down-Dash: " + player.BindableKb["downDash"].ToString(), new Vector2(450, 640), Color.White);
                    }
                    if (pauseRect.Intersects(mouseRect) || rebindingPause)
                    {
                        spriteBatch.DrawString(optionsText, "Pause: " + player.BindableKb["pause"].ToString(), new Vector2(450, 670), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Pause: " + player.BindableKb["pause"].ToString(), new Vector2(450, 670), Color.White);
                    }

                    fullscreenRect = new Rectangle(920, 450, 555, 32);
                    if (fullscreen)
                    {
                        if (fullscreenRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Fullscreen toggle: On", new Vector2(920, 450), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Fullscreen toggle: On", new Vector2(920, 450), Color.White);
                        }
                    }
                    else
                    {
                        if (fullscreenRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Fullscreen toggle: Off", new Vector2(920, 450), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Fullscreen toggle: Off", new Vector2(920, 450), Color.White);
                        }
                    }
                    optionsReturnRect = new Rectangle(50, 65, 695, 32);

                    if (paused)
                    {
                        if (optionsReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to game", new Vector2(50, 65), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to game", new Vector2(50, 65), Color.White);
                        }
                    }
                    else
                    {
                        if (optionsReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to menu", new Vector2(50, 65), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to menu", new Vector2(50, 65), Color.White);
                        }
                    }
                    break;
                case GameState.Game:
                    mainScreen.DrawTilesFromMap(spriteBatch, @"..\..\..\..\Resources\levelExports\platformDemo", tileList);
                    //Draws potatos to test DrawLevel

                    foreach (GameObject g in objectList)
                    {

                        g.Draw(spriteBatch);
                        if (animationOn == true)
                        {
                            if (g is Player)
                            {
                                Player p = (Player)g;

                                if (p.PlayerState == PlayerState.IdleLeft)
                                {
                                    DrawIdle(SpriteEffects.FlipHorizontally);
                                }
                                else if (p.PlayerState == PlayerState.IdleRight)
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
                                else if (p.PlayerState == PlayerState.Fall)
                                {
                                    DrawIdle(SpriteEffects.None);
                                }
                                else if( p.PlayerState == PlayerState.RollLeft)
                                {
                                    DrawWalking(SpriteEffects.FlipHorizontally);
                                }
                                else if (p.PlayerState == PlayerState.RollRight)
                                {
                                    DrawWalking(SpriteEffects.None);
                                }
                                else if (p.PlayerState == PlayerState.HitStunLeft)
                                {
                                    DrawIdle(SpriteEffects.FlipHorizontally);
                                }
                                else if (p.PlayerState == PlayerState.HitStunRight)
                                {
                                    DrawIdle(SpriteEffects.None);
                                }

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
                    else
                    {
                        //Draw collectibles
                        spriteBatch.Draw(testSprite, new Rectangle(135, 55, 40, 40), Color.White);
                        spriteBatch.DrawString(menuText, player.CollectedChickens.ToString(), new Vector2(200, 60), Color.Orange);
                    }
                    break;

                case GameState.GameOver:
                    spriteBatch.DrawString(menuText, "You beat a level, but you shouldn't see this yet.", new Vector2(350, 200), Color.White);
                    break;
            }
            if (paused && currentState == GameState.Game)
            {
                spriteBatch.DrawString(menuText, "Paused", new Vector2(870, 400), Color.White);
                optionsReturnRect = new Rectangle(700, 500, 540, 32);
                if (optionsReturnRect.Intersects(mouseRect))
                {
                    spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(700, 500), Color.Green);
                }
                else
                {
                    spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(700, 500), Color.White);
                }

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
        public bool LeftMouseSinglePress(ButtonState n)
        {
            if (ms.LeftButton == n && previousMs.LeftButton != n)
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
                    if (n is Player)
                    {
                        Player p = (Player)n;                       
                    //Add extra buffer to dimensions? Different way of doing this?
                    Rectangle screenSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                    if (!screenSize.Contains(p.Hitbox))
                    {
                        if (p.Hitbox.X < 0)
                        {
                            if (mainScreen.ChangeLevel("left"))
                            {
                                Rectangle temp = p.Hitbox;
                                temp.X = GraphicsDevice.Viewport.Width;
                                p.Hitbox = temp;
                            }
                            else
                            {
                                p.Hitbox = p.LastCheckpoint;
                            }
                        }
                        else if (p.Hitbox.X > GraphicsDevice.Viewport.Width)
                        {
                            if (mainScreen.ChangeLevel("right"))
                            {
                                Rectangle temp = p.Hitbox;
                                temp.X = 0;
                                p.Hitbox = temp;
                            }
                            else
                            {
                                p.Hitbox = p.LastCheckpoint;
                            }
                        }

                        if (p.Hitbox.Y < 0)
                        {
                            if (mainScreen.ChangeLevel("up"))
                            {
                                Rectangle temp = p.Hitbox;
                                temp.Y = GraphicsDevice.Viewport.Height;
                                p.Hitbox = temp;
                            }

                        }
                        else if (p.Hitbox.Y > GraphicsDevice.Viewport.Height)
                        {
                            if (mainScreen.ChangeLevel("down"))
                            {
                                Rectangle temp = p.Hitbox;
                                temp.Y = 0;
                                p.Hitbox = temp;
                            }
                            else
                            {
                                p.Hitpoints = 0;
                            }

                        }
                    }

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

            enemy = new Enemy(new Rectangle(890, 500, 75, 75), jellyBoi, 16, 60);
            enemy2 = new Enemy(new Rectangle(225, 250, 75, 75), jellyBoi, 4, 60);
            enemy3 = new Enemy(new Rectangle(500, 250, 75, 75), jellyBoi, 4, 60, 5, 2 , 100);
            AddObjectToList(enemy);
            AddObjectToList(enemy2);
            AddObjectToList(enemy3);
            player = new Player(5, collisionTest , new Rectangle(450, 350, 75, 75), Color.White);
            
            //default movement
            player.BindableKb.Add("left", Keys.A);
            player.BindableKb.Add("right", Keys.D);
            player.BindableKb.Add("jump", Keys.Space);
            player.BindableKb.Add("roll", Keys.LeftShift);
            player.BindableKb.Add("downDash", Keys.S);
            player.BindableKb.Add("pause", Keys.P);
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

            if (timeCounter >=  10*secondsPerFrame) //if 3 frames have passed
            {
                 currentFrame++; //move to next frame 
                 if (currentFrame >= 4) currentFrame = 1; //if it reaches the end of the spritesheet, go back to the beginning


                timeCounter -= 10*secondsPerFrame; //reduce timeCounter so it can restart process
            }
            
            
        }
        public void DebugKeyboardInputs()
        {
            //Must hold down P, O, and G at the same time to activate level editor
            if (kb.IsKeyDown(player.BindableKb["pause"]) && kb.IsKeyDown(Keys.O) && kb.IsKeyDown(Keys.G))
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
            if (SingleKeyPress(Keys.F11))
            {
                if(animationOn == true)
                {
                    animationOn = false;
                }
                else
                {
                    animationOn = true;
                }
      
            }

            //Add more inputs here
        }

        public void DrawWalking( SpriteEffects flip) //this is for test will edit when we have actual animation assets
        {
           
            spriteBatch.Draw(
                spriteSheet,
                new Vector2(player.Hitbox.X ,player.Hitbox.Y),
                new Rectangle(widthOfASingleSprite * currentFrame, 0, widthOfASingleSprite, spriteSheet.Height),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                flip,
                0.0f);
         
        }
        public void DrawIdle( SpriteEffects flip) //this is for test will edit when we have actual animation assets
        {
                spriteBatch.Draw(
                spriteSheet,
                 new Vector2(player.Hitbox.X , player.Hitbox.Y),
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
