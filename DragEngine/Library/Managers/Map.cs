
using System.Collections.Generic;

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
    }
}
