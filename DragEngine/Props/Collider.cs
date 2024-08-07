
namespace DragEngine
{
    public enum ColliderType
    {
        Quad, Circle, Image 
    };

    public class Collider : Prop
    {
        public ColliderType type;
        public bool isTrigger = false;

        public Collider(ColliderType type = ColliderType.Quad, bool isTrigger = false) : base()
        {
            this.type = type;
            this.isTrigger = isTrigger;
        }

    }
}
