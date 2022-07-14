namespace Neo.Cryptography.BLS12_381
{
    public static class Helper
    {
        private const int G1 = 48;
        private const int G2 = 96;
        private const int Gt = 576;

        public static byte[] Add(this byte[] p1, byte[] p2)
        {
            if (p1.Length != p2.Length)
                throw new Exception($"Bls12381 operation fault, type:format, error:type mismatch");
            byte[] result;
            switch (p1.Length)
            {
                case G1:
                    result = new G1Affine(new G1Projective(G1Affine.FromCompressed(p1)) + new G1Projective(G1Affine.FromCompressed(p2))).ToCompressed();
                    break;
                case G2:
                    result = new G2Affine(new G2Projective(G2Affine.FromCompressed(p1)) + new G2Projective(G2Affine.FromCompressed(p2))).ToCompressed();
                    break;
                case Gt:
                    result = (BLS12_381.Gt.FromBytes(p1) + BLS12_381.Gt.FromBytes(p2)).ToArray();
                    break;
                default:
                    throw new Exception($"Bls12381 operation fault, type:format, error:valid point length");
            }
            return result;
        }

        public static byte[] Mul(this byte[] p, long x)
        {
            Scalar X = x < 0 ? -new Scalar(Convert.ToUInt64(Math.Abs(x))) : new Scalar(Convert.ToUInt64(Math.Abs(x)));
            byte[] result;
            switch (p.Length)
            {
                case G1:
                    result = new G1Affine(G1Affine.FromCompressed(p) * X).ToCompressed();
                    break;
                case G2:
                    result = new G2Affine(G2Affine.FromCompressed(p) * X).ToCompressed();
                    break;
                case Gt:
                    result = (BLS12_381.Gt.FromBytes(p) * X).ToArray();
                    break;
                default:
                    throw new Exception($"Bls12381 operation fault, type:format, error:valid point length");
            }
            return result;
        }

        public static byte[] Pairing(this byte[] p1, byte[] p2)
        {
            if (p1.Length != G1 || p2.Length != G2)
                throw new Exception($"Bls12381 operation fault, type:format, error:type mismatch");
            return Bls12.Pairing(G1Affine.FromCompressed(p1), G2Affine.FromCompressed(p2)).ToArray();
        }
    }
}
