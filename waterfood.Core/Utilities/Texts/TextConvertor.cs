using waterfood.Core.Utilities.Dates;

namespace waterfood.Core.Utilities.Texts
{
    public static class TextConvertor
    {
        public static string ToDash(this string value)
        {
            return value.Replace(" ", "-");
        }

        public static string UnDash(this string value)
        {
            return value.Replace("-", " ");
        }
    }
}
