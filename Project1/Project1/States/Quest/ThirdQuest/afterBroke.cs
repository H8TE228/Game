﻿using System;
using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Controls;
using System.Collections.Generic;
using Project1;
using Project1.States;
using Project1.States.ToolsState;
using Microsoft.Xna.Framework.Input;
using Project1.States.Info;
using Project1.States.lvlState;
using Project1.States.ToolsState.escStates;
using Microsoft.Xna.Framework.Media;
using Project1.States.Quest.SecondQuest;
using Project1.States.Quest.ThirdQuest.Test3;
using Project1.States.Quest.ThirdQuest.Test3.maze;

namespace Project1.States.Quest.ThirdQuest
{
    internal class afterBroke : State
    {
        private List<string> _songs = new List<string> { "Dawn", "Horizon", "Sity" };
        private int _currentSongIndex = 0;
        private Song _currentSong;

        private Texture2D _backgroundTexture;
        private Texture2D _textUnder;
        private SpriteFont _font;
        private List<Component> _components;
        private string[] _texts =
        {
            "Спустившись в подвал я не ожидал увидеть лунный свет,",
            "Но судя по всему потолок церкви давным давно обвалился и теперь.",
            "Ох черт, здесь ничаниется развилка, скорее всего лабиринт какой-то..."
        };
        private int _textIndex = 0;
        private MouseState previousMouseState;
        private bool _displayText = true;
        private bool _displayFrame = true;

        public override StateInfo SaveStateInfo()
        {
            return new StateInfo(_textIndex, _backgroundTexture);
        }

        public afterBroke(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
        {
            _currentSong = _content.Load<Song>("Sound/" + _songs[_currentSongIndex]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_currentSong);

            _backgroundTexture = _content.Load<Texture2D>("Background/underChurch");
            _textUnder = _content.Load<Texture2D>("Controls/button");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            _font = buttonFont;

            _components = new List<Component>()
            {
            };

            previousMouseState = Mouse.GetState();

            if (stateInfo != null)
            {
                _textIndex = stateInfo.TextIndex;
                _backgroundTexture = stateInfo.BackgroundImage;
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            float scaleX = (float)_graphicsDevice.Viewport.Width / _backgroundTexture.Width;
            float scaleY = (float)_graphicsDevice.Viewport.Height / _backgroundTexture.Height;
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);

            float textUnderX = 0;
            float textUnderY = _graphicsDevice.Viewport.Height - _textUnder.Height;

            if (_displayFrame)
            {
                spriteBatch.Draw(_textUnder, new Rectangle((int)textUnderX, (int)textUnderY, _graphicsDevice.Viewport.Width, _textUnder.Height), Color.White);
            }

            if (_displayText)
            {
                string currentText = _texts[_textIndex];
                Vector2 textSize = _font.MeasureString(currentText);
                Vector2 textPosition = new Vector2((float)(_graphicsDevice.Viewport.Width - textSize.X) / 2, textUnderY + (_textUnder.Height - textSize.Y) / 2);

                spriteBatch.DrawString(_font, currentText, textPosition, Color.White);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (MediaPlayer.State == MediaState.Stopped)
            {
                _currentSongIndex = (_currentSongIndex + 1) % _songs.Count;
                _currentSong = _content.Load<Song>("Sound/" + _songs[_currentSongIndex]);
                MediaPlayer.Play(_currentSong);
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                _textIndex++;

                if (_textIndex >= _texts.Length)
                {
                    _displayText = false;
                    _displayFrame = false;
                }

                if (_textIndex == 2)
                {
                    _game.ChangeState(new mazeFirst(_game, _graphicsDevice, _content));
                }
            }

            previousMouseState = mouseState;


            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
