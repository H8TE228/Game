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

internal class fourthState : State
{
    private Texture2D _bossTexture;
    private Vector2 _bossPosition;

    private Texture2D _backgroundTexture;
    private Texture2D _textUnder;
    private SpriteFont _font;
    private List<Component> _components;
    private string[] _texts =
    {
        "Не могу сказать, что это было слишком сложно, но и легко тоже не было.",
        "Думаю, что можно продолжить движение прямо, да и вариантов особо нет.",
        "В конце коридора стоял мужчина, спиной ко мне.",
        "Подойдя поближе он повернулся ко мне.",
        "В нем я узнал...",
        "...",
        "Своего отца...",
        "Не думал, что когда-нибудь увижу тебя, сказал я.",
        "Я тоже, ответил он.",
        "Я вообще думал, что ты давно сгинул в небытие, как твоя мать.",
        "Что ты вообще здесь делаешь? Спросил я.",
        "Трудно все это, сынок, сразу так не объяснить.",
        "А ты попытайся, может все-таки сможешь.",
        "Понимаешь,я не совсем тот человек, кем ты меня знал.",
        "По правде говоря я и не совсем человек...",
        "Так зачем ты пришел?",
        "Я все это время хотел отомстить за тебя Саркону, хотел найти тебя...",
        "Тот, кого ты искал все это время - стоит перед тобой.",
        "Нет... Проронил я.",
        "ДА, я тот, кто убил твою мать, я тот, кого столь долгое время ты искал.",
        "Я тот, кто руководит одной из самых опасных группировок на нашей родине.",
        "Опять же, как руководит.",
        "Если бы я действительно был лидером, то сейчас в окружении меня сидели бы люди,",
        "Но как ты видишь я тут один.",
        "Потому что я подчиняю нужных мне люмпенов, а потом...",
        "Отпускаю.",
        "Но ты не подумай, я не какой-то там злодей.",
        "Я вербую только всяких отбросов, которые ничего не добились \n и уже никогда ничего не добьются.",
        "Мы оба с тобой понимаем, к чему ведет этот разговор.",
        "И чтобы наш с тобой поединок был честным я не стану применять какую-либо магию на тебя.",
        "Только мечи, ну и чтобы тебе было еще чуть проще, я улучшу\n твои боевые навыки, чтобы бой был интереснее.",
        "Теперь вы имеете здоровье равное 500, урон равный 270, броню равную 50, \n восстановление здоровья равное 200.",
        "Ну что, начнем?"
    };
    private int _textIndex = 0;
    private MouseState previousMouseState;
    private bool _displayText = true;
    private bool _displayFrame = true;

    public fourthState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo = null) : base(game1, graphicsDevice, content)
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

        if (_textIndex >= 3)
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

            if (_textIndex == 32)
            {
                _game.ChangeState(new lastFight(_game, _graphicsDevice, _content));
            }
        }

        previousMouseState = mouseState;

        foreach (var component in _components)
            component.Update(gameTime);
    }
}

