using System.Globalization;
using System.Reflection;

namespace ParsianNews.Utilities;

public static class DateConvertor
{
    private static CultureInfo? _culture;
    public static CultureInfo GetPersianCulture()
    {
        if (_culture == null)
        {
            _culture = new CultureInfo("fa-IR");
            DateTimeFormatInfo formatInfo = _culture.DateTimeFormat;
            formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            var monthNames = new[]
            {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
            formatInfo.AbbreviatedMonthNames =
                formatInfo.MonthNames =
                formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
            formatInfo.AMDesignator = "ق.ظ";
            formatInfo.PMDesignator = "ب.ظ";
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
            Calendar cal = new PersianCalendar();

            FieldInfo fieldInfo = _culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance)!;
            if (fieldInfo != null)
                fieldInfo.SetValue(_culture, cal);

            FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance)!;
            if (info != null)
                info.SetValue(formatInfo, cal);

            _culture.NumberFormat.NumberDecimalSeparator = "/";
            _culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            _culture.NumberFormat.NumberNegativePattern = 0;
        }
        return _culture;
    }

    public static string ToShamsi(this DateTime date, string format = "dddd dd MMMM yyyy")
    {
        return date.ToString(format, GetPersianCulture());
    }
}