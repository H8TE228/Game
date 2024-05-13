using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.States;
using Project1;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Project1.Components;
using Project1.States.Info;
using Project1.States.lvlState;
using Project1.States.ToolsState;
using Project1.States.ToolsState.escStates;

internal class firstFinal : State
{
    private Texture2D _bossTexture;
    private Vector2 _bossPosition;

    private Texture2D _backgroundTexture;
    private Texture2D _textUnder;
    private SpriteFont _font;
    private List<Component> _components;
    private string[] _texts =
    {
        "Правильный выбор, сынок, теперь весь мир у наших ног.",
        "На этом история заканчивается.",
        "Отец показал мне потенциал своей силы, обучил меня своему ремеслу.",
        "Мы захватили сначала власть в нашей стране, затем соседние.",
        "Ну а после весь мир.",
        "...",
        "Правильный ли выбор я сделал???"
    };
    private int _textIndex = 0;
    private MouseState previousMouseState;
    private bool _displayText = true;
    private bool _displayFrame = true;

    public firstFinal(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
    {
        _bossTexture = _content.Load<Texture2D>("ggEnemy/father");
        _bossPosition = new Vector2((_graphicsDevice.Viewport.Width - _bossTexture.Width) / 2, (_graphicsDevice.Viewport.Height - _bossTexture.Height) / 2);

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

        if (_textIndex == 0)
        {
            spriteBatch.Draw(_bossTexture, _bossPosition, Color.White);
        }

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

        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            _textIndex++;

            if (_textIndex >= _texts.Length)
            {
                _displayText = false;
                _displayFrame = false;
            }

            if (_textIndex == 1)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/black");
            }

            if (_textIndex == 7)
            {
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }
        }

        previousMouseState = mouseState;

        foreach (var component in _components)
            component.Update(gameTime);
    }
}

