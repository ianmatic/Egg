﻿using Microsoft.Xna.Framework;
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

        //DO NOT ADD DIRECTLY TO THIS LIST
        List<GameObject> objectList;
        Stack<GameObject> sortHolder;

        List<Texture2D> tileSpriteList;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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

            AddObjectToList(new CapturedChicken(1, testSprite, new Rectangle(0, 0, 30, 30), Color.Red));
            AddObjectToList(new CapturedChicken(5, testSprite, new Rectangle(0, 15, 30, 30), Color.Blue));
            AddObjectToList(new CapturedChicken(4, testSprite, new Rectangle(0, 30, 30, 30), Color.Green));
            AddObjectToList(new CapturedChicken(3, testSprite, new Rectangle(0, 45, 30, 30), Color.Yellow));
            AddObjectToList(new CapturedChicken(2, testSprite, new Rectangle(0, 60, 30, 30), Color.White));
            AddObjectToList(new Tile(6, bottomRectangle, new Rectangle(300, 300, 500, 300), Tile.TileType.Normal));
            AddObjectToList(new Tile(7, sideRectangle, new Rectangle(900, 0, 100, 500), Tile.TileType.Normal));
            AddObjectToList(new Tile(8, topRectangle, new Rectangle(100, 0, 700, 100), Tile.TileType.Normal));

            player = new Player(9, testSprite, new Rectangle(300, 200, 50, 50), Color.Wheat, 50, 50);
            AddObjectToList(player);

        }


    }

   



}
