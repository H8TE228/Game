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
using Project1.States.Quest.firstQuest.Test;

namespace Project1.States.Quest.fourthQuest
{
    public class lastQuest : State
    {
        private int _straightTest = 0;

        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public lastQuest(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/underChurch");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var straightGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 200),
                Text = "Противостоять ему",
                Scale = 0.75f
            };
            straightGameButton.Click += straightGameButton_Click;

            var agilButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 400),
                Text = "Сдаться",
                Scale = 0.75f
            };
            agilButton.Click += agilButton_Click;

            _components = new List<Component>()
            {
                straightGameButton,
                agilButton,
            };
        }

        private void agilButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new firstFinal(_game, _graphicsDevice, _content));
        }

        private void straightGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new resistDialoge(_game, _graphicsDevice, _content));
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
