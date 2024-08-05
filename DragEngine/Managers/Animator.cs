

using System.Collections.Generic;

namespace DragEngine
{
    public class Animator
    {
        public Dictionary<string, Animation> animations = new Dictionary<string,Animation>();

        public void AddAnimation(string name, Animation animation) => animations.Add(name, animation);
        public void AddAnimation(string name) => animations.Add(name, new Animation());
        public void RemoveAnimation(string name) => animations.Remove(name);
    }
}
