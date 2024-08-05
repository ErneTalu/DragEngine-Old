using System;
using System.Linq;
using System.Reflection;

namespace DragEngine
{
    public class prop
    {
        public VarObject varObject;

    }

    public static class propUtils
    {
        public static T AddProp<T>(this VarObject varObject, params object[] constructorArgs) where T : prop
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                constructorArgs.Select(arg => arg.GetType()).ToArray(),
                null);

            if (constructor == null)
            {
                constructor = typeof(T).GetConstructor(Type.EmptyTypes);
                if (constructor == null)
                {
                    throw new InvalidOperationException($"The type {typeof(T).Name} must have a constructor with the specified parameters or a parameterless constructor.");
                }
                T newProp = (T)constructor.Invoke(null);
                newProp.varObject = varObject; 
                varObject.props.Add(newProp);
                return newProp;
            }

            T newPropWithArgs = (T)constructor.Invoke(constructorArgs);
            newPropWithArgs.varObject = varObject; 
            varObject.props.Add(newPropWithArgs);
            return newPropWithArgs;
        }
        public static T GetProp<T>(this VarObject varObject) where T : prop
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
