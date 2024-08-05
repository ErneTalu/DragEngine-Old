using System;
using System.Collections.Generic;

namespace DragEngine
{
    public class Vector2
    {
        public float x, y;
        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public Vector2(float X, float Y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}";
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x * scalar, vector.y * scalar);
        }
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2(vector.x / scalar, vector.y / scalar);
        }

        public static Vector2 zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public static Vector2 Center(Vector2 pos, Vector2 scale)
        {
            return new Vector2((int)pos.x + (int)scale.x / 2, (int)pos.y + (int)scale.y / 2);
        }

        public static Vector2 GetDirection(Vector2 from, Vector2 to)
        {
            return new Vector2(to.x - from.x, to.y - from.y);
        }

        public Vector2 normalized
        {
            get
            {
                float length = Length();
                if (length == 0) return new Vector2(0, 0); 
                return new Vector2(x / length , y / length) * 5;
            }
        }

        public float magnitude
        {
            get { return (float)Math.Sqrt(x * x + y * y); }
        }

        public static float GetDistance(Vector2 point1, Vector2 point2)
        {
            if (point1 != null && point2 != null)
            {
                double x = Math.Abs(point2.x - point1.x);
                double y = Math.Abs(point1.y - point2.y);
                return (float)Math.Sqrt((x*x) + (y*y));
            }
            return 0;
        }


    }
}
