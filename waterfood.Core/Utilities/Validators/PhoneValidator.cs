using waterfood.Core.Utilities.Numbers;
using System.Text.RegularExpressions;

namespace waterfood.Core.Utilities.Validators
{
    public static class PhoneValidator
    {
        public static bool IsValidPhone(this string? value)
        {
            if (value == null)
                return false;
            
            //09708417721
            if(value.Length != 11)
                return false;

            if(!value.StartsWith("0"))
                return false;

            value = value.Substring(1);

            return value.IsDecimal();

        }
    }
}
