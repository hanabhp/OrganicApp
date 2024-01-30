using System.Globalization;

namespace waterfood.Core.Utilities.Dates
{
    public static class DateConvertor
    {
        public static int DiffMinutes(this DateTime? date)
        {
            if (date == null) return 0;

            if (date.HasValue)
            {
                if(date < DateTime.Now) return 0;

                var diff = date - DateTime.Now;

                return diff.HasValue ? diff.Value.Minutes : 0;
            }

            return 0;
        }

        public static int DiffHours(this DateTime? date)
        {
            if (date == null) return 0;

            if (date.HasValue)
            {
                if (date < DateTime.Now) return 0;

                var diff = date - DateTime.Now;

                return diff.HasValue ? diff.Value.Hours : 0;
            }

            return 0;
        }

        public static int DiffDays(this DateTime? date)
        {
            if (date == null) return 0;

            if (date.HasValue)
            {
                if (date < DateTime.Now) return 0;

                var diff = date - DateTime.Now;

                return diff.HasValue ? diff.Value.Days : 0;
            }

            return 0;
        }

        public static DateTime FirstDay(this DateTime date)
        {
            var shamsi = date.ToShamsi();
            var split = shamsi.Split('/');

            var day = split[0] + "/" + split[1] + "/" + "01";
            return day.ToMiladi();
        }

        public static DateTime LastDay(this DateTime date)
        {
            var shamsi = date.ToShamsi();
            var split = shamsi.Split('/');

            int month = int.Parse(split[1]);
            var day = split[0] + "/" + split[1] + "/";

            if (month <= 6)
            {
                day += "31";
            }
            else if (month > 6 && month <= 11)
            {
                day += "30";
            }
            else
            {
                day += "29";
            }

            return day.ToMiladi();
        }

        public static DateTime ToMiladi(this string value)
        {
            string persianDate = value;
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            return DateTime.ParseExact(persianDate,
                "yyyy/MM/dd", persianCulture);
        }

        public static int GetWeekIndex(this DateTime value)
        {
            var name = value.GetDayOfWeekName();
            switch (name)
            {
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                case "Sunday":
                    return 7;
                default:
                    return 0;
            }
        }

        public static string ToShamsi(this DateTime value)
        {
            if (value.Year == 1)
                return "N/A";

            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }

        public static byte[] ToBytes(this DateTime value)
        {
            if (value.Year == 1)
                return Array.Empty<byte>();

            return BitConverter.GetBytes(value.ToBinary());
        }

        public static int GetShamsiMonth(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return value.Month;
        }

        public static int ShamsiYear(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return value.Year;
        }

        public static string GetDayOfWeekName(this DateTime value)
        {
            Dictionary<string, string[]> DayOfWeeks = new Dictionary<string, string[]>();
            DayOfWeeks.Add("en", new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            DayOfWeeks.Add("fa", new string[] { "یک شنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" });
            DayOfWeek a = value.DayOfWeek;
            int day = (int)a;

            return DayOfWeeks["en"][day];
        }

        public static string GetShamsiMonthName(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            switch (pc.GetMonth(value))
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
            }

            return "N/A";
        }

        public static string ToShamsi(this DateTime? value)
        {
            if (value != null)
            {
                DateTime newValue = DateTime.Parse(value.ToString());
                PersianCalendar pc = new PersianCalendar();
                return pc.GetYear(newValue) + "/" + pc.GetMonth(newValue).ToString("00") + "/" +
                       pc.GetDayOfMonth(newValue).ToString("00");
            }
            else
            {
                return "نامشخص";
            }
        }

        public static string[] GetCorrentHourFromPickerValue(string hour)
        {
            string[] result = { "0", "0", "AM" };
            var hourPart = "";
            var zonePart = "";
            if (hour.Contains("ظ"))
            {
                hourPart = hour.Substring(0, hour.Length - 4);
                zonePart = hour.Substring(hour.Length - 3);

                if (zonePart.Trim() == "ق.ظ" || zonePart.Trim().ToLower() == "am")
                {
                    result = hourPart.Split(":");
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = "AM";
                }
                else if (zonePart.Trim() == "ب.ظ" || zonePart.Trim().ToLower() == "pm")
                {
                    result = hourPart.Split(":");
                    //int tempHour = int.Parse(result[0]);
                    //tempHour += 12;
                    //result[0] = tempHour.ToString();
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = "PM";
                }
            }

            else if (hour.ToLower().Contains("m"))
            {
                hour = hour.ToLower().Trim();
                if (hour.ToLower().Contains("am"))
                {
                    int index = hour.IndexOf("am");
                    hour = hour.Trim();
                    hour = hour.Remove(index);
                    result = hour.Split(":");
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = "AM";
                }
                else if (hour.ToLower().Contains("pm"))
                {
                    int index = hour.IndexOf("pm");
                    hour = hour.Remove(index);
                    hour = hour.Trim();
                    result = hour.Split(":");
                    Array.Resize(ref result, result.Length + 1);
                    result[result.Length - 1] = "PM";
                }
            }

            return result;
        }

    }
}
