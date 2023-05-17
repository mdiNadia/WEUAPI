namespace Application.ExtensionMethods
{
    public static class ConvertStringToDateTime
    {
        public static DateTime StringToDateTime(this string string_date)
        {
            DateTime date = DateTime.Parse(string_date);
            return date;
        }
    }
}
