using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace PersianDateTime;

public readonly struct PersianDateTime : IComparable, IComparable<PersianDateTime>, IEquatable<PersianDateTime>
{
    private readonly DateTime dateData;

    private static readonly DateTime MinValue = new DateTime(622, 3, 22);
    private static readonly PersianCalendar persianCalendar = new PersianCalendar();


    public PersianDateTime(DateTime dateTime)
    {
        dateData = dateTime >= MinValue ? dateTime : MinValue;

        TimeOfDay = dateData.TimeOfDay;
        Kind = dateData.Kind;
        Ticks = dateData.Ticks;
        Nanosecond = dateData.Nanosecond;
        Microsecond = dateData.Microsecond;
        Millisecond = dateData.Millisecond;
        Second = dateData.Second;
        Minute = dateData.Minute;
        Hour = dateData.Hour;
        TimePeriod = Hour < 12 ? "قبل از ظهر" : "بعد از ظهر";
        Day = persianCalendar.GetDayOfMonth(dateData);
        Month = persianCalendar.GetMonth(dateData);
        Year = persianCalendar.GetYear(dateData);
        DayOfWeek = (PersianDayOfWeek)(int)dateData.DayOfWeek;
        MonthOfYear = (PersianMonth)Month;
    }
    public PersianDateTime() : this(MinValue) { }
    public PersianDateTime(long ticks) : this(new DateTime(ticks)) { }
    public PersianDateTime(int year, int month, int day) : this(year, month, day, 0, 0, 0, 0) { }
    public PersianDateTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute, second, 0) { }
    public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond) : this(new DateTime(year, month, day, hour, minute, second, millisecond, persianCalendar)) { }
    public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond) : this(new DateTime(year, month, day, hour, minute, second, millisecond, microsecond, persianCalendar)) { }


    public static PersianDateTime Now => new PersianDateTime(DateTime.Now);
    public static PersianDateTime UtcNow => new PersianDateTime(DateTime.UtcNow);
    public static PersianDateTime Today => new PersianDateTime(DateTime.Today);

    public PersianDateTime Date => new PersianDateTime(dateData.Date);
    public TimeSpan TimeOfDay { get; }
    public DateTimeKind Kind { get; }

    public long Ticks { get; }

    public int Nanosecond { get; }
    public int Microsecond { get; }
    public int Millisecond { get; }
    public int Second { get; }
    public int Minute { get; }
    public int Hour { get; }
    public string TimePeriod { get; }
    public int Day { get; }
    public int Month { get; }
    public int Year { get; }

    public PersianDayOfWeek DayOfWeek { get; }
    public PersianMonth MonthOfYear { get; }


    public override string ToString() => ConvertToPersianNumbers($"{Year}/{Month:D2}/{Day:D2} {Hour:D2}:{Minute:D2}:{Second:D2}");
    public string ToLongDateString() => ToString("D");
    public string ToLongTimeString() => ToString("T");
    public string ToShortDateString() => ToString("d");
    public string ToShortTimeString() => ToString("t");

    public static bool IsLeapYear(int year) => persianCalendar.IsLeapYear(year);

    public PersianDateTime Add(TimeSpan value) => new PersianDateTime(dateData.Add(value));
    public PersianDateTime AddTicks(long value) => new PersianDateTime(dateData.AddTicks(value));
    public PersianDateTime AddMicroseconds(double value) => new PersianDateTime(dateData.AddMicroseconds(value));
    public PersianDateTime AddMilliseconds(double value) => new PersianDateTime(dateData.AddMilliseconds(value));
    public PersianDateTime AddSeconds(double value) => new PersianDateTime(dateData.AddSeconds(value));
    public PersianDateTime AddMinutes(double value) => new PersianDateTime(dateData.AddMinutes(value));
    public PersianDateTime AddHours(double value) => new PersianDateTime(dateData.AddHours(value));
    public PersianDateTime AddDays(double value) => new PersianDateTime(dateData.AddDays(value));
    public PersianDateTime AddMonths(int months) => new PersianDateTime(dateData.AddMonths(months));
    public PersianDateTime AddYears(int value) => new PersianDateTime(dateData.AddYears(value));

    public PersianDateTime Subtract(TimeSpan value) => new PersianDateTime(dateData.Subtract(value));
    public TimeSpan Subtract(PersianDateTime value) => dateData.Subtract(value.dateData);

    public static int DayOfYear(PersianDateTime value) => persianCalendar.GetDayOfYear(value.dateData);
    public static int DaysInMonth(int year, int month) => persianCalendar.GetDaysInMonth(year, month);
    public static int DaysInYear(int year) => persianCalendar.GetDaysInYear(year);

    public static int Compare(PersianDateTime t1, PersianDateTime t2) => DateTime.Compare(t1.dateData, t2.dateData);
    public int CompareTo(object? value) => dateData.CompareTo(value);
    public int CompareTo(PersianDateTime value) => dateData.CompareTo(value.dateData);

    public static bool Equals(PersianDateTime t1, PersianDateTime t2) => t1.Ticks == t2.Ticks;
    public bool Equals(PersianDateTime value) => Ticks == value.Ticks;
    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is PersianDateTime))
            return false;

        if (Object.Equals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        PersianDateTime item = (PersianDateTime)obj;

        if (item.Ticks == default || this.Ticks == default)
            return false;
        else
            return item.Ticks == this.Ticks;
    }


    public static PersianDateTime operator +(PersianDateTime d, TimeSpan t) => new PersianDateTime(d.dateData + t);
    public static PersianDateTime operator -(PersianDateTime d, TimeSpan t) => new PersianDateTime(d.dateData - t);
    public static TimeSpan operator -(PersianDateTime d1, PersianDateTime d2) => new TimeSpan(d1.Ticks - d2.Ticks);
    public static bool operator ==(PersianDateTime d1, PersianDateTime d2) => d1.Ticks == d2.Ticks;
    public static bool operator !=(PersianDateTime d1, PersianDateTime d2) => d1.Ticks != d2.Ticks;
    public static bool operator <(PersianDateTime t1, PersianDateTime t2) => t1.Ticks < t2.Ticks;
    public static bool operator <=(PersianDateTime t1, PersianDateTime t2) => t1.Ticks <= t2.Ticks;
    public static bool operator >(PersianDateTime t1, PersianDateTime t2) => t1.Ticks > t2.Ticks;
    public static bool operator >=(PersianDateTime t1, PersianDateTime t2) => t1.Ticks >= t2.Ticks;


    public static PersianDateTime Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input));

        if (TryParse(input, out PersianDateTime result))
            return result;

        throw new FormatException("The input string was not in a correct format.");
    }
    public static bool TryParse(string input, out PersianDateTime result)
    {
        result = default;

        if (string.IsNullOrWhiteSpace(input)) return false;

        try
        {
            char[] dateSeparators = ['/', '-', ' '];
            char[] timeSeparators = [':', ' '];

            string[] dateTimeParts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (dateTimeParts.Length == 0)
                return false;

            string[] dateParts = dateTimeParts[0].Split(dateSeparators, StringSplitOptions.RemoveEmptyEntries);

            if (dateParts.Length != 3)
                return false;

            if (!int.TryParse(dateParts[0], out int year) || !int.TryParse(dateParts[1], out int month) || !int.TryParse(dateParts[2], out int day))
                return false;

            int hour = 0, minute = 0, second = 0, millisecond = 0;

            if (dateTimeParts.Length > 1)
            {
                string[] timeParts = dateTimeParts[1].Split(timeSeparators, StringSplitOptions.RemoveEmptyEntries);

                if (timeParts.Length > 0 && (!int.TryParse(timeParts[0], out hour) || hour < 0 || hour > 23))
                    return false;
                if (timeParts.Length > 1 && (!int.TryParse(timeParts[1], out minute) || minute < 0 || minute > 59))
                    return false;
                if (timeParts.Length > 2 && (!int.TryParse(timeParts[2], out second) || second < 0 || second > 59))
                    return false;
                if (timeParts.Length > 3 && (!int.TryParse(timeParts[3], out millisecond) || millisecond < 0 || millisecond > 999))
                    return false;
            }

            if (dateTimeParts.Length > 2)
            {
                string amPmIndicator = dateTimeParts[2];

                if (amPmIndicator == "ب.ظ" || amPmIndicator == "PM")
                {
                    if (hour < 12)
                        hour += 12;
                }
                else if (amPmIndicator == "ق.ظ" || amPmIndicator == "AM")
                {
                    if (hour == 12)
                        hour = 0;
                }
            }

            DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, persianCalendar);

            result = new PersianDateTime(dateTime);

            return true;
        }
        catch
        {
            return false;
        }
    }


    public string ToString([StringSyntax("DateTimeFormat")] string? format)
    {
        if (string.IsNullOrEmpty(format)) return ToString();

        var s = new StringBuilder(format);

        if (format.Length > 1)
        {
            s.Replace("ss", Second.ToString("D2"))
              .Replace("s", Second.ToString())
              .Replace("mm", Minute.ToString("D2"))
              .Replace("m", Minute.ToString())
              .Replace("hh", Hour.ToString("D2"))
              .Replace("h", Hour.ToString())
              .Replace("HH", Hour.ToString("D2"))
              .Replace("H", Hour.ToString())
              .Replace("dddd", ToDisplay(DayOfWeek))
              .Replace("ddd", ToDisplay(DayOfWeek))
              .Replace("dd", Day.ToString("D2"))
              .Replace("d", Day.ToString())
              .Replace("MMMM", MonthOfYear.ToString())
              .Replace("MMM", MonthOfYear.ToString())
              .Replace("MM", Month.ToString("D2"))
              .Replace("M", Month.ToString())
              .Replace("yyyy", Year.ToString())
              .Replace("yyy", Year.ToString())
              .Replace("yy", (Year % 100).ToString("D2"))
              .Replace("y", (Year % 100).ToString())
              .Replace("tt", TimePeriod)
              .Replace("t", Hour < 12 ? "ق.ظ" : "ب.ظ");

            return ConvertToPersianNumbers(s.ToString());
        }
        else
        {
            s.Replace("d", $"{Year}/{Month:D2}/{Day:D2}")
              .Replace("D", $"{ToDisplay(DayOfWeek)}, {Day:D2} {MonthOfYear}, {Year}")
              .Replace("f", $"{ToDisplay(DayOfWeek)}, {Day:D2} {MonthOfYear}, {Year} {Hour:D2}:{Minute:D2}")
              .Replace("F", $"{ToDisplay(DayOfWeek)}, {Day:D2} {MonthOfYear}, {Year} {Hour:D2}:{Minute:D2}:{Second:D2}")
              .Replace("g", $"{Year}/{Month}/{Day} {Hour}:{Minute:D2}")
              .Replace("G", $"{Year}/{Month}/{Day} {Hour}:{Minute:D2}:{Second:D2}")
              .Replace("m", $"{MonthOfYear} {Day:D2}")
              .Replace("M", $"{MonthOfYear} {Day:D2}")
              .Replace("t", $"{Hour:D2}:{Minute:D2}")
              .Replace("T", $"{Hour:D2}:{Minute:D2}:{Second:D2}")
              .Replace("y", $"{MonthOfYear} {Year}")
              .Replace("Y", $"{MonthOfYear} {Year}");

            return ConvertToPersianNumbers(s.ToString());
        }
    }


    public override int GetHashCode() => dateData.GetHashCode();


    private static string ConvertToPersianNumbers(string input)
    {
        char[] persianDigits = { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
        return new string(input.Select(c => char.IsDigit(c) ? persianDigits[c - '0'] : c).ToArray());
    }


    static string ToDisplay(Enum value)
    {
        var attribute = value.GetType().GetField(value.ToString())
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return value.ToString();

        var propValue = attribute.GetType().GetProperty("Name").GetValue(attribute, null);
        return propValue.ToString();
    }
}


public enum PersianDayOfWeek
{
    [Display(Name = "یکشنبه")]
    YekShanbe,
    [Display(Name = "دو شنبه")]
    Doshanbe,
    [Display(Name = "سه شنبه")]
    Seshanbe,
    [Display(Name = "چهارشنبه")]
    CharChange,
    [Display(Name = "پنجشنبه")]
    Panjshanbe,
    [Display(Name = "جمعه")]
    Jome,
    [Display(Name = "شنبه")]
    Shanbe
}
public enum PersianMonth
{
    فروردین = 1,
    اردیبهشت,
    خرداد,
    تیر,
    مرداد,
    شهریور,
    مهر,
    آبان,
    آذر,
    دی,
    بهمن,
    اسفند
}
