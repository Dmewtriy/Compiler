using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Models
{
    public abstract class AstNode
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public int Position { get; set; }
    }

    public class EnumDeclNode : AstNode
    {
        public List<EnumCaseNode> Cases { get; set; } = new List<EnumCaseNode>();
    }

    public class EnumCaseNode : AstNode
    {
        
    }
}
