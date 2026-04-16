namespace Compiler.Models
{
    public class SyntaxError
    {
        public string Fragment { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public int AbsoluteIndex { get; set; }
        public int Length { get; set; }
    }

    public class Parser
    {
        private List<Token> _tokens;
        private int _index;
        public List<SyntaxError> Errors { get; } = new List<SyntaxError>();

        private Token CurrentToken => _index < _tokens.Count ? _tokens[_index] : null;
        private bool CurrentTokenIsEOF => CurrentToken.Code == TokenCodes.EOF;


        public void Parse(List<Token> tokens)
        {
            _tokens = tokens.Where(t => t.Code != TokenCodes.SPACE).ToList();
            if (_tokens.Count == 0) return;
            Errors.Clear();
            _index = 0;
            var lastToken = _tokens.LastOrDefault();
            _tokens.Add(new Token
            {
                Type = TokenType.EOF,
                Code = TokenCodes.EOF,
                Lexeme = "EOF",
                Line = lastToken != null ? lastToken.Line : 1,
                StartPos = lastToken != null ? lastToken.EndPos + 1 : 1,
                AbsoluteIndex = lastToken != null ? lastToken.AbsoluteIndex + lastToken.Lexeme.Length : 0,
                EndPos = lastToken != null ? lastToken.EndPos + 4 : 4
            });

            while (_index < _tokens.Count && _tokens[_index].Code != TokenCodes.EOF)
            {
                ParseZ();
            }
        }

        private void Match(TokenCodes expectedCode, params TokenCodes[] followSet)
        {
            if (CurrentToken != null && CurrentToken.Code == expectedCode)
            {
                _index++;
            }
            else
            {
                Token errorToken = CurrentToken ?? _tokens.LastOrDefault();
                string lexeme = errorToken?.Lexeme ?? "Конец файла (EOF)";

                Errors.Add(new SyntaxError
                {
                    Fragment = lexeme,
                    Location = $"Строка: {errorToken?.Line ?? 0}, Позиция: {errorToken?.StartPos ?? 0}",
                    Description = $"Ожидался токен {expectedCode}, но найден {lexeme}",
                    AbsoluteIndex = errorToken?.AbsoluteIndex ?? 0,
                    Length = errorToken?.Lexeme?.Length ?? 1
                });

                Recover(followSet);
            }
        }

        private void Recover(TokenCodes[] followSet)
        {
            if (followSet == null || followSet.Length == 0) return;

            while (CurrentToken != null)
            {
                if (followSet.Contains(CurrentToken.Code))
                {
                    if (CurrentToken.Code == TokenCodes.ID && (_index + 1 < _tokens.Count ? _tokens[_index + 1] : null).Code == TokenCodes.ID) _index++;
                    return;
                }
                if (_index < _tokens.Count - 1) _index++;
                if (followSet.Contains(CurrentToken.Code))
                {
                    return;
                }
            }
        }

        private void ParseZ()
        {
            Match(TokenCodes.ENUM, TokenCodes.ID, TokenCodes.EOF);
            if (!EnsureNotEOF()) return;
            Match(TokenCodes.ID, TokenCodes.LBRACE, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);

            if (!EnsureNotEOF()) return;
            Match(TokenCodes.LBRACE, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);

            if (!EnsureNotEOF()) return;
            ParseEnumBody();

            if (!EnsureNotEOF()) return;
            Match(TokenCodes.RBRACE, TokenCodes.SEMICOLON, TokenCodes.ID, TokenCodes.EOF);

            Match(TokenCodes.SEMICOLON, TokenCodes.ENUM, TokenCodes.EOF);
        }
        private bool EnsureNotEOF()
        {
            if (CurrentTokenIsEOF)
            {
                EndOfFileHasCome();
                return false;
            }
            return true;
        }
        private void EndOfFileHasCome()
        {
            Token errorToken = CurrentToken ?? _tokens.LastOrDefault();
            string lexeme = errorToken?.Lexeme ?? "Конец файла (EOF)";

            Errors.Add(new SyntaxError
            {
                Fragment = lexeme,
                Location = $"Строка: {errorToken?.Line ?? 0}, Позиция: {errorToken?.StartPos ?? 0}",
                Description = $"Незаконченное выражение",
                AbsoluteIndex = errorToken?.AbsoluteIndex ?? 0,
                Length = errorToken?.Lexeme?.Length ?? 1
            });
        }

        private void ParseEnumBody()
        {
            if (CurrentToken != null && CurrentToken.Code == TokenCodes.EOF) return;
            else if (CurrentToken != null)
            {
                ParseCases();
            }
        }

        private void ParseCases()
        {
            while (CurrentToken != null && CurrentToken.Code != TokenCodes.EOF && CurrentToken.Code != TokenCodes.SEMICOLON && CurrentToken.Code != TokenCodes.RBRACE)
            {
                ParseCase();
            }
        }

        private void ParseCase()
        {
            Match(TokenCodes.CASE, TokenCodes.ID, TokenCodes.SEMICOLON, TokenCodes.EOF);
            Match(TokenCodes.ID, TokenCodes.SEMICOLON, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);
            Match(TokenCodes.SEMICOLON, TokenCodes.CASE, TokenCodes.ID, TokenCodes.RBRACE, TokenCodes.EOF);
        }
    }
}