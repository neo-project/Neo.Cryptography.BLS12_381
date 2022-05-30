namespace Neo.Cryptography.BLS12_381;

interface IMillerLoopDriver<T>
{
    public T DoublingStep(in T f);
    public T AdditionStep(in T f);
    public static abstract T SquareOutput(in T f);
    public static abstract T Conjugate(in T f);
    public static abstract T One { get; }
}
