using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace Compiler.Models
{
    public enum TokenType
    {
        KEYWORD = 1,
        IDENTIFIER = 2,
        DELIMITER = 3,
        END_OPERATOR = 4,
        WHITESPACE = 5,
        INVALID_TOKEN = 6,
        EOF = 7
    }

    public enum TokenCodes
    {
        ID = 1,
        ENUM = 2,
        CASE = 3,
        SPACE = 4,
        LBRACE = 5,
        RBRACE = 6,
        SEMICOLON = 7,
        ERROR = 8,
        EOF = 9
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public TokenCodes Code { get; set; }
        public string Lexeme { get; set; }
        public int Line { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public int AbsoluteIndex { get; set; }

        public int NumericCode => (int)Code;
        public string TypeName => Type switch
        {
            TokenType.KEYWORD => "Ключевое слово",
            TokenType.IDENTIFIER => "Идентификатор",
            TokenType.DELIMITER => "Разделитель",
            TokenType.END_OPERATOR => "Конец оператора",
            TokenType.INVALID_TOKEN => "Недопустимый символ",
            TokenType.WHITESPACE => "Пробел",
            TokenType.EOF => "Конец файла",
            _ => "Неизвестно"
        };
    }

    public class Scanner
    {
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

                if (c == ' ' || c == '\t' || c == '\r')
                {
                    string spaces = "";
                    while (i < input.Length && (input[i] == ' ' || input[i] == '\t' || input[i] == '\r'))
                    {
                        spaces += input[i];
                        i++; col++;
                    }
                    tokens.Add(new Token
                    {
                        Type = TokenType.WHITESPACE,
                        Code = TokenCodes.SPACE,
                        Lexeme = spaces,
                        Line = line,
                        StartPos = startCol,
                        EndPos = col - 1,
                        AbsoluteIndex = startAbs
                    });
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

                    TokenCodes code = lexeme switch
                    {
                        "enum" => TokenCodes.ENUM,
                        "case" => TokenCodes.CASE,
                        _ => TokenCodes.ID
                    };

                    TokenType type = (code == TokenCodes.ID) ? TokenType.IDENTIFIER : TokenType.KEYWORD;
                    tokens.Add(new Token 
                    { 
                        Type = type,
                        Code = code,
                        Lexeme = lexeme, 
                        Line = line, 
                        StartPos = startCol, 
                        EndPos = col - 1, 
                        AbsoluteIndex = startAbs 
                    });
                    continue;
                }

                if (c == '{' || c == '}' || c == ';')
                {
                    TokenCodes code = c switch
                    {
                        '{' => TokenCodes.LBRACE,
                        '}' => TokenCodes.RBRACE,
                        ';' => TokenCodes.SEMICOLON,
                        _ => TokenCodes.ERROR
                    };

                    TokenType type = (c == ';') ? TokenType.END_OPERATOR : TokenType.DELIMITER;
                    tokens.Add(new Token
                    {
                        Type = type,
                        Code = code,
                        Lexeme = c.ToString(),
                        Line = line,
                        StartPos = startCol,
                        EndPos = col,
                        AbsoluteIndex = startAbs
                    });
                    i++; col++;
                    continue;
                }

                tokens.Add(new Token { Type = TokenType.INVALID_TOKEN, Code = TokenCodes.ERROR, Lexeme = c.ToString(), Line = line, StartPos = startCol, EndPos = col, AbsoluteIndex = startAbs });
                i++; col++;
            }

            return tokens;
        }
    }
}
