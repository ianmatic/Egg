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
        private int walkSpeed;
        private int walkDistance;
        private int walkProgress;
        private bool faceRight;

        private EnemyState status;

        //Small hitboxes used in collision detection
        private Rectangle bottomChecker;
        private Rectangle topChecker;
        private Rectangle rightChecker;
        private Rectangle leftChecker;

        private int verticalVelocity = 0;
        private int horizontalVelocity = 0;

        public int HitstunTimer
        {
            get { return hitstunTimer; }
        }
        public int WalkSpeed
        {
            get { return this.walkSpeed; }
        }

        public int WalkDistance
        {
            get { return this.walkDistance; }
        }

        public bool FacingRight
        {
            get { return this.faceRight; }
        }

        public EnemyState GetStatus()
        {
            return this.status;
        }


        public enum EnemyState
        {
            Idle,
            Hitstun,
            WalkLeft,
            WalkRight
        }

        //Constructor for moving enemy
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int maxHitstunTime, int walkSpeed, int walkDistance)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = true;
            this.walkSpeed = walkSpeed;
            this.walkDistance = walkDistance;
            this.maxHitstunTime = maxHitstunTime;


            bottomChecker = new Rectangle(hitbox.X, hitbox.Y + hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            topChecker = new Rectangle(hitbox.X, hitbox.Y - hitbox.Height, hitbox.Width, Math.Abs(verticalVelocity));
            rightChecker = new Rectangle(hitbox.X + hitbox.Width, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);
            leftChecker = new Rectangle(hitbox.X - hitbox.Width, hitbox.Y, Math.Abs(horizontalVelocity), hitbox.Height);

            walkProgress = 0;
        }

        //Constructor for stationary enemy
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int maxHitstunTime)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = true;
            this.walkSpeed = 0;
            this.walkDistance = 0;
            this.horizontalVelocity = 1;
            this.verticalVelocity = 1;
            this.maxHitstunTime = maxHitstunTime;
            walkProgress = 0;
        }

        //Implementation of FSM, called every update loop
        public void UpdateEnemyData()
        {
            switch (status)
            {
                case EnemyState.Idle:
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
                        isActive = false;
                    }
                    break;
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
                sb.Draw(defaultSprite, hitbox, Color.White);
            }
            else if (isActive && status == EnemyState.Hitstun)
            {
                sb.Draw(defaultSprite, hitbox, Color.Red);
            }
        }

        //Moves enemy
        public override void Movement()
        {
            if (walkSpeed == 0)
            {
                return;
            }

            Point temp = hitbox.Location;

            temp.X += walkSpeed;
            hitbox.Location = temp;

            walkProgress += 1;

            if (walkProgress == walkDistance)
            {
                walkSpeed *= -1;
            }
            
        }

        public override void FiniteState()
        {
            if (isActive)
            {
                if (walkSpeed == 0 && hitstunTimer == 0)
                {
                    this.status = EnemyState.Idle;
                }
                else if (walkSpeed > 0 && hitstunTimer == 0)
                {
                    this.status = EnemyState.WalkRight;
                }
                else if (walkSpeed < 0 && hitstunTimer == 0)
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
            if (hitbox.Intersects(p.Hitbox))
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
            if (topChecker.Intersects(t.Hitbox))
            {
                verticalVelocity = 0;
                output = true;
            }
            else if (bottomChecker.Intersects(t.Hitbox))
            {
                verticalVelocity = 0;
                output = true;
            }
            if (rightChecker.Intersects(t.Hitbox))
            {
                horizontalVelocity = 0;
                output = true;
            }
            else if (leftChecker.Intersects(t.Hitbox))
            {
                horizontalVelocity = 0;
                output = true;
            }

            return output;
        }

        //Shouldn't check its collision against other enemies unless we decide enemies bounce off of each other instead of pass through
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }

    }
}
