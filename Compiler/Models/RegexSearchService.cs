using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public class RegexSearchService
    {
        public List<RegexSearchResult> Search(string text, string pattern)
        {
            var results = new List<RegexSearchResult>();
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return results;

            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                CalculateLineAndPosition(text, match.Index, out int line, out int posInLine);

                results.Add(new RegexSearchResult
                {
                    Fragment = match.Value,
                    Location = $"Строка {line}, позиция {posInLine}",
                    AbsoluteIndex = match.Index,
                    Length = match.Length
                });
            }

            return results;
        }

        private void CalculateLineAndPosition(string text, int absoluteIndex, out int line, out int positionInLine)
        {
            line = 1;
            int lastNewLineIndex = -1;

            for (int i = 0; i < absoluteIndex; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    lastNewLineIndex = i;
                }
            }

            positionInLine = absoluteIndex - lastNewLineIndex;
        }
    }
}
