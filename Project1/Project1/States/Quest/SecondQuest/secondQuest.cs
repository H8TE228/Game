using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1;
using Project1.Components;
using Project1.Controls;
using Project1.States;
using Project1.States.Quest.firstQuest.Test;
using Project1.States.Quest.SecondQuest.Test2.edge;
using Project1.States.Quest.SecondQuest.Test2.Forest;
using System;
using System.Collections.Generic;


namespace Project1.States.Quest.SecondQuest
{
    public class secondQuest : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public secondQuest(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/edge");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var forestGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 200),
                Text = "Пойти через лес",
                Scale = 0.75f
            };
            forestGameButton.Click += forestGameButton_Click;

            var edgetButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 400),
                Text = "Пойти по опушке",
                Scale = 0.75f
            };
            edgetButton.Click += edgeButton_Click;

            _components = new List<Component>()
            {
                forestGameButton,
                edgetButton,
            };
        }

        private void edgeButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new textEdge(_game, _graphicsDevice, _content));
        }

        private void forestGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new forestText(_game, _graphicsDevice, _content));
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
