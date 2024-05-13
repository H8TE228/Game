using System;
using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;
        private float _scale = 1f;

        #endregion

        #region Properties

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(_texture.Width * _scale), (int)(_texture.Height * _scale));
            }
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColour = Color.White;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Position, null, colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            if (!string.IsNullOrEmpty(Text))
            {

                var textSize = _font.MeasureString(Text);
                var textPosition = new Vector2(
                    Position.X + (_texture.Width * Scale - textSize.X) / 2,
                    Position.Y + (_texture.Height * Scale - textSize.Y) / 2);

                spriteBatch.DrawString(_font, Text, textPosition, PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
