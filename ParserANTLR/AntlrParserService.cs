using Antlr4.Runtime;

namespace ParserANTLR
{
    public class AntlrParserService
    {
        public List<SyntaxError> Parse(string sourceCode)
        {
            var inputStream = new AntlrInputStream(sourceCode);
            var lexer = new ParserANTLRLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new ParserANTLRParser(commonTokenStream);

            var errorListener = new CustomErrorListener();

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(errorListener);

            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);

            parser.program();

            return errorListener.Errors;
        }
    }
}
