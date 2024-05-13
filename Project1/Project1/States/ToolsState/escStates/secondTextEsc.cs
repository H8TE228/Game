using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.States.Info;
using Project1;
using Project1.States;
using Project1.States.Quest.firstQuest;

namespace Project1.States.ToolsState.escStates
{
    public class secondTextEsc : State
    {
        private StateInfo _stateInfo;

        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public secondTextEsc(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content, StateInfo stateInfo) : base(game1, graphicsDevice, content)
        {
            _stateInfo = stateInfo;

            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 400),
                Text = "Вернутся в игру",
                Scale = 0.75f
            };
            mainMenuButton.Click += mainMenuButton_Click;

            var saveMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 200),
                Text = "Сохранить игру",
                Scale = 0.75f
            };
            saveMenuButton.Click += saveMenuButton_Click;

            var returnMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 600),
                Text = "В глваное меню",
                Scale = 0.75f
            };
            returnMenuButton.Click += returnMenuButton_Click;

            _components = new List<Component>()
            {
                mainMenuButton,
                saveMenuButton,
                returnMenuButton
            };

        }

        private void returnMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void saveMenuButton_Click(object sender, EventArgs e)
        {
        }

        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SecondState(_game, _graphicsDevice, _content, _stateInfo));
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
