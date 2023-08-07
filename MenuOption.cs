using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class MenuOption : Sprite
    {
        private SpriteFont gameFont;
        private TextBox textBox;
        private int x;
        private int y;
        private MouseState oState;

        public MenuOption(string texture, Vector2 pos, string text) : base(texture, pos)
        {
            gameFont = Globals.Content.Load<SpriteFont>("gameFont");
            textBox = new TextBox(gameFont);
            textBox.Text = text;
        }

        public void SetText(string text)
        {
            textBox.Text = text;
        }

        public void SetRotation(float rotation)
        {
            base.rotation = rotation;
        }

        public void SetScale(float scale)
        {
            base.scale = scale;
        }

        public void OffsetText(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetTextRotation(float rotation)
        {
            textBox.rotation = rotation;
        }

        public void SetTextScale(float scale)
        {
            textBox.scale = scale;
        }

        public void Update(CollisionRect rect)
        {
            MouseState mState = Mouse.GetState();

            if (rect.Contains(mState.Position))
            {
                SetScale(1.3f);
                SetTextScale(1.3f);
            }
            else
            {
                SetScale(1f);
                SetTextScale(1f);
            }
            oState = mState;
        }

        public void Draw()
        {
            Draw(Color.White);
            textBox.Draw((int)pos.X + x, (int)pos.Y - 300 + y, Color.White);
        }
    }
}
