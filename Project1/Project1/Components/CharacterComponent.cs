using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Components
{
    internal class CharacterComponent : Component
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; set; }

        public CharacterComponent(Texture2D texture, Vector2 position, float scale)
        {
            Texture = texture;
            Position = position;
            Scale = scale;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            // Логика обновления компонента персонажа, если необходимо
        }
    }
}
