using System.Text.RegularExpressions;

namespace Application.ExtensionMethods
{
    public static class ValidateFields
    {
        public static bool IsEmail(this string email)
        {
            //@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            const string re = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return Regex.IsMatch(email, re);
        }
        public static bool IsMobile(this string mobile)
        {
            const string re = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            return Regex.IsMatch(mobile, re);
        }
    }
}
