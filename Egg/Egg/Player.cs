﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

enum PlayerState
{
    IdleLeft,
    IdleRight,

    WalkLeft,
    WalkRight,

    RollLeft,
    RollRight,

    JumpLeft,
    JumpRight,

    FloatLeft,
    FloatRight,

    Fall,
    DownDash,

    BounceLeft,
    BounceRight,

    HitStunRight,
    HitStunLeft
}
namespace Egg
{
    class Player : GameObject
    {
        //################
        #region FIELDS
        //Fields
        private KeyboardState kb;
        private KeyboardState previousKb; //used to prevent jump spamming

        private int hitpoints;

        private double miliseconds; //used for float/downdash/roll/hitstun
        private double downDashDelay; //used for downdash
        private double floatDelay;
        private double rollDelay;
        private double hitStunDelay;    

        private Rectangle lastCheckpoint;
        
        //for collision
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle sideChecker;
        private bool bottomIntersects;
        private bool topIntersects;
        private bool isDebugging = false;
        private bool debugEnemyCollision = false; 
        private bool playerVisible = true;
        private bool bounceLockout = false;
        private bool shouldBounce = false;
        Tile temp; //used to make sure player checks collision against only 
                   //1 tile when necessary (as opposed to all of them each frame like usual)

        //for directionality and FSM
        private bool isFacingRight;
        private PlayerState playerState;

        //for movement
        private bool rollInAir;
        private bool isRolling;
        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;
        private bool inHitStun;
        private bool rollEnd;
        private bool hasRolledInAir;
        
        //for float
        private bool hasFloated;
        private Vector2 previousPlayerPosition; //positions used to check if the player is going up or down
        private Vector2 playerPosition;

        private Color color;

        //for rebinding keys
        private Dictionary<string, Keys> bindableKb;

        GameTime gameTime;
        #endregion
        //################

        //################
        #region PROPERTIES
        //Properties
        public PlayerState PlayerState
        {
            get { return playerState; }
            set { playerState = value; }
        }
        public int Hitpoints
        {
            get { return hitpoints; }
            set { hitpoints = value; }
        }
        public int VerticalVelocity
        {
            get { return verticalVelocity; }
            set { verticalVelocity = value; }
        }
        public int HorizontalVelocity
        {
            get { return horizontalVelocity; }
            set { verticalVelocity = value; }
        }
        public bool IsDebugging
        {
            get { return isDebugging; }
        }
        public bool IsFacingRight
        {
            get { return isFacingRight; }
        }
        public override Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public bool InBounceLockout
        {
            get { return bounceLockout; }
        }

        public Rectangle LastCheckpoint
        {
            get { return lastCheckpoint; }
            set { this.lastCheckpoint = value; }
        }
        public bool InHitStun
        {
            get { return inHitStun; }
            set { inHitStun = value; }
        }
        #endregion
        //################

        //################
        #region CONSTRUCTOR
        //Constructor for player
        public Player(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;
            this.lastCheckpoint = hitbox;

            isActive = true;
            hitpoints = 5;

            hasGravity = true; //no point other than it must be implement since it inherets GameObject


            bottomIntersects = false;
            topIntersects = false;
            hasFloated = false;
            isRolling = false;
            rollInAir = false;
            rollEnd = false;
            hasRolledInAir = false;

            gameTime = new GameTime();
            downDashDelay = 13;
            floatDelay = 50;
            rollDelay = 30;
            hitStunDelay = 20;
            miliseconds = 2;

            bindableKb = new Dictionary<string, Keys>();
        }
        #endregion
        //################

