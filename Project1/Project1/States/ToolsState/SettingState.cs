using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Project1.Controls;
using System;
using System.Collections.Generic;
using Project1;
using Project1.States;

namespace Project1.States.ToolsState
{
    public class SettingState : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        private bool _isSoundOn = true;
        private bool _isEffectOn = true;

        public SettingState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var backGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 800),
                Text = "Назад",
                Scale = 0.75f
            };
            backGameButton.Click += BackGameButton_Click;

            var soundButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                Text = "Звук: Вкл \n(Также можно на клавишу M)",
                Scale = 0.75f
            };
            soundButton.Click += SoundButton_Click;

            var effectButton = new Button(buttonTexture, buttonFont) // Создаем кнопку для управления звуком
            {
                Position = new Vector2(0, 200),
                Text = "Эффекты: Вкл \n(Также можно на клавишу N)",
                Scale = 0.75f
            };
            effectButton.Click += effectButton_Click;

            _components = new List<Component>()
            {
                backGameButton,
                soundButton,
                effectButton
            };

        }

        private void effectButton_Click(object sender, EventArgs e)
        {
            _isEffectOn = !_isEffectOn; // Переключаем состояние звуковых эффектов

            if (_isEffectOn)
            {
                ((Button)sender).Text = "Эффекты: Вкл \n(Также можно на клавишу N)";
            }
            else
            {
                ((Button)sender).Text = "Эффекты: Выкл \n(Также можно на клавишу N)";
            }

            // Вызываем статический метод из класса AudioManager для управления звуками в игре
            AudioManager.IsSoundEnabled = _isEffectOn;
        }

        private void BackGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void SoundButton_Click(object sender, EventArgs e)
        {
            _isSoundOn = !_isSoundOn;

            if (_isSoundOn)
            {
                ((Button)sender).Text = "Звук: Вкл \n(Также можно на клавишу M)";
            }
            else
            {
                ((Button)sender).Text = "Звук: Выкл \n(Также можно на клавишу M)";
            }

            MediaPlayer.IsMuted = !_isSoundOn;
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

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
