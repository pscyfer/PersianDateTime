using Persian.Date;

// Example usage
// Create a PersianDateTime instance with the current date and time
PersianDateTime now = PersianDateTime.Now;
Console.WriteLine(now.ToString("F")); // Output: دوشنبه, ۱۳۹۹/۰۷/۰۶ ۱۵:۳۰:۲۵

// Create a PersianDateTime instance with a specific date
PersianDateTime persianDate = new PersianDateTime(1400, 1, 1);
Console.WriteLine(persianDate.ToString("D")); // Output: ۱۴۰۰/۰۱/۰۱

// Add a time interval to a PersianDateTime
PersianDateTime futureDate = persianDate.AddDays(7);
Console.WriteLine(futureDate.ToString("D")); // Output: ۱۴۰۰/۰۱/۰۸

// Format a PersianDateTime as a string
string formattedDate = persianDate.ToString("yyyy/MM/dd"); // "۱۴۰۰/۰۱/۰۸"

// Check if a year is a leap year in the Persian calendar
bool isLeapYear = PersianDateTime.IsLeapYear(1400); // true