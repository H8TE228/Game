using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Project1.States;

namespace Project1
{
    public class Game1 : Game
    {
        KeyboardState previousKeyboardState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private State _currentState;
        private State _nextState;
        bool isFullScreen = true;

        private Song[] _songs;
        private int _currentSongIndex = 0;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = isFullScreen;
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            Song song1 = Content.Load<Song>("Sound/Sity");
            Song song2 = Content.Load<Song>("Sound/Horizon");
            Song song3 = Content.Load<Song>("Sound/Dawn");

            MediaPlayer.Play(song1);
            MediaPlayer.IsRepeating = true;

            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            _songs = new Song[] { song1, song2, song3 };
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                _currentSongIndex++;
                if (_currentSongIndex >= _songs.Length)
                {
                    _currentSongIndex = 0;
                }

                MediaPlayer.Play(_songs[_currentSongIndex]);
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                MouseState mouseState = Mouse.GetState();
                Point mousePosition = new Point(mouseState.X, mouseState.Y);
                Rectangle gameWindowBounds = Window.ClientBounds;

                if (gameWindowBounds.Contains(mousePosition))
                {
                    KeyboardState keyboardState = Keyboard.GetState();

                    if (keyboardState.IsKeyDown(Keys.N) && previousKeyboardState.IsKeyUp(Keys.N))
                    {
                        AudioManager.IsSoundEnabled = !AudioManager.IsSoundEnabled;
                    }

                    if (keyboardState.IsKeyDown(Keys.M) && previousKeyboardState.IsKeyUp(Keys.M))
                    {
                        MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
                    }

                    previousKeyboardState = keyboardState;

                    if (_nextState != null)
                    {
                        _currentState = _nextState;
                        _nextState = null;
                    }

                    _currentState.Update(gameTime);
                    _currentState.PostUpdate(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
