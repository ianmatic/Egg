using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Egg
{
    class Player : GameObject
    {
        //FSM states
        enum PlayerState
        {
            IdleLeft,
            IdleRight,

            WalkLeft,
            WalkRight,

            RollLeft,
            RollRight,

            JumpLeft,
            JumpRight,

            FloatLeft,
            FloatRight,

            Fall,
            DownDash,

            BounceLeft,
            BounceRight,

            HitStunRight,
            HitStunLeft
        }

        //fields
        private int health;
        private KeyboardState kb;
        private int timer;
        PlayerState playerState;
        Enemy enemy;
        Platform platform;

        //Constructor
        public Player()
        {
            kb = Keyboard.GetState();
            enemy = new Enemy();
            platform = new Platform();
            timer = 2;

            //FSM
            switch (playerState)
            {
                case PlayerState.IdleLeft:
                    if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //HitStun
                    break;

                case PlayerState.IdleRight:
                    if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.JumpRight;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    //HitStun
                    break;

                case PlayerState.WalkLeft:
                    if (kb.IsKeyUp(Keys.A))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    //HitStun
                    break;

                case PlayerState.WalkRight:
                    if (kb.IsKeyUp(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollRight;
                    }
                    //HitStun
                    break;
                case PlayerState.RollLeft:
                    if (hitbox.Intersects(enemy.Hitbox))
                    {
                        //bounce in opposite direction
                        playerState = PlayerState.BounceRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }
                    break;
                case PlayerState.RollRight:
                    if (hitbox.Intersects(enemy.Hitbox))
                    {
                        //bounce in opposite direction
                        playerState = PlayerState.BounceLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                    }
                    else if (kb.IsKeyUp(Keys.LeftShift))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (!hitbox.Intersects(platform.Hitbox) && !hitbox.Intersects(enemy.Hitbox))
                    {
                        playerState = PlayerState.Fall;
                    }
                    break;

                case PlayerState.JumpLeft:
                    while (timer > 1)
                    {
                        timer--;
                    }
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    //HitStun
                    break;

                case PlayerState.JumpRight:
                    while (timer > 1)
                    {
                        timer--;
                    }
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        playerState = PlayerState.FloatLeft;
                    }
                    playerState = PlayerState.Fall;

                    timer = 2;
                    //HitStun
                    break;

                case PlayerState.Fall:
                    if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    //HitStun
                    break;

                case PlayerState.DownDash:
                    if (hitbox.Intersects(enemy.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.BounceLeft;
                    }
                    else if (hitbox.Intersects(enemy.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.BounceRight;
                    }
                    //not sure how to determine what direction player will face if no key is pressed
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.IdleLeft;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.IdleRight;
                    }
                    else if (hitbox.Intersects(platform.Hitbox) && kb.IsKeyDown(Keys.LeftShift))
                    {
                        playerState = PlayerState.RollLeft;
                    }
                    break;

                case PlayerState.BounceLeft:
                    while (timer > 0)
                    {
                        timer--;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    break;

                case PlayerState.BounceRight:
                    while (timer > 0)
                    {
                        timer--;
                    }
                    playerState = PlayerState.Fall;
                    timer = 2;
                    break;

                    //HitStun cases

            }
        }
        public override void Draw(SpriteBatch sb)
        {
           
        }
    }
}
