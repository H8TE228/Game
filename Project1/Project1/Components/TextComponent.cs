using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Components
{
    public class TextComponent : Component
    {
        private SpriteFont _font;
        private string _text;
        private Vector2 _position;
        private Color _color; // Добавляем параметр для цвета текста

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public TextComponent(SpriteFont font, string text, Vector2 position, Color color) // Добавляем цвет в конструктор
        {
            _font = font;
            _text = text;
            _position = position;
            _color = color; // Устанавливаем цвет
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, _color); // Используем заданный цвет
        }

        public override void Update(GameTime gameTime)
        {
            // Здесь можно добавить логику обновления, если это необходимо
        }
    }
}
