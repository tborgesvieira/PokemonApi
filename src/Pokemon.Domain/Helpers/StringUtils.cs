using System.Linq;

namespace Pokemon.Domain.Helpers
{
    public static class StringUtils
    {
        public static string ApenasNumeros(this string str)
        {
            if (str is null) return null;

            return new string(str.Where(char.IsDigit).ToArray());
        }
    }
}
