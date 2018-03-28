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
    /// <summary>
    /// Represents a single tile of level geometry
    /// </summary>
    class Tile : GameObject
    {
        private TileType type;

        public TileType Type
        {
            get { return type; }
        }
        public enum TileType
        {
            Damaging,
            Normal,
            NoCollision,
            Moving
        }

        public override void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                sb.Draw(this.defaultSprite, this.hitbox, Color.White);          
            }
        }

        public Tile(int drawLevel, Texture2D defaultSprite, Rectangle hitbox, TileType type)
        {
            this.drawLevel = drawLevel;
            this.defaultSprite = defaultSprite;
            this.hitbox = hitbox;
            this.isActive = true;
            this.hasGravity = false;
            this.type = type;
        }

        public override void CheckColliderAgainstPlayer(Player p)
        {
            if (p.CollisionCheck(this))
            {
                if (this.Type == TileType.Damaging)
                {
                    //Damage player
                }
            }
        }

        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            e.CollisionCheck(this);      
        }
        public override void FiniteState()
        {
            throw new NotImplementedException();
        }
        public override void Movement()
        {
            throw new NotImplementedException();
        }

    }
}
