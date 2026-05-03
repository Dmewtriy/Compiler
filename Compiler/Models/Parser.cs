public class Parser
{
    private List<Token> _tokens;
    private int _pos;
    private int _tempCounter;

    public List<string> Errors { get; } = new List<string>();
    public List<Tetrad> Tetrads { get; } = new List<Tetrad>();

    private Token Current => _pos < _tokens.Count ? _tokens[_pos] : _tokens[_tokens.Count - 1];

    public void Parse(List<Token> tokens)
    {
        _tokens = tokens;
        _pos = 0;
        _tempCounter = 1;
        Errors.Clear();
        Tetrads.Clear();

        if (tokens.Count == 0 || tokens[0].Type == TokenType.EOF) return;

        ParseE();

        if (Current.Type != TokenType.EOF)
        {
            AddError($"Лишние символы в конце выражения: {Current.Value}");
        }
    }

    private string NewTemp() => $"t{_tempCounter++}";

    private void AddError(string msg)
    {
        Errors.Add($"Ошибка синтаксиса (поз {Current.Position}): {msg}");
    }

    private Token Match(TokenType expected)
    {
        if (Current.Type == expected)
        {
            var t = Current;
            _pos++;
            return t;
        }
        AddError($"Ожидался {expected}, но найден {Current.Value}");
        return null;
    }

    // E -> T A
    private string ParseE()
    {
        string leftArg = ParseT();

        while (Current.Type == TokenType.Plus || Current.Type == TokenType.Minus)
        {
            string op = Current.Value;
            _pos++;
            string rightArg = ParseT();

            if (leftArg != null && rightArg != null)
            {
                string result = NewTemp();
                Tetrads.Add(new Tetrad { Op = op, Arg1 = leftArg, Arg2 = rightArg, Result = result });
                leftArg = result;
            }
            else
            {
                leftArg = null;
            }
        }
        return leftArg;
    }

    // T -> F B
    private string ParseT()
    {
        string leftArg = ParseF();

        while (Current.Type == TokenType.Mul || Current.Type == TokenType.Div || Current.Type == TokenType.Mod)
        {
            string op = Current.Value;
            _pos++;
            string rightArg = ParseF();

            if (leftArg != null && rightArg != null)
            {
                string result = NewTemp();
                Tetrads.Add(new Tetrad { Op = op, Arg1 = leftArg, Arg2 = rightArg, Result = result });
                leftArg = result;
            }
            else
            {
                leftArg = null;
            }
        }
        return leftArg;
    }

    // F -> num | $id | ( E )
    private string ParseF()
    {
        if (Current.Type == TokenType.Number || Current.Type == TokenType.Id)
        {
            string val = Current.Value;
            _pos++;
            return val;
        }
        else if (Current.Type == TokenType.LParen)
        {
            _pos++;
            string val = ParseE();
            if (Match(TokenType.RParen) == null) return null;
            return val;
        }
        else
        {
            AddError("Ожидалось число, идентификатор или '('");
            return null;
        }
    }
}