using static Neo.Cryptography.BLS12_381.MillerLoopUtility;

namespace Neo.Cryptography.BLS12_381;

partial class G2Prepared
{
    class Adder : IMillerLoopDriver<object?>
    {
        public G2Projective Curve;
        public readonly G2Affine Base;
        public readonly List<(Fp2, Fp2, Fp2)> Coeffs;

        public Adder(in G2Affine q)
        {
            Curve = new G2Projective(in q);
            Base = q;
            Coeffs = new(68);
        }

        object? IMillerLoopDriver<object?>.DoublingStep(in object? f)
        {
            var coeffs = DoublingStep(ref Curve);
            Coeffs.Add(coeffs);
            return null;
        }

        object? IMillerLoopDriver<object?>.AdditionStep(in object? f)
        {
            var coeffs = AdditionStep(ref Curve, in Base);
            Coeffs.Add(coeffs);
            return null;
        }

        public static object? SquareOutput(in object? f) => null;

        public static object? Conjugate(in object? f) => null;

        public static object? One => null;
    }
}
