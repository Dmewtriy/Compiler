namespace Compiler.Models
{
    public class SemanticError
    {
        public string Message { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }

        public override string ToString()
        {
            return $"Строка {Line}, Поз {Position}";
        }
    }
}
