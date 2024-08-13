using System.Collections.Generic;

namespace DragEngine
{
    public class VarObject : Prop
    {
        public string name;
        public Vector2 position, scale;
        public List<Prop> props = new List<Prop>();

        public VarObject parent;
        public VarObject[] child;

        public VarObject(Vector2 position, Vector2 scale, string name = null)
        {
            this.position = position;
            this.scale = scale;
            this.name = name;
            varObject = this;
            DragEngine.RegisterVarObject(this);
        }

    }
}
