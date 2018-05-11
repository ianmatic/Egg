using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg
{

    class Enemy : GameObject
    {
        /* Class designed to handle both stationary and moving enemies. 
         * Latest commit: Setup most of below, including constructors, fields and properties, FSM transitions, and default draw method
         * 
         * TODO:
         * 
         * Implement FSM
         * Update draw so it reflects the FSM
         * Method to trigger hitstun
         */

        //How long the enemy is in hitstun before dying.
        private int hitstunTimer;
        private int maxHitstunTime = 60;

        //Horizontal and vertical components of the enemy's speed
        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;

        //How many times Movement is called by default before the enemy turns around
        private int walkDistance;
        
        //How many loops of Movement the enemy has performed
        private int walkProgress;

        //Prevents collision glitchiness / debugging stuff
        int collideTimeout = 0;
        int maxTimeout = 3;
        bool inTimeout = false;
        bool debugCollision = false;

        private EnemyState status;

        //Small hitboxes used in collision detection
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle rightChecker;
        private Rectangle leftChecker;

        

        public int HitstunTimer
        {
            get { return hitstunTimer; }
        }

        public int HorizontalVelocity
        {
            get { return this.horizontalVelocity; }
        }

        public int VerticalVelocity
        {
            get { return this.verticalVelocity; }
        }

        public int WalkDistance
        {
            get { return this.walkDistance; }
        }

        public EnemyState GetStatus()
        {
            return this.status;
        }

        public bool DebugCollision
        {
            get { return debugCollision; }
            set { this.debugCollision = value; }
        }


        public enum EnemyState
        {
            Idle,
            Hitstun,
            WalkLeft,
            WalkRight
        }

        
        /// <summary>
        /// Creates an enemy that moves horizontally and vertically       
        /// </summary>
        /// <param name="hitbox">Collision box of the enemy</param>
        /// <param name="defaultSprite">Sprite used to draw the enemy</param>
        /// <param name="drawLevel">Order of when drawn to screen (bigger number -> closer to front)</param>
        /// <param name="maxHitstunTime">Length of time the enemy is in hitstun before dying</param>
        /// <param name="horizontalVelocity">Speed of enemy in the x direction</param>
        /// <param name="verticalVelocity">Speed of enemy in the y direction</param>
        /// <param name="walkDistance"></param>
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int maxHitstunTime, int horizontalVelocity, int verticalVelocity, int walkDistance)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = true;
            this.horizontalVelocity = horizontalVelocity;
            this.verticalVelocity = verticalVelocity;
            this.walkDistance = walkDistance;
            this.maxHitstunTime = maxHitstunTime;


            bottomChecker = new Rectangle(hitbox.X, hitbox.Y + hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            topChecker = new Rectangle(hitbox.X, hitbox.Y - hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            rightChecker = new Rectangle(hitbox.X + hitbox.Width, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);
            leftChecker = new Rectangle(hitbox.X - hitbox.Width, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);

            walkProgress = 0;
        }

        /// <summary>
        /// Creates a stationary enemy
        /// </summary>
        /// <param name="hitbox">Collision box of the enemy</param>
        /// <param name="defaultSprite">Sprite used to draw the enemy</param>
        /// <param name="drawLevel">Order of when drawn to screen (bigger number -> closer to front)</param>
        /// <param name="maxHitstunTime">Length of time the enemy is in hitstun before dying</param>
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int maxHitstunTime)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = true;
            this.horizontalVelocity = 0;
            this.verticalVelocity = 0;
            this.walkDistance = 0;          
            this.maxHitstunTime = maxHitstunTime;
            walkProgress = 0;
        }

        //Implementation of FSM, called every update loop
        public void UpdateEnemyData()
        {
            switch (status)
            {
                case EnemyState.Idle:
                    Movement();
                    break;
                case EnemyState.WalkLeft:
                    Movement();
                    break;
                case EnemyState.WalkRight:
                    Movement();
                    break;
                case EnemyState.Hitstun:
                    hitstunTimer += 1;
                    if (hitstunTimer > maxHitstunTime)
                    {
                        isActive = true;
                        hitstunTimer = 0;
                    }
                    break;
            }
            if (inTimeout)
            {
                collideTimeout++;
                if (collideTimeout == maxTimeout)
                {
                    collideTimeout = 0;
                }
            
            }
        }

        //Causes enemy to enter hitstun animation and eventually die. 
        public void TriggerHitstun()
        {
            hitstunTimer++;
        }

        //Default for now, should change what sprite is drawn depending on FSM
        public override void Draw(SpriteBatch sb)
        {
            if (isActive && status != EnemyState.Hitstun)
            {
                sb.Draw(defaultSprite, hitbox, Color.Gray);

                if (debugCollision)
                {
                    sb.Draw(defaultSprite, topChecker, Color.Blue);
                    sb.Draw(defaultSprite, bottomChecker, Color.Purple);
                    sb.Draw(defaultSprite, rightChecker, Color.Red);
                    sb.Draw(defaultSprite, leftChecker, Color.Yellow);
                }
                
            }
            else if (isActive && status == EnemyState.Hitstun)
            {
                sb.Draw(defaultSprite, hitbox, Color.Gray);
            }
        }

        //Moves enemy
        public override void Movement()
        {
            if (horizontalVelocity == 0 && verticalVelocity == 0)
            {
                return;
            }

            Point temp = hitbox.Location;

            temp.X += horizontalVelocity;
            temp.Y += verticalVelocity;
            hitbox.Location = temp;

            walkProgress += 1;

            if (walkProgress == walkDistance)
            {
                horizontalVelocity *= -1;
                verticalVelocity *= -1;
                walkProgress = 0;
            }

            bottomChecker = new Rectangle(hitbox.X, hitbox.Y + hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            topChecker = new Rectangle(hitbox.X, hitbox.Y - 5, hitbox.Width, Math.Abs(verticalVelocity));
            rightChecker = new Rectangle(hitbox.X + hitbox.Width, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);
            leftChecker = new Rectangle(hitbox.X - 5, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);
            

        }

        public override void FiniteState()
        {
            if (isActive)
            {
                if (horizontalVelocity == 0 && hitstunTimer == 0)
                {
                    this.status = EnemyState.Idle;
                }
                else if (horizontalVelocity > 0 && hitstunTimer == 0)
                {
                    this.status = EnemyState.WalkRight;
                }
                else if (horizontalVelocity < 0 && hitstunTimer == 0)
                {
                    this.status = EnemyState.WalkLeft;
                }
                else
                {
                    this.status = EnemyState.Hitstun;
                }
            }
        }

        public override void CheckColliderAgainstPlayer(Player p)
        {
            if (hitbox.Intersects(p.Hitbox) && isActive)
            {
                if (p.PlayerState == PlayerState.RollRight || p.PlayerState == PlayerState.RollLeft || p.PlayerState == PlayerState.DownDash)
                {
                    TriggerHitstun();
                }
            }
        }

        /// <summary>
        /// Checks collision between the enemy and a tile, returns true if colliding
        /// </summary>
        /// <param name="t">The tile to check collision against.</param>
        /// <returns></returns>
        public bool CollisionCheck(Tile t)
        {
            bool output = false;

            //if (t.Hitbox.Intersects(this.hitbox) && isActive)
            //{
            //    horizontalVelocity *= -1;
            //    verticalVelocity *= -1;
            //    walkProgress = 0;
            //    return true;
            //}

            //return false;

            if (t.Hitbox.Intersects(topChecker) && !inTimeout)
            {
                horizontalVelocity *= -1;
                verticalVelocity *= -1;
                walkProgress = 0;
                inTimeout = true;
                output = true;
            }
            else if (t.Hitbox.Intersects(bottomChecker) && !inTimeout)
            {
                horizontalVelocity *= -1;
                verticalVelocity *= -1;
                walkProgress = 0;
                inTimeout = true;
                output = true;
            }
            else if (t.Hitbox.Intersects(leftChecker) && !inTimeout)
            {
                horizontalVelocity *= -1;
                verticalVelocity *= -1;
                walkProgress = 0;
                inTimeout = true;
                output = true;
            }
            else if (t.Hitbox.Intersects(rightChecker) && !inTimeout)
            {
                horizontalVelocity *= -1;
                verticalVelocity *= -1;
                walkProgress = 0;
                inTimeout = true;
                output = true;
            }

            return output;
            
        }

        //Shouldn't check its collision against other enemies unless we decide enemies bounce off of each other instead of pass through
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            //throw new NotImplementedException();
        }

    }
}
