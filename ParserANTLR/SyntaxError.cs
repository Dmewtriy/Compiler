namespace ParserANTLR
{
    public class SyntaxError
    {
        public string Fragment { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public int AbsoluteIndex { get; set; }
        public int Length { get; set; }
    }
}
