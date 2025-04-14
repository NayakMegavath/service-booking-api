using System.Text.RegularExpressions;

namespace ServiceApp.Helpers
{
    public class UtilityHelper
    {
        public static bool IsValidEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Basic email format validation using regex
            return Regex.IsMatch(input, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }

        public static bool IsValidPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Basic phone number format validation (adjust regex as needed for your expected format)
            // This example checks for 10-15 digits, optionally with hyphens or spaces
            return Regex.IsMatch(input, @"^\d{10,15}$|^\d{3}-\d{3}-\d{4}$|^\d{3} \d{3} \d{4}$");
        }
    }
}
