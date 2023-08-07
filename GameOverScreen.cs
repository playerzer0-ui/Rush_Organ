using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class GameOverScreen
    {
        //collisions
        private CollisionRect bgGray;
        private CollisionCircle playCircle;
        private CollisionCircle stopCircle;
        private CollisionRect submitRect;

        //sprite and text
        private Sprite playSprite;
        private Sprite stopSprite;
        private Sprite submitSprite;
        TextBox description;
        TextBox play;
        TextBox stop;

        private SpriteFont gameFont;
        private SpriteFont small;

        public CollisionCircle PlayCircle { get => playCircle; set => playCircle = value; }
        public CollisionCircle StopCircle { get => stopCircle; set => stopCircle = value; }
        public CollisionRect SubmitRect { get => submitRect; set => submitRect = value; }

        public void Load()
        {
            bgGray = new CollisionRect(640, 360, 2000, 2000);
            playSprite = new Sprite("play_button", new Vector2(350, 300));
            stopSprite = new Sprite("stop_button", new Vector2(900, 300));
            submitSprite = new Sprite("submit button", new Vector2(640, 600));

            gameFont = Globals.Content.Load<SpriteFont>("gameFontLarge");
            small = Globals.Content.Load<SpriteFont>("gameFont");

            description = new TextBox(gameFont);
            description.Text = "It's finished, submit now!";
            play = new TextBox(small);
            play.Text = "click to play";
            stop = new TextBox(small);
            stop.Text = "click to stop";

            submitRect = new CollisionRect(640, 600, 552, 104);
            playCircle = new CollisionCircle(350, 300, 42);
            stopCircle = new CollisionCircle(900, 300, 42);
        }

        

        public void Draw()
        {
            bgGray.DrawRect(Color.Gray);
            playSprite.Draw(Color.White);
            stopSprite.Draw(Color.White);
            description.Draw(640, 200, Color.White);
            submitSprite.Draw(Color.White);
            play.Draw(350, 380, Color.White);
            stop.Draw(900, 380, Color.White);
        }
    }
}
