using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Controls;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Project1.States.ToolsState;
using Project1.States.EpisodeState;
using Microsoft.Xna.Framework.Media;

namespace Project1.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture;

        private List<string> _songs = new List<string> { "Dawn", "Horizon", "Sity" };
        private int _currentSongIndex = 0;
        private Song _currentSong;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _currentSong = _content.Load<Song>("Sound/" + _songs[_currentSongIndex]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_currentSong);

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                Text = "Новая Игра",
                Scale = 0.75f
            };
            newGameButton.Click += NewGameButton_Click;

            var choiseButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 200),
                Text = "Выбрать эпизод",
                Scale = 0.75f
            };
            choiseButton.Click += choiseButton_Click;

            var settingGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 400),
                Text = "Настройки",
                Scale = 0.75f
            };
            settingGameButton.Click += settingGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 600),
                Text = "Выход",
                Scale = 0.75f
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                choiseButton,
                quitGameButton,
                settingGameButton
            };

            _backgroundTexture = _content.Load<Texture2D>("Background/MainMenu");
        }

        private void choiseButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new choiseEpisode(_game, _graphicsDevice, _content));
        }

        private void settingGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SettingState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            float scaleX = (float)_graphicsDevice.Viewport.Width / _backgroundTexture.Width;
            float scaleY = (float)_graphicsDevice.Viewport.Height / _backgroundTexture.Height;

            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));

        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                _currentSongIndex = (_currentSongIndex + 1) % _songs.Count;
                _currentSong = _content.Load<Song>("Sound/" + _songs[_currentSongIndex]);
                MediaPlayer.Play(_currentSong);
            }

            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
