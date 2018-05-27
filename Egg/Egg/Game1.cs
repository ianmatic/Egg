using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
            LevelSelect,
            Game,
            GameOver
        }

        List<Texture2D> tileList;
        List<Enemy> enemyList;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont menuText;
        SpriteFont titleText;
        SpriteFont optionsText;
        Texture2D collectibleEgg;
        Texture2D menu;
        Texture2D options;
        //test textures
        Texture2D bottomRectangle;
        Texture2D topRectangle;
        Texture2D sideRectangle;
        Texture2D collisionTest;

        Level currentLevel;
        int eggCounter = 0;
        int levelCount = 0;
        int totalLevels;
        bool hasDrawnEggsForEndScreen = false;
        bool theEnd = false;

        bool fullPlaythrough = false;

        GameState currentState;
        KeyboardState kb;
        KeyboardState oldKB;
        Enemy enemy;
        Enemy enemy2;
        Enemy enemy3;

        //menu navigation
        bool paused = false;
        bool windowedMode = false; //Fullscreen by default
        MouseState ms;
        MouseState previousMs;
        Rectangle mouseRect; //rects used for clicking on choices in options
        Rectangle startRect;
        Rectangle levelRect;
        Rectangle optionsRect;
        Rectangle quitRect;

        //level selection rectangles
        Rectangle levelReturnRect;
        Rectangle level1Rect;
        Rectangle level2Rect;
        Rectangle level3Rect;
        Rectangle level4Rect;
        Rectangle level5Rect;
        Rectangle level6Rect;
        Rectangle level7Rect;
        Rectangle level8Rect;
        Rectangle level9Rect;

        //level selection textures
        Texture2D level1Thumbnail;
        Texture2D level2Thumbnail;
        Texture2D level3Thumbnail;
        Texture2D level4Thumbnail;
        Texture2D level5Thumbnail;
        Texture2D level6Thumbnail;
        Texture2D level7Thumbnail;
        Texture2D level8Thumbnail;
        Texture2D level9Thumbnail;


        int levelSelector = 0;

        Rectangle optionsReturnRect;
        Rectangle fullscreenRect;
        Rectangle musicRect;
        Rectangle effectsRect;
        Rectangle leftRect;
        Rectangle rightRect;
        Rectangle jumpRect;
        Rectangle downRect;
        Rectangle rollRect;
        Rectangle pauseRect;

        //rebinding keyboard logic
        bool rebindingLeft = false; //these are used to highlight the key the player is rebinding
        bool rebindingRight = false;
        bool rebindingJump = false;
        bool rebindingDown = false;
        bool rebindingRoll = false;
        bool rebindingPause = false;
        Keys[] keys;

        Player player;

        #region animation fields
        public Texture2D spriteSheet;
        int currentFrame = 1;
        double fps = 60.0;
        double secondsPerFrame;
        double timeCounter = 0;
        Dictionary<int, Texture2D> walkFrameDictionary;
        Dictionary<int, Texture2D> walkFrameDictionaryLeft;
        Dictionary<int, Texture2D> rollFrameDictionary;
        Dictionary<int, Texture2D> rollFrameDictionaryLeft;
        Dictionary<int, Texture2D> flutterFrameDictionaryLeft;
        Dictionary<int, Texture2D> flutterFrameDictionary;
        Dictionary<int, Texture2D> shellOffFlutterFrameDictionaryLeft;
        Dictionary<int, Texture2D> shellOffFlutterFrameDictionary;
        Dictionary<int, Texture2D> walkNoShellDictionary;
        Dictionary<int, Texture2D> walkNoShellDictionaryLeft;
        Dictionary<int, Texture2D> rollNoShellDictionary;
        Dictionary<int, Texture2D> rollNoShellDictionaryLeft;
        Dictionary<int, Texture2D> hitStunFullShellDictionary;
        Dictionary<int, Texture2D> hitStunFullShellDictionaryLeft;
        Dictionary<int, Texture2D> hitStunShellDictionary;
        Dictionary<int, Texture2D> hitStunShellDictionaryLeft;
        Dictionary<int, Texture2D> hitStunNoShellDictionary;
        Dictionary<int, Texture2D> hitStunNoShellDictionaryLeft;
        GameTime gameTime = new GameTime();
        #endregion
        //enemy texture
        Texture2D jellyBoi;
        #region frames of animation
        Texture2D walkCycle1;
        Texture2D walkCycle2;
        Texture2D walkCycle3;
        Texture2D walkCycle4;
        Texture2D walkCycleLeft1;
        Texture2D walkCycleLeft2;
        Texture2D walkCycleLeft3;
        Texture2D walkCycleLeft4;
        Texture2D rollCycle1;
        Texture2D rollCycle2;
        Texture2D rollCycle3;
        Texture2D rollCycle4;
        Texture2D rollCycleLeft1;
        Texture2D rollCycleLeft2;
        Texture2D rollCycleLeft3;
        Texture2D rollCycleLeft4;
        Texture2D flutterCycle1;
        Texture2D flutterCycle2;
        Texture2D flutterCycle3;
        Texture2D flutterCycle4;
        Texture2D flutterCycleLeft1;
        Texture2D flutterCycleLeft2;
        Texture2D flutterCycleLeft3;
        Texture2D flutterCycleLeft4;
        Texture2D shellOffFlutter1;
        Texture2D shellOffFlutter2;
        Texture2D shellOffFlutter3;
        Texture2D shellOffFlutter4;
        Texture2D shellOffFlutterLeft1;
        Texture2D shellOffFlutterLeft2;
        Texture2D shellOffFlutterLeft3;
        Texture2D shellOffFlutterLeft4;
        Texture2D walkNoShell1;
        Texture2D walkNoShell2;
        Texture2D walkNoShell3;
        Texture2D walkNoShell4;
        Texture2D walkNoShellLeft1;
        Texture2D walkNoShellLeft2;
        Texture2D walkNoShellLeft3;
        Texture2D walkNoShellLeft4;
        Texture2D rollNoShell1;
        Texture2D rollNoShell2;
        Texture2D rollNoShell3;
        Texture2D rollNoShell4;
        Texture2D rollNoShellLeft1;
        Texture2D rollNoShellLeft2;
        Texture2D rollNoShellLeft3;
        Texture2D rollNoShellLeft4;
        Texture2D hitStunFullShell;
        Texture2D hitStunFullShellLeft;
        Texture2D hitStunShell;
        Texture2D hitStunNoShell;
        Texture2D hitStunShellLeft;
        Texture2D hitStunNoShellLeft;

        #endregion
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
        public Texture2D enemy1;
        public Texture2D flag;
        public Texture2D egg;

        int tempcounter = 0;

        public Texture2D backGround;

        //DO NOT ADD DIRECTLY TO THIS LIST
        List<GameObject> objectList;
        Stack<GameObject> sortHolder;

        List<Texture2D> tileSpriteList;


        //Map Builder Tool
        Mappy Builder = new Mappy();

        //sound effects and music
        SoundEffect bounceSound;
        SoundEffect coinSound;
        SoundEffect deathSound;
        SoundEffect downDashSound;
        SoundEffect hitStunSound;
        SoundEffect jumpSound;
        SoundEffect menuSelectSound;
        SoundEffect rollSound;
        SoundEffect walkSound;
        SoundEffect checkpointSound;
        SoundEffect flutterSound;
        Song menuMusic;
        Song gameMusic; //levels 1 2 3
        Song gameMusic2; //levels 4 5 6
        Song gameMusic3; //levels 7 8 9
        bool gameSongStart = true; //bools used to ensure song only plays once (instead of every frame)
        bool menuSongStart = true;

        double soundCounter = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            graphics.ToggleFullScreen(); //fullscreen on
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
            menuText = Content.Load<SpriteFont>("menutext");
            titleText = Content.Load<SpriteFont>("titletext");
            optionsText = Content.Load<SpriteFont>("optionsText");
            menu = Content.Load<Texture2D>("menuTexture");
            options = Content.Load<Texture2D>("optionsTexture");
            jellyBoi = Content.Load<Texture2D>("jellyboi");
            level1Thumbnail = Content.Load<Texture2D>("thumbnails\\level1thumbnail");
            level2Thumbnail = Content.Load<Texture2D>("thumbnails\\level2thumbnail");
            level3Thumbnail = Content.Load<Texture2D>("thumbnails\\level3thumbnail");
            level4Thumbnail = Content.Load<Texture2D>("thumbnails\\level4thumbnail");
            level5Thumbnail = Content.Load<Texture2D>("thumbnails\\level5thumbnail");
            level6Thumbnail = Content.Load<Texture2D>("thumbnails\\level6thumbnail");
            level7Thumbnail = Content.Load<Texture2D>("thumbnails\\level7thumbnail");
            level8Thumbnail = Content.Load<Texture2D>("thumbnails\\level8thumbnail");
            level9Thumbnail = Content.Load<Texture2D>("thumbnails\\level9thumbnail");

            bounceSound = Content.Load<SoundEffect>("bounce");
            coinSound = Content.Load<SoundEffect>("coin");
            deathSound = Content.Load<SoundEffect>("death");
            downDashSound = Content.Load<SoundEffect>("downdash");
            hitStunSound = Content.Load<SoundEffect>("hitstun");
            jumpSound = Content.Load<SoundEffect>("jump");
            menuSelectSound = Content.Load<SoundEffect>("menuselect");
            rollSound = Content.Load<SoundEffect>("roll");
            walkSound = Content.Load<SoundEffect>("walk");
            flutterSound = Content.Load<SoundEffect>("flutter");
            checkpointSound = Content.Load<SoundEffect>("checkpoint");
            menuMusic = Content.Load<Song>("menumusic");
            gameMusic = Content.Load<Song>("gamemusic");
            gameMusic2 = Content.Load<Song>("gamemusic2");
            gameMusic3 = Content.Load<Song>("gamemusic3");

            //immediately play main menu music, reduce volume to 50% for balance
            MediaPlayer.Play(menuMusic);
            MediaPlayer.Volume = .5f;
            MediaPlayer.IsRepeating = true; //music is repeating

            tileSpriteList = new List<Texture2D>();
            //Put tile loop here

            enemyList = new List<Enemy>();
            //animation Stuff
            spriteSheet = Content.Load<Texture2D>("sprites");
            PotatoDebugging();
            #region loading and storing animations
            walkFrameDictionary = new Dictionary<int, Texture2D>();
            walkFrameDictionaryLeft = new Dictionary<int, Texture2D>();
            rollFrameDictionary = new Dictionary<int, Texture2D>();
            rollFrameDictionaryLeft = new Dictionary<int, Texture2D>();
            flutterFrameDictionary = new Dictionary<int, Texture2D>();
            flutterFrameDictionaryLeft = new Dictionary<int, Texture2D>();
            shellOffFlutterFrameDictionary = new Dictionary<int, Texture2D>();
            shellOffFlutterFrameDictionaryLeft = new Dictionary<int, Texture2D>();
            walkNoShellDictionary = new Dictionary<int, Texture2D>();
            walkNoShellDictionaryLeft = new Dictionary<int, Texture2D>();
            rollNoShellDictionary = new Dictionary<int, Texture2D>();
            rollNoShellDictionaryLeft = new Dictionary<int, Texture2D>();
            hitStunFullShellDictionary = new Dictionary<int,Texture2D>();
            hitStunFullShellDictionaryLeft = new Dictionary<int,Texture2D>();
            hitStunShellDictionary = new Dictionary<int,Texture2D>();
            hitStunShellDictionaryLeft = new Dictionary<int,Texture2D>();
            hitStunNoShellDictionary = new Dictionary<int,Texture2D>();
            hitStunNoShellDictionaryLeft = new Dictionary<int,Texture2D>() ;

            walkCycle1 = Content.Load<Texture2D>("walkCycle 1");
            walkCycle2 = Content.Load<Texture2D>("walkCycle2");
            walkCycle3 = Content.Load<Texture2D>("walkCycle3");
            walkCycle4 = Content.Load<Texture2D>("walkCycle4");
            walkCycleLeft1 = Content.Load<Texture2D>("walkCycleLeft1");
            walkCycleLeft2 = Content.Load<Texture2D>("walkCycleLeft2");
            walkCycleLeft3 = Content.Load<Texture2D>("walkCycleLeft3");
            walkCycleLeft4 = Content.Load<Texture2D>("walkCycleLeft4");
            rollCycle1 = Content.Load<Texture2D>("rollCycle1");
            rollCycle2 = Content.Load<Texture2D>("rollCycle2");
            rollCycle3 = Content.Load<Texture2D>("rollCycle3");
            rollCycle4 = Content.Load<Texture2D>("rollCycle4");
            rollCycleLeft1 = Content.Load<Texture2D>("rollCycleLeft1");
            rollCycleLeft2 = Content.Load<Texture2D>("rollCycleLeft2");
            rollCycleLeft3 = Content.Load<Texture2D>("rollCycleLeft3");
            rollCycleLeft4 = Content.Load<Texture2D>("rollCycleLeft4");
            flutterCycle1 = Content.Load<Texture2D>("chickenFly1");
            flutterCycle2 = Content.Load<Texture2D>("chickenFly2");
            flutterCycle3 = Content.Load<Texture2D>("chickenFly3");
            flutterCycle4 = Content.Load<Texture2D>("chickenFly4");
            flutterCycleLeft1 = Content.Load<Texture2D>("chickenFlyL1");
            flutterCycleLeft2 = Content.Load<Texture2D>("chickenFlyL2");
            flutterCycleLeft3 = Content.Load<Texture2D>("chickenFlyL3");
            flutterCycleLeft4 = Content.Load<Texture2D>("chickenFlyL4");
            shellOffFlutter1 = Content.Load<Texture2D>("flySO1");
            shellOffFlutter2 = Content.Load<Texture2D>("flySO2");
            shellOffFlutter3 = Content.Load<Texture2D>("flySO3");
            shellOffFlutter4 = Content.Load<Texture2D>("flySO4");
            shellOffFlutterLeft1 = Content.Load<Texture2D>("flySOLeft1");
            shellOffFlutterLeft2 = Content.Load<Texture2D>("flySOLeft2");
            shellOffFlutterLeft3 = Content.Load<Texture2D>("flySOLeft3");
            shellOffFlutterLeft4 = Content.Load<Texture2D>("flySOLeft4");
            walkNoShell1 = Content.Load<Texture2D>("walkNoShell1");
            walkNoShell2 = Content.Load<Texture2D>("walkNoShell2");
            walkNoShell3 = Content.Load<Texture2D>("walkNoShell3");
            walkNoShell4 = Content.Load<Texture2D>("walkNoShell4");
            walkNoShellLeft1 = Content.Load<Texture2D>("walkNoShellL1");
            walkNoShellLeft2 = Content.Load<Texture2D>("walkNoShellL2");
            walkNoShellLeft3 = Content.Load<Texture2D>("walkNoShellL3");
            walkNoShellLeft4 = Content.Load<Texture2D>("walkNoShellL4");
            rollNoShell1 = Content.Load<Texture2D>("rollingNoShell");
            rollNoShell2 = Content.Load<Texture2D>("rollSO2");
            rollNoShell3 = Content.Load<Texture2D>("rollSO3");
            rollNoShell4 = Content.Load<Texture2D>("rollSO4");
            rollNoShellLeft1 = Content.Load<Texture2D>("rollSOL1");
            rollNoShellLeft2 = Content.Load<Texture2D>("rollSOL2");
            rollNoShellLeft3 = Content.Load<Texture2D>("rollSOL3");
            rollNoShellLeft4 = Content.Load<Texture2D>("rollSOL4");
            hitStunFullShell = Content.Load<Texture2D>("takeDamageFullShell");
            hitStunFullShellLeft = Content.Load<Texture2D>("takeDamageFullShellLeft");
            hitStunShell = Content.Load<Texture2D>("takeDamageShell");
            hitStunShellLeft = Content.Load<Texture2D>("takeDamageShellLeft");
            hitStunNoShell = Content.Load<Texture2D>("takeDamageNoShell");
            hitStunNoShellLeft = Content.Load<Texture2D>("takeDamageNoShellLeft");


            walkFrameDictionary.Add(1, walkCycle1);
            walkFrameDictionary.Add(2, walkCycle2);
            walkFrameDictionary.Add(3, walkCycle3);
            walkFrameDictionary.Add(4, walkCycle4);
            walkFrameDictionaryLeft.Add(1, walkCycleLeft1);
            walkFrameDictionaryLeft.Add(2, walkCycleLeft2);
            walkFrameDictionaryLeft.Add(3, walkCycleLeft3);
            walkFrameDictionaryLeft.Add(4, walkCycleLeft4);
            rollFrameDictionary.Add(1, rollCycle1);
            rollFrameDictionary.Add(2, rollCycle2);
            rollFrameDictionary.Add(3, rollCycle3);
            rollFrameDictionary.Add(4, rollCycle4);
            rollFrameDictionaryLeft.Add(1, rollCycleLeft1);
            rollFrameDictionaryLeft.Add(2, rollCycleLeft2);
            rollFrameDictionaryLeft.Add(3, rollCycleLeft3);
            rollFrameDictionaryLeft.Add(4, rollCycleLeft4);
            flutterFrameDictionary.Add(1, flutterCycle1);
            flutterFrameDictionary.Add(2, flutterCycle2);
            flutterFrameDictionary.Add(3, flutterCycle3);
            flutterFrameDictionary.Add(4, flutterCycle4);
            flutterFrameDictionaryLeft.Add(1, flutterCycleLeft1);
            flutterFrameDictionaryLeft.Add(2, flutterCycleLeft2);
            flutterFrameDictionaryLeft.Add(3, flutterCycleLeft3);
            flutterFrameDictionaryLeft.Add(4, flutterCycleLeft4);
            shellOffFlutterFrameDictionary.Add(1, shellOffFlutter1);
            shellOffFlutterFrameDictionary.Add(2, shellOffFlutter2);
            shellOffFlutterFrameDictionary.Add(3, shellOffFlutter3);
            shellOffFlutterFrameDictionary.Add(4, shellOffFlutter4);
            shellOffFlutterFrameDictionaryLeft.Add(1, shellOffFlutterLeft1);
            shellOffFlutterFrameDictionaryLeft.Add(2, shellOffFlutterLeft2);
            shellOffFlutterFrameDictionaryLeft.Add(3, shellOffFlutterLeft3);
            shellOffFlutterFrameDictionaryLeft.Add(4, shellOffFlutterLeft4);
            walkNoShellDictionary.Add(1, walkNoShell1);
            walkNoShellDictionary.Add(2, walkNoShell2);
            walkNoShellDictionary.Add(3, walkNoShell3);
            walkNoShellDictionary.Add(4, walkNoShell4);
            walkNoShellDictionaryLeft.Add(1, walkNoShellLeft1);
            walkNoShellDictionaryLeft.Add(2, walkNoShellLeft2);
            walkNoShellDictionaryLeft.Add(3, walkNoShellLeft3);
            walkNoShellDictionaryLeft.Add(4, walkNoShellLeft4);
            rollNoShellDictionary.Add(1, rollNoShell1);
            rollNoShellDictionary.Add(2, rollNoShell2);
            rollNoShellDictionary.Add(3, rollNoShell3);
            rollNoShellDictionary.Add(4, rollNoShell4);
            rollNoShellDictionaryLeft.Add(1, rollNoShellLeft1);
            rollNoShellDictionaryLeft.Add(2, rollNoShellLeft2);
            rollNoShellDictionaryLeft.Add(3, rollNoShellLeft3);
            rollNoShellDictionaryLeft.Add(4, rollNoShellLeft4);
            hitStunFullShellDictionary.Add(1, hitStunFullShell);
            hitStunFullShellDictionaryLeft.Add(1, hitStunFullShellLeft);
            hitStunShellDictionary.Add(1, hitStunShell);
            hitStunShellDictionaryLeft.Add(1, hitStunShellLeft);
            hitStunNoShellDictionary.Add(1, hitStunNoShell);
            hitStunNoShellDictionaryLeft.Add(1, hitStunNoShellLeft);
            #endregion

            totalLevels = Directory.GetDirectories(@"..\..\..\..\Resources\Levels").Length;

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

            enemy1 = Content.Load<Texture2D>(@"e1");
            flag = Content.Load<Texture2D>(@"flag");
            egg = Content.Load<Texture2D>(@"egg");


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

            tileList.Add(enemy1);
            tileList.Add(egg);
            tileList.Add(flag);
            #endregion

            backGround = Content.Load<Texture2D>("bg");


            currentLevel = new Level(1);

            currentLevel.CurrentScreen.UpdateTiles(tileList);

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
                if (SingleKeyPress(player.BindableKb["pause"]) && currentState == GameState.Game)
                {
                    menuSelectSound.Play();
                    paused = true;
                }
                //FSM for switching between main menu, game, and level transition screens
                switch (currentState)
                {
                    case GameState.Menu:
                        if (SingleKeyPress(Keys.Enter) || (startRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            fullPlaythrough = true;
                            menuSelectSound.Play();
                            MediaPlayer.Stop(); //stop playing menu music when transitioning to game

                            currentState = GameState.Game;

                            //need to make a temp dict to transfer keys from old player to new player
                            Dictionary<String, Keys> tempRebindableKb = new Dictionary<string, Keys>();
                            tempRebindableKb.Add("left", player.BindableKb["left"]);
                            tempRebindableKb.Add("right", player.BindableKb["right"]);
                            tempRebindableKb.Add("jump", player.BindableKb["jump"]);
                            tempRebindableKb.Add("roll", player.BindableKb["roll"]);
                            tempRebindableKb.Add("downDash", player.BindableKb["downDash"]);
                            tempRebindableKb.Add("pause", player.BindableKb["pause"]);

                            //new player made each game to fix animation glitch where player wouldn't regain egg shell at the end of the game
                            player = new Player(5, collisionTest, new Rectangle(450, 350, 75, 75), Color.White, Content);

                            player.BindableKb.Add("left", tempRebindableKb["left"]);
                            player.BindableKb.Add("right", tempRebindableKb["right"]);
                            player.BindableKb.Add("jump", tempRebindableKb["jump"]);
                            player.BindableKb.Add("roll", tempRebindableKb["roll"]);
                            player.BindableKb.Add("downDash", tempRebindableKb["downDash"]);
                            player.BindableKb.Add("pause", tempRebindableKb["pause"]);
                            IncrementLevel();
                        }
                        else if (SingleKeyPress(Keys.Tab) || (optionsRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            menuSelectSound.Play();
                            currentState = GameState.Options;
                        }
                        else if (SingleKeyPress(Keys.Space) || (levelRect.Intersects(mouseRect)) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            menuSelectSound.Play();
                            currentState = GameState.LevelSelect;
                        }
                        else if (SingleKeyPress(Keys.Escape) || (quitRect.Intersects(mouseRect)) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            Exit();
                        }
                        break;
                    case GameState.LevelSelect:
                        if (SingleKeyPress(Keys.Space) || (levelReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            menuSelectSound.Play();
                            currentState = GameState.Menu;
                        }
                        //select levels by clicking on box or pressing corresponding number key
                        if ((level1Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D1) || SingleKeyPress(Keys.NumPad1))
                        {
                            //true if starting from the first level, essentially a full playthrough
                            fullPlaythrough = true;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(1);
                            currentState = GameState.Game;
                        }
                        else if ((level2Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D2) || SingleKeyPress(Keys.NumPad2))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(2);
                            currentState = GameState.Game;
                        }
                        else if ((level3Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D3) || SingleKeyPress(Keys.NumPad3))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(3);
                            currentState = GameState.Game;
                        }
                        else if ((level4Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D4) || SingleKeyPress(Keys.NumPad4))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(4);
                            currentState = GameState.Game;
                        }
                        else if ((level5Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D5) || SingleKeyPress(Keys.NumPad5))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(5);
                            currentState = GameState.Game;
                        }
                        else if ((level6Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D6) || SingleKeyPress(Keys.NumPad6))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(6);
                            currentState = GameState.Game;
                        }
                        else if ((level7Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D7) || SingleKeyPress(Keys.NumPad7))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(7);
                            currentState = GameState.Game;
                        }
                        else if ((level8Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D8) || SingleKeyPress(Keys.NumPad8))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(8);
                            currentState = GameState.Game;
                        }
                        else if ((level9Rect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) 
                            || SingleKeyPress(Keys.D9) || SingleKeyPress(Keys.NumPad9))
                        {
                            fullPlaythrough = false;
                            menuSelectSound.Play();
                            levelSelector = SelectLevel(9);
                            currentState = GameState.Game;
                        }
                        else
                        {
                            levelSelector = 0;
                        }
                        levelCount = levelSelector;
                        break;
                    case GameState.Options:
                        if (SingleKeyPress(Keys.Tab) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            menuSelectSound.Play();
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
                            //only play the sound once, instead of every frame
                            if (leftRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }

                            rebindingLeft = true;

                            rebindingRight = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingRoll = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["left"] = keys[0];
                                rebindingLeft = false;
                            }
                        }
                        if ((rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRight)
                        {
                            //only play the sound once, instead of every frame
                            if (rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }

                            rebindingRight = true;

                            rebindingLeft = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingRoll = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["right"] = keys[0];
                                rebindingRight = false;
                            }
                        }
                        if ((rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRoll)
                        {
                            //only play the sound once, instead of every frame
                            if (rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }

                            rebindingRoll = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingJump = false;
                            rebindingDown = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["roll"] = keys[0];
                                rebindingRoll = false;
                            }
                        }
                        if ((jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingJump)
                        {
                            //only play the sound once, instead of every frame
                            if (jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }

                            rebindingJump = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingDown = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["jump"] = keys[0];
                                rebindingJump = false;
                            }
                        }
                        if ((downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingDown)
                        {
                            //only play the sound once, instead of every frame
                            if (downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }

                            rebindingDown = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingJump = false;
                            rebindingPause = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["downDash"] = keys[0];
                                rebindingDown = false;
                            }
                        }
                        if ((pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingPause)
                        {
                            //only play the sound once, instead of every frame
                            if (pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                            {
                                menuSelectSound.Play();
                            }
                            rebindingPause = true;

                            rebindingLeft = false;
                            rebindingRight = false;
                            rebindingRoll = false;
                            rebindingJump = false;
                            rebindingDown = false;

                            keys = Keyboard.GetState().GetPressedKeys();
                            if (keys.Length > 0 && keys[0] != Keys.Tab)
                            {
                                menuSelectSound.Play();
                                player.BindableKb["pause"] = keys[0];
                                rebindingPause = false;
                            }
                        }
                        if (fullscreenRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            menuSelectSound.Play();
                            windowedMode = !windowedMode;
                            graphics.ToggleFullScreen();
                        }
                        if (musicRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            //toggle if music is played (muted/not muted)
                            menuSelectSound.Play();
                            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                        }
                        if (effectsRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                        {
                            menuSelectSound.Play();
                            //toggle sound effect on/off (on is 100%, off is 0%)
                            if (SoundEffect.MasterVolume == 1)
                            {
                                SoundEffect.MasterVolume = 0;
                            }
                            else
                            {
                                SoundEffect.MasterVolume = 1;
                            }
                        }
                        break;
                    case GameState.Game:
                        //play the game music only once at the beginning of the actual game
                        if (gameSongStart)
                        {
                            if (currentLevel.LevelNumber <= 3)
                            {
                                MediaPlayer.Play(gameMusic);
                            }
                            else if (currentLevel.LevelNumber > 3 && currentLevel.LevelNumber <= 6)
                            {
                                MediaPlayer.Play(gameMusic2);
                            }
                            else if (currentLevel.LevelNumber > 6 && currentLevel.LevelNumber <= 9)
                            {
                                MediaPlayer.Play(gameMusic3);
                            }

                            gameSongStart = false;
                        }
                        GameUpdateLoop();
                        break;

                    case GameState.GameOver:
                        //reset gameSongStart to ensure it plays during the next playthrough
                        gameSongStart = true;

                        if (menuSongStart)
                        {
                            MediaPlayer.Play(menuMusic);
                            menuSongStart = false;
                        }


                        if (levelCount >= totalLevels && fullPlaythrough)
                        {
                            theEnd = true;
                        }
                        else
                        {
                            theEnd = false;
                        }
                        if (currentLevel.LevelNumber != 9 
                            && (SingleKeyPress(Keys.Enter) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))))
                        {
                            //reset the player stats and everything necessary for next level
                            menuSelectSound.Play();
                            player.CollectedChickens.Clear();
                            soundCounter = 0;
                            player.IsRolling = false;
                            player.Hitpoints = 5;
                            IncrementLevel();
                            if (theEnd)
                            {
                                tempcounter = 0;
                                eggCounter = 0;
                                hasDrawnEggsForEndScreen = false;
                                currentState = GameState.Menu;
                            }
                            else
                            {
                                tempcounter = 0;
                                eggCounter = 0;
                                hasDrawnEggsForEndScreen = false;

                                //stop playing menu music and ensure it plays during next playhthrough
                                MediaPlayer.Stop();
                                menuSongStart = true;

                                currentState = GameState.Game;
                            }

                        }
                        //return to the main menu instead of going to the next level
                        if (SingleKeyPress(Keys.Back) || (levelReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                        {
                            //reset everything necessary for selecting the next level
                            fullPlaythrough = false;
                            tempcounter = 0;
                            eggCounter = 0;
                            menuSongStart = true;
                            hasDrawnEggsForEndScreen = false;
                            menuSelectSound.Play();
                            currentState = GameState.Menu;
                            levelCount = 0;
                            player.CollectedChickens.Clear();
                            soundCounter = 0;
                            player.Hitpoints = 5;
                            player.IsRolling = false;
                        }
                        else if (currentLevel.LevelNumber == 10 && fullPlaythrough)
                        {
                            if ((SingleKeyPress(Keys.Enter) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))))
                            {
                                tempcounter = 0;
                                eggCounter = 0;
                                menuSongStart = true;
                                hasDrawnEggsForEndScreen = false;
                                menuSelectSound.Play();
                                currentState = GameState.Menu;
                                levelCount = 0;
                                player.CollectedChickens.Clear();
                                soundCounter = 0;
                                player.Hitpoints = 5;
                                player.IsRolling = false;
                            }
                        }
                        break;
                }

                DebugKeyboardInputs();
            }
            else
            {
                if (currentState == GameState.Options)
                {
                    if (musicRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        //toggle if the music is played (muted/not muted)
                        menuSelectSound.Play();
                        MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                    }
                    if (effectsRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        //toggle if the sound effects are played (volume 0% or 100%)
                        menuSelectSound.Play();
                        if (SoundEffect.MasterVolume == 1)
                        {
                            SoundEffect.MasterVolume = 0;
                        }
                        else
                        {
                            SoundEffect.MasterVolume = 1;
                        }
                    }
                    if (fullscreenRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                        windowedMode = !windowedMode;
                        graphics.ToggleFullScreen();
                    }
                }
                //Game paused screen
                if (currentState == GameState.Game)
                {
                    //player returns to the menu from the pause screen
                    if (SingleKeyPress(Keys.Back) || (levelReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                    {
                        //reset everything necessary for next level
                        menuSelectSound.Play();
                        MediaPlayer.Stop();
                        currentState = GameState.Menu;
                        paused = false;
                        levelCount = 0;
                        gameSongStart = true;
                        player.CollectedChickens.Clear();
                        soundCounter = 0;
                        player.Hitpoints = 5;
                        player.IsRolling = false;
                        MediaPlayer.Play(menuMusic);

                    }
                }
                if (SingleKeyPress(Keys.Tab) || (optionsReturnRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)))
                {
                    menuSelectSound.Play();
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
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (leftRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }

                    rebindingLeft = true;

                    rebindingRight = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingRoll = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["left"] = keys[0];
                        rebindingLeft = false;
                    }
                }
                if ((rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRight)
                {
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (rightRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }

                    rebindingRight = true;

                    rebindingLeft = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingRoll = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["right"] = keys[0];
                        rebindingRight = false;
                    }
                }
                if ((rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingRoll)
                {
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (rollRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }
                    rebindingRoll = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingJump = false;
                    rebindingDown = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["roll"] = keys[0];
                        rebindingRoll = false;
                    }
                }
                if ((jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingJump)
                {
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (jumpRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }

                    rebindingJump = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingDown = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["jump"] = keys[0];
                        rebindingJump = false;
                    }
                }
                if ((downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingDown)
                {
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (downRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }

                    rebindingDown = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingJump = false;
                    rebindingPause = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["downDash"] = keys[0];
                        rebindingDown = false;
                    }
                }
                if ((pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed)) || rebindingPause)
                {
                    //only play the sound when the user clicks on this box, instead of every frame
                    if (pauseRect.Intersects(mouseRect) && LeftMouseSinglePress(ButtonState.Pressed))
                    {
                        menuSelectSound.Play();
                    }

                    rebindingPause = true;

                    rebindingLeft = false;
                    rebindingRight = false;
                    rebindingRoll = false;
                    rebindingJump = false;
                    rebindingDown = false;

                    keys = Keyboard.GetState().GetPressedKeys();
                    if (keys.Length > 0 && keys[0] != Keys.Tab)
                    {
                        menuSelectSound.Play();
                        player.BindableKb["pause"] = keys[0];
                        rebindingPause = false;
                    }
                }
                if (SingleKeyPress(player.BindableKb["pause"]) && currentState != GameState.Options)
                {
                    //resume the game music when unpausing
                    MediaPlayer.Resume();
                    menuSelectSound.Play();

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
            //only do so when not paused to prevent walk cycle from animating while game is paused
            if (!paused)
            {
                UpdateAnimation(gameTime);
            }



            //modified spriteBatch begin so the images are scaled by nearest neighbor instead of getting antialiased
            //this makes it so the pixel art keeps crisp lines
            spriteBatch.Begin(SpriteSortMode.Immediate);
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            //draw background
            spriteBatch.Draw(backGround, new Rectangle(0, 0, 1920, 1080), Color.White);
            //Draws sprites & text based on FSM
            switch (currentState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.DrawString(titleText, "Egg", new Vector2(880, 310), Color.White);

                    startRect = new Rectangle(690, 450, 515, 32);
                    if (startRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Enter to Start", new Vector2(690, 450), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Enter to Start", new Vector2(690, 450), Color.White);
                    }

                    levelRect = new Rectangle(595, 550, 720, 32);
                    if (levelRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Space for Level Select", new Vector2(595, 550), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Space for Level Select", new Vector2(595, 550), Color.White);
                    }

                    optionsRect = new Rectangle(650, 670, 545, 32);
                    if (optionsRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(680, 650), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Tab for Options", new Vector2(680, 650), Color.White);
                    }

                    quitRect = new Rectangle(690, 750, 515, 32);
                    if (quitRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Escape to Quit", new Vector2(690, 750), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Escape to Quit", new Vector2(690, 750), Color.White);
                    }
                    break;
                case GameState.LevelSelect:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, 1920, 1080), Color.LightCyan);

                    //first row
                    level1Rect = new Rectangle(425, 250, 240, 135);
                    level2Rect = new Rectangle(835, 250, 240, 135);
                    level3Rect = new Rectangle(1245, 250, 240, 135);

                    //second row
                    level4Rect = new Rectangle(425, 470, 240, 135);
                    level5Rect = new Rectangle(835, 470, 240, 135);
                    level6Rect = new Rectangle(1245, 470, 240, 135);

                    //third row
                    level7Rect = new Rectangle(425, 690, 240, 135);
                    level8Rect = new Rectangle(835, 690, 240, 135);
                    level9Rect = new Rectangle(1245, 690, 240, 135);


                    //Level 1
                    if (level1Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level1Thumbnail, level1Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level1Thumbnail, level1Rect, Color.White);
                    }
                    //Level 2
                    if (level2Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level2Thumbnail, level2Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level2Thumbnail, level2Rect, Color.White);
                    }
                    //Level 3
                    if (level3Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level3Thumbnail, level3Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level3Thumbnail, level3Rect, Color.White);
                    }
                    //Level 4
                    if (level4Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level4Thumbnail, level4Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level4Thumbnail, level4Rect, Color.White);
                    }
                    //Level 5
                    if (level5Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level5Thumbnail, level5Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level5Thumbnail, level5Rect, Color.White);
                    }
                    //Level 6
                    if (level6Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level6Thumbnail, level6Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level6Thumbnail, level6Rect, Color.White);
                    }
                    //Level 7
                    if (level7Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level7Thumbnail, level7Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level7Thumbnail, level7Rect, Color.White);
                    }
                    //Level 8
                    if (level8Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level8Thumbnail, level8Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level8Thumbnail, level8Rect, Color.White);
                    }
                    //Level 9
                    if (level9Rect.Intersects(mouseRect))
                    {
                        spriteBatch.Draw(level9Thumbnail, level9Rect, Color.YellowGreen);
                    }
                    else
                    {
                        spriteBatch.Draw(level9Thumbnail, level9Rect, Color.White);
                    }

                    levelReturnRect = new Rectangle(50, 65, 850, 32);
                    if (levelReturnRect.Intersects(mouseRect))
                    {
                        spriteBatch.DrawString(menuText, "Press Space to return to the Menu", new Vector2(50, 65), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(menuText, "Press Space to return to the Menu", new Vector2(50, 65), Color.White);
                    }

                    break;
                case GameState.Options:
                    spriteBatch.Draw(options, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.DrawString(menuText, "Options", new Vector2(850, 300), Color.White);
                    spriteBatch.DrawString(menuText, "Rebind keys", new Vector2(450, 450), Color.White);
                    leftRect = new Rectangle(450, 520, 250, 24);
                    rightRect = new Rectangle(450, 550, 250, 24);
                    rollRect = new Rectangle(450, 580, 250, 24);
                    jumpRect = new Rectangle(450, 610, 250, 24);
                    downRect = new Rectangle(450, 640, 250, 24);
                    pauseRect = new Rectangle(450, 670, 250, 24);
                    //Move Left button
                    if (leftRect.Intersects(mouseRect) || rebindingLeft)
                    {
                        spriteBatch.DrawString(optionsText, "Move Left: " + player.BindableKb["left"].ToString(), new Vector2(450, 520), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Move Left: " + player.BindableKb["left"].ToString(), new Vector2(450, 520), Color.White);
                    }
                    //Move Right button
                    if (rightRect.Intersects(mouseRect) || rebindingRight)
                    {
                        spriteBatch.DrawString(optionsText, "Move Right: " + player.BindableKb["right"].ToString(), new Vector2(450, 550), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Move Right: " + player.BindableKb["right"].ToString(), new Vector2(450, 550), Color.White);
                    }
                    //Roll button
                    if (rollRect.Intersects(mouseRect) || rebindingRoll)
                    {
                        spriteBatch.DrawString(optionsText, "Roll: " + player.BindableKb["roll"].ToString(), new Vector2(450, 580), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Roll: " + player.BindableKb["roll"].ToString(), new Vector2(450, 580), Color.White);
                    }
                    //Jump button
                    if (jumpRect.Intersects(mouseRect) || rebindingJump)
                    {
                        spriteBatch.DrawString(optionsText, "Jump: " + player.BindableKb["jump"].ToString(), new Vector2(450, 610), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Jump: " + player.BindableKb["jump"].ToString(), new Vector2(450, 610), Color.White);
                    }
                    //Down-Dash button
                    if (downRect.Intersects(mouseRect) || rebindingDown)
                    {
                        spriteBatch.DrawString(optionsText, "Down-Dash: " + player.BindableKb["downDash"].ToString(), new Vector2(450, 640), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Down-Dash: " + player.BindableKb["downDash"].ToString(), new Vector2(450, 640), Color.White);
                    }
                    //Pause button
                    if (pauseRect.Intersects(mouseRect) || rebindingPause)
                    {
                        spriteBatch.DrawString(optionsText, "Pause: " + player.BindableKb["pause"].ToString(), new Vector2(450, 670), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(optionsText, "Pause: " + player.BindableKb["pause"].ToString(), new Vector2(450, 670), Color.White);
                    }

                    //Fullscreen button
                    fullscreenRect = new Rectangle(920, 450, 555, 32);
                    if (windowedMode)
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
                    else
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
                    //Music button
                    musicRect = new Rectangle(920, 510, 555, 32);
                    if (MediaPlayer.IsMuted)
                    {
                        if (musicRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Music toggle: Off", new Vector2(920, 510), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Music toggle: Off", new Vector2(920, 510), Color.White);
                        }
                    }
                    else
                    {
                        if (musicRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Music toggle: On", new Vector2(920, 510), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Music toggle: On", new Vector2(920, 510), Color.White);
                        }
                    }
                    //Sound effects button
                    effectsRect = new Rectangle(920, 570, 555, 32);
                    if (SoundEffect.MasterVolume == 1)
                    {
                        if (effectsRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Sound effects toggle: On", new Vector2(920, 570), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Sound effects toggle: On", new Vector2(920, 570), Color.White);
                        }
                    }
                    else
                    {
                        if (effectsRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Sound effects toggle: Off", new Vector2(920, 570), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Sound effects toggle: Off", new Vector2(920, 570), Color.White);
                        }
                    }



                    optionsReturnRect = new Rectangle(50, 65, 800, 32);

                    //display different top left text depending on if in main menu or paused game
                    if (paused)
                    {
                        if (optionsReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to the Game", new Vector2(50, 65), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to the Game", new Vector2(50, 65), Color.White);
                        }
                    }
                    else
                    {
                        if (optionsReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to the Menu", new Vector2(50, 65), Color.Green);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Tab to return to the Menu", new Vector2(50, 65), Color.White);
                        }
                    }
                    break;
                case GameState.Game:
                    currentLevel.CurrentScreen.DrawTilesFromMap(spriteBatch, tileList);

                    //levelName.CurrentScreen.DrawTilesFromMap
                    //entitiesScreen.DrawTilesFromMap(spriteBatch, "does having a dumb thing here break stuff?", tileList);
                    //Draws potatos to test DrawLevel
                    #region Draw Player
                    player.Draw(spriteBatch);

                    if (player.PlayerState == PlayerState.IdleLeft)
                    {
                        DrawIdle(true);
                    }
                    else if (player.PlayerState == PlayerState.IdleRight)
                    {
                        DrawIdle(false);
                    }
                    else if (player.PlayerState == PlayerState.WalkLeft)
                    {
                        DrawWalking(true);
                    }
                    else if (player.PlayerState == PlayerState.WalkRight)
                    {
                        DrawWalking(false);
                    }
                    else if (player.PlayerState == PlayerState.JumpLeft)
                    {
                        DrawIdle(true);
                    }
                    else if (player.PlayerState == PlayerState.JumpRight)
                    {
                        DrawIdle(false);
                    }
                    else if (player.PlayerState == PlayerState.Fall)
                    {
                        DrawFalling();
                    }
                    else if (player.PlayerState == PlayerState.RollLeft)
                    {
                        DrawRoll(true);
                    }
                    else if (player.PlayerState == PlayerState.RollRight)
                    {
                        DrawRoll(false);
                    }
                    else if (player.PlayerState == PlayerState.HitStunLeft)
                    {
                        DrawHitStun(true);
                    }
                    else if (player.PlayerState == PlayerState.HitStunRight)
                    {
                        DrawHitStun(false);
                    }
                    else if (player.PlayerState == PlayerState.DownDash)
                    {
                        DrawDownDash();
                    }
                    else if (player.PlayerState == PlayerState.BounceLeft)
                    {
                        DrawRoll(true);
                    }
                    else if (player.PlayerState == PlayerState.BounceRight)
                    {
                        DrawRoll(false);
                    }
                    else if (player.PlayerState == PlayerState.FloatLeft)
                    {
                        DrawFlutter(true);
                    }
                    else if (player.PlayerState == PlayerState.FloatRight)
                    {
                        DrawFlutter(false);
                    }
                    #endregion

                    int row = 0;
                    int column = 0;
                    for (int tempRow = 0; tempRow < currentLevel.ScreenArray.GetLength(0); tempRow++)
                    {
                        for (int tempColumn = 0; tempColumn < currentLevel.ScreenArray.GetLength(1); tempColumn++)
                        {
                            if (currentLevel.ScreenArray[tempRow, tempColumn] == currentLevel.CurrentScreen)
                            {
                                row = tempRow;
                                column = tempColumn;
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
                        spriteBatch.Draw(collectibleEgg, new Rectangle(135, 55, 40, 40), Color.White);
                        spriteBatch.DrawString(menuText, player.CollectedChickens.Count.ToString(), new Vector2(200, 60), Color.Orange);
                    }
                    break;

                case GameState.GameOver:
                    if (!theEnd)
                    {
                        spriteBatch.Draw(menu, new Rectangle(0, 0, 1920, 1080), Color.LightSeaGreen);
                        spriteBatch.DrawString(titleText, "Level " + currentLevel.LevelNumber + " Complete!" , new Vector2(660, 320), Color.White);
                        spriteBatch.DrawString(menuText, "Chickens Rescued: ", new Vector2(500, 450), Color.White);

                        optionsReturnRect = new Rectangle(640, 650, 600, 32);

                        //last level, can't progress further
                        if (currentLevel.LevelNumber != 9)
                        {
                            if (optionsReturnRect.Intersects(mouseRect))
                            {
                                spriteBatch.DrawString(menuText, "Press Enter to continue", new Vector2(670, 650), Color.Yellow);
                            }
                            else
                            {
                                spriteBatch.DrawString(menuText, "Press Enter to continue", new Vector2(670, 650), Color.White);
                            }
                        }

                        levelReturnRect = new Rectangle(500, 750, 955, 32);
                        if (levelReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(500, 750), Color.Yellow);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(500, 750), Color.White);
                        }

                        //draw total eggs in game
                        //have to hardcode any levels that aren't purely linear, because eggs are only loaded when a screen is entered,
                        //which doesn't always happen if the level isn't linear
                        if (currentLevel.LevelNumber == 5)
                        {
                            for (int i = 0; i < 14; i++)
                            {

                                if (i < 10)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000), 450, 50, 50), Color.Gray);
                                }
                                else if (i >= 10 && i <= 19)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 600, 550, 50, 50), Color.Gray);
                                }
                                else if (i >= 20 && i <= 29)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 1200, 650, 50, 50), Color.Gray);
                                }
                            }
                        }
                        else if (currentLevel.LevelNumber == 6)
                        {
                            for (int i = 0; i < 10; i++)
                            {

                                if (i < 10)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000), 450, 50, 50), Color.Gray);
                                }
                                else if (i >= 10 && i <= 19)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 600, 550, 50, 50), Color.Gray);
                                }
                                else if (i >= 20 && i <= 29)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 1200, 650, 50, 50), Color.Gray);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < currentLevel.TotalChickensInLevel; i++)
                            {

                                if (i < 10)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000), 450, 50, 50), Color.Gray);
                                }
                                else if (i >= 10 && i <= 19)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 600, 550, 50, 50), Color.Gray);
                                }
                                else if (i >= 20 && i <= 29)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 1200, 650, 50, 50), Color.Gray);
                                }
                            }
                        }
                        //draw each egg player collected
                        for (int i = 0; i < tempcounter; i++)
                        {
                            if (player.CollectedChickens.Count > 0)
                            {
                                if (i < 10)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle(i * 60 + 1000, 450, 50, 50), Color.White);
                                }
                                else if (i >= 10 && i <= 19)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 600, 550, 50, 50), Color.White);
                                }
                                else if (i >= 20 && i <= 29)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((i * 60 + 1000) - 1200, 650, 50, 50), Color.White);
                                }
                            }

                        }

                        //draw new egg every 10 frames
                        if (eggCounter % 10 == 0 && !hasDrawnEggsForEndScreen)
                        {
                            if (player.CollectedChickens.Count > 0)
                            {
                                coinSound.Play();
                                if (tempcounter < 9)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle(tempcounter * 60 + 1000, 450, 50, 50), Color.White);
                                }
                                else if (tempcounter >= 10 && tempcounter < 19)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((tempcounter * 60 + 1000) - 600, 550, 50, 50), Color.White);
                                }
                                else if (tempcounter >= 20 && tempcounter < 29)
                                {
                                    spriteBatch.Draw(collectibleEgg, new Rectangle((tempcounter * 60 + 1000) - 1200, 650, 50, 50), Color.White);
                                }
                                tempcounter++;
                            }
                        }

                        if (tempcounter >= player.CollectedChickens.Count)
                        {
                            hasDrawnEggsForEndScreen = true;
                        }
                        eggCounter++;
                    }
                    else
                    {
                        spriteBatch.Draw(menu, new Rectangle(0, 0, 1920, 1080), Color.LightSeaGreen);
                        spriteBatch.DrawString(titleText, "Game Complete!", new Vector2(720, 320), Color.White);
                        spriteBatch.Draw(collectibleEgg, new Rectangle(750, 450, 350, 350), Color.White);
                        levelReturnRect = new Rectangle(530, 950, 955, 32);
                        if (levelReturnRect.Intersects(mouseRect))
                        {
                            spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(525, 950), Color.Yellow);
                        }
                        else
                        {
                            spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(525, 950), Color.White);
                        }

                    }



                    

                    break;
            }
            //pause the game
            if (paused && currentState == GameState.Game)
            {
                //pause the music
                MediaPlayer.Pause();

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


                levelReturnRect = new Rectangle(500, 600, 955, 32);
                if (levelReturnRect.Intersects(mouseRect))
                {
                    spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(500, 600), Color.Green);
                }
                else
                {
                    spriteBatch.DrawString(menuText, "Press Backspace to return to the Menu", new Vector2(500, 600), Color.White);
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
            Tile[,] tileSet = currentLevel.CurrentScreen.UpdateTiles(tileList);

            #region CheckPlayer           
            //Add extra buffer to dimensions? Different way of doing this?
            Rectangle screenSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            if (!screenSize.Contains(player.Hitbox))
            {
                #region ChangeScreens
                if (player.Hitbox.X < 5)
                {
                    switch (currentLevel.ChangeScreen("left"))
                    {
                        case 1:
                            Rectangle temp = player.Hitbox;
                            temp.X = GraphicsDevice.Viewport.Width;
                            player.Hitbox = temp;
                            break;

                        case 0:
                            KillPlayer();
                            if (player.LastCheckpoint == null)
                            {
                                currentLevel.CurrentScreen = currentLevel.StartScreen;
                                Rectangle r = player.Hitbox;
                                r.X = GraphicsDevice.Viewport.Width / 2;
                                r.Y = GraphicsDevice.Viewport.Height / 3;
                                player.Hitbox = r;
                            }
                            else
                            {
                                currentLevel.CurrentScreen = player.LastCheckpoint.OriginScreen;
                                player.Hitbox = player.LastCheckpoint.Hitbox;
                            }                        
                            break;

                        case -1:
                            currentState = GameState.GameOver;
                            break;
                    }

                }
                else if (player.Hitbox.X > GraphicsDevice.Viewport.Width - 5)
                {
                    switch (currentLevel.ChangeScreen("right"))
                    {
                        case 1:
                            Rectangle temp = player.Hitbox;
                            temp.X = 0;
                            player.Hitbox = temp;
                            break;

                        case 0:
                            KillPlayer();
                            if (player.LastCheckpoint == null)
                            {
                                currentLevel.CurrentScreen = currentLevel.StartScreen;
                                Rectangle r = player.Hitbox;
                                r.X = GraphicsDevice.Viewport.Width / 2;
                                r.Y = GraphicsDevice.Viewport.Height / 3;
                                player.Hitbox = r;
                            }
                            else
                            {
                                currentLevel.CurrentScreen = player.LastCheckpoint.OriginScreen;
                                player.Hitbox = player.LastCheckpoint.Hitbox;
                            }
                            break;

                        case -1:
                            currentState = GameState.GameOver;
                            break;
                    }
                }

                if (player.Hitbox.Y < 0)
                {
                    switch (currentLevel.ChangeScreen("up"))
                    {
                        case 1:
                            //set going up to true for one frame while player transitons up a screen
                            //so that the player can float effectively
                            player.GoingUp = true;
                            player.ScreenUpExtraBoost();
                            Rectangle temp = player.Hitbox;
                            temp.Y = GraphicsDevice.Viewport.Height - 20;
                            player.Hitbox = temp;
                            break;

                        case 0:
                            //do nothing so that the player can jump above the level
                            break;

                        case -1:
                            currentState = GameState.GameOver;
                            break;
                    }
                }
                else if (player.Hitbox.Y > GraphicsDevice.Viewport.Height)
                {
                    switch (currentLevel.ChangeScreen("down"))
                    {
                        case 1:
                            Rectangle temp = player.Hitbox;
                            temp.Y = 0;
                            player.Hitbox = temp;
                            break;

                        case 0:
                            KillPlayer();
                            if (player.LastCheckpoint == null)
                             {
                                currentLevel.CurrentScreen = currentLevel.StartScreen;
                                Rectangle r = player.Hitbox;
                                r.X = GraphicsDevice.Viewport.Width / 2;
                                r.Y = GraphicsDevice.Viewport.Height / 3;
                                player.Hitbox = r;
                            }
                            else
                            {
                                //player falls off screen (and shouldn't transition)
                                currentLevel.CurrentScreen = player.LastCheckpoint.OriginScreen;
                                player.Hitbox = player.LastCheckpoint.Hitbox;
                            }
                            break;

                        case -1:
                            currentState = GameState.GameOver;
                            break;
                    }
                }
                #endregion
            }
            else
            {
                //set to false if not transitioning up
                player.GoingUp = false;
            }

            player.FiniteState();


            //This should work on any enemy (i.e. enemy list of a screen), fix this later!
            foreach (Enemy e in currentLevel.CurrentScreen.Enemies)
            {
                if (!player.InBounceLockout)
                {
                    player.CheckColliderAgainstEnemy(e);
                }

                e.FiniteState();
                e.UpdateEnemyData();
                e.CheckColliderAgainstPlayer(this.player);
            }

            #endregion

            foreach (GameObject n in currentLevel.CurrentScreen.GameObjs)
            {               
                n.CheckColliderAgainstPlayer(this.player);
                foreach (Enemy e in currentLevel.CurrentScreen.Enemies)
                {
                    n.CheckColliderAgainstEnemy(e);
                }               
            } // end foreach

            foreach (Tile t in tileSet)
            {
                if (t.DefaultSprite != null)
                {
                    t.CheckColliderAgainstPlayer(this.player);

                    foreach (Enemy e in enemyList)
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
                enemyList.Add(e);
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
            collectibleEgg = Content.Load<Texture2D>("neutralEgg");
            bottomRectangle = Content.Load<Texture2D>("red");
            sideRectangle = Content.Load<Texture2D>("blue");
            topRectangle = Content.Load<Texture2D>("green");
            collisionTest = Content.Load<Texture2D>("white");




            enemy = new Enemy(new Rectangle(890, 500, 75, 75), jellyBoi, 16, 60);
            enemy2 = new Enemy(new Rectangle(225, 250, 75, 75), jellyBoi, 4, 60);
            enemy3 = new Enemy(new Rectangle(500, 250, 75, 75), jellyBoi, 4, 60, 5, 2, 100);
            AddObjectToList(enemy);
            AddObjectToList(enemy2);
            AddObjectToList(enemy3);
            player = new Player(5, collisionTest, new Rectangle(450, 350, 75, 75), Color.White, Content);


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
            //soundcounter used for syncing sound with animation
            soundCounter += time.ElapsedGameTime.TotalSeconds;

            //every 10 frames the sound is played
            if (soundCounter >= 10 * secondsPerFrame && currentState == GameState.Game)
            {
                //flutter sound
                if (player.PlayerState == PlayerState.FloatLeft || player.PlayerState == PlayerState.FloatRight)
                {
                    flutterSound.Play();
                }
                //walk sound
                if (player.PlayerState == PlayerState.WalkLeft || player.PlayerState == PlayerState.WalkRight)
                {
                    walkSound.Play();
                }
                soundCounter -= 10 * secondsPerFrame;
            }

            if (timeCounter >= 3 * secondsPerFrame) //if 3 frames have passed
            {
                currentFrame++; //move to next frame 
                if (currentFrame >= 4) currentFrame = 1; //if it reaches the end of the spritesheet, go back to the beginning


                timeCounter -= 3 * secondsPerFrame; //reduce timeCounter so it can restart process
            }


        }
        public void DebugKeyboardInputs()
        {
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

            //player dies
            if (SingleKeyPress(Keys.F9) || player.Hitpoints <= 0)
            {
                if (player.LastCheckpoint == null)
                {
                    currentLevel.CurrentScreen = currentLevel.StartScreen;
                    Rectangle r = player.Hitbox;
                    r.X = GraphicsDevice.Viewport.Width / 2;
                    r.Y = GraphicsDevice.Viewport.Height / 3;
                    player.Hitbox = r;
                }
                else
                {
                    currentLevel.CurrentScreen = player.LastCheckpoint.OriginScreen;
                    player.Hitbox = new Rectangle(player.LastCheckpoint.X, player.LastCheckpoint.Y, 75, 75);
                }

                KillPlayer();
            }


        }

        public void DrawWalking(bool isFlipped) //this is for test will edit when we have actual animation assets
        {
            if (isFlipped == false)
            {
                if(player.Hitpoints > 2)
                { 
                player.DefaultSprite = walkFrameDictionary[currentFrame];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionary[currentFrame];
                }
            }
            else
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = walkFrameDictionaryLeft[currentFrame];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionaryLeft[currentFrame];
                }
            }
        }
        public void DrawIdle(bool isFlipped) //this is for test will edit when we have actual animation assets
        {

            if (isFlipped == false)
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = walkFrameDictionary[1];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionary[1];
                }
            }
            else
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = walkFrameDictionaryLeft[1];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionaryLeft[1];
                }
            }
        }

        public void DrawFalling()
        {
            if (player.PreviousPlayerState == PlayerState.WalkLeft ||
                player.PreviousPlayerState == PlayerState.RollLeft ||
                player.PreviousPlayerState == PlayerState.JumpLeft ||
                player.PreviousPlayerState == PlayerState.HitStunLeft)
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = walkFrameDictionaryLeft[1];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionaryLeft[1];
                }
            }
            else if (player.PreviousPlayerState == PlayerState.WalkRight ||
                player.PreviousPlayerState == PlayerState.RollRight ||
                player.PreviousPlayerState == PlayerState.JumpRight ||
                player.PreviousPlayerState == PlayerState.HitStunRight)
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = walkFrameDictionary[1];
                }
                else
                {
                    player.DefaultSprite = walkNoShellDictionary[1];
                }
            }
        }

        public void DrawRoll(bool isFlipped)
        {
            if (isFlipped == true)
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = rollFrameDictionaryLeft[currentFrame];
                }
                else
                {
                    player.DefaultSprite = rollNoShellDictionaryLeft[currentFrame];
                }
            }
            else
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = rollFrameDictionary[currentFrame];
                }
                else
                {
                    player.DefaultSprite = rollNoShellDictionary[currentFrame];
                }
            }
        }

        public void DrawFlutter(bool isFlipped)
        {
            if(isFlipped == true)
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = flutterFrameDictionaryLeft[currentFrame];

                }
                else
                {
                    player.DefaultSprite = shellOffFlutterFrameDictionaryLeft[currentFrame];
                }
            }
            else
            {
                if (player.Hitpoints > 2)
                {
                    player.DefaultSprite = flutterFrameDictionary[currentFrame];
                }
                else
                {
                    player.DefaultSprite = shellOffFlutterFrameDictionary[currentFrame];
                }

            }
        }
        public void DrawDownDash()
        {
            player.DefaultSprite = rollFrameDictionary[1];
        }

        public void DrawHitStun(bool isFlipped)
        {
            if(isFlipped == false)
            {
                if(player.Hitpoints == 4 || player.Hitpoints ==5)
                {
                    player.DefaultSprite = hitStunFullShellDictionary[1];
                }
                else if(player.Hitpoints == 3)
                {
                    player.DefaultSprite = hitStunShellDictionary[1];
                }
                else
                {
                    player.DefaultSprite = hitStunNoShellDictionary[1];
                }
            }
            else
            {
                if (player.Hitpoints == 4 || player.Hitpoints == 5)
                {
                    player.DefaultSprite = hitStunFullShellDictionaryLeft[1];
                }
                else if (player.Hitpoints == 3)
                {
                    player.DefaultSprite = hitStunShellDictionaryLeft[1];
                }
                else
                {
                    player.DefaultSprite = hitStunNoShellDictionaryLeft[1];
                }
            }
        }

        void IncrementLevel()
        {

            if (levelCount >= totalLevels)
            {
                //Put end screen here
                if (fullPlaythrough)
                {
                    theEnd = true;
                }
                else
                {
                    theEnd = false;
                }

                //Move this code to when the Next button is pressed
                levelCount = 0;
                currentState = GameState.GameOver;
                return;
            }
            theEnd = false;           
            //Increments level and sets up next level
            levelCount++;
            currentLevel = new Level(levelCount);
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            player.LastCheckpoint = null;

            Rectangle temp = player.Hitbox;

            temp.X = GraphicsDevice.Viewport.Width / 2;
            temp.Y = GraphicsDevice.Viewport.Height / 3;

            player.Hitbox = temp;

            player.PutInFallState();
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            currentLevel.CurrentScreen = currentLevel.StartScreen;

            
        }
        int SelectLevel(int selection)
        {
            currentLevel = new Level(selection);
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            player.LastCheckpoint = null;

            Rectangle temp = player.Hitbox;

            temp.X = GraphicsDevice.Viewport.Width / 2;
            temp.Y = GraphicsDevice.Viewport.Height / 3;

            player.Hitbox = temp;

            player.PutInFallState();
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            currentLevel.CurrentScreen = currentLevel.StartScreen;
            currentLevel.CurrentScreen = currentLevel.StartScreen;

            return selection;
        }
        /// <summary>
        /// simple method that kills the player and sets them up for their next life
        /// </summary>
        void KillPlayer()
        {
            deathSound.Play();
            player.PlayerState = PlayerState.Fall;
            player.HorizontalVelocity = 0;
            player.VerticalVelocity = 0;
            player.Hitpoints = 5;
            player.InHitStun = false;
        }
    }
}
