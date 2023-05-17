namespace Application.ExtensionMethods
{
    public static class AsTimeAgo
    {
        public static string TimeAgo(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} ثانیه قبل", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} دقیقه قبل", timeSpan.Minutes) :
                    "1 دقیقه قبل";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} ساعت قبل", timeSpan.Hours) :
                    "1 ساعت قبل";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} روز قبل", timeSpan.Days) :
                    "دیروز";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} ماه قبل", timeSpan.Days / 30) :
                    "یکماه قبل";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} سال قبل", timeSpan.Days / 365) :
                    "یکسال قبل";
            }

            return result;
        }
    }
}
