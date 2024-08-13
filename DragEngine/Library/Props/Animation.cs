using System.Collections.Generic;
using System.Drawing;

namespace DragEngine
{
    public class Animation : Prop
    {
        List<Image> frames = new List<Image>();
        int increment, delayCount = 0;
        bool isFlipped = false;

        public void AddFrame(Image frame) => frames.Add(frame);
        public void AddTileFrames(Image tileSheet, int tileWidth, int tileHeight) => frames.AddRange(TileToImage(tileSheet, tileWidth, tileHeight));
        public void RemoveFrame(Image frame) => frames.Remove(frame);

        int delay = 10;

        public void Flip(bool flip)
        {
            if (isFlipped != flip)
            {
                isFlipped = flip;
                for (int i = 0; i < frames.Count; i++)
                {
                    frames[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
        }

        public void PlayOneFrame(Sprite sprite)
        {
            delayCount++;
            if (delayCount % delay == 0)
            {
                increment = (increment == frames.Count) ? 0 : increment + 1;
            }

            sprite.image = frames[increment - 1];
        }

        public static List<Image> TileToImage(Image tileSheet, int tileWidth, int tileHeight)
        {
            int columns = tileSheet.Width / tileWidth;
            int rows = tileSheet.Height / tileHeight;

            List<Image> Images = new List<Image>();

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Rectangle tileRect = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                    Bitmap tile = new Bitmap(tileWidth, tileHeight);

                    using (Graphics g = Graphics.FromImage(tile))
                    {
                        g.DrawImage(tileSheet, new Rectangle(0, 0, tileWidth, tileHeight), tileRect, GraphicsUnit.Pixel);
                    }

                    Images.Add(tile);
                }
            }

            return Images;
        }
    }
}
