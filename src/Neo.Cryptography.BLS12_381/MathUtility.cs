namespace Neo.Cryptography.BLS12_381;

static class MathUtility
{
    public static (ulong result, ulong carry) Adc(ulong a, ulong b, ulong carry)
    {
        ulong result = unchecked(a + b + carry);
        carry = ((a & b) | ((a | b) & (~result))) >> 63;
        return (result, carry);
    }

    public static (ulong result, ulong borrow) Sbb(ulong a, ulong b, ulong borrow)
    {
        ulong result = unchecked(a - b - borrow);
        borrow = (((~a) & b) | (~(a ^ b)) & result) >> 63;
        return (result, borrow);
    }

    public static (ulong low, ulong high) Mac(ulong z, ulong x, ulong y, ulong carry)
    {
        ulong high = BigMul(x, y, out ulong low);
        (low, carry) = Adc(low, carry, 0);
        (high, _) = Adc(high, 0, carry);
        (low, carry) = Adc(low, z, 0);
        (high, _) = Adc(high, 0, carry);
        return (low, high);
    }

    public static ulong BigMul(ulong a, ulong b, out ulong low)
    {
        uint al = (uint)a;
        uint ah = (uint)(a >> 32);
        uint bl = (uint)b;
        uint bh = (uint)(b >> 32);

        ulong mull = ((ulong)al) * bl;
        ulong t = ((ulong)ah) * bl + (mull >> 32);
        ulong tl = ((ulong)al) * bh + (uint)t;

        low = tl << 32 | (uint)mull;

        return ((ulong)ah) * bh + (t >> 32) + (tl >> 32);
    }
}
