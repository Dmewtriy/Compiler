using Antlr4.Runtime;

namespace ParserANTLR
{
    public class CustomErrorListener : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
    {
        public List<SyntaxError> Errors { get; } = new List<SyntaxError>();
        public bool HasErrors => Errors.Count > 0;

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            var lexer = (Lexer)recognizer;
            AddError(msg, line, charPositionInLine, lexer.CharIndex, 1, lexer.Text?.Substring(lexer.CharIndex, 1) ?? "");
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            int length = offendingSymbol.StopIndex - offendingSymbol.StartIndex + 1;
            AddError(msg, line, charPositionInLine, offendingSymbol.StartIndex, length, offendingSymbol.Text);
        }

        private void AddError(string msg, int line, int charPos, int absIndex, int length, string text)
        {
            Errors.Add(new SyntaxError
            {
                Fragment = text ?? "---",
                Location = $"Строка {line}, поз. {charPos+1}",
                Description = msg,
                AbsoluteIndex = absIndex,
                Length = length > 0 ? length : 1
            });
        }
    }
}
