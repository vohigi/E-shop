namespace EShop.Data.Helpers
{
    public static class TextHelper
    {
        public static string ConvertToFullName(string firstName, string secondName, string lastName)
        {
            return $"{lastName}{ExistingName(firstName)}{ExistingName(secondName)}";
        }

        public static string GetShortString(string text, int length = 20)
        {
            return !string.IsNullOrEmpty(text) && text.Length > length
                ? text.Substring(0, length) + "..." 
                : text ?? "";
        }
        
        private static string ExistingName(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : $" {name}";
        }
        
    }
}