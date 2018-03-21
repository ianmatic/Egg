using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Egg
{
    class Tile : GameObject
    {
        public override void Draw(SpriteBatch sb)
        {
            throw new NotImplementedException();
        }

        public override void Movement()
        {
            throw new NotImplementedException();
        }

        public override void FiniteState()
        {
            throw new NotImplementedException();
        }

        public Tile(int drawLevel, Texture2D defaultSprite, Rectangle hitbox)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.isActive = true;
            this.hasGravity = false;
        }

        //In progress
        /*public bool CollidingWithCharacter(GameObject g)
        {
            if (g is Player)
            {
                return false;
            }
            else if (g is Enemy)
            {
                if (g.HasGravity == false)
                {
                    return false;
                }
                else
                {

                }
            }
            else
            {
                return false;
            }
        }*/

    }
}
