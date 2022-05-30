using static Neo.Cryptography.BLS12_381.MillerLoopUtility;

namespace Neo.Cryptography.BLS12_381;

partial class Bls12
{
    class Adder : IMillerLoopDriver<Fp12>
    {
        public G2Projective Curve;
        public readonly G2Affine Base;
        public readonly G1Affine P;

        public Adder(in G1Affine p, in G2Affine q)
        {
            Curve = new(q);
            Base = q;
            P = p;
        }

        Fp12 IMillerLoopDriver<Fp12>.DoublingStep(in Fp12 f)
        {
            var coeffs = DoublingStep(ref Curve);
            return Ell(in f, in coeffs, in P);
        }

        Fp12 IMillerLoopDriver<Fp12>.AdditionStep(in Fp12 f)
        {
            var coeffs = AdditionStep(ref Curve, in Base);
            return Ell(in f, in coeffs, in P);
        }

        static Fp12 IMillerLoopDriver<Fp12>.SquareOutput(in Fp12 f) => f.Square();

        static Fp12 IMillerLoopDriver<Fp12>.Conjugate(in Fp12 f) => f.Conjugate();

        static Fp12 IMillerLoopDriver<Fp12>.One => Fp12.One;
    }
}
