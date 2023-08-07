using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class KeyPiece : Sprite
    {
        private bool clicked = false;
        private CollisionRect colRect;
        private SoundEffect sound;
        private MouseState oState;
        private KeyboardState nState;
        float clickTime = 0.2f;
        float clickMaxTime = 0.2f;

        public KeyPiece(string texture, Vector2 pos, CollisionRect colRect, SoundEffect sound) : base(texture, pos)
        {
            this.colRect = colRect;
            this.sound = sound;
        }
        public bool Clicked { get => clicked; set => clicked = value; }

        public void Update(GameTime gt, Keys keys)
        {
            //check input
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();

            if (colRect.Contains(mState.Position) && (mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released) || kState.IsKeyDown(keys) && nState.IsKeyUp(keys))
            {
                sound.Play();
                clicked = true;
                if (AudioRecorder.isRecording)
                {
                    AudioRecorder.sounds.Add(sound);
                }
            }

            if (clicked)
            {
                clickedKey(gt);
            }

            oState = mState;
            nState = kState;
        }

        public void Draw()
        {
            if (clicked)
            {
                Draw(Color.Gray);
            }
            else
            {
                Draw(Color.White);
            }
        }

        public void clickedKey(GameTime gt)
        {

            float time = (float)gt.ElapsedGameTime.TotalSeconds;
            clicked = true;

            clickTime -= time;
            if (clickTime < 0)
            {
                clickTime = clickMaxTime;
                clicked = false;
            }
        }
    }
}
