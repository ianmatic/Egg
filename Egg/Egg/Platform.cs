using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egg
{
    //This class is an artifact from a very early phase of development and serves no purpose other than to hold code to copy and paste with later
    class Platform : GameObject
    {
        public override void Draw(SpriteBatch sb)
        {

        }
        public override void Movement()
        {
            throw new NotImplementedException();
        }

        public override void FiniteState()
        {
            throw new NotImplementedException();
        }

        public override void CheckColliderAgainstPlayer(Player p)
        {
            throw new NotImplementedException();
        }

        public override void CheckColliderAgainstEnemy(Enemy e)
        {
            throw new NotImplementedException();
        }

        private void DrawWalking(SpriteEffects flip)
        {
            //this is what Chris had, feel free to use or remove as needed.
            //spriteBatch.Draw(
            //marioTexture,
            //marioPosition,
            //new Rectangle(widthOfSingleSprite * currentFrame, 0, 
            //widthOfSingleSprite, marioTexture.Height),
            //Color.White,
            //0.0f,
            //Vector2.Zero,
            //1.0f,
            //flip,
            //0.0f);
        }

        private void DrawIdle(SpriteEffects flip)
        {
            //spriteBatch.Draw(
            //marioTexture,
            //marioPosition,
            //new Rectangle(0, 0, widthOfSingleSprite, marioTexture.Height),
            //Color.White,
            //0.0f,
            //Vector2.Zero,
            //1.0f,
            //flip,
            //0.0f);
        }
    }
}
