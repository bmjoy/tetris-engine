namespace Tetris.Engine
{
    public static class BlockTypeExtension
    {
        private static readonly string[] L = {
                    @"0100
                      0100
                      0100
                      0100",
                    @"0000
                      1111
                      0000
                      0000",
                    @"0010
                      0010
                      0010
                      0010",
                    @"0000
                      0000
                      1111
                      0000" };

        private static readonly string[] J = {
                    @"0000
                      0110
                      0010
                      0010", 
                    @"0000
                      0010
                      1110
                      0000",
                    @"0000
                      0100
                      0100
                      0110", 
                    @"0000
                      1110
                      1000
                      0000" };

        private static readonly string[] I = {
                    @"0000
                      0011
                      0010
                      0010", 
                    @"0000
                      0000
                      0111
                      0001",
                    @"0000
                      0001
                      0001
                      0011", 
                    @"0000
                      0000
                      0100
                      0111" };

        private static readonly string[] O = {
                    @"0000
                      0110
                      0110
                      0000", 
                    @"0000
                      0110
                      0110
                      0000",
                    @"0000
                      0110
                      0110
                      0000", 
                    @"0000
                      0110
                      0110
                      0000" };

        private static readonly string[] S = {
                    @"0000
                      0100
                      0110
                      0010", 
                    @"0000
                      0011
                      0110
                      0000",
                    @"0000
                      0100
                      0110
                      0010", 
                    @"0000
                      0011
                      0110
                      0000" };

        private static readonly string[] T = {
                    @"0000
                      0100
                      1110
                      0000", 
                    @"0000
                      0100
                      0110
                      0100",
                    @"0000
                      0000
                      1110
                      0100", 
                    @"0000
                      0100
                      1100
                      0100" };

        private static readonly string[] Z = {
                    @"0000
                      0010
                      0110
                      0100", 
                    @"0000
                      1100
                      0110
                      0000",
                    @"0000
                      0010
                      0110
                      0100",
                    @"0000
                      1100
                      0110
                      0000" };


        public static bool[][] GetRotations(this BlockType type, int rotationIndex)
        {
            rotationIndex = rotationIndex % 4;

            switch (type)
            {
                case BlockType.I: return I[rotationIndex].StringToBoolMatrix();
                case BlockType.J: return J[rotationIndex].StringToBoolMatrix();
                case BlockType.Z: return Z[rotationIndex].StringToBoolMatrix();
                case BlockType.O: return O[rotationIndex].StringToBoolMatrix();
                case BlockType.S: return S[rotationIndex].StringToBoolMatrix();
                case BlockType.L: return L[rotationIndex].StringToBoolMatrix();
                case BlockType.T: return T[rotationIndex].StringToBoolMatrix();
            }

            return null;
        }
    }
}
