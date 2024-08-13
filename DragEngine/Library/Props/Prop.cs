using System;
using System.Reflection;

namespace DragEngine
{
    public class Prop
    {
        public VarObject varObject;
        public Vector2 position => varObject.position;
        public Vector2 scale => varObject.scale;

        #region Props
        public Physics physics => varObject.GetProp<Physics>();
        public Sprite sprite => varObject.GetProp<Sprite>();
        public Collider collider => varObject.GetProp<Collider>();
        public Text text => varObject.GetProp<Text>();
        public Animation animation => varObject.GetProp<Animation>();
        #endregion

    }

    public static class PropUtils
    {
        public static T AddProp<T>(this Prop prop, params object[] constructorArgs) where T : Prop
        {
            ConstructorInfo[] constructors = typeof(T).GetConstructors();

            if (constructors.Length == 0)
                throw new InvalidOperationException("No constructors found for type " + typeof(T).FullName);

            ConstructorInfo constructor = constructors[0]; 
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i < constructorArgs.Length)
                {
                    args[i] = constructorArgs[i];
                }
                else
                {
                    args[i] = parameters[i].DefaultValue;
                }
            }

            T newProp = (T)constructor.Invoke(args);
            newProp.varObject = prop.varObject;
            prop.varObject.props.Add(newProp);

            return newProp;
        }
        public static void RemoveProp<T>(this Prop prop, params object[] constructorArgs) where T : Prop
        {
            foreach (var item in prop.varObject.props)
            {
                if (item is T)
                {
                    prop.varObject.props.Remove(item);
                }
            }
        }
        public static T GetProp<T>(this Prop prop) where T : Prop
        {
            foreach (var item in prop.varObject.props)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
        public static bool TryGetProp<T>(this Prop prop, out T newprop) where T : Prop
        {
            foreach (var item in prop.varObject.props)
            {
                if (item is T p)
                {
                    newprop = p;
                    return true;
                }
            }
            newprop = null;
            return false;
        }
    }
}
