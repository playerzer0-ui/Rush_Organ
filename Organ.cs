using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class Organ
    {
        //white keys
        private KeyPiece doSprite;
        private KeyPiece reSprite;
        private KeyPiece miSprite;
        private KeyPiece faSprite;
        private KeyPiece solSprite;
        private KeyPiece laSprite;
        private KeyPiece siSprite;
        private KeyPiece do1Sprite;
        private KeyPiece re1Sprite;
        private KeyPiece mi1Sprite;

        //black keys
        private Sprite black1;
        private Sprite black2;
        private Sprite black3;
        private Sprite black4;
        private Sprite black5;
        private Sprite black6;
        private Sprite black7;

        //sounds
        private SoundEffect doSound;
        private SoundEffect reSound;
        private SoundEffect miSound;
        private SoundEffect faSound;
        private SoundEffect solSound;
        private SoundEffect laSound;
        private SoundEffect siSound;
        private SoundEffect do1Sound;
        private SoundEffect re1Sound;
        private SoundEffect mi1Sound;

        //textbox
        TextBox dueTime;
        TextBox beginText;
        TextBox numPad;
        private KeyboardState nState;
        private MouseState oState;

        //font
        private SpriteFont large;
        private SpriteFont small;

        //timer
        private int deadLine = 10;
        private float tick = 1f;
        private float maxTick = 1f;
        private int beginDeadLine = 3;
        private float beginTime = 1f;
        private float maxBeginTime = 1f;
        private float endTime = 2f;
        private float maxEndTime = 2f;
        private bool begin = false;
        private bool prepare = true;
        private bool gameOver = false;

        //rectangles
        private CollisionRect bgGray;

        AudioRecorder recorder;
        GameOverScreen gameOverScreen = new GameOverScreen();
        public Organ() { }
        public void Load()
        {
            recorder = new AudioRecorder();
            gameOverScreen.Load();
            //font
            large = Globals.Content.Load<SpriteFont>("gameFontLarge");
            small = Globals.Content.Load<SpriteFont>("gameFont");

            //text box
            dueTime = new TextBox(large);
            dueTime.Text = $"{deadLine}";
            beginText = new TextBox(large);
            beginText.Text = $"{beginDeadLine}";
            numPad = new TextBox(large);
            numPad.Text = "1   2   3   4   5   6   7   8   9   0";

            //load black, jump by 95
            black1 = new Sprite("keys/black_key", new Vector2(252, 264));
            black2 = new Sprite("keys/black_key", new Vector2(347, 264));
            black3 = new Sprite("keys/black_key", new Vector2(537, 264));
            black4 = new Sprite("keys/black_key", new Vector2(632, 264));
            black5 = new Sprite("keys/black_key", new Vector2(727, 264));
            black6 = new Sprite("keys/black_key", new Vector2(917, 264));
            black7 = new Sprite("keys/black_key", new Vector2(1012, 264));

            //sounds
            doSound = Globals.Content.Load<SoundEffect>("sounds/do");
            reSound = Globals.Content.Load<SoundEffect>("sounds/re");
            miSound = Globals.Content.Load<SoundEffect>("sounds/mi");
            faSound = Globals.Content.Load<SoundEffect>("sounds/fa");
            solSound = Globals.Content.Load<SoundEffect>("sounds/sol");
            laSound = Globals.Content.Load<SoundEffect>("sounds/la");
            siSound = Globals.Content.Load<SoundEffect>("sounds/si");
            do1Sound = Globals.Content.Load<SoundEffect>("sounds/do1");
            re1Sound = Globals.Content.Load<SoundEffect>("sounds/re1");
            mi1Sound = Globals.Content.Load<SoundEffect>("sounds/mi1");

            //rectangles
            bgGray = new CollisionRect(640, 360, 2000, 2000);

            //load white
            doSprite = new KeyPiece("keys/left", new Vector2(205, 360), new CollisionRect(205, 360, 90, 450), doSound);
            reSprite = new KeyPiece("keys/middle", new Vector2(300, 360), new CollisionRect(300, 360, 90, 450), reSound);
            miSprite = new KeyPiece("keys/right", new Vector2(395, 360), new CollisionRect(395, 360, 90, 450), miSound);
            faSprite = new KeyPiece("keys/left", new Vector2(490, 360), new CollisionRect(490, 360, 90, 450), faSound);
            solSprite = new KeyPiece("keys/middle", new Vector2(585, 360), new CollisionRect(585, 360, 90, 450), solSound);
            laSprite = new KeyPiece("keys/middle", new Vector2(680, 360), new CollisionRect(680, 360, 90, 450), laSound);
            siSprite = new KeyPiece("keys/right", new Vector2(775, 360), new CollisionRect(775, 360, 90, 450), siSound);
            do1Sprite = new KeyPiece("keys/left", new Vector2(870, 360), new CollisionRect(870, 360, 90, 450), do1Sound);
            re1Sprite = new KeyPiece("keys/middle", new Vector2(965, 360), new CollisionRect(965, 360, 90, 450), re1Sound);
            mi1Sprite = new KeyPiece("keys/right", new Vector2(1060, 360), new CollisionRect(1060, 360, 90, 450), mi1Sound);
        }

        public void Update(GameTime gt)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            if (prepare)
            {
                BeginCountDown(gt);
                beginText.Text = $"{beginDeadLine}";
                if(beginDeadLine <= 0)
                {
                    prepare = false;
                    begin = true;
                }
            }
            if (begin)
            {
                dueTime.Text = $"{deadLine}";
                AudioRecorder.isRecording = true;

                doSprite.Update(gt, Keys.D1);
                reSprite.Update(gt, Keys.D2);
                miSprite.Update(gt, Keys.D3);
                faSprite.Update(gt, Keys.D4);
                solSprite.Update(gt, Keys.D5);
                laSprite.Update(gt, Keys.D6);
                siSprite.Update(gt, Keys.D7);
                do1Sprite.Update(gt, Keys.D8);
                re1Sprite.Update(gt, Keys.D9);
                mi1Sprite.Update(gt, Keys.D0);

                CountDown(gt);

                if(deadLine < 0)
                {
                    deadLine = 0;
                    begin = false;
                }
            }
            if(!begin && !prepare)
            {
                recorder.StopRecording();
                EndCountDown(gt);
            }
            if (gameOver)
            {
                if (gameOverScreen.SubmitRect.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
                {
                    Game1.GameState = 0;
                    prepare = true;
                    begin = false;
                    gameOver = false;
                    deadLine = 10;
                    dueTime.Text = $"{deadLine}";
                    beginDeadLine = 3;
                    recorder.ClearRecording();
                }
                if(gameOverScreen.PlayCircle.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
                {
                    recorder.isPlaying = true;
                }
                if (gameOverScreen.StopCircle.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
                {

                    recorder.StopPlayBack();
                }
            }

            if (AudioRecorder.isRecording)
            {
                recorder.StartRecording(gt);
            }
            if (recorder.isPlaying)
            {
                recorder.PlayRecording(gt);
            }


            nState = kState;
            oState = mState;
        }

        public void Draw()
        {
            //white draw
            doSprite.Draw();
            reSprite.Draw();
            miSprite.Draw();
            faSprite.Draw();
            solSprite.Draw();
            laSprite.Draw();
            siSprite.Draw();
            do1Sprite.Draw();
            re1Sprite.Draw();
            mi1Sprite.Draw();


            //draw black
            black1.Draw(Color.White);
            black2.Draw(Color.White);
            black3.Draw(Color.White);
            black4.Draw(Color.White);
            black5.Draw(Color.White);
            black6.Draw(Color.White);
            black7.Draw(Color.White);

            //draw text
            dueTime.Draw(640, 80, Color.White);
            numPad.Draw(640, 480, Color.Black);
            if (prepare)
            {
                bgGray.DrawRect(new Color(117, 117, 117, 190));
                beginText.Draw(640, 360, Color.White);
            }
            if(!begin && !prepare)
            {
                beginText.Text = "FINISHED";
                bgGray.DrawRect(new Color(117, 117, 117, 190));
                beginText.Draw(640, 360, Color.White);
            }
            if (gameOver)
            {
                gameOverScreen.Draw();
            }

            //Globals.spriteBatch.DrawString(small, "list" + String.Join(",", AudioRecorder.sounds), new Vector2(10, 10), Color.White);
            //Globals.spriteBatch.DrawString(small, "i: " + recorder.index, new Vector2(10, 30), Color.White);
            //Globals.spriteBatch.DrawString(small, "c: " + recorder.count, new Vector2(10, 50), Color.White);
            //Globals.spriteBatch.DrawString(small, "p: " + recorder.isPlaying, new Vector2(10, 70), Color.White);
            //Globals.spriteBatch.DrawString(small, "r: " + AudioRecorder.isRecording, new Vector2(10, 90), Color.White);
        }

        public void CountDown(GameTime gt)
        {
            float timer = (float)gt.ElapsedGameTime.TotalSeconds;
            tick -= timer;
            if(tick < 0)
            {
                deadLine -= 1;
                tick = maxTick;
            }
        }

        public void BeginCountDown(GameTime gt)
        {
            float timer = (float)gt.ElapsedGameTime.TotalSeconds;

            beginTime -= timer;
            if(beginTime < 0)
            {
                beginDeadLine -= 1;
                beginTime = maxBeginTime;
            }
        }

        public void EndCountDown(GameTime gt)
        {
            float timer = (float)gt.ElapsedGameTime.TotalSeconds;

            endTime -= timer;
            if(endTime < 0)
            {
                endTime = maxEndTime;
                gameOver = true;
            }
        }
    }
}
