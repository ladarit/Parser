using System.Text.RegularExpressions;

namespace GovernmentParse.Helpers
{
    public static class InputValidator
    {
        public static string Сorrect(string text)
        {
            var lawNumbers = Regex.Matches(text.RemoveOddSpaces(), @"\S+[^\s\.\;\:\-\+]");
            text = string.Empty;
            foreach (var law in lawNumbers)
                text += law;
            return Regex.Replace(text, @"\,$", string.Empty);
        }
    }
}
