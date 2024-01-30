namespace waterfood.Core.Utilities.Numbers
{
    public static class NumberChecker
    {
        public static bool IsDecimal(this string? value)
        {
            if (value == null)
                return false;

            return decimal.TryParse(value, out var n);
        }
    }
}
