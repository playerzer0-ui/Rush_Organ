using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class Menu
    {
        private MenuOption musicSheet;
        private MenuOption door;
        private MenuOption manual;
        private Sprite title;

        private CollisionRect manualRect;
        private CollisionRect musicRect;
        private CollisionRect doorRect;

        private MouseState oState;

        public void Load()
        {
            musicSheet = new MenuOption("music_sheet", new Vector2(640, 800), "play some music!");
            door = new MenuOption("door", new Vector2(940, 900), "nah I'm out...");
            manual = new MenuOption("manual", new Vector2(340, 900), "what do I do?");
            title = new Sprite("title", new Vector2(640, 200));

            door.SetRotation(0.5f);
            manual.SetRotation(-0.5f);
            door.OffsetText(70, 0);
            manual.OffsetText(-100, 0);
            door.SetTextRotation(0.5f);
            manual.SetTextRotation(-0.5f);

            manualRect = new CollisionRect(204, 700, 426, 450);
            musicRect = new CollisionRect(639, 700, 443, 450);
            doorRect = new CollisionRect(1075, 700, 426, 450);
        }

        public void Update()
        {
            MouseState mState = Mouse.GetState();

            if(manualRect.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
            {
                Game1.GameState = 2;
            }
            if (musicRect.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
            {
                Game1.GameState = 1;
            }
            if (doorRect.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && oState.LeftButton == ButtonState.Released)
            {
                Game1.GameState = -1;
            }

            door.Update(doorRect);
            musicSheet.Update(musicRect);
            manual.Update(manualRect);
            oState = mState;
        }

        public void Draw()
        {
            door.Draw();
            manual.Draw();
            musicSheet.Draw();
            title.Draw(Color.White);
            //manualRect.DrawRect(new Color(255, 0, 0, 128));
            //musicRect.DrawRect(new Color(255, 0, 0, 128));
            //doorRect.DrawRect(new Color(255, 0, 0, 128));
        }
    }
}
