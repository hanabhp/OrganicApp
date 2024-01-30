namespace waterfood.Core.Utilities.Generators
{
    public class TokenGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
