﻿using System;
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

namespace Project1.States.ToolsState
{
    public class choiseEpisode : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public choiseEpisode(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/BlurMainMenu");
            
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var textGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 100),
                Text = "Выбрать текстовый эпизод",
                Scale = 0.75f
            };
            textGameButton.Click += textGameButton_Click;


            var fightMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 300),
                Text = "Выбрать боевой эпизод",
                Scale = 0.75f
            };
            fightMenuButton.Click += fightMenuButton_Click;

            var questMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 500),
                Text = "Выбрать квест",
                Scale = 0.75f
            };
            questMenuButton.Click += questMenuButton_Click;

            var returnMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 700),
                Text = "Назад",
                Scale = 0.75f
            };
            returnMenuButton.Click += returnMenuButton_Click;

            _components = new List<Component>()
            {
                fightMenuButton,
                returnMenuButton,
                textGameButton,
                questMenuButton
            };
        }

        private void questMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new questEpisode(_game, _graphicsDevice, _content));
        }

        private void returnMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void textGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new textEpisods(_game, _graphicsDevice, _content));
        }

        private void fightMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new fightEpisodes(_game, _graphicsDevice, _content));
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

