using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VideoFragmentCodeChallenge.DataModel;
using VideoFragmentCodeChallenge.Services.Exceptions;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge.Services.Implementations
{
    /* Coding Challenge: Things I thought of
     * 
     *      Since it's realistic that someone would input a comma in 
     */

    internal class FragmentParserService : IFragmentParserService
    {
        private readonly Regex digitRegex = new Regex(@"^(\d+):(\d+)$", RegexOptions.Compiled);
        private readonly Regex trimRegex = new Regex(@"[\s+]?[,]?[.]?", RegexOptions.Compiled);

        public Fragment Parse(string fragmentAsLine)
        {
            if (fragmentAsLine == null)
            {
                throw new ArgumentNullException(nameof(fragmentAsLine));
            }

            // Removes Whitespace, Commas, and Periods: 
            //      "   2,341  :10.299" => "2341:10299"
            var trimmedFragmentAsLine = trimRegex.Replace(fragmentAsLine, "");

            // Obtains 3 groups: the match itself, start time fragment, end time fragment:
            //      "2341:10299" => [{"2341:10299", "2341", "10299"}]
            var matchedValues = digitRegex.Match(trimmedFragmentAsLine);

            if (matchedValues.Groups.Count != 3)
            {
                throw new FragmentParserServiceException($"Error parsing fragment from line \"{fragmentAsLine}\": line is not in the correct format. Expected: \"<digits> : <digits>\"");
            }

            var newFragment = new Fragment();

            try
            {
                newFragment.StartTime = int.Parse(matchedValues.Groups[1].Value);
                newFragment.EndTime = int.Parse(matchedValues.Groups[2].Value);
            }
            catch (Exception ex)
            {
                throw new FragmentParserServiceException($"Error parsing fragment from line \"{fragmentAsLine}\": Failed to parse number.", ex);
            }

            if (newFragment.EndTime < newFragment.StartTime)
            {
                throw new FragmentParserServiceException($"Error parsing fragment from line \"{fragmentAsLine}\": Fragment StartTime cannot be greater than fragment EndTime.");
            }

            return newFragment;
        }
    }
}
