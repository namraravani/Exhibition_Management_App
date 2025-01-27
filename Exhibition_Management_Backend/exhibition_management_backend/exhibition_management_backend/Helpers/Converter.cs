using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace exhibition_management_backend.Helpers
{
    public static class Converter
    {
        public static DateTime ConvertStringToDate(string dateString, string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new ArgumentException("Input date string cannot be null or empty.");
            }

            
            if (DateTime.TryParseExact(
                    dateString,
                    format,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
            {
                return parsedDate;
            }
            else
            {
                throw new FormatException($"Input date string '{dateString}' is not in the correct format '{format}'.");
            }
        }

        public static DateTime ConvertStringToDateOnlyLegacy(string dateString, string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                throw new ArgumentException("Input date string cannot be null or empty.");
            }

            if (DateTime.TryParseExact(
                    dateString,
                    format,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
            {
                // Extract only the date component
                return parsedDate.Date;
            }
            else
            {
                throw new FormatException($"Input date string '{dateString}' is not in the correct format '{format}'.");
            }
        }



    }
}
