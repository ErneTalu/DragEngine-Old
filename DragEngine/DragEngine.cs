using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

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
            while (true)
            {
                try
                {
                    Time.Update();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    Update();
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
        
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;    
            g.InterpolationMode = InterpolationMode.High;
            g.Clear(Color.White);
            List<Sprite> Render = new List<Sprite>();

            for (int i = 0; i < varObjects.Count; i++)
            {
                Render.Add(varObjects[i].GetProp<Sprite>());
            }

            foreach (Sprite s in Render)
            {
                VarObject v = s.varObject;

                if (s.type == SpriteType.Quad) g.FillRectangle(new SolidBrush(s.color), (int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y);
                if (s.type == SpriteType.Circle) g.FillEllipse(new SolidBrush(s.color), (int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y);
                if (s.type == SpriteType.Image) g.DrawImageUnscaledAndClipped(s.image ,new Rectangle((int)v.position.x, (int)v.position.y, (int)v.scale.x, (int)v.scale.y));
            }
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
    }

    public static class Debug
    {
        public static void Log(object message) => Console.WriteLine(message);
    }
}
