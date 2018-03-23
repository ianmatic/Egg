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
        //fields
        protected Rectangle hitbox;
        protected Texture2D defaultSprite;
        protected int drawLevel;
        protected bool isActive;
        protected bool hasGravity;

        public int X
        {
            get { return hitbox.X; }
            set { hitbox.X = value; }
        }
        public int Y
        {
            get { return hitbox.Y; }
            set { hitbox.Y = value; }
        }

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
        public bool HasGravity
        {
            get { return hasGravity; }
            set { hasGravity = value; }
        }


        public abstract void Draw(SpriteBatch sb);
        /// <summary>
        /// implement movement mechanics for appropriate gameObjects
        /// </summary>
        public abstract void Movement();

        public abstract void FiniteState();

        public abstract void CheckColliderAgainstPlayer(Player p);
    }
}
