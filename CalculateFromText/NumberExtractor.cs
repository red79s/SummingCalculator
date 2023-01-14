using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculateFromText
{
    public class NumberExtractor
    {
        private Regex _numRegex = new Regex("[0-9,. ]+");
        public double GetNumberFromLine(string line)
        {
            line = line.Trim().Replace((char)160, ' ').Replace(" ", "").Replace("\t", "");
            var matches = _numRegex.Matches(line);
            if (matches.Count == 0)
                return 0;

            double result = 0;

            foreach (var match in matches.ToList())
            {
                if (!match.Success)
                    continue;

                if (string.IsNullOrWhiteSpace(match.Value))
                    continue;

                var strValue = match.Value.Trim().Trim('.').Trim(',');

                if (DateTime.TryParse(strValue, out DateTime dateTime))
                {
                    if (dateTime > DateTime.Now.AddMonths(-12) && dateTime < DateTime.Now.AddMonths(12))
                    {
                        continue;
                    }
                }

                var strVal = strValue.Replace(",", ".").Replace(" ", "");
                double.TryParse(strVal, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            }

            return result;
        }
    }
}