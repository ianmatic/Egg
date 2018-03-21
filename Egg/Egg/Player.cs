using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Egg
{
    class Player : GameObject
    {
        //FSM states
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

        //fields
        private KeyboardState kb;
        private int timer;
        private Rectangle hitBox;
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle sideChecker;
        private bool isFacingRight;
        PlayerState playerState;
        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;
        private Color color;

        Enemy enemy;
        Platform platform;
        GameTime gameTime;
        private int hitstunTimer;

        //Constructor
        public Player(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color, int x, int y)
            
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;

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
            kb = Keyboard.GetState();
            enemy = new Enemy(hitBox, defaultSprite, drawLevel, hitstunTimer);
            platform = new Platform();
            timer = 2;
            hasGravity = true;

            gameTime = new GameTime();
        }


        /// <summary>
        /// determines player state based on input and collision with enemies/platforms
        /// </summary>
        public override void FiniteState()
        {
            hitBox.X += horizontalVelocity;
            hitBox.Y -= verticalVelocity;
            //FSM
            switch (playerState)
            {
                case PlayerState.IdleLeft:
                    isFacingRight = false;
                    Decelerate(horizontalVelocity, 2, 0);
                    if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //HitStun
                    break;

                case PlayerState.IdleRight:
                    isFacingRight = true;
                    Decelerate(horizontalVelocity, 2, 0);
                    if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    //HitStun
                    break;

                case PlayerState.WalkLeft:
                    isFacingRight = false;
                    Accelerate(horizontalVelocity, 5, 10);
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
                    //HitStun
                    break;

                case PlayerState.WalkRight:
                    isFacingRight = true;
                    Accelerate(horizontalVelocity, 5, 10);
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
                    //HitStun
                    break;
                case PlayerState.RollLeft:
                    isFacingRight = false;
                    Accelerate(horizontalVelocity, 7, 15);
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
                    else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }
                    break;
                case PlayerState.RollRight:
                    isFacingRight = true;
                    Accelerate(horizontalVelocity, 7, 15);
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
                    else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }
                    break;

                case PlayerState.JumpLeft:
                    isFacingRight = false;
                    Accelerate(verticalVelocity, 3, 5);
                    while (timer > 1)
                    {
                        timer--;
                    }
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    //HitStun
                    break;

                case PlayerState.JumpRight:
                    isFacingRight = true;
                    Accelerate(verticalVelocity, 3, 5);
                    while (timer > 1)
                    {
                        timer--;
                    }
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    playerState = PlayerState.Fall;

                    timer = 2;
                    //HitStun
                    break;

                case PlayerState.Fall:
                    Decelerate(verticalVelocity, 3, 0);
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
                    while (timer > 0)
                    {
                        timer--;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    break;

                case PlayerState.BounceRight:
                    isFacingRight = true;
                    while (timer > 0)
                    {
                        timer--;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    break;

                    //HitStun cases

            }
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, this.color);
        }

        /// <summary>
        /// slowdown the object by the rate until the limit velocity is reached 
        /// </summary>
        /// <param name="velocityType"></param>
        /// <param name="rate"></param>
        public void Decelerate(int velocityType, int rate, int limit)
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
        }
        /// <summary>
        /// speed up the object by the rate until the limit velocity is reached
        /// </summary>
        /// <param name="velocityType"></param>
        /// <param name="rate"></param>
        /// <param name="limit"></param>
        public void Accelerate(int velocityType, int rate, int limit)
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
                if (velocityType > limit)
                {
                    velocityType -= rate;
                }
            }
        }
        /// <summary>
        /// Checks if hitboxes around player touch platforms
        /// </summary>
        public bool CollisionCheck()
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
        public override void Movement()
        {
            //nothing for now
        }
    }
}
