using System.Reflection;

namespace Neo.Cryptography.BLS12_381;

interface INumber<T> where T : unmanaged, INumber<T>
{
    //static abstract int Size { get; }
    //static abstract ref readonly T Zero { get; }
    //static abstract ref readonly T One { get; }

    //static abstract T operator -(in T x);
    //static abstract T operator +(in T x, in T y);
    //static abstract T operator -(in T x, in T y);
    //static abstract T operator *(in T x, in T y);

    abstract T Square();
}

static class NumberExtensions
{
    public static int Size<T>() where T : unmanaged, INumber<T>
    {
        var propertyInfo = typeof(T).GetProperty("Size", BindingFlags.Static);
        if (propertyInfo != null)
        {
            return (int)propertyInfo.GetValue(null);
        }
        throw new InvalidOperationException("Property Size not found on type " + typeof(T).Name);
    }

    public static T Zero<T>() where T : unmanaged, INumber<T>
    {
        var propertyInfo = typeof(T).GetProperty("Zero", BindingFlags.Static);
        if (propertyInfo != null)
        {
            return (T)propertyInfo.GetValue(null);
        }
        throw new InvalidOperationException("Property Zero not found on type " + typeof(T).Name);
    }

    public static T One<T>() where T : unmanaged, INumber<T>
    {
        // Try to get the 'One' property.
        var propertyInfo = typeof(T).GetProperty("One", BindingFlags.Static | BindingFlags.Public);
        if (propertyInfo != null)
        {
            return (T)propertyInfo.GetValue(null);
        }

        // If 'One' is not a property, try to get it as a field.
        var fieldInfo = typeof(T).GetField("One", BindingFlags.Static | BindingFlags.Public);
        if (fieldInfo != null)
        {
            return (T)fieldInfo.GetValue(null);
        }

        throw new InvalidOperationException("Property or Field 'One' not found on type " + typeof(T).Name);
    }

    public static T Add<T>(in T x, in T y) where T : unmanaged, INumber<T>
    {
        // Define the parameter types for the multiplication operator
        Type[] paramTypes = { typeof(T).MakeByRefType(), typeof(T).MakeByRefType() };

        // Attempt to find the multiplication operator
        MethodInfo method = typeof(T).GetMethod("op_Addition", BindingFlags.Static | BindingFlags.Public, null, paramTypes, null);

        if (method != null)
        {
            return (T)method.Invoke(null, new object[] { x, y });
        }
        throw new InvalidOperationException("Addition operator not found for type " + typeof(T).Name);
    }

    public static T Subtract<T>(in T x, in T y) where T : unmanaged, INumber<T>
    {
        // Define the parameter types for the multiplication operator
        Type[] paramTypes = { typeof(T).MakeByRefType(), typeof(T).MakeByRefType() };

        // Attempt to find the multiplication operator
        MethodInfo method = typeof(T).GetMethod("op_Subtraction", BindingFlags.Static | BindingFlags.Public, null, paramTypes, null);

        if (method != null)
        {
            return (T)method.Invoke(null, new object[] { x, y });
        }
        throw new InvalidOperationException("Subtraction operator not found for type " + typeof(T).Name);
    }

    public static T Multiply<T>(in T x, in T y) where T : unmanaged, INumber<T>
    {
        // Define binding flags to search for public and static members
        var bindingFlags = BindingFlags.Static | BindingFlags.Public;

        // Define the parameter types for the multiplication operator
        Type[] paramTypes = { typeof(T).MakeByRefType(), typeof(T).MakeByRefType() };

        // Attempt to find the multiplication operator
        MethodInfo method = typeof(T).GetMethod("op_Multiply", bindingFlags, null, paramTypes, null);

        if (method != null)
        {
            return (T)method.Invoke(null, new object[] { x, y });
        }

        throw new InvalidOperationException("Multiplication operator not found for type " + typeof(T).Name);
    }

    public static T Negate<T>(in T x) where T : unmanaged, INumber<T>
    {
        // Define binding flags to search for public and static members
        var bindingFlags = BindingFlags.Static | BindingFlags.Public;

        // Define the parameter types for the multiplication operator
        Type[] paramTypes = { typeof(T).MakeByRefType() };

        // Attempt to find the multiplication operator
        MethodInfo method = typeof(T).GetMethod("op_UnaryNegation", bindingFlags, null, paramTypes, null);

        if (method != null)
        {
            return (T)method.Invoke(null, new object[] { x });
        }
        throw new InvalidOperationException("Unary negation operator not found for type " + typeof(T).Name);
    }

    public static T PowVartime<T>(this T self, ulong[] by) where T : unmanaged, INumber<T>
    {
        // Although this is labeled "vartime", it is only
        // variable time with respect to the exponent.
        var res = One<T>();
        for (int j = by.Length - 1; j >= 0; j--)
        {
            for (int i = 63; i >= 0; i--)
            {
                res = res.Square();
                if (((by[j] >> i) & 1) == 1)
                {
                    res = Multiply(res, self);
                }
            }
        }
        return res;
    }
}
