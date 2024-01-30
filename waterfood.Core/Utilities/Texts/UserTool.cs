using waterfood.Data.Entities.Users;

namespace waterfood.Core.Utilities.Texts
{
    public static class UserTool
    {
        public static string FullName(this User? value)
        {
            if (value == null) return "";
            return value.FirstName + " " + value.LastName;
        }

        public static string Avatar(this User? value)
        {
            return value == null ? "default.png" : value.UserAvatar;
        }
    }
}
