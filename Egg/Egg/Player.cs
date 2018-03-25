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
        private double miliseconds; //used for float/downdash
        private double delay; //used for float/downdash

        private Rectangle hitBox;

        //for collision
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle sideChecker;

        //for directionality
        private bool isFacingRight;

        private PlayerState playerState;

        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;

        private Color color;

        //testing collision
        Enemy enemy;
        Platform platform;

        GameTime gameTime;
        private int hitstunTimer; //need for enemy constructor

        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }

        //Property
        public Rectangle HitBox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }
        public PlayerState PlayerState
        {
            get { return playerState; }
            set { playerState = value; }
        }

        //Constructor for player
        public Player(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color, int x, int y)
            
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;

            //checkers used for collision detection, one on top of Player, one below, and one to the side
            bottomChecker = new Rectangle(x, y + hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            topChecker = new Rectangle(x, y - hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            if (isFacingRight)
            {
                sideChecker = new Rectangle(x + hitBox.Width, y, Math.Abs(horizontalVelocity), hitbox.Height);
            }
            else
            {
                sideChecker = new Rectangle(x - hitBox.Width, y, Math.Abs(horizontalVelocity), hitbox.Height);
            }

            enemy = new Enemy(hitBox, defaultSprite, drawLevel, hitstunTimer);
            platform = new Platform();
            hasGravity = true;

            gameTime = new GameTime();
            delay = 30;
            miliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        /// <summary>
        /// determines player state based on input and collision with enemies/platforms
        /// </summary>
        public override void FiniteState()
        {
            kb = Keyboard.GetState();

            //FSM
            switch (playerState)
            {

                case PlayerState.IdleLeft:
                    isFacingRight = false;
                    Movement();

                    if (kb.IsKeyDown(Keys.Space))
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

                case PlayerState.IdleRight:
                    isFacingRight = true;
                    Movement();

                    if (kb.IsKeyDown(Keys.Space))
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

                case PlayerState.WalkLeft:
                    isFacingRight = false;
                    Movement();

                    if (kb.IsKeyUp(Keys.A))
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
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    //Remember to implement HitStun here
                    break;

                case PlayerState.WalkRight:
                    isFacingRight = true;
                    Movement();

                    if (kb.IsKeyUp(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    //Remember to implement HitStun here
                    break;
                case PlayerState.RollLeft:
                    isFacingRight = false;
                    Movement();

                    //touching an enemy
                    if (hitbox.Intersects(enemy.Hitbox))
                    {
                        //bounce in opposite direction
                        playerState = PlayerState.BounceRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    /*else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }*/
                    break;
                case PlayerState.RollRight:
                    isFacingRight = true;
                    Movement();

                    //touching an enemy
                    if (hitbox.Intersects(enemy.Hitbox))
                    {
                        //bounce in opposite direction
                        playerState = PlayerState.BounceLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    /*else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }*/
                    break;

                case PlayerState.JumpLeft:
                    isFacingRight = false;
                    Movement();


                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }
                    
                    playerState = PlayerState.Fall;
                    miliseconds = 2;
                    //HitStun
                    break;

                case PlayerState.JumpRight:
                    isFacingRight = true;
                    Movement();

                    while (miliseconds > 1)
                    {
                        miliseconds--;
                    }
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }
                    playerState = PlayerState.Fall;

                    miliseconds = 2;
                    //HitStun
                    break;
                case PlayerState.FloatLeft:
                    Movement();
                    break;
                case PlayerState.FloatRight:
                    Movement();
                    break;
                case PlayerState.Fall:
                    Movement();
                    if (kb.IsKeyDown(Keys.LeftAlt))
                    {
                        playerState = PlayerState.DownDash;
                    }
                    if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    //HitStun
                    break;

                case PlayerState.DownDash:
                    Movement();
                    if (hitbox.Intersects(enemy.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.BounceLeft;
                    }
                    else if (hitbox.Intersects(enemy.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.BounceRight;
                    }
                    //not sure how to determine what direction player will face if no key is pressed
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    break;

                case PlayerState.BounceLeft:
                    isFacingRight = false;
                    while (miliseconds > 0)
                    {
                        miliseconds--;
                    }
                    playerState = PlayerState.Fall;
                    miliseconds = 2;
                    break;

                case PlayerState.BounceRight:
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
                    velocityType -= rate;
                }
            }
            else
            {
                if (velocityType < limit)
                {
                    velocityType += rate;
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
            else
            {
                if (isFacingRight)
                {
                    if (velocityType < limit)
                    {
                        velocityType += rate;
                    }
                }
                else
                {
                    //move negatively (decrease value past 0 until negative limit is hit)
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
        /// Checks if hitboxes around player touch platforms
        /// </summary>
        public bool CollisionCheck(Tile t)
        {
            bool output = false;
            if (topChecker.Intersects(platform.Hitbox))
            {
                verticalVelocity = 0;
                output = true;
            }
            else if (bottomChecker.Intersects(platform.Hitbox))
            {
                verticalVelocity = 0;
                output = true;
            }
            if (sideChecker.Intersects(platform.Hitbox))
            {
                horizontalVelocity = 0;
                output = true;
            }

            return output;
        }
        /// <summary>
        /// Calls accelerate/decelerate methods based on FSM state, the direction is accounted for in the methods
        /// </summary>
        public override void Movement()
        {
            
            //Idle
            if (playerState == PlayerState.IdleLeft || playerState == PlayerState.IdleRight)
            {
                if (horizontalVelocity <= 10 && horizontalVelocity >= -10)
                {
                    Decelerate(horizontalVelocity, 1, 0, false);
                }
                else if (horizontalVelocity > 10 || horizontalVelocity < -10)
                {
                    Decelerate(horizontalVelocity, 2, 0, false);
                }
            }
            //Walk
            else if (playerState == PlayerState.WalkLeft || playerState == PlayerState.WalkRight)
            {
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
                verticalVelocity = -30;
                playerState = PlayerState.Fall;
            }
            else if (playerState == PlayerState.FloatLeft || playerState == PlayerState.FloatRight)
            {
                verticalVelocity = 0;
            }
            //Fall
            else if (playerState == PlayerState.Fall)
            {
                Accelerate(verticalVelocity, 2, 30, true);
            }
            //Down-dash
            else if (playerState == PlayerState.DownDash)
            {
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
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, this.color);
        }
        //not applicable
        public override void CheckColliderAgainstPlayer(Player p)
        {
            //do nothing
        }
    }
}
