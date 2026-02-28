using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public enum TokenType
    {
        KEYWORD = 1,
        IDENTIFIER = 2,
        DELIMITER = 3,
        END_OPERATOR = 4,
        WHITESPACE = 5,
        INVALID_TOKEN = 6
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Lexeme { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public int AbsoluteIndex { get; set; }

        public int Code => (int)Type;
        public string TypeName => Type switch
        {
            TokenType.KEYWORD => "Ключевое слово",
            TokenType.IDENTIFIER => "Идентификатор",
            TokenType.DELIMITER => "Разделитель",
            TokenType.END_OPERATOR => "Конец оператора",
            TokenType.INVALID_TOKEN => "Недопустимый символ",
            TokenType.WHITESPACE => "Пробел",
            _ => "Неизвестно"
        };
    }

    public class Scanner
    {
        private readonly HashSet<string> _keywords = new HashSet<string> { "enum", "case" };
        private readonly HashSet<char> _delimiters = new HashSet<char> { '{', '}' };

        public List<Token> Analyze(string input)
        {
            var tokens = new List<Token>();
            int line = 1;
            int col = 1;
            int i = 0;

            while (i < input.Length)
            {
                char c = input[i];

                int startCol = col;
                int startAbs = i;

                if (c == '\n')
                {
                    line++;
                    col = 1;
                    i++;
                    continue;
                }

                if (c == ' ')
                {
                    string spaces = "";
                    while (i < input.Length && input[i] == ' ')
                    {
                        spaces += input[i];
                        i++; col++;
                    }
                    tokens.Add(new Token
                    {
                        Type = TokenType.WHITESPACE,
                        Lexeme = spaces,
                        Line = line,
                        StartPos = startCol,
                        EndPos = col - 1,
                        AbsoluteIndex = startAbs
                    });
                    continue;
                }

                if (c == ';')
                {
                    tokens.Add(new Token
                    {
                        Type = TokenType.END_OPERATOR,
                        Lexeme = ";",
                        Line = line,
                        StartPos = startCol,
                        EndPos = col,
                        AbsoluteIndex = startAbs
                    });
                    col++; i++;
                    continue;
                }

                if (char.IsLetter(c) || c == '_')
                {
                    string lexeme = "";
                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                    {
                        lexeme += input[i];
                        col++; i++;
                    }

                    TokenType type = _keywords.Contains(lexeme) ? TokenType.KEYWORD : TokenType.IDENTIFIER;
                    tokens.Add(new Token { Type = type, Lexeme = lexeme, Line = line, StartPos = startCol, EndPos = col - 1, AbsoluteIndex = startAbs });
                    continue;
                }

                if (_delimiters.Contains(c))
                {
                    tokens.Add(new Token { Type = TokenType.DELIMITER, Lexeme = c.ToString(), Line = line, StartPos = startCol, EndPos = col, AbsoluteIndex = startAbs });
                    col++; i++;
                    continue;
                }

                tokens.Add(new Token { Type = TokenType.INVALID_TOKEN, Lexeme = c.ToString(), Line = line, StartPos = startCol, EndPos = col, AbsoluteIndex = startAbs });
                col++; i++;
            }

            return tokens;
        }
    }
}
