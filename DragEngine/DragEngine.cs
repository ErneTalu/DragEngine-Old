using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace DragEngine
{
    public class Canvas : Form
    {
        public Canvas() => this.DoubleBuffered = true; // Renderin yanıp sönmesi, ikide bir kaybolması vb. yi engelliyor
    }

    public abstract class DragEngine
    {
        public static Vector2 ScreenSize = new Vector2(700,700);
        public string ScreenTitle = "";
        public Canvas Window = null;
        Thread GameLoopThread = null;
        public static List<VarObject> varObjects = new List<VarObject>();

        public const int FixedUpdateRate = 50; 
        public const float FixedDeltaTime = 0.05f;

        public DragEngine(Vector2 screenSize, string screenTitle)
        {
            ScreenSize = screenSize;
            ScreenTitle = screenTitle;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = ScreenTitle;
            Window.Paint += Renderer;
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.SetApartmentState(ApartmentState.STA);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void GameLoop()
        {
            Awake();
            Start();
            DateTime nextFixedUpdate = DateTime.Now;
            while (true)
            {
                try
                {
                    Time.Update();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    if (DateTime.Now >= nextFixedUpdate)
                    {
                        FixedUpdate();
                        nextFixedUpdate = DateTime.Now.AddMilliseconds(FixedUpdateRate);
                    }
                    Update();
                    LateUpdate();
                    Thread.Sleep(1);
                }
                catch (Exception) { }
            }
        }

        public static void RegisterVarObject(VarObject varObject)
        {
            if (varObject != null)
                varObjects.Add(varObject);
        }
        public static VarObject GetVarObject(string name)
        {
            foreach (VarObject v in varObjects)
                if (v.name == name) return v;

            return null;
        }
        public static void Each<T>(IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
        public static void Destroy(ref object Object)
        {
            Object = null;
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;    
            g.InterpolationMode = InterpolationMode.High;
            g.Clear(Color.White);

            List<Sprite> renderSprites = new List<Sprite>();
            List<Text> renderTexts = new List<Text>();

            for (int i = 0; i < varObjects.Count; i++)
            {
                if (varObjects[i].TryGetProp(out Sprite sprite))
                    renderSprites.Add(sprite);

                if (varObjects[i].TryGetProp(out Text text))
                    renderTexts.Add(text);
            }

            foreach (Sprite s in renderSprites)
            {
                VarObject v = s.varObject;

                if (s.type == SpriteType.Quad) g.FillRectangle(new SolidBrush(s.color), (int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y);
                if (s.type == SpriteType.Circle) g.FillEllipse(new SolidBrush(s.color), (int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y);
                if (s.type == SpriteType.Image) g.DrawImageUnscaledAndClipped(s.image ,new Rectangle((int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y));
            }
            foreach (Text t in renderTexts)
            {
                g.DrawString(t.text, t.font, t.brush, t.varObject.position.x, t.varObject.position.y);
            }
        }
        private void Physics()
        {
            foreach (VarObject v in varObjects)
            {
                if (v.TryGetProp(out Physics p))
                {
                    if (p.gravity == false && p.velocity == Vector2.zero) 
                        return;

                    p.Update();
                }
            }
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
    }

    public static class Debug
    {
        public static void Log(object message) => Console.WriteLine(message);
        public static void LogWarning(object message) => Console.WriteLine("Warning : " + message);
        public static void LogError(object message) => Console.WriteLine("Error : "+ message);
    }
}
