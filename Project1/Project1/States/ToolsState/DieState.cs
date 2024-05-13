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

namespace Project1.States.ToolsState
{
    public class DieState : State
    {
        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public DieState(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/Die");
            SoundEffect _knifeOutSoundEffect = _content.Load<SoundEffect>("Sound/die");
            AudioManager.PlaySoundEffect(_knifeOutSoundEffect);

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var MainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(600, 800),
                Text = "Главное меню",
                Scale = 0.75f
            };
            MainMenuButton.Click += MainMenuButton_Click;

            _components = new List<Component>()
            {
                MainMenuButton,

            };
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
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

