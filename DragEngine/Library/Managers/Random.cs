
namespace DragEngine
{
    public class Random
    {
        public static int Range(int min, int max, bool zeroContains = true)
        {
            int random = new System.Random().Next(min, max);
            while (!zeroContains && random == 0) random = new System.Random().Next(min, max);

            return random;
        }
        public static int Select(int a, int b)
        {
            return new System.Random().Next(0,2) == 0 ? a : b;
        }
    }
}
