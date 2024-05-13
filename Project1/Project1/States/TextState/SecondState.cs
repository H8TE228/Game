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
using Project1.States.Quest.SecondQuest;

internal class SecondState : State
{
    private Texture2D _backgroundTexture;
    private Texture2D _textUnder;
    private SpriteFont _font;
    private List<Component> _components;
    private string[] _texts =
    {
        "Вот я и вышел из этого треклятого города, который мне уже порядком надоел.",
        "Я понимал, что скорее всего, мое путешествие - это путь в один конец,",
        "Ведь банда головорезов была напрочь отбитая.",
        "Они никогда никого не жалели, ни детей, ни женщин, ни стариков.",
        "Мой отец часто говорил, что у этих людей есть какая-то цель, \n он все время как будто пытался их оправдать.",
        "Он говорил, что во всем вина короля...",
        "Вина короля, что они грабят, убивают и сжигают поселения нашей страны",
        "Раньше я думал, что это шайка самых обычных люмпенов.",
        "Отчасти так и есть, но эти отбросы чертовски хороши в бою.",
        "Выйти против одного из них один на один равносильно смерти.",
        "К вечеру я наконец дошел до леса, который отделял меня от церкви.",
        "Мне отец рассказывал историю, что один раз ему довелось драться с одним из главарей этой банды.",
        "Чудом стало то, что он сумел выжить.",
        "По его рассказам, он вернулся домой весь в крови, одежда и вовсе превратилась в лохмотья какой-то ткани.",
        "Он почти два месяца пролежал в лазарете.",
        "Собственно именно там он и повстречал мою маму.",
        "Она работала самой обычной медсестрой, старше отца на 6 летю",
        "Но что-то промелькнуло между ними...",
        "Он клялся защищать ее и меня...",
        "Обещал, что больше не будет переходить дорогу Саркону.",
        "Я ему верил, но почему-то есть такое ощущения, что он как-то связан с ним.",
        "Причем связь эта гораздо глубже чем простая вражда.", 
        "В один момент я думал, что он и вовсе работал с ним, но что-то не поделил,",
        "Поэтому был избит одним из Сарконовских людей.",
        "Но уже я ни в чем не уверен...",
        "Надеюсь, что при встрече с отцом, я смогу что-то у него узнать..."
    };
    private int _textIndex = 0;
    private MouseState previousMouseState;
    private bool _displayText = true;
    private bool _displayFrame = true;

    public override StateInfo SaveStateInfo()
    {
        return new StateInfo(_textIndex, _backgroundTexture);
    }

    public SecondState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
    {
        _backgroundTexture = _content.Load<Texture2D>("Background/black");
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

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            StateInfo currentStateInfo = SaveStateInfo();
            _game.ChangeState(new secondTextEsc(_game, _graphicsDevice, _content, currentStateInfo));
            return;
        }

        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            _textIndex++;

            if (_textIndex >= _texts.Length)
            {
                _displayText = false;
                _displayFrame = false;
            }

            if (_textIndex == 5)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/edge");
            }
            if (_textIndex == 26)
            {
                _game.ChangeState(new secondCondition(_game, _graphicsDevice, _content));
            }
        }

        previousMouseState = mouseState;

        foreach (var component in _components)
            component.Update(gameTime);
    }
}
