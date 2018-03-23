using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg
{
    //Defines a chicken that needs to be saved by the player
    class CapturedChicken : GameObject
    {
        Color color;

        public CapturedChicken(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;
            this.isActive = true;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, this.color);
        }
        //Chickens won't move, so left unimplemented.
        public override void Movement()
        {
            throw new NotImplementedException();
        }

        //Unknown what will be done with this
        public override void FiniteState()
        {
            throw new NotImplementedException();
        }

        //Checks if player has collected chicken
        public override void CheckColliderAgainstPlayer(Player p)
        {
            if (hitbox.Intersects(p.Hitbox))
            {
                //Run some method on P to update saved chickens
                isActive = false;
            }
        }

        //Enemies pass through chickens
        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }
    }
}
