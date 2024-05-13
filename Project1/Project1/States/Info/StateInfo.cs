using Microsoft.Xna.Framework.Graphics;

namespace Project1.States.Info
{
    public class StateInfo
    {
        public int TextIndex { get; }
        public Texture2D BackgroundImage { get; }

        public StateInfo(int textIndex, Texture2D backgroundImage)
        {
            TextIndex = textIndex;
            BackgroundImage = backgroundImage;
        }
    }
}

