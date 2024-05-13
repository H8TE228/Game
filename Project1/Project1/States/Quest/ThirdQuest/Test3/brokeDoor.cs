using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Timers;
using Project1.Components;
using Project1.Controls;
using Project1.States.ToolsState;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.States;

namespace Project1.States.Quest.ThirdQuest.Test3
{
    internal class brokeDoor : State
    {
        private CountdownTimer _timer;
        private TimeSpan _elapsedTime = TimeSpan.Zero;
        private SpriteFont _timerFont;
        private Vector2 _timerPosition = new Vector2(10, 10);

        private SoundEffect _brokeDoor;

        private int _straightTest = 0;

        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public brokeDoor(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {

            _brokeDoor = _content.Load<SoundEffect>("Sound/brokeDoor");

            _backgroundTexture = _content.Load<Texture2D>("Background/doorChurch");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var straightGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 200),
                Text = "Сломать",
                Scale = 0.75f
            };
            straightGameButton.Click += straightGameButton_Click;

            _timerFont = _content.Load<SpriteFont>("Fonts/Font");

            _components = new List<Component>()
            {
                straightGameButton,
                new TextComponent(_timerFont, "", _timerPosition, Color.Red)
            };

            _timer = new CountdownTimer(TimeSpan.FromSeconds(10));
            _timer.Start();
        }

        private void straightGameButton_Click(object sender, EventArgs e)
        {
            _straightTest += 1;
            AudioManager.PlaySoundEffect(_brokeDoor);
            if (_straightTest == 70) { _game.ChangeState(new afterBroke(_game, _graphicsDevice, _content)); }
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
            _timer.Update(gameTime);
            _elapsedTime += gameTime.ElapsedGameTime;

            var remainingTime = TimeSpan.FromSeconds(20) - _elapsedTime;
            var timerText = $"Осталось времени до прихода новой волны зомби: {remainingTime.ToString("ss\\.fff")}";
            (_components[1] as TextComponent).Text = timerText;

            if (_elapsedTime >= TimeSpan.FromSeconds(20))
            {
                _game.ChangeState(new DieState(_game, _graphicsDevice, _content));
            }

            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
