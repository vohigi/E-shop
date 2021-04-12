namespace EShop.Data.Helpers
{
    public static class TextHelper
    {
        public static string ConvertToFullName(string firstName, string secondName, string lastName)
        {
            return $"{lastName}{ExistingName(firstName)}{ExistingName(secondName)}";
        }

        private static string ExistingName(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : $" {name}";
        }
    }
}