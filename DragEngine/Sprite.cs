using System;
using System.Drawing;

namespace DragEngine
{
    public enum SpriteType
    {
        Quad, Image, Circle
    };

    public class Sprite : prop
    {
        public SpriteType type;
        public Image image;

        public Color color = Color.Black;
        

        public Sprite(Color color, SpriteType type = SpriteType.Quad, Image image = null) : base()
        {
            this.color = color;
            this.type = type;
            this.image = image;
        }

    }
}
