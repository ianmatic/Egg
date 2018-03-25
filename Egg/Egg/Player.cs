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
        private bool bottomIntersects;
        private bool topIntersects;
        private bool sideIntersects;

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

            enemy = new Enemy(hitBox, defaultSprite, drawLevel, hitstunTimer);
            platform = new Platform();
            hasGravity = true;


            bottomIntersects = false;
            topIntersects = false;
            sideIntersects = false;

            gameTime = new GameTime();
            delay = 13;
            miliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        /// <summary>
        /// determines player state based on input and collision with enemies/platforms
        /// </summary>
        public override void FiniteState()
        {
            kb = Keyboard.GetState();

            bottomChecker = new Rectangle(X, Y + hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            if (verticalVelocity == 0 && !kb.IsKeyDown(Keys.Space))
            {
                bottomChecker = new Rectangle(X, Y + hitbox.Height, hitbox.Width, 1);
            }
            
            topChecker = new Rectangle(X, Y - hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            if (isFacingRight)
            {
                sideChecker = new Rectangle(X + hitBox.Width, Y, Math.Abs(horizontalVelocity), hitbox.Height);
            }
            else
            {
                sideChecker = new Rectangle(X - hitBox.Width, Y, Math.Abs(horizontalVelocity), hitbox.Height);
            }

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


                    if(!bottomIntersects)
                    {
                        playerState = PlayerState.Fall;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    else if (kb.IsKeyUp(Keys.A))
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

                case PlayerState.WalkRight:
                    isFacingRight = true;
                    Movement();
                    if (!bottomIntersects)
                    {
                        playerState = PlayerState.Fall;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    else if (kb.IsKeyUp(Keys.D))
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

                    //Remember to implement HitStun here
                    break;
                case PlayerState.RollLeft:
                    isFacingRight = false;
                    Movement();

                    if (!bottomIntersects)
                    {
                        playerState = PlayerState.Fall;
                    }
                    else if (hitbox.Intersects(enemy.Hitbox))
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
                    break;
                case PlayerState.RollRight:
                    isFacingRight = true;
                    Movement();

                    if (!bottomIntersects)
                    {
                        playerState = PlayerState.Fall;
                    }
                    else if (hitbox.Intersects(enemy.Hitbox))
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

                    delay = 13;
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
            //if vertical velocity is 0, bottom checker is 0, so it has to pretend to be size 1
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
            bottomIntersects = false;

            if (topChecker.Intersects(t.Hitbox))
            {
                verticalVelocity = 0;
                topIntersects = true;
                output = true;
            }
            else if (bottomChecker.Intersects(t.Hitbox))
            {
                verticalVelocity = 0;
                hitbox.Y = t.Y - hitbox.Height;
               if (kb.IsKeyDown(Keys.A))
                {
                    playerState = PlayerState.WalkLeft;
                }
                else if (kb.IsKeyDown(Keys.D))
                {
                    playerState = PlayerState.WalkRight;
                }
                else if (isFacingRight && !kb.IsKeyDown(Keys.D))
                {
                    playerState = PlayerState.IdleRight;
                }
                else if (!isFacingRight && !kb.IsKeyDown(Keys.A))
                {
                    playerState = PlayerState.IdleLeft;
                }

                bottomIntersects = true;
                output = true;
            }
            if (sideChecker.Intersects(t.Hitbox))
            {
                horizontalVelocity = 0;
                sideIntersects = true;
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
