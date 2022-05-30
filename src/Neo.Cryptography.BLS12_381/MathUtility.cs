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
        ulong high = Math.BigMul(x, y, out ulong low);
        (low, carry) = Adc(low, carry, 0);
        (high, _) = Adc(high, 0, carry);
        (low, carry) = Adc(low, z, 0);
        (high, _) = Adc(high, 0, carry);
        return (low, high);
    }
}