        /// <summary>
        /// speed up the object by the rate until the limit velocity is reached
        /// </summary>
        /// <param name="velocityType"></param>
        /// <param name="rate"></param>
        /// <param name="limit"></param>
        public void Accelerate(int velocityType, int rate, int limit, bool vertical)
        {
            if (vertical)
            {
                if (velocityType < limit)
                {
                    velocityType += rate;
                    verticalVelocity = velocityType;
                }
            }
            else //horizontal
            {
                if (isFacingRight)
                {
                    if (velocityType < limit)
                    {
                        velocityType += rate;
                    }
                }
                else //facing left
                {
                    //moving left means moving negatively (decrease value past 0 until negative limit is hit)
                    limit -= limit * 2;
                    if (velocityType > limit)
                    {
                        velocityType -= rate;
                    }
                }
                horizontalVelocity = velocityType;
            }
        }
        /// <summary>
        /// Checks if enemies are touching player
        /// </summary>
        /// <param name="e"></param>
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            if (bottomChecker.Intersects(e.Hitbox))
            {
                shouldBounce = true;
            }
            else
            {
                shouldBounce = false;
            }
            if (hitbox.Intersects(e.Hitbox) && e.IsActive && !bounceLockout && !inHitStun)
            {
                debugEnemyCollision = true;
                if (playerState == PlayerState.RollRight)
                {
                    X = e.X - hitbox.Width - 1;
                    playerState = PlayerState.BounceLeft;
                    debugEnemyCollision = false;
                    rollEnd = true;
                    isRolling = false;
                    rollDelay = 30;
                }
                else if (playerState == PlayerState.RollLeft)
                {
                    X = e.X + (hitbox.Width * 2) + 1;
                    playerState = PlayerState.BounceRight;
                    debugEnemyCollision = false;
                    rollEnd = true;
                    isRolling = false;
                    rollDelay = 30;
                }
                else if (playerState == PlayerState.DownDash && shouldBounce)
                {
                    Y = e.Y - hitbox.Height - 1;
                    if (IsFacingRight)
                    {
                        playerState = PlayerState.BounceRight;
                    }
                    else
                    {
                        playerState = PlayerState.BounceLeft;
                    }
                    debugEnemyCollision = false;
                    rollEnd = false;
                }
                else
                {
                    rollEnd = false;
                }
                //player takes damage if not rolling, bouncing, downdashing, or in hitstun
                if (playerState != PlayerState.RollLeft && playerState != PlayerState.RollRight && playerState != PlayerState.DownDash &&
                    playerState != PlayerState.BounceLeft && playerState != PlayerState.BounceRight && playerState != PlayerState.HitStunLeft 
                    && playerState != PlayerState.HitStunRight)
                {
                    hitpoints--;
                }

                bounceLockout = true;
            }
            else
            {
                debugEnemyCollision = false;
            }
        }
        /// <summary>
        /// Checks if hitboxes around player touch Tile t
        /// </summary>
        public bool CollisionCheck(Tile t)
        {
            bool output = false;

            //wall collision (collision box next to player depending on direction facing)
            if (sideChecker.Intersects(t.Hitbox))
            {
                horizontalVelocity = 0; //stop player from moving through wall
                if (isFacingRight && t.X > hitbox.X) //player facing right and tile is to the right of player
                {
                    hitbox.X = t.X - hitbox.Width + 1; //place player left of tile
                }
                else if (t.X < hitbox.X) //player facing left and tile is to the left of player
                {
                    hitbox.X = t.X + t.Hitbox.Width -1; //place player right of tile
                }

                if (playerState == PlayerState.Fall)
                {
                    if (isFacingRight)
                    {
                        hitbox.X -= 1;
                    }
                    else
                    {
                        hitbox.X += 1;
                    }

                }
                output = true;
            }
            //ceiling collision (collision box above player)
            if (topChecker.Intersects(t.Hitbox))
            {
                if (topIntersects) //this is used to ensure player is placed at the ceiling only once per jump
                {
                    hitbox.Y = t.Y + t.Hitbox.Height; //place player at ceiling (illusion of hitting it)
                }
                topIntersects = false; //set to false so that the player isn't placed to the ceiling again until they touch the ground
                verticalVelocity = (int)(Math.Abs(verticalVelocity) * .75); //launch the player downwards
                output = true;
            }
            //floor collision (collision box below player)
            else if (bottomChecker.Intersects(t.Hitbox))
            {
                if (!(playerState == PlayerState.BounceLeft || playerState == PlayerState.BounceRight) && !inHitStun)
                {
                    verticalVelocity = 0; //stop the player from falling
                    hitbox.Y = t.Y - hitbox.Height; //place the player on top of tile
                }
                else
                {
                    hitbox.Y = t.Y - hitbox.Height - 1; //place the player on top of tile
                }


                hasRolledInAir = false;
                bottomIntersects = true;
                //FSM states are changed here so that the player can move after touching the ground

                if (!inHitStun)
                {
                    //Roll Left
                    if ((SingleKeyPress(Keys.LeftShift) && !isFacingRight) || ((isRolling && !isFacingRight) && !rollEnd))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //Walk Left
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    //Roll Right
                    else if ((SingleKeyPress(Keys.LeftShift) && isFacingRight) || ((isRolling && isFacingRight) && !rollEnd))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    //Walk Right
                    else if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    //Idle Right
                    else if (isFacingRight && !kb.IsKeyDown(Keys.D) && playerState != PlayerState.BounceLeft && playerState != PlayerState.RollRight)
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    //Idle Left
                    else if (!isFacingRight && !kb.IsKeyDown(Keys.A) && playerState != PlayerState.BounceRight && playerState != PlayerState.RollLeft)
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                }
                temp = t;
                //everytime the player lands from a jump (or falls), the next time they jump they will hit the ceiling
                topIntersects = true;
                output = true;
            }
            else if (t.Equals(temp))
            {
                bottomIntersects = false;
            }
            return output;
        }
        /// <summary>
        /// slowdown the object by the rate until the limit velocity is reached 
        /// </summary>
        /// <param name="velocityType"></param>
        /// <param name="rate"></param>
        public void Decelerate(int velocityType, int rate, int limit, bool vertical)
        {

            if (isFacingRight)
            {
                if (velocityType > limit)
                {
                    velocityType -= rate; //reduce velocity normally

                    if (!bottomIntersects && !isRolling && !inHitStun)
                    {
                        playerState = PlayerState.Fall;
                    }
                }
            }
            else
            {
                if (velocityType < limit)
                {
                    velocityType += rate; //increase velocity since moving left is negative
                    //needed to prevent player from hovering in air if they decelerate on an edge
                    if (!bottomIntersects && !isRolling && !inHitStun)
                    {
                        playerState = PlayerState.Fall;
                    }
                }
            }

            if (vertical)
            {
                verticalVelocity = velocityType;
            }
            else
            {
                horizontalVelocity = velocityType;
            }
        }
        /// <summary>
        /// draws the player for normal game and debugging
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            if (isDebugging)
            {
                //press C to toggle player transparency
                if (SingleKeyPress(Keys.C))
                {
                    playerVisible = !playerVisible;
                }
                if (playerVisible)
                {
                    sb.Draw(defaultSprite, hitbox, Color.Black);
                    sb.Draw(defaultSprite, bottomChecker, Color.Black);
                    sb.Draw(defaultSprite, sideChecker, Color.Red);
                    sb.Draw(defaultSprite, topChecker, Color.Cyan);
                    if (inHitStun && hitStunDelay % 5 == 0)
                    {
                        sb.Draw(defaultSprite, hitbox, Color.Orange);
                    }
                    else if (inHitStun && hitStunDelay % 5 != 0)
                    {
                        sb.Draw(defaultSprite, hitbox, Color.Purple);
                    }
                }
                else
                {
                    sb.Draw(defaultSprite, hitbox, Color.Transparent);
                    sb.Draw(defaultSprite, bottomChecker, Color.Black);
                    sb.Draw(defaultSprite, sideChecker, Color.Red);
                    sb.Draw(defaultSprite, topChecker, Color.Cyan);
                    if (inHitStun && hitStunDelay % 5 == 0)
                    {
                        sb.Draw(defaultSprite, hitbox, Color.Orange);
                    }
                    else if (inHitStun && hitStunDelay % 5 != 0)
                    {
                        sb.Draw(defaultSprite, hitbox, Color.Purple);
                    }
                }

            }
            else
            {
                if (inHitStun && hitStunDelay % 5 == 0)
                {
                    sb.Draw(defaultSprite, hitbox, Color.Orange);
                }
                else if (inHitStun && hitStunDelay % 5 != 0)
                {
                    sb.Draw(defaultSprite, hitbox, Color.Purple);
                }
                else
                {
                    sb.Draw(defaultSprite, hitbox, color);
                }

            }

        }
        /// <summary>
        /// determines player state based on input and collision with enemies/platforms
        /// </summary>
        public override void FiniteState()
        {
            //previousPosition tracks player from previous frame
            previousPlayerPosition = playerPosition;
            playerPosition = new Vector2(X, Y); //player position of current frame


            //previousKb used to prevent jump spamming (holding down space) 
            previousKb = kb;
            kb = Keyboard.GetState();

            //Debugging code
            if (SingleKeyPress(Keys.F8))
            {
                //switch between debugging and not everytime you press combo
                isDebugging = !isDebugging;
            }

            //################
            #region COLLISIONBOXES
            if (horizontalVelocity > 0)
            {
                //X is right of player, Y is the same as player, width depends on horizontalVelocity, height is same as player
                sideChecker = new Rectangle(X + hitbox.Width, Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
            }
            //Facing left
            else if (horizontalVelocity < 0)
            {
                //X is same as player (which is left edge), Y is the same as player
                //width depends on horizontalVelocity, height is same as player
                sideChecker = new Rectangle(X - Math.Abs(horizontalVelocity), Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
            }
            else
            {
                if (isFacingRight)
                {
                    sideChecker = new Rectangle(X + hitbox.Width, Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
                }
                else
                {
                    sideChecker = new Rectangle(X - Math.Abs(horizontalVelocity), Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
                }
            }
            //height is player height with vertical velocity added on (subtracting makes the height go "up" aka toward the ceiling)
            if (verticalVelocity <= 0)
            {
                topChecker = new Rectangle(X + 10, Y - Math.Abs(verticalVelocity), hitbox.Width - 20, Math.Abs(verticalVelocity));
            }
            else
            {
                topChecker = new Rectangle(X + 10, Y, hitbox.Width - 20, 0);
            }



            bottomChecker = new Rectangle(X + 10, Y + hitbox.Height, hitbox.Width - 20, Math.Abs(verticalVelocity));

            if (verticalVelocity == 0 && !kb.IsKeyDown(Keys.Space))
            {
                bottomChecker = new Rectangle(X + 10, Y + hitbox.Height, hitbox.Width - 20, 1);
            }
            #endregion
            //################

            //float gets reset to false whenever player touches ground
            if (bottomIntersects)
            {
                hasFloated = false;
            }

            if (inHitStun)
            {
                if (isFacingRight)
                {
                    playerState = PlayerState.HitStunLeft;
                }
                else
                {
                    playerState = PlayerState.HitStunRight;
                }
                hitStunDelay -= miliseconds;
                if (hitStunDelay <= 0)
                {
                    inHitStun = false;
                    hitStunDelay = 20;
                    playerState = PlayerState.Fall;
                }
            }
            //################
            #region FINITESTATE
            //FSM
            switch (playerState)
            {
                //##################
                #region IDLE STATES
                //##################
                //Idle Left
                case PlayerState.IdleLeft:
                    isFacingRight = false;
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunRight;
                    }
                    Movement();

                    if (!debugEnemyCollision)
                    {
                        if (SingleKeyPress(Keys.Space))
                        {
                            playerState = PlayerState.JumpLeft;
                        }
                        else if (kb.IsKeyDown(Keys.D))
                        {
                            playerState = PlayerState.WalkRight;
                        }
                        else if (kb.IsKeyDown(Keys.A))
                        {
                            playerState = PlayerState.WalkLeft;
                        }
                        else if (SingleKeyPress(Keys.LeftShift))
                        {
                            playerState = PlayerState.RollLeft;
                        }
                    }
                    //Remember to implement HitStun here
                    break;

                //Idle Right
                case PlayerState.IdleRight:
                    isFacingRight = true;
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunLeft;
                    }
                    Movement();

                    if (!debugEnemyCollision)
                    {
                        if (SingleKeyPress(Keys.Space))
                        {
                            playerState = PlayerState.JumpRight;
                        }
                        else if (kb.IsKeyDown(Keys.D))
                        {
                            playerState = PlayerState.WalkRight;
                        }
                        else if (kb.IsKeyDown(Keys.A))
                        {
                            playerState = PlayerState.WalkLeft;
                        }
                        else if (SingleKeyPress(Keys.LeftShift))
                        {
                            playerState = PlayerState.RollRight;
                        }
                    }
                    break;

                #endregion
                //################

                //################
                #region WALK STATES
                //Walk Left
                case PlayerState.WalkLeft:
                    isFacingRight = false;
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunRight;
                    }
                    Movement();

                    if (!debugEnemyCollision)
                    {
                        if (SingleKeyPress(Keys.Space))
                        {
                            playerState = PlayerState.JumpLeft;
                        }
                        else if (kb.IsKeyUp(Keys.A)) //stop moving left
                        {
                            playerState = PlayerState.IdleLeft;
                        }
                        else if (kb.IsKeyDown(Keys.D))
                        {
                            playerState = PlayerState.WalkRight;
                        }
                        else if (SingleKeyPress(Keys.LeftShift))
                        {
                            playerState = PlayerState.RollLeft;
                        }
                        if (!bottomIntersects) //not touching ground
                        {
                            playerState = PlayerState.Fall;
                        }
                    }
                    
                    //Remember to implement HitStun here
                    break;
                //Walk Right
                case PlayerState.WalkRight:
                    isFacingRight = true;
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunLeft;
                    }
                    Movement();


                    if (!debugEnemyCollision)
                    {
                        if (SingleKeyPress(Keys.Space))
                        {
                            playerState = PlayerState.JumpRight;
                        }
                        else if (kb.IsKeyUp(Keys.D)) //stop moving right
                        {
                            playerState = PlayerState.IdleRight;
                        }
                        else if (kb.IsKeyDown(Keys.A)) //moving left
                        {
                            playerState = PlayerState.WalkLeft;
                        }
                        else if (SingleKeyPress(Keys.LeftShift))
                        {
                            playerState = PlayerState.RollRight;
                        }
                        if (!bottomIntersects) //not touching ground
                        {
                            playerState = PlayerState.Fall;
                        }
                    }
                    
                    //Remember to implement HitStun here
                    break;
                #endregion
                //################

                //################
                #region ROLL STATES
                //Roll Left
                case PlayerState.RollLeft:
                    isFacingRight = false;
                    Movement();
                    if (!bottomIntersects && !isRolling) //not touching ground
                    {
                        playerState = PlayerState.Fall;
                    }
                    if (!isRolling && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (!isRolling && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (SingleKeyPress(Keys.Space) && !isRolling)
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    else if (!isRolling && bottomIntersects)
                    {
                        playerState = PlayerState.IdleLeft;
                    }

                    break;

                //Roll Right
                case PlayerState.RollRight:
                    isFacingRight = true;
                    Movement();
                    if (!bottomIntersects && !isRolling) //not touching ground
                    {
                        playerState = PlayerState.Fall;
                    }
                    if (SingleKeyPress(Keys.Space) && !isRolling)
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    else if (!isRolling && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (!isRolling && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (!isRolling && bottomIntersects)
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    break;
                #endregion
                //################

                //################
                #region JUMP STATES
                //Jump Left
                case PlayerState.JumpLeft:
                    isFacingRight = false;
                    Movement();

                    //default to fall if no other condition is met (no hitstun here, use fall's hitstun)
                    playerState = PlayerState.Fall;
                    break;

                //Jump Right
                case PlayerState.JumpRight:
                    isFacingRight = true;
                    Movement();

                    //default to fall if no other condition is met (no hitstun here, use fall's hitstun)
                    playerState = PlayerState.Fall;
                    break;
                #endregion
                //################

                //################
                #region FLOAT STATES
                //Float Left
                case PlayerState.FloatLeft:
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunRight;
                    }
                    else
                    {
                        //if the player lets go of space, player stops floating
                        if (SingleKeyPress(Keys.LeftShift))
                        {
                            rollInAir = true;
                            playerState = PlayerState.RollLeft;
                        }
                        if (SingleKeyPress(Keys.S))
                        {
                            playerState = PlayerState.DownDash;
                        }
                        if (kb.IsKeyUp(Keys.Space))
                        {
                            playerState = PlayerState.Fall;
                        }
                        else
                        {
                            isFacingRight = false;
                            Movement();
                        }
                    }
                   
                    break;

                //Float Right
                case PlayerState.FloatRight:
                    if (debugEnemyCollision)
                    {
                        playerState = PlayerState.HitStunLeft;
                    }
                    else
                    {
                        //if the player lets go of space, player stops floating
                        if (SingleKeyPress(Keys.LeftShift))
                        {
                            rollInAir = true;
                            playerState = PlayerState.RollRight;
                        }
                        if (SingleKeyPress(Keys.S))
                        {
                            playerState = PlayerState.DownDash;
                        }
                        if (kb.IsKeyUp(Keys.Space))
                        {
                            playerState = PlayerState.Fall;
                        }
                        else
                        {
                            isFacingRight = true;
                            Movement();
                        }
                    }
                    break;
                #endregion
                //################

                //################
                #region FALL STATE
                //Fall 
                case PlayerState.Fall:
                    Movement();
                    if (debugEnemyCollision && isFacingRight)
                    {
                        playerState = PlayerState.HitStunLeft;
                    }
                    else if (debugEnemyCollision && !isFacingRight)
                    {
                        playerState = PlayerState.HitStunRight;
                    }
                    else
                    {
                        if (SingleKeyPress(Keys.LeftShift) && !hasRolledInAir)
                        {
                            rollInAir = true;
                            if (isFacingRight)
                            {
                                playerState = PlayerState.RollRight;
                            }
                            else
                            {
                                playerState = PlayerState.RollLeft;
                            }
                        }
                        if (SingleKeyPress(Keys.S))
                        {
                            playerState = PlayerState.DownDash;
                        }
                        //previous is less than current since going down means y increasing
                        if (!hasFloated && previousPlayerPosition.Y < playerPosition.Y)
                        {
                            if (kb.IsKeyDown(Keys.A) && kb.IsKeyUp(Keys.D) && kb.IsKeyDown(Keys.Space))
                            {
                                PlayerState = PlayerState.FloatLeft;
                                hasFloated = true;
                            }
                            else if (kb.IsKeyDown(Keys.D) && kb.IsKeyUp(Keys.A) && kb.IsKeyDown(Keys.Space))
                            {
                                playerState = PlayerState.FloatRight;
                                hasFloated = true;
                            }
                            else if (kb.IsKeyDown(Keys.Space))
                            {
                                if (isFacingRight)
                                {
                                    playerState = PlayerState.FloatRight;
                                }
                                else
                                {
                                    playerState = PlayerState.FloatLeft;
                                }
                                hasFloated = true;
                            }
                        }
                    }
                    
                    //adjust delays to determine how long delay is for downdash and float
                    downDashDelay = 13;
                    floatDelay = 50;
                    //HitStun
                    break;
                #endregion
                //################

                //################
                #region DOWNDASH
                case PlayerState.DownDash:
                    if (debugEnemyCollision && shouldBounce)
                    {
                        if (isFacingRight)
                        {
                            playerState = PlayerState.BounceRight;
                        }
                        else
                        {
                            playerState = PlayerState.BounceLeft;
                        }
                    }
                    Movement(); //movement is after to prevent player from touching ground during downdash if they touch an enemy

                    //Implement interaction with enemy here
                    break;
                #endregion
                //################

                //################
                #region BOUNCE STATES
                //Bounce Left
                case PlayerState.BounceLeft:
                    Movement();
                    break;

                //Bounce Right
                case PlayerState.BounceRight:
                    Movement();
                    break;

                //Remember to implement HitStun here
                #endregion
                //################
                case PlayerState.HitStunLeft:
                    Movement();
                    break;
                case PlayerState.HitStunRight:
                    Movement();
                    break;
            }
            #endregion
            //################
        }
        /// <summary>
        /// Calls accelerate/decelerate methods based on FSM state, the direction is accounted for in the methods
        /// </summary>
        public override void Movement()
        {
            if (kb.IsKeyUp(Keys.A) && kb.IsKeyUp(Keys.D) && verticalVelocity == 0)
            {
                bool temp = isFacingRight;
                isFacingRight = !isFacingRight;
                Decelerate(horizontalVelocity, 1, 0, false);
                isFacingRight = temp;
            }

            //Idle
            if (playerState == PlayerState.IdleLeft || playerState == PlayerState.IdleRight)
            {
                //slow-down and stop the player if they are walking
                if (horizontalVelocity <= 10 && horizontalVelocity >= -10)
                {
                    Decelerate(horizontalVelocity, 1, 0, false);
                }
                //slow-down and stop the player if they are rolling
                else if (horizontalVelocity > 10 || horizontalVelocity < -10)
                {
                    Decelerate(horizontalVelocity, 2, 0, false);
                }

                bounceLockout = false;
            }
            //Walk
            else if (playerState == PlayerState.WalkLeft || playerState == PlayerState.WalkRight)
            {
                //slow-down the player if they were rolling
                if (horizontalVelocity > 10 || horizontalVelocity < -10)
                {
                    Decelerate(horizontalVelocity, 1, 10, false);
                }
                Accelerate(horizontalVelocity, 5, 10, false);
            }
            //Roll
            else if (playerState == PlayerState.RollLeft || playerState == PlayerState.RollRight)
            {
                bounceLockout = false;
                isRolling = true;
                Accelerate(horizontalVelocity, 6, 18, false);
                if (SingleKeyPress(Keys.Space))
                {
                    isRolling = false;
                    rollDelay = 30;
                }
                if (!bottomIntersects && !rollInAir) //mimic gravity while rolling
                {
                    Accelerate(verticalVelocity, 2, 30, true);
                }
                if (rollInAir)
                {
                    verticalVelocity = 0;
                }
                rollDelay -= miliseconds;
                if (rollDelay <= 0)
                {
                    if (!bottomIntersects)
                    {
                        hasRolledInAir = true;
                    }

                    isRolling = false;
                    if (rollInAir == true)
                    {
                        Decelerate(horizontalVelocity, 10, 10, false);
                    }
                    rollInAir = false;
                    rollDelay = 30;
                    if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (!isFacingRight)
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (isFacingRight)
                    {
                        playerState = PlayerState.IdleRight;
                    }
                }
            }
            //Jump
            else if (playerState == PlayerState.JumpLeft || playerState == PlayerState.JumpRight)
            {
                //give huge start velocity then transition to fall and allow gravity to create arch
                verticalVelocity = -30;
                playerState = PlayerState.Fall;
            }
            //Float
            else if (playerState == PlayerState.FloatLeft || playerState == PlayerState.FloatRight)
            {
                //player stops falling
                verticalVelocity = 0;
                floatDelay -= miliseconds; //reduce delay (timer) until 0, then player starts falling again
                if (floatDelay <= 0)
                {

                    hasFloated = true;
                    playerState = PlayerState.Fall;
                    floatDelay = 50; //reset the delay
                }
                else if (kb.IsKeyDown(Keys.A))
                {
                    isFacingRight = false;
                    Accelerate(horizontalVelocity, 1, 5, false);

                }
                else if (kb.IsKeyDown(Keys.D))
                {
                    isFacingRight = true;
                    Accelerate(horizontalVelocity, 1, 5, false);
                }
            }
            //Fall
            else if (playerState == PlayerState.Fall)
            {
                bounceLockout = false;
                //Gravity
                Accelerate(verticalVelocity, 2, 30, true);

                if (kb.IsKeyDown(Keys.A))
                {
                    //temp used to return isFacingRight to original state
                    bool temp = isFacingRight;
                    isFacingRight = false;
                    Accelerate(horizontalVelocity, 1, 10, false);
                    isFacingRight = temp;
                }
                if (kb.IsKeyDown(Keys.D))
                {
                    bool temp = isFacingRight;
                    isFacingRight = true;
                    Accelerate(horizontalVelocity, 1, 10, false);
                    isFacingRight = temp;
                }
            }
            //Down-dash
            else if (playerState == PlayerState.DownDash)
            {
                //stop in midair (illusion of delay)
                horizontalVelocity = 0;
                verticalVelocity = 0;

                bounceLockout = false;

                downDashDelay -= miliseconds;
                if (downDashDelay <= 0)
                {
                    Accelerate(verticalVelocity, 35, 60, true);
                }
            }
            //Bounce
            else if (playerState == PlayerState.BounceLeft || playerState == PlayerState.BounceRight)
            {
                if (isFacingRight && verticalVelocity != 0) //in air
                {
                    horizontalVelocity = 20;
                }
                else if (!isFacingRight && verticalVelocity != 0) //in air
                {
                    horizontalVelocity = -20;
                }
                else if (isFacingRight)
                {
                    horizontalVelocity = -20;
                }
                else
                {
                    horizontalVelocity = 20;
                }

                verticalVelocity = -30;
                if (playerState == PlayerState.BounceLeft)
                {
                    isFacingRight = false;
                }
                else
                {
                    isFacingRight = true;
                }
                playerState = PlayerState.Fall;
            }
            else if (playerState == PlayerState.HitStunLeft || playerState == PlayerState.HitStunRight)
            {
                if (!inHitStun)
                {
                    verticalVelocity = 0;
                    if (isFacingRight && verticalVelocity != 0) //in air
                    {
                        horizontalVelocity = 10;
                    }
                    else if (!isFacingRight && verticalVelocity != 0) //in air
                    {
                        horizontalVelocity = -10;
                    }
                    else if (isFacingRight)
                    {
                        horizontalVelocity = -10;
                    }
                    else
                    {
                        horizontalVelocity = 10;
                    }
                }

                inHitStun = true;
                if (inHitStun)
                {
                    Accelerate(verticalVelocity, 2, 30, true);
                    isFacingRight = !isFacingRight;
                    Decelerate(horizontalVelocity, 1, 0, false);
                    isFacingRight = !isFacingRight;
                }
            }

            X += horizontalVelocity;
            Y += verticalVelocity;
        }
        /// <summary>
        /// used to prevent holding down a key from spamming an action
        /// </summary>
        /// <param name="pressedKey"></param>
        /// <returns></returns>
        public bool SingleKeyPress(Keys pressedKey)
        {
            if (kb.IsKeyDown(pressedKey) && previousKb.IsKeyUp(pressedKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Implement when working on enemy collision
       
        //not applicable
        public override void CheckColliderAgainstPlayer(Player p)
        {
            //do nothing
        }
       


        //animation fields
      
      

        

       

    }
}
