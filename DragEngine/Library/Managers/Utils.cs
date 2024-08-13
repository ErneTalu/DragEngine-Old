using System.Collections.Generic;
using System;

namespace DragEngine
{
    public static class Utils
    {
        public static void Each<T>(IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}
