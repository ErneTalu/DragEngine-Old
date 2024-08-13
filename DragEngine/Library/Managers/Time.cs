using System;
using System.Diagnostics;

namespace DragEngine
{
    public static class Time
    {
        private static Stopwatch stopwatch = new Stopwatch();
        private static float lastUpdateTime;

        public static float deltaTime { get; private set; }
        public static float time { get; private set; }

        public static void Update()
        {
            float currentTime = (float)stopwatch.Elapsed.TotalSeconds * 6;

            time = currentTime;
            deltaTime = currentTime - lastUpdateTime;
            lastUpdateTime = currentTime;
        }

        static Time()
        {
            stopwatch.Start();
        }
    }
}
