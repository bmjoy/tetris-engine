namespace Tetris.Engine
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        public static bool[][] StringToBoolMatrix(this string str)
        {
            var bools = new Regex("\\d+").Replace(str, string.Empty).Select(x => x == '1').ToArray();
            return new[] {
                             bools.Take(4).ToArray(),
                             bools.Skip(4).Take(4).ToArray(),
                             bools.Skip(8).Take(4).ToArray(),
                             bools.Skip(12).Take(4).ToArray(),
                         };
        }
    }
}
