using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Egg
{
    abstract class GameObject
    {
        private Rectangle hitbox;
        private Texture2D defaultSprite;
        private int drawLevel;
        private bool isActive;

        //The order/layer the sprite should be drawn on screen. 
        public int DrawLevel
        {
            get { return this.drawLevel; }
            set { this.drawLevel = value; }
        }

        public Texture2D DefaultSprite
        {
            get { return this.defaultSprite; }
        }

        public Rectangle Hitbox
        {
            get { return this.hitbox; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        } 



        public abstract void Draw(SpriteBatch sb);

    }
}
