namespace Compiler.Models
{
    public class SymbolTable
    {
        private HashSet<string> _globalEnums = new HashSet<string>();

        public void Clear()
        {
            _globalEnums.Clear();
        }

        public bool DeclareEnum(string name)
        {
            return _globalEnums.Add(name);
        }
    }
}
