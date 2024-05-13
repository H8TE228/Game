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
    public class fightEpisodes : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public fightEpisodes(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var firstGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                Text = "Первый бой",
                Scale = 0.75f
            };
            firstGameButton.Click += firstGameButton_Click;

            var finalGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 400),
                Text = "Финальный бой",
                Scale = 0.75f
            };
            finalGameButton.Click += finalGameButton_Click;


            var secondFightButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 200),
                Text = "Второй бой",
                Scale = 0.75f
            };
            secondFightButton.Click += secondFightButton_Click;

            var returnMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 600),
                Text = "Назад",
                Scale = 0.75f
            };
            returnMenuButton.Click += returnMenuButton_Click;

            _components = new List<Component>()
            {
                secondFightButton,
                returnMenuButton,
                firstGameButton, 
                finalGameButton
            };
        }

        private void returnMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        
        private void firstGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new firstLvlState(_game, _graphicsDevice, _content));
        }

        private void finalGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new lastFight(_game, _graphicsDevice, _content));
        }

        private void secondFightButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new secondFight(_game, _graphicsDevice, _content));
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

