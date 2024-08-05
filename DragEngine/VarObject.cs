using System.Collections.Generic;

namespace DragEngine
{
    public class VarObject
    {
        public Vector2 position, scale;

        public List<prop> props = new List<prop>();
        public prop prop;

        public string tag;

        public VarObject(Vector2 position, Vector2 scale)
        {
            this.position = position;
            this.scale = scale;
            DragEngine.RegisterVarObject(this);
        }



    }
}
