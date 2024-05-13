using Project1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Controls;
using System;
using System.Collections.Generic;
using Project1;
using Project1.States;
using Project1.States.ToolsState;

namespace Project1.States.EpisodeState
{
    public class GameState : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public GameState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                Text = "Создать нового героя",
                Scale = 0.75f
            };
            newGameButton.Click += newGameButton_Click;

            var choiseButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 200),
                Text = "Выбрать эпизод",
                Scale = 0.75f
            };
            choiseButton.Click += choiseButton_Click;

            var backGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 600),
                Text = "Назад",
                Scale = 0.75f
            };
            backGameButton.Click += BackGameButton_Click;

            _components = new List<Component>()
            {
                backGameButton,
                newGameButton,
                choiseButton,
            };
        }

        private void choiseButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new choiseEpisode(_game, _graphicsDevice, _content));
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new StartState(_game, _graphicsDevice, _content));
        }

        private void BackGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
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
