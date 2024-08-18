
using System.Collections.Generic;
using System.Drawing;

namespace DragEngine
{
    public static class Map
    {
        public static List<string[,]> Maps = new List<string[,]>();
        public static int CurrentMapIndex = 0;

        public static void AddMap(string[,] room) => Maps.Add(room);
        public static void RemoveMap(string[,] room ) => Maps.Remove(room);

        public static string[,] CurrentMap => Maps[CurrentMapIndex];

        public static List<Vector2> GetTiles(string tileName)
        {
            List<Vector2> tiles = new List<Vector2>();
            for (int x = 0; x < CurrentMap.GetLength(1); x++)
            {
                for (int y = 0; y < CurrentMap.GetLength(0); y++)
                {
                    if (CurrentMap[y,x] == tileName)
                    {
                        tiles.Add(new Vector2(x * 50,y * 50));
                    }
                }
            }

            return tiles;
        }

        public static void LoadMapFromImage(Bitmap image)
        {
            string[,] map = new string[image.Height, image.Width];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    string tileName = GetTileNameFromColor(pixelColor);
                    map[y, x] = tileName;
                }
            }

            AddMap(map);
        }

        private static string GetTileNameFromColor(Color color)
        {
            if (color == Color.Red) return "g";
            if (color == Color.Blue) return "p";
            if (color == Color.Gray) return "f";
            if (color == Color.Yellow) return "e";
            if (color == Color.Black) return "a"; 
            return ""; 
        }
    }
}
