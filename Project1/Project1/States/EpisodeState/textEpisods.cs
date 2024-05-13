using System;
using System.Collections.Generic;
using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Controls;
using Microsoft.Xna.Framework.Audio;
using Project1.States.lvlState;
using Project1;
using Project1.States;
using Project1.States.EpisodeState;
using Project1.States.ToolsState;

namespace Project1.States.EpisodeState
{
    public class textEpisods : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public textEpisods(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var firstGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                Text = "Первый текстовый фрагмент",
                Scale = 0.75f
            };
            firstGameButton.Click += firstGameButton_Click;

            var secondGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 200),
                Text = "Второй текстовый фрагмент",
                Scale = 0.75f
            };
            secondGameButton.Click += secondGameButton_Click;


            var thurdMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 400),
                Text = "Третий текстовый эпизод",
                Scale = 0.75f
            };
            thurdMenuButton.Click += thurdMenuButton_Click;

            var forthMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 600),
                Text = "Четвертый текстовый эпизод",
                Scale = 0.75f
            };
            forthMenuButton.Click += forthMenuButton_Click;

            var returnMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 800),
                Text = "Назад",
                Scale = 0.75f
            };
            returnMenuButton.Click += returnMenuButton_Click;

            _components = new List<Component>()
            {
                thurdMenuButton,
                returnMenuButton,
                firstGameButton,
                secondGameButton,
                forthMenuButton
            };
        }

        private void forthMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new fourthState(_game, _graphicsDevice, _content));
        }

        private void secondGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SecondState(_game, _graphicsDevice, _content));
        }

        private void returnMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void firstGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new StartState(_game, _graphicsDevice, _content));
        }

        private void thurdMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new thirdState(_game, _graphicsDevice, _content));
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

