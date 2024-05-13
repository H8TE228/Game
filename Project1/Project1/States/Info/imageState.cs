using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Processor.States.Info
{
    public class ImageState
    {
        public Texture2D ImageIndex { get; }
        public Texture2D BackgroundImage { get; }
        public ImageState(Texture2D imageIndex)
        {
            ImageIndex = imageIndex;
        }
    }
}
