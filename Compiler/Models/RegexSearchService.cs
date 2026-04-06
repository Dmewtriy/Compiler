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
                    Position = $"Строка {line}, позиция {posInLine}",
                    AbsoluteIndex = match.Index,
                    Length = match.Length
                    });
                }

            return results;
        }

        public List<RegexSearchResult> SearchFioWithAutomaton(string text)
        {
            var results = new List<RegexSearchResult>();
            if (string.IsNullOrEmpty(text)) return results;

            int state = 0;
            int startIndex = 0;
            int currentIndex = 0;

            while (currentIndex < text.Length)
            {
                char c = text[currentIndex];

                bool isUpper = char.IsUpper(c) && IsRussian(c);
                bool isLower = char.IsLower(c) && IsRussian(c);
                bool isDot = (c == '.');
                bool isSpace = (c == ' ');

                switch (state)
                {
                    case 0:
                        if (isUpper) { state = 1; startIndex = currentIndex; }
                        break;
                    case 1:
                        state = isDot ? 2 : 0;
                        break;
                    case 2:
                        if (isSpace) state = 6;
                        else if (isUpper) state = 3;
                        else state = 0;
                        break;
                    case 6:
                        state = isUpper ? 3 : 0;
                        break;
                    case 3:
                        state = isDot ? 4 : 0;
                        break;
                    case 4:
                        if (isSpace) state = 7;
                        else if (isUpper) state = 5;
                        else state = 0;
                        break;
                    case 7:
                        state = isUpper ? 5 : 0;
                        break;
                    case 5:
                        if (isLower)
                        {
                            state = 5;
                        }
                        else
                        {
                            SaveResult(text, startIndex, currentIndex, results);
                            state = 0;
                            continue;
                        }
                        break;
                }

                if (state == 0 && startIndex != 0 && !isSpace)
                {
                    currentIndex = startIndex;
                    startIndex = 0;
                }

                currentIndex++;
            }

            if (state == 5)
            {
                SaveResult(text, startIndex, currentIndex, results);
            }

            return results;
        }

        private void SaveResult(string text, int start, int end, List<RegexSearchResult> results)
        {
            int length = end - start;
            string fragment = text.Substring(start, length);

            int line = 1, posInLine = 1;
            for (int i = 0; i < start; i++)
            {
                if (text[i] == '\n') { line++; posInLine = 1; }
                else { posInLine++; }
            }

            results.Add(new RegexSearchResult
            {
                Fragment = fragment,
                Position = $"Строка {line}, поз. {posInLine}",
                AbsoluteIndex = start,
                Length = length
            });
        }

        private bool IsRussian(char c) => (c >= 'А' && c <= 'я') || c == 'Ё' || c == 'ё';

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
