using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    public struct PersianDateTime
    {
        private readonly DateTime _dateData;
        private PersianCalendar _persianCalendar = new PersianCalendar();
        private readonly DateTime MinValue = new DateTime(622, 3, 22);

        public PersianDateTime() => _dateData = MinValue;
        public PersianDateTime(long ticks) : this(new DateTime(ticks)) { }
        public PersianDateTime(DateTime dateTime) => _dateData = (dateTime >= MinValue) ? dateTime : MinValue;
        public PersianDateTime(int year, int month, int day) : this(year, month, day, 0, 0, 0, 0) { }
        public PersianDateTime(int year, int month, int day, int hour, int minute, int second) : this(year, month, day, hour, minute, second, 0) { }
        public PersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond) => _dateData = new DateTime(year, month, day, hour, minute, second, millisecond, _persianCalendar);

        public static PersianDateTime Now => new PersianDateTime(DateTime.Now);
        public static PersianDateTime Today => Now.Date;

        public PersianDateTime Date => new PersianDateTime(_dateData.Date);
        public TimeSpan TimeOfDay => _dateData.TimeOfDay;

        public long Ticks => _dateData.Ticks;

        public int Millisecond => _dateData.Millisecond;
        public int Second => _dateData.Second;
        public int Minute => _dateData.Minute;
        public int Hour => _dateData.Hour;
        public string TimePeriod => (Hour < 12) ? "قبل از ظهر" : "بعد از ظهر";
        public int Day => _persianCalendar.GetDayOfMonth(_dateData);
        public int Month => _persianCalendar.GetMonth(_dateData);
        public int Year => _persianCalendar.GetYear(_dateData);

        public PersianDayOfWeek DayOfWeek => (PersianDayOfWeek)(int)_dateData.DayOfWeek;
        public PersianMonth MonthOfYear => (PersianMonth)Month;

        public int DayOfYear => _persianCalendar.GetDayOfYear(_dateData);
        public int DaysInMonth => _persianCalendar.GetDaysInMonth(Year, Month);
        public int DaysInYear => _persianCalendar.GetDaysInYear(Year);

        public override string ToString() => ConvertToPersianNumbers($"{Year}/{Month:D2}/{Day:D2} {Hour:D2}:{Minute:D2}:{Second:D2}");

        public string ToString(string? format)
        {
            if (!string.IsNullOrEmpty(format) && format.Length > 1)
            {
                format = format.Replace("ss", Second.ToString("D2")).Replace("s", Second.ToString()).Replace("mm", Minute.ToString("D2")).Replace("m", Minute.ToString()).Replace("hh", Hour.ToString("D2")).Replace("h", Hour.ToString()).Replace("HH", Hour.ToString("D2")).Replace("H", Hour.ToString("D2")).Replace("dddd", DayOfWeek.ToString()).Replace("ddd", DayOfWeek.ToString()).Replace("dd", Day.ToString("D2")).Replace("d", Day.ToString()).Replace("MMMM", MonthOfYear.ToString()).Replace("MMM", MonthOfYear.ToString()).Replace("MM", Month.ToString("D2")).Replace("M", Month.ToString()).Replace("yyyy", Year.ToString()).Replace("yyy", Year.ToString()).Replace("yy", (Year % 100).ToString("D2")).Replace("y", (Year % 100).ToString()).Replace("tt", TimePeriod).Replace("t", (Hour < 12) ? "ق.ظ" : "ب.ظ");
            }
            else if (!string.IsNullOrEmpty(format))
            {
                format = format.Replace("d", $"{Year}/{Month:D2}/{Day:D2}").Replace("D", $"{DayOfWeek}, {Day:D2} {MonthOfYear}, {Year}").Replace("f", $"{DayOfWeek}, {Day:D2} {MonthOfYear}, {Year} {Hour:D2}:{Minute:D2}").Replace("F", $"{DayOfWeek}, {Day:D2} {MonthOfYear}, {Year} {Hour:D2}:{Minute:D2}:{Second:D2}").Replace("g", $"{Year}/{Month}/{Day} {Hour}:{Minute:D2}").Replace("G", $"{Year}/{Month}/{Day} {Hour}:{Minute:D2}:{Second:D2}").Replace("m", $"{MonthOfYear} {Day:D2}").Replace("M", $"{MonthOfYear} {Day:D2}").Replace("t", $"{Hour:D2}:{Minute:D2}").Replace("T", $"{Hour:D2}:{Minute:D2}:{Second:D2}").Replace("y", $"{MonthOfYear} {Year}").Replace("Y", $"{MonthOfYear} {Year}");
            }

            return ConvertToPersianNumbers(format);
        }

        public string ToLongDateString() => ToString("D");
        public string ToLongTimeString() => ToString("T");
        public string ToShortDateString() => ToString("d");
        public string ToShortTimeString() => ToString("t");

        public static bool IsLeapYear(int year) => new PersianCalendar().IsLeapYear(year);

        public PersianDateTime Add(TimeSpan value) => new PersianDateTime(_dateData.Add(value));
        public PersianDateTime AddMilliseconds(double value) => new PersianDateTime(_dateData.AddMilliseconds(value));
        public PersianDateTime AddSeconds(double value) => new PersianDateTime(_dateData.AddSeconds(value));
        public PersianDateTime AddMinutes(double value) => new PersianDateTime(_dateData.AddMinutes(value));
        public PersianDateTime AddHours(double value) => new PersianDateTime(_dateData.AddHours(value));
        public PersianDateTime AddDays(double value) => new PersianDateTime(_dateData.AddDays(value));
        public PersianDateTime AddMonths(int months) => new PersianDateTime(_dateData.AddMonths(months));
        public PersianDateTime AddYears(int value) => new PersianDateTime(_dateData.AddYears(value));

        public static int Compare(PersianDateTime t1, PersianDateTime t2) => DateTime.Compare(t1._dateData, t2._dateData);

        public static PersianDateTime operator +(PersianDateTime d, TimeSpan t) => new PersianDateTime(d._dateData + t);
        public static PersianDateTime operator -(PersianDateTime d, TimeSpan t) => new PersianDateTime(d._dateData - t);
        public static TimeSpan operator -(PersianDateTime d1, PersianDateTime d2) => new TimeSpan(d1.Ticks - d2.Ticks);
        public static bool operator ==(PersianDateTime d1, PersianDateTime d2) => d1.Ticks == d2.Ticks;
        public static bool operator !=(PersianDateTime d1, PersianDateTime d2) => d1.Ticks != d2.Ticks;
        public static bool operator <(PersianDateTime t1, PersianDateTime t2) => t1.Ticks < t2.Ticks;
        public static bool operator <=(PersianDateTime t1, PersianDateTime t2) => t1.Ticks <= t2.Ticks;
        public static bool operator >(PersianDateTime t1, PersianDateTime t2) => t1.Ticks > t2.Ticks;
        public static bool operator >=(PersianDateTime t1, PersianDateTime t2) => t1.Ticks >= t2.Ticks;


        public override bool Equals([NotNullWhen(true)] object? value)
        {
            if (value is PersianDateTime)
            {
                return Ticks == ((PersianDateTime)value).Ticks;
            }
            return false;
        }
        public bool Equals(PersianDateTime value) => Ticks == value.Ticks;
        public static bool Equals(PersianDateTime t1, PersianDateTime t2) => t1.Ticks == t2.Ticks;

        public override int GetHashCode() => _dateData.GetHashCode();

        private static string ConvertToPersianNumbers(string input)
        {
            char[] persianDigits = { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            return new string(input.Select(c => char.IsDigit(c) ? persianDigits[c - '0'] : c).ToArray());
        }
    }


    public enum PersianDayOfWeek
    {
        یکشنبه = 0,
        دوشنبه = 1,
        سه‌شنبه = 2,
        چهارشنبه = 3,
        پنجشنبه = 4,
        جمعه = 5,
        شنبه = 6
    }
    public enum PersianMonth
    {
        فروردین = 1,
        اردیبهشت = 2,
        خرداد = 3,
        تیر = 4,
        مرداد = 5,
        شهریور = 6,
        مهر = 7,
        آبان = 8,
        آذر = 9,
        دی = 10,
        بهمن = 11,
        اسفند = 12
    }
}
