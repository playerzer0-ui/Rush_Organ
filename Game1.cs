using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NodeTesting.models;

namespace Rush_Organ
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int GameState = 0;

        Organ organ = new Organ();
        Menu menu = new Menu();
        HowTo howTo = new HowTo();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // GLOBALS
            Globals.Content = Content;
            Globals.spriteBatch = _spriteBatch;
            Globals.graphics = _graphics;

            //loading
            menu.Load();
            organ.Load();
            howTo.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (GameState)
            {
                case 0:
                    menu.Update();
                    break;
                case 1:
                    organ.Update(gameTime);
                    break;
                case 2:
                    howTo.Update();
                    break;

                default:
                    Exit();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(117, 117, 117));

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (GameState)
            {
                case 0:
                    menu.Draw();
                    break;
                case 1:
                    organ.Draw();
                    break;
                case 2:
                    howTo.Draw();
                    break;

                default:
                    Exit();
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}