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

internal class StartState : State
{
    private Texture2D _backgroundTexture;
    private Texture2D _textUnder;
    private SpriteFont _font;
    private List<Component> _components;
    private string[] _texts =
    {
        "Я уже и не помню, как давно это было, тогда был еще совсем мал.",
        "То время, когда моих родителей похитил Cаркон и банда его подхалимов...",
        "Они варварски напали на нашу деревню, разграбили ее... Сожгли ее...",
        "Ввалились в наш горящий дом.",
        "Они начали скидывать все с полок, со стола. Перевернули весь дом вверх дном.",
        "Я не знаю что они искали, но исходя из того, как яростно кричали это что-то было очень ценным.",
        "Потерпев неудачу в своих поисках, они схватили моего отца.",
        "Моя мать побежала на них, попыталась сделать что-то, \n но ее придавило горящей балкой, которая упала сверху.",
        "Они ушли, я побежал к своей матери, но она уже не дышала...",
        "Я выбежал из горящего дома, но налетчиков уже и след простыл.",
        "В попытках найти хоть одного живого человека, я потерпел неудачу.",
        "Я пробродил до утра, пожар в деревне уже угас.",
        "В своих поисках я нашел некоторые съестные припасы.",
        "Тогда я поклялся отомстить, а самое главное попытаться найти своего отца.",
        "Я отправился в Халитьед - столицу нашей необъятной родины.",
        "Именно здесь находилось предполагаемое логово Саркона.",
        "Прошло 20 лет, как я поселился в городе.",
        "Все то время я прожил в таверне беслптано, помогая хозяину заведения.",
        "Благо он оказался понимающим и смог дать мне кров над головой.",
        "Я пытался найти место, где бы прятались эти нехорошие люди.",
        "По некоторым наводкам стражников, они расположились в заброженной церкви, которая была за городом.", 
        "Вот я наконец готов пойти на встречу к своим путешествиям.",
        "Но проблемы появились уже при выходе из города.",
        "В последнее время ужесточили меры прохождения через ворота, в связи с этим \n oхранники не хотели пускать меня. Поэтому пришлось пробиваться силой...",
        ""
    };
    private int _textIndex = 0;
    private MouseState previousMouseState;
    private bool _displayText = true;
    private bool _displayFrame = true;

    public override StateInfo SaveStateInfo()
    {
        return new StateInfo(_textIndex, _backgroundTexture);
    }

    public StartState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
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
            _game.ChangeState(new escState(_game, _graphicsDevice, _content, currentStateInfo));
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

            if (_textIndex == 2)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/VillageFire");
            }
            if (_textIndex == 3)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/black");
            }
            if (_textIndex == 16)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/village");
            }
            if (_textIndex == 17)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/tavern");
            }
            if (_textIndex == 18)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/home");
            }
            if (_textIndex == 20)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/church");
            }
            if (_textIndex == 22)
            {
                _backgroundTexture = _content.Load<Texture2D>("Background/gates");
            }
            if (_textIndex == 24)
            {
                _game.ChangeState(new firstLvlState(_game, _graphicsDevice, _content));
            }
        }

        previousMouseState = mouseState;

        foreach (var component in _components)
            component.Update(gameTime);
    }
}
