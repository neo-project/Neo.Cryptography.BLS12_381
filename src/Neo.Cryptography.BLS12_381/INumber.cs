namespace Neo.Cryptography.BLS12_381;

interface INumber<T> where T : unmanaged, INumber<T>
{
    static abstract ref readonly T Zero { get; }
    static abstract ref readonly T One { get; }

    static abstract T operator -(in T x);
    static abstract T operator +(in T x, in T y);
    static abstract T operator -(in T x, in T y);
    static abstract T operator *(in T x, in T y);

    abstract T Square();
}

static class NumberExtensions
{
    public static T PowVartime<T>(this T self, ulong[] by) where T : unmanaged, INumber<T>
    {
        // Although this is labeled "vartime", it is only
        // variable time with respect to the exponent.
        var res = T.One;
        for (int j = by.Length - 1; j >= 0; j--)
        {
            for (int i = 63; i >= 0; i--)
            {
                res = res.Square();
                if (((by[j] >> i) & 1) == 1)
                {
                    res *= self;
                }
            }
        }
        return res;
    }
}
