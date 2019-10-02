namespace PBS.Business.Utilities.Helpers
{
    public static class StringHelpers
    {
        public static string[] FindWords (string input)
        {
            if (!string.IsNullOrEmpty (input) && !string.IsNullOrWhiteSpace (input))
            {
                return input.Split (' ');
            }

            return null;
        }
    }
}
