namespace waterfood.Core.Utilities.Generators
{
    public class CodeGenerator
    {
        public static string GenerateUniqueCode()
        {
            Random a = new Random(Guid.NewGuid().GetHashCode());
            return a.Next(1000, 9999).ToString().Replace("-","");
        }
    }
}
