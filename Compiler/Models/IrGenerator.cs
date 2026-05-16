using Compiler.Models;

public class IrGenerator
{
    public List<IrInstruction> Generate(List<EnumDeclNode> enumNodes)
    {
        var instructions = new List<IrInstruction>();
        if (enumNodes == null) return instructions;

        foreach (var enumNode in enumNodes)
        {
            if (enumNode == null) continue;

            instructions.Add(new IrInstruction { Command = "ENUM", Name = enumNode.Name });

            int index = 0;
            foreach (var c in enumNode.Cases)
            {
                instructions.Add(new IrInstruction { Command = "CASE", Name = c.Name, Value = index++ });
            }

            instructions.Add(new IrInstruction { Command = "END" });
        }

        return instructions;
    }
}