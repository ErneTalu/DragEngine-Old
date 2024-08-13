using System;
using System.Windows.Input;

namespace DragEngine
{
    public static class Input
    {
        private static readonly Key[] HorizontalKeys = { Key.A , Key.D};
        private static readonly Key[] VerticalKeys = { Key.W, Key.S};
        private static readonly Key[] Horizontal2Keys = { Key.Left, Key.Right };
        private static readonly Key[] Vertical2Keys = { Key.Up, Key.Down };
        private static readonly Key[] JumpKeys = { Key.Space };

        public static float GetAxis(string axisName)
        {
            switch (axisName)
            {
                case "Horizontal": return GetNormalizedAxis(HorizontalKeys);
                case "Vertical": return GetNormalizedAxis(VerticalKeys);
                case "Horizontal2": return GetNormalizedAxis(Horizontal2Keys);
                case "Vertical2": return GetNormalizedAxis(Vertical2Keys);
                case "Jump": return GetNormalizedAxis(JumpKeys);
            }
            return 0f;
        }
        public static float GetAxisRaw(string axisName)
        {
            switch (axisName)
            {
                case "Horizontal": return GetAxisFromKeys(HorizontalKeys);
                case "Vertical": return GetAxisFromKeys(VerticalKeys);
                case "Horizontal2": return GetAxisFromKeys(Horizontal2Keys);
                case "Vertical2": return GetAxisFromKeys(Vertical2Keys);
                case "Jump": return GetAxisFromKeys(JumpKeys);
            }
            return 0f;
        }

        public static bool GetKeyDown(Key key) { return (Keyboard.GetKeyStates(key) & KeyStates.Down) > 0; }
        public static bool GetKeyDown(string key) { Enum.TryParse(key, true, out Key result); return (Keyboard.GetKeyStates(result) & KeyStates.Down) > 0; }

        public static bool GetMouseButtonDown(int button)
        {
            switch (button)
            {
                case 0: return Mouse.LeftButton > 0;
                case 1: return Mouse.RightButton > 0;
                case 2: return Mouse.MiddleButton > 0;
            }
            return false;
        }

        private static float GetAxisFromKeys(Key[] keys)
        {
            float axisValue = 0f;

            foreach (var key in keys)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    if (key == Key.W || key == Key.A || key == Key.Left || key == Key.Up) axisValue -= 1f;
                    else if (key == Key.S || key == Key.D || key == Key.Right || key == Key.Down || key == Key.Space) axisValue += 1f;
                }
            }

            return axisValue;
        }
        private static float GetNormalizedAxis(Key[] keys)
        {
            float axisValue = 0f;

            foreach (var key in keys)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    if (key == Key.W || key == Key.A || key == Key.Left || key == Key.Up) axisValue -= Time.deltaTime;
                    else if (key == Key.S || key == Key.D || key == Key.Right || key == Key.Down || key == Key.Space) axisValue += Time.deltaTime;              
                }
            }

            return MathUtils.Clamp(axisValue, -1f, 1f);
        }


    }

    public static class MathUtils
    {
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
