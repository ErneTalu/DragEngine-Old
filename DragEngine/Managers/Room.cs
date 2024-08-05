
using System.Collections.Generic;

namespace DragEngine
{
    public static class Room
    {
        public static List<string[,]> Rooms = new List<string[,]>();
        public static int CurrentRoomIndex = 0;

        public static void AddRoom(string[,] room) => Rooms.Add(room);
        public static void RemoveRoom(string[,] room ) => Rooms.Remove(room);

        public static string[,] CurrentRoom()
        {
            return Rooms[CurrentRoomIndex];
        }

        public static List<Vector2> GetTiles(string tileName)
        {
            List<Vector2> tiles = new List<Vector2>();
            for (int x = 0; x < CurrentRoom().GetLength(1); x++)
            {
                for (int y = 0; y < CurrentRoom().GetLength(0); y++)
                {
                    if (CurrentRoom()[y,x] == tileName)
                    {
                        tiles.Add(new Vector2(x * 50,y * 50));
                    }
                }
            }

            return tiles;
        }
    }
}
