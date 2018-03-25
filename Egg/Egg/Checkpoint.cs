using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Egg
{
    class Checkpoint : GameObject
    {
        public Checkpoint(int drawLevel, Texture2D defaultSprite, Rectangle hitbox)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.isActive = true;
            this.hasGravity = false;
        }

        //No movement of checkpoints
        public override void Movement()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                sb.Draw(defaultSprite, hitbox, Color.White);
            }
        }

        //Doesn't change states
        public override void FiniteState()
        {
            throw new NotImplementedException();
        }

        public override void CheckColliderAgainstPlayer(Player p)
        {
            if (hitbox.Intersects(p.Hitbox))
            {
                //Update player's checkpoint
            }
        }

        //Enemies don't need a spawn point
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }
    }
}
