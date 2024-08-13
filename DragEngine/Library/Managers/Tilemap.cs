using System;
using System.Drawing;

namespace DragEngine
{
    public class Tilemap
    {
        public Bitmap TileImage { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public Sprite[,] Tiles { get; private set; }

        public Tilemap(Bitmap tileImage, int tileWidth, int tileHeight, int mapWidth, int mapHeight)
        {
            TileImage = tileImage;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            Tiles = new Sprite[mapWidth, mapHeight];

            GenerateTilemap();
        }

        private void GenerateTilemap()
        {
            int tileRows = TileImage.Height / TileHeight;
            int tileCols = TileImage.Width / TileWidth;

            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    Bitmap tileBitmap = GetTileBitmap(x % tileCols, y % tileRows);

                    Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
                    Sprite tileSprite = new VarObject(position, new Vector2(TileWidth, TileHeight)).AddProp<Sprite>(Color.White, SpriteType.Image, tileBitmap);

                    Tiles[x, y] = tileSprite;
                }
            }
        }

        private Bitmap GetTileBitmap(int tileX, int tileY)
        {
            Bitmap tileBitmap = new Bitmap(TileWidth, TileHeight);

            using (Graphics g = Graphics.FromImage(tileBitmap))
            {
                g.DrawImage(TileImage, new Rectangle(0, 0, TileWidth, TileHeight), tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight, GraphicsUnit.Pixel);
            }

            return tileBitmap;
        }
    }
}
