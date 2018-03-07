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
        /* Class designed to handle both stationary and moving players. 
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
        private int walkSpeed;
        private int walkDistance;
        private bool faceRight;
        private EnemyState status;

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
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int hitstunTimer, int walkSpeed, int walkDistance)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = false;
            this.walkSpeed = walkSpeed;
            this.walkDistance = walkDistance;
        }

        //Constructor for stationary enemy
        public Enemy(Rectangle hitbox, Texture2D defaultSprite, int drawLevel, int hitstunTimer)
        {
            this.hitbox = hitbox;
            this.defaultSprite = defaultSprite;
            this.drawLevel = drawLevel;
            this.isActive = false;
            this.walkSpeed = 0;
            this.walkDistance = 0;
        }

        //FSM transitions
        public void UpdateEnemyStatus()
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
        //Implementation of FSM
        public void UpdateEnemyData()
        {
            switch (status)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.WalkLeft:
                    break;
                case EnemyState.WalkRight:
                    break;
                case EnemyState.Hitstun:
                    break;
            }
        }

        //Placeholder
        public void TriggerHitstun()
        {

        }

        //Default for now, should change what sprite is drawn depending on FSM
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, Color.White);
        }
    }
}
