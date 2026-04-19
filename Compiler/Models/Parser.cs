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


        public List<EnumDeclNode> Parse(List<Token> tokens)
        {
            _tokens = tokens.Where(t => t.Code != TokenCodes.SPACE).ToList();
            if (_tokens.Count == 0) return [];
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

            var astNodes = new List<EnumDeclNode>();

            while (_index < _tokens.Count && _tokens[_index].Code != TokenCodes.EOF)
            {
                var node = ParseZ();
                if (node != null) astNodes.Add(node);
            }

            return astNodes;
        }

        private Token Match(TokenCodes expectedCode, params TokenCodes[] followSet)
        {
            Token current = CurrentToken;
            if (current != null && current.Code == expectedCode)
            {
                _index++;
                return current;
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
                return null;
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

        private EnumDeclNode ParseZ()
        {
            var enumKeyword = Match(TokenCodes.ENUM, TokenCodes.ID, TokenCodes.EOF);
            var idToken = Match(TokenCodes.ID, TokenCodes.LBRACE, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);

            var node = new EnumDeclNode
            {
                Name = idToken?.Lexeme ?? "Unknown",
                Line = enumKeyword?.Line ?? 0,
                Position = enumKeyword?.StartPos ?? 0
            };

            Match(TokenCodes.LBRACE, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);
            node.Cases = ParseEnumBody();

            Match(TokenCodes.RBRACE, TokenCodes.SEMICOLON, TokenCodes.ID, TokenCodes.EOF);

            Match(TokenCodes.SEMICOLON, TokenCodes.ENUM, TokenCodes.EOF);

            return node;
        }


        private List<EnumCaseNode> ParseEnumBody()
        {
            if (CurrentToken != null && (CurrentToken.Code == TokenCodes.RBRACE || CurrentToken.Code == TokenCodes.EOF))
                return new List<EnumCaseNode>();

            return ParseCases();
        }

        private List<EnumCaseNode> ParseCases()
        {
            var cases = new List<EnumCaseNode>();
            while (CurrentToken != null && CurrentToken.Code != TokenCodes.EOF && CurrentToken.Code != TokenCodes.SEMICOLON && CurrentToken.Code != TokenCodes.RBRACE)
            {
                var c = ParseCase();
                if (c != null) cases.Add(c);
            }
            return cases;
        }

        private EnumCaseNode ParseCase()
        {
            Match(TokenCodes.CASE, TokenCodes.ID, TokenCodes.SEMICOLON, TokenCodes.EOF);
            var idToken = Match(TokenCodes.ID, TokenCodes.SEMICOLON, TokenCodes.CASE, TokenCodes.RBRACE, TokenCodes.EOF);
            Match(TokenCodes.SEMICOLON, TokenCodes.CASE, TokenCodes.ID, TokenCodes.SEMICOLON, TokenCodes.RBRACE, TokenCodes.EOF);

            if (idToken == null) return null;

            return new EnumCaseNode
            {
                Name = idToken.Lexeme,
                Line = idToken.Line,
                Position = idToken.StartPos
            };
        }
    }
}