using System.Drawing;

namespace DragEngine
{
    public class Text : Prop
    {
        public string text = "New Text";
        public Font font;
        public Brush brush;

        public Text(string text, int size = 24, Color color = default, Font font = default) : base()
        {
            this.text = text;
            this.font = font ?? new Font("Arial", size);
            this.brush = new SolidBrush(color == default ? Color.Black : color);
        }
    }
}
