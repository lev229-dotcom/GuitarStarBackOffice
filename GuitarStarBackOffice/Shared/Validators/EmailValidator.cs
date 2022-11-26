using System.Globalization;
using System.Text.RegularExpressions;

namespace GuitarStarBackOffice.Shared.Validators
{
    public static class EmailValidator
    {
        public static bool CheckIsValidEmail(string strIn)
        {
            bool invalid = false;
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            try
            {
                strIn = Regex.Replace(strIn, "(@)(.+)$", new MatchEvaluator(DomainMapper), RegexOptions.None);
            }
            catch
            {
                return false;
            }

            if (invalid)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(strIn, "^([\\w-]+(?:\\.[\\w-]+)*)@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$", RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }

            string DomainMapper(Match match)
            {
                IdnMapping idnMapping = new IdnMapping();
                string text = match.Groups[2].Value;
                try
                {
                    text = idnMapping.GetAscii(text);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }

                return match.Groups[1].Value + text;
            }
        }
    }
}
