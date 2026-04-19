using System.Text;

namespace Compiler.Models
{
    public class AstVisualizer
    {
        public string GenerateTreeText(List<EnumDeclNode> nodes)
        {
            if (nodes == null || nodes.Count == 0)
                return "AST дерево пусто (нет корректных объявлений).";

            StringBuilder sb = new StringBuilder();

            foreach (var node in nodes)
            {
                sb.AppendLine($"EnumDeclNode");
                sb.AppendLine($"├── Name: \"{node.Name}\"");
                sb.AppendLine($"└── Cases:");

                for (int i = 0; i < node.Cases.Count; i++)
                {
                    bool isLast = (i == node.Cases.Count - 1);
                    string prefix = isLast ? "    └── " : "    ├── ";
                    sb.AppendLine($"{prefix}EnumCaseNode (Name: \"{node.Cases[i].Name}\")");
                }
                sb.AppendLine();
            }

            return sb.ToString().Trim();
        }
    }
}
