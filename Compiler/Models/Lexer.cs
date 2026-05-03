using System.Collections.Generic;

public class Lexer
{
    public List<string> Errors { get; } = new List<string>();

    public List<Token> Tokenize(string text)
    {
        Errors.Clear();
        var tokens = new List<Token>();
        int i = 0;

        while (i < text.Length)
        {
            char c = text[i];
            if (char.IsWhiteSpace(c)) { i++; continue; }

            if (char.IsDigit(c))
            {
                string num = "";
                while (i < text.Length && char.IsDigit(text[i])) { num += text[i++]; }
                tokens.Add(new Token { Type = TokenType.Number, Value = num, Position = i - num.Length });
                continue;
            }

            if (c == '$')
            {
                int start = i;
                string id = "$";
                i++;
                if (i < text.Length && char.IsLetter(text[i]))
                {
                    while (i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'))
                    {
                        id += text[i++];
                    }
                    tokens.Add(new Token { Type = TokenType.Id, Value = id, Position = start });
                }
                else
                {
                    Errors.Add($"Ошибка лексера на поз {start+1}: ожидалась буква после '$'");
                }
                continue;
            }

            switch (c)
            {
                case '+': tokens.Add(new Token { Type = TokenType.Plus, Value = "+", Position = i }); break;
                case '-': tokens.Add(new Token { Type = TokenType.Minus, Value = "-", Position = i }); break;
                case '*': tokens.Add(new Token { Type = TokenType.Mul, Value = "*", Position = i }); break;
                case '/': tokens.Add(new Token { Type = TokenType.Div, Value = "/", Position = i }); break;
                case '%': tokens.Add(new Token { Type = TokenType.Mod, Value = "%", Position = i }); break;
                case '(': tokens.Add(new Token { Type = TokenType.LParen, Value = "(", Position = i }); break;
                case ')': tokens.Add(new Token { Type = TokenType.RParen, Value = ")", Position = i }); break;
                default: Errors.Add($"Неизвестный символ '{c}' на поз {i+1}"); break;
            }
            i++;
        }
        tokens.Add(new Token { Type = TokenType.EOF, Value = "EOF", Position = i });
        return tokens;
    }
}