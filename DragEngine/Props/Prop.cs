using System;
using System.Linq;
using System.Reflection;

namespace DragEngine
{
    public class Prop
    {
        public VarObject varObject;

    }

    public static class propUtils
    {
        public static T AddProp<T>(this VarObject varObject, params object[] constructorArgs) where T : Prop
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
            newProp.varObject = varObject;
            varObject.props.Add(newProp);

            return newProp;
        }
        public static void RemoveProp<T>(this VarObject varObject, params object[] constructorArgs) where T : Prop
        {
            foreach (var item in varObject.props)
            {
                if (item is T)
                {
                    varObject.props.Remove(item);
                }
            }
        }
        public static T GetProp<T>(this VarObject varObject) where T : Prop
        {
            foreach (var item in varObject.props)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
    }
}
