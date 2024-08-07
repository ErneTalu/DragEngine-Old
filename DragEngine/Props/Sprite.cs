using System;
using System.Drawing;

namespace DragEngine
{
    public enum SpriteType
    {
        Quad, Circle, Image
    };

    public class Sprite : Prop
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

        public void FlipImageY() => image.RotateFlip(RotateFlipType.Rotate180FlipY);
        public void FlipImageX() => image.RotateFlip(RotateFlipType.Rotate180FlipX);

    }
}
