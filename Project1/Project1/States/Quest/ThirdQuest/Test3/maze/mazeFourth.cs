using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Project1.States.Info;
using Project1.States.Quest.ThirdQuest.Test3;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Timers;
using Project1.Components;
using Project1.Controls;
using Project1.States.Quest.ThirdQuest;
using Project1.States.ToolsState;
using Project1.States;
using Project1.States.Quest.firstQuest.Test;

namespace Project1.States.Quest.ThirdQuest.Test3.maze
{
    internal class mazeFourth : State
    {
        private int _straightTest = 0;

        private Texture2D _backgroundTexture;
        private List<Component> _components;

        public mazeFourth(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("Background/underChurch");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var righttGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 200),
                Text = "Пойти направо",
                Scale = 0.75f
            };
            righttGameButton.Click += rightGameButton_Click;

            var leftButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(625, 400),
                Text = "Пойти налево",
                Scale = 0.75f
            };
            leftButton.Click += leftButton_Click;

            _components = new List<Component>()
            {
                righttGameButton,
                leftButton,
            };
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new fourthState(_game, _graphicsDevice, _content));
        }

        private void rightGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new mazeFirst(_game, _graphicsDevice, _content));
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
