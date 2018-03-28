using System;
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
        //fields
        private KeyboardState kb;
        private KeyboardState previousKb; //used to prevent jump spamming

        private double miliseconds; //used for float/downdash
        private double delay; //used for float/downdash

        //for collision
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle sideChecker;
        private bool bottomIntersects;
        private bool topIntersects;

        //for directionality and FSM
        private bool isFacingRight;
        private PlayerState playerState;

        //for movement
        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;

        private Color color;

        GameTime gameTime;

        //Property
        public PlayerState PlayerState
        {
            get { return playerState; }
            set { playerState = value; }
        }
        public Rectangle BottomChecker
        {
            get { return bottomChecker; }
        }
        public Rectangle TopChecker
        {
            get { return topChecker; }
        }
        public Rectangle SideChecker
        {
            get { return sideChecker; }
        }

        //Constructor for player
        public Player(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color, int x, int y)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;

            hasGravity = true; //no point other than it must be implement since it inherets GameObject


            bottomIntersects = false;
            topIntersects = false;

            gameTime = new GameTime();
            delay = 13;
            miliseconds = 2;
        }
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
        /// Checks if hitboxes around player touch Tile t
        /// </summary>
        public bool CollisionCheck(Tile t)
        {
            bool output = false;
            bottomIntersects = false;

            //wall collision (collision box next to player depending on direction facing)
            if (sideChecker.Intersects(t.Hitbox))
            {
                horizontalVelocity = 0; //stop player from moving through wall
                if (isFacingRight && t.X > hitbox.X) //player facing right and tile is to the right of player
                {
                    hitbox.X = t.X - hitbox.Width; //place player left of tile
                }
                else if (t.X < hitbox.X) //player facing left and tile is to the left of player
                {
                    hitbox.X = t.X + t.Hitbox.Width; //place player right of tile
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
                verticalVelocity = 0; //stop the player from falling
                hitbox.Y = t.Y - hitbox.Height; //place the player on top of tile

                bottomIntersects = true;
                //FSM states are changed here so that the player can move after touching the ground

                //Roll Left
                if (kb.IsKeyDown(Keys.A) && kb.IsKeyDown(Keys.LeftShift))
                {
                    playerState = PlayerState.RollLeft;
                }
                //Walk Left
                else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
                {
                    playerState = PlayerState.WalkLeft;
                }
                //Roll Right
                else if (kb.IsKeyDown(Keys.D) && kb.IsKeyDown(Keys.LeftShift))
                {
                    playerState = PlayerState.RollRight;
                }
                //Walk Right
                else if (kb.IsKeyDown(Keys.D))
                {
                    playerState = PlayerState.WalkRight;
                }
                //Idle Right
                else if (isFacingRight && !kb.IsKeyDown(Keys.D))
                {
                    playerState = PlayerState.IdleRight;
                }
                //Idle Left
                else if (!isFacingRight && !kb.IsKeyDown(Keys.A))
                {
                    playerState = PlayerState.IdleLeft;
                }

                //everytime the player lands from a jump (or falls), the next time they jump they will hit the ceiling
                topIntersects = true; 
                output = true;
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
                }
            }
            else
            {
                if (velocityType < limit)
                {
                    velocityType += rate; //increase velocity since moving left is negative
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
        /// determines player state based on input and collision with enemies/platforms
        /// </summary>
        public override void FiniteState()
        {
            //previousKb used to prevent jump spamming (holding down space) 
            previousKb = kb;
            kb = Keyboard.GetState();

            if (isFacingRight)
            {
                //X is right of player, Y is the same as player, width depends on horizontalVelocity, height is same as player
                sideChecker = new Rectangle(X + hitbox.Width, Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
            }
            //Facing left
            else
            {
                //X is same as player (which is left edge), Y is the same as player
                //width depends on horizontalVelocity, height is same as player
                sideChecker = new Rectangle(X - Math.Abs(horizontalVelocity), Y + 10, Math.Abs(horizontalVelocity), hitbox.Height - 20);
            }
            //height is player height with vertical velocity added on (subtracting makes the height go "up" aka toward the ceiling)
            topChecker = new Rectangle(X + 10, Y - Math.Abs(verticalVelocity), hitbox.Width - 20, Math.Abs(verticalVelocity));


            bottomChecker = new Rectangle(X + 10, Y + hitbox.Height, hitbox.Width - 20, Math.Abs(verticalVelocity));

            if (verticalVelocity == 0 && !kb.IsKeyDown(Keys.Space))
            {
                bottomChecker = new Rectangle(X + 10, Y + hitbox.Height, hitbox.Width - 20, 1);
            }

            //FSM
            switch (playerState)
            {
                //Idle Left
                case PlayerState.IdleLeft:
                    isFacingRight = false;
                    Movement();

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
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //Remember to implement HitStun here
                    break;

                //Idle Right
                case PlayerState.IdleRight:
                    isFacingRight = true;
                    Movement();

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
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    //Remember to implement HitStun here
                    break;
                
                    //Walk Left
                case PlayerState.WalkLeft:
                    isFacingRight = false;
                    Movement();

                     if(!bottomIntersects) //not touching ground
                    {
                         playerState = PlayerState.Fall;
                    }
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
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //Remember to implement HitStun here
                    break;
                //Walk Right
                case PlayerState.WalkRight:
                    isFacingRight = true;
                    Movement();

                    if (!bottomIntersects) //not touching ground
                    {
                        playerState = PlayerState.Fall;
                    }
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
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }

                    //Remember to implement HitStun here
                    break;

                //Roll Left
                case PlayerState.RollLeft:
                    isFacingRight = false;
                    Movement();

                    if (!bottomIntersects) //not touching ground
                    {
                        playerState = PlayerState.Fall;
                    }
                    if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A)) //release shift to return to walking left
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D)) //release shift and walk right
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (SingleKeyPress(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    break;

                //Roll Right
                case PlayerState.RollRight:
                    isFacingRight = true;
                    Movement();

                    if (!bottomIntersects) //not touching ground
                    {
                        playerState = PlayerState.Fall;
                    }
                    if (SingleKeyPress(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A)) //release shift and walk left
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D)) //release shift and return to walking right
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    break;

                //Jump Left
                case PlayerState.JumpLeft:
                    isFacingRight = false;
                    Movement();

                    if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }
                    //Float

                    //default to fall if no other condition is met (no hitstun here, use fall's hitstun)
                    playerState = PlayerState.Fall;
                    break;

                //Jump Right
                case PlayerState.JumpRight:
                    isFacingRight = true;
                    Movement();

                    if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }
                    //Float

                    //default to fall if no other condition is met (no hitstun here, use fall's hitstun)
                    playerState = PlayerState.Fall;
                    break;

                //Float Left
                case PlayerState.FloatLeft:
                    //Need to fully implement
                    Movement();
                    break;

                //Float Right
                case PlayerState.FloatRight:
                    //Need to fully implement
                    Movement();
                    break;

                //Fall 
                case PlayerState.Fall:
                    Movement();
                    if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }

                    //adjust delay to determine how long delay is for downdash
                    delay = 13;
                    //HitStun
                    break;

                case PlayerState.DownDash:
                    Movement();
                    //Implement interaction with enemy here
                    break;

                //Bounce Left
                case PlayerState.BounceLeft:
                    //Adjusting this causes glitches, so leave alone for now
                    isFacingRight = false;
                    while (miliseconds > 0)
                    {
                        miliseconds--;
                    }
                    playerState = PlayerState.Fall;
                    miliseconds = 2;
                    break;

                //Bounce Right
                case PlayerState.BounceRight:
                    //Adjusting this causes glitches, so leave alone for now
                    isFacingRight = true;
                    while (miliseconds > 0)
                    {
                        miliseconds--;
                    }
                    playerState = PlayerState.Fall;
                    miliseconds = 2;
                    break;

                    //Remember to implement HitStun here

            }
        }
        /// <summary>
        /// Calls accelerate/decelerate methods based on FSM state, the direction is accounted for in the methods
        /// </summary>
        public override void Movement()
        {
            
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
                Accelerate(horizontalVelocity, 7, 15, false);
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
                //Need to finish implementing
                verticalVelocity = 0;
            }
            //Fall
            else if (playerState == PlayerState.Fall)
            {
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

                delay -= miliseconds;
                if (delay <= 0)
                {
                    Accelerate(verticalVelocity, 35, 50, true);
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
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }
        //not applicable
        public override void CheckColliderAgainstPlayer(Player p)
        {
            //do nothing
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, this.color);
            //sb.Draw(defaultSprite, bottomChecker, Color.Black);
            //sb.Draw(defaultSprite, sideChecker, Color.Red);
            //sb.Draw(defaultSprite, topChecker, Color.Cyan);
        }

    }
}
