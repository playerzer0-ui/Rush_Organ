using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class HowTo
    {
        private Sprite howSprite;
        private CollisionRect rect;
        private MouseState oState;

        public void Load()
        {
            howSprite = new Sprite("howTo", new Vector2(640, 360));
            rect = new CollisionRect(187, 37, 374, 73);
        }

        public void Update()
        {
            MouseState mState = Mouse.GetState();
            if(rect.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
            {
                Game1.GameState = 0;
            }

            oState = mState;
        }

        public void Draw()
        {
            howSprite.Draw(Color.White);

        }
    }
}
