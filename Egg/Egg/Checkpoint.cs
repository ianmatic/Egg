using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Egg
{
    class Checkpoint : GameObject
    {
        private Screen originScreen;
        bool startCheckpointSound = true; //used to prevent sound from stacking when player touches checkpoint
        public Screen OriginScreen
        {
            get { return originScreen; }
        }

        public Checkpoint(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Screen originScreen)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.isActive = true;
            this.hasGravity = false;
            this.originScreen = originScreen;
        }
        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                sb.Draw(defaultSprite, hitbox, Color.Bisque);
            }
        }
        public override void CheckColliderAgainstPlayer(Player p)
        {
            if (hitbox.Intersects(p.Hitbox))
            {
                //if the player touches the checkpoint for the first time, only make the sound once
                if (startCheckpointSound)
                {
                    p.CheckpointSound.Play();
                    startCheckpointSound = false;
                }

                p.LastCheckpoint = this;
            }
            if (!this.Equals(p.LastCheckpoint))
            {
                //only play the sound if this isn't the player's current checkpoint
                this.startCheckpointSound = true;
            }
        }

        //Enemies don't need a spawn point
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            //throw new NotImplementedException();
        }
        //Doesn't change states
        public override void FiniteState()
        {
            throw new NotImplementedException();
        }
        //No movement of checkpoints
        public override void Movement()
        {
            throw new NotImplementedException();
        }
    }
}
