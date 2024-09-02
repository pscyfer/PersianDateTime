# PersianDateTime

The `PersianDateTime` struct is a powerful alternative to the standard `System.DateTime` structure in C#, specifically designed to handle Persian (Solar Hijri) dates with enhanced functionality and ease of use.

## Features

- Seamless manipulation and representation of Persian calendar dates.
- Constructors for creating PersianDateTime instances with various combinations of year, month, day, hour, minute, second, and millisecond values.
- Properties to access individual components of the date and time, such as day, month, year, hour, minute, and second.
- Support for Persian day of the week and Persian month representation.
- Convenient methods for date formatting and conversion.
- Arithmetic operations for adding or subtracting time intervals.

## Usage

To use the `PersianDateTime` struct in your C# project, simply include the `PersianDateTime.cs` file in your source code. Then, you can create instances of `PersianDateTime` and leverage its properties and methods to work with Persian dates seamlessly.

```csharp
using System;

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
```
