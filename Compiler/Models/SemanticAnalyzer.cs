using Compiler.Models;

public class SemanticAnalyzer
{
    private SymbolTable _symbolTable = new SymbolTable();
    public List<SemanticError> Errors { get; private set; } = new List<SemanticError>();

    // Теперь метод возвращает список "чистых" узлов
    public List<EnumDeclNode> Analyze(List<EnumDeclNode> allNodes)
    {
        Errors.Clear();
        _symbolTable.Clear();

        var validatedNodes = new List<EnumDeclNode>();

        foreach (var node in allNodes)
        {
            if (!_symbolTable.DeclareEnum(node.Name))
            {
                AddError($"Ошибка: перечисление '{node.Name}' уже объявлено ранее.", node.Line, node.Position);
                continue;
            }

            var validCases = new List<EnumCaseNode>();
            var currentEnumCases = new HashSet<string>();

            foreach (var c in node.Cases)
            {
                if (!currentEnumCases.Add(c.Name))
                {
                    AddError($"Ошибка: идентификатор '{c.Name}' уже объявлено ранее в '{node.Name}'.", c.Line, c.Position);
                    continue;
                }

                validCases.Add(c);
            }

            node.Cases = validCases;
            validatedNodes.Add(node);
        }

        return validatedNodes;
    }

    private void AddError(string msg, int line, int pos)
    {
        Errors.Add(new SemanticError { Message = msg, Line = line, Position = pos });
    }
}