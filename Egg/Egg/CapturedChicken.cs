using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg
{
    //Being used to debug the draw system, feel free to make structural changes later
    class CapturedChicken : GameObject
    {
        Color color;

        public CapturedChicken(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, Color color)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.color = color;
        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(defaultSprite, hitbox, this.color);
        }
        public override void Movement()
        {
            throw new NotImplementedException();
        }

        public override void FiniteState()
        {
            throw new NotImplementedException();
        }
    }
}
