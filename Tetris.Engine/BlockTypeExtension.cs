using System;

namespace Tetris.Engine
{
    using Extensions;

    public static class BlockTypeExtension
    {
        private const int RotationsO = 1;
        private const int RotationsI = 2;
        private const int RotationsJ = 4;
        private const int RotationsZ = 2;
        private const int RotationsS = 2;
        private const int RotationsL = 4;
        private const int RotationsT = 4;

        private const int MatrixLengthO = 4;
        private const int MatrixLengthI = 4;
        private const int MatrixLengthJ = 3;
        private const int MatrixLengthZ = 3;
        private const int MatrixLengthS = 3;
        private const int MatrixLengthL = 3;
        private const int MatrixLengthT = 3;

        private static readonly string[] I = {
            @"0000
              1111
              0000
              0000",
            @"0010
              0010
              0010
              0010" };

        private static readonly string[] L = {
            @"000
              111
              100",
            @"110
              010
              010",
            @"001
              111
              000",
            @"010
              010
              011" };

        private static readonly string[] J = {
            @"000
              111
              001",
            @"001
              001
              011",
            @"000
              100
              111",
            @"011
              010
              010" };

        private static readonly string[] O = {
            @"0000
              0110
              0110
              0000" };

        private static readonly string[] S = {
            @"000
              011
              110",
            @"010
              011
              001" };

        private static readonly string[] T = {
            @"000
              111
              010",
            @"010
              110
              010",
            @"000
              010
              111",
            @"010
              011
              010" };

        private static readonly string[] Z = {
            @"000
              110
              011",
            @"001
              011
              010" };

        public static bool[][] Rotation(this BlockType type, int rotationIndex)
        {
            switch (type)
            {
                case BlockType.O: return O[0].StringToBoolMatrix(MatrixLengthO);
                case BlockType.I: return I[Math.Abs(rotationIndex) % RotationsI].StringToBoolMatrix(MatrixLengthI);
                case BlockType.J: return J[Math.Abs(rotationIndex) % RotationsJ].StringToBoolMatrix(MatrixLengthJ);
                case BlockType.Z: return Z[Math.Abs(rotationIndex) % RotationsZ].StringToBoolMatrix(MatrixLengthZ);
                case BlockType.S: return S[Math.Abs(rotationIndex) % RotationsS].StringToBoolMatrix(MatrixLengthS);
                case BlockType.L: return L[Math.Abs(rotationIndex) % RotationsL].StringToBoolMatrix(MatrixLengthL);
                case BlockType.T: return T[Math.Abs(rotationIndex) % RotationsT].StringToBoolMatrix(MatrixLengthT);
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        public static int BlockDimension(this BlockType type)
        {
            switch (type)
            {
                case BlockType.O: return MatrixLengthO;
                case BlockType.I: return MatrixLengthI;
                case BlockType.J: return MatrixLengthJ;
                case BlockType.Z: return MatrixLengthZ;
                case BlockType.S: return MatrixLengthS;
                case BlockType.L: return MatrixLengthL;
                case BlockType.T: return MatrixLengthT;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }

        public static int BlockRotations(this BlockType type)
        {
            switch (type)
            {
                case BlockType.O: return RotationsO;
                case BlockType.I: return RotationsI;
                case BlockType.J: return RotationsJ;
                case BlockType.Z: return RotationsZ;
                case BlockType.S: return RotationsS;
                case BlockType.L: return RotationsL;
                case BlockType.T: return RotationsT;
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
