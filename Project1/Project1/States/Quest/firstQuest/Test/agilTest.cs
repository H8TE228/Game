using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1;
using Project1.Controls;
using Project1.States;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Project1.States.Quest.firstQuest.Test
{
    internal class agilTest : State
    {
        private SoundEffect _up;

        private Button _agilityCheckButton;
        private string _randomText = "";
        private Texture2D _backgroundTexture;
        private Rectangle _backgroundRectangle;

        public agilTest(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _up = _content.Load<SoundEffect>("Sound/up");
            _backgroundRectangle = new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            _backgroundTexture = content.Load<Texture2D>("Background/Blurgates");
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            _agilityCheckButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2((_graphicsDevice.Viewport.Width - buttonTexture.Width * 0.5f) / 2, (_graphicsDevice.Viewport.Height - buttonTexture.Height * 0.5f) / 2), // Центрируем кнопку
                Text = "Проверка на ловкость (Минимум 10)",
                Scale = 0.7f // Увеличиваем масштаб кнопки
            };
            _agilityCheckButton.Click += AgilityCheckButton_Click;
            _agilityCheckButton.Position = new Vector2(625, 200);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, _backgroundRectangle, Color.White);
            // Отрисовываем кнопку
            _agilityCheckButton.Draw(gameTime, spriteBatch);

            // Отрисовываем текст
            var font = _content.Load<SpriteFont>("Fonts/Font");
            var textPosition = new Vector2((_graphicsDevice.Viewport.Width - font.MeasureString(_randomText).X) / 2, 300);
            spriteBatch.DrawString(font, _randomText, textPosition, Color.Green); // Изменяем цвет текста на зеленый

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _agilityCheckButton.Update(gameTime);
        }

        private async void AgilityCheckButton_Click(object sender, EventArgs e)
        {
            // Задержка на 1 секунду
            await Task.Delay(1000);

            // Генерируем случайное число от 1 до 20
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 21);

            if (randomNumber < 10)
            {
               _randomText = $"Надо попробовать еще раз: {randomNumber}";
                await Task.Delay(1500);
                _game.ChangeState(new firstQuest(_game, _graphicsDevice, _content)); 
            }
            else
            {
                AudioManager.PlaySoundEffect(_up);
                _randomText = $"Отлично, я на свободе: {randomNumber}";
                await Task.Delay(1500);
                _game.ChangeState(new SecondState(_game, _graphicsDevice, _content));
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
