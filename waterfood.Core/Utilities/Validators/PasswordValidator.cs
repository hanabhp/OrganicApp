using System.Text.RegularExpressions;

namespace waterfood.Core.Utilities.Validators
{
    public static class PasswordValidator
    {
        public static bool IsValidPassword(this string? value)
        {
            if (value == null)
                return false;

            if (value.Length < 8)
                return false;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum6Chars = new Regex(@".{6,}");

            var isValidated = hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasMinimum6Chars.IsMatch(value);

            return isValidated;
        }
    }
}
