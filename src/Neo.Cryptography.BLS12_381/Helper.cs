using System;
using System.Runtime.InteropServices;

namespace Neo.Cryptography.BLS12_381
{
    public static class Helper
    {
        private const int G1 = 96;
        private const int G2 = 192;
        private const int Gt = 576;

        public static byte[] Add(this byte[] p1, byte[] p2)
        {
            if (p1.Length != p2.Length)
                throw new Exception($"Bls12381 operation fault, type:format, error:type mismatch");
            return p1.Length switch
            {
                G1 => new G1Affine(new G1Projective(G1Affine.FromUncompressed(p1)) + new G1Projective(G1Affine.FromUncompressed(p2))).ToUncompressed(),
                G2 => new G2Affine(new G2Projective(G2Affine.FromUncompressed(p1)) + new G2Projective(G2Affine.FromUncompressed(p2))).ToUncompressed(),
                Gt => (BLS12_381.Gt.FromBytesArray(p1) + BLS12_381.Gt.FromBytesArray(p2)).ToBytesArray(),
                _ => throw new Exception($"Bls12381 operation fault, type:format, error:valid point length")
            };
        }

        public static byte[] Mul(this byte[] p, long x)
        {
            Scalar X = x < 0 ? -new Scalar(Convert.ToUInt64(Math.Abs(x))):new Scalar(Convert.ToUInt64(Math.Abs(x)));
            return p.Length switch
            {
                G1 => new G1Affine(G1Affine.FromUncompressed(p) * X).ToUncompressed(),
                G2 => new G2Affine(G2Affine.FromUncompressed(p) * X).ToUncompressed(),
                Gt => (BLS12_381.Gt.FromBytesArray(p) * X).ToBytesArray(),
                _ => throw new Exception($"Bls12381 operation fault, type:format, error:valid point length")
            };
        }

        public static byte[] Pairing(this byte[] p1, byte[] p2)
        {
            if (p1.Length != G1 || p2.Length != G2)
                throw new Exception($"Bls12381 operation fault, type:format, error:type mismatch");
            return Bls12.Pairing(G1Affine.FromUncompressed(p1), G2Affine.FromUncompressed(p2)).ToBytesArray();
        }
    }
}
