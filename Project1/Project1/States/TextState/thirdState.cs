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

internal class thirdState : State
{
    private Texture2D _backgroundTexture;
    private Texture2D _textUnder;
    private SpriteFont _font;
    private List<Component> _components;
    private string[] _texts =
    {
        "Мда, не хотел я ввязываться в дрки, но эти ребята сами напросились.",
        "Итак, я уже почти у своей долгожданной цели.",
        "Думаю, что можно здесь переночевать и ужа завтра начать движение.",
        "Перед сном я также нашел некоторые съестные припасы.",
        "Я лег спать в одной из палаток, ночи сейчас достаточно теплые, \n поэтому спать я лег в дали от костра.",
        "Уровень персонажа повышел, суммарное здоровье увеличено на 100,\n суммарный урон увеличен на 23.",
        "Суммарное восстановление увеличено до 60",
        "Утром я проснулся с мыслью, что надо выдвигаться ночью, безопаснее будет.",
        "К ночи я уже подошел к церкви, стояла мертвенная тишина.",
        "Меня это немного напрягало, но делать особо делать нечего, придется двигаться дальше...",
        "Вдруг со стороны кладбища послышался шорох.",
        "Я пошел проверить, но это было явно не лучшим решением..."
    };
    private int _textIndex = 0;
    private MouseState previousMouseState;
    private bool _displayText = true;
    private bool _displayFrame = true;

    public override StateInfo SaveStateInfo()
    {
        return new StateInfo(_textIndex, _backgroundTexture);
    }

    public thirdState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
    {
        _backgroundTexture = _content.Load<Texture2D>("Background/camp");
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

        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            _textIndex++;

            if (_textIndex >= _texts.Length)
            {
                _displayText = false;
                _displayFrame = false;
            }

            if (_textIndex == 7)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/nightChurch");
            }

            if (_textIndex == 12)
            {
                _game.ChangeState(new secondFight(_game, _graphicsDevice, _content));
            }
        }

        previousMouseState = mouseState;

        foreach (var component in _components)
            component.Update(gameTime);
    }
}
