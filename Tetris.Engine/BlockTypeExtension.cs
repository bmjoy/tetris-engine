using System;

namespace Tetris.Engine
{
    using Extensions;

    public static class BlockTypeExtension
    {
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


        public static bool[][] GetRotations(this BlockType type, int rotationIndex)
        {
            switch (type)
            {
                case BlockType.O: return O[0].StringToBoolMatrix(4);
                case BlockType.I: return I[rotationIndex % 2].StringToBoolMatrix(4);
                case BlockType.J: return J[rotationIndex % 4].StringToBoolMatrix(3);
                case BlockType.Z: return Z[rotationIndex % 2].StringToBoolMatrix(3);
                case BlockType.S: return S[rotationIndex % 2].StringToBoolMatrix(3);
                case BlockType.L: return L[rotationIndex % 4].StringToBoolMatrix(3);
                case BlockType.T: return T[rotationIndex % 4].StringToBoolMatrix(3);
            }

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
