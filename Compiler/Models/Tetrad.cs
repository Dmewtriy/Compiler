public enum TokenType
{
    Number, Id, Plus, Minus, Mul, Div, Mod, LParen, RParen, EOF
}

public class Token
{
    public TokenType Type { get; set; }
    public string Value { get; set; }
    public int Position { get; set; }
}

public class Tetrad
{
    public string Op { get; set; }
    public string Arg1 { get; set; }
    public string Arg2 { get; set; }
    public string Result { get; set; }

    public override string ToString() => $"{Op}\t{Arg1}\t{Arg2}\t{Result}";
}