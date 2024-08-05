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
        

        public Sprite() : base()
        {
            this.color = Color.Red;
            this.type = SpriteType.Quad;
            this.image = null;
        }

    }
}
