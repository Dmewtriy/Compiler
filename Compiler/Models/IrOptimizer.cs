public class IrOptimizer
{
    public List<IrInstruction> OptimizeDeduplicationOnly(List<IrInstruction> originalIr)
    {
        var finalIr = new List<IrInstruction>();
        var currentBlock = new List<IrInstruction>();

        foreach (var inst in originalIr)
        {
            currentBlock.Add(inst);
            if (inst.Command == "END")
            {
                finalIr.AddRange(RemoveDuplicatesFromBlock(currentBlock));
                currentBlock.Clear();
            }
        }
        return finalIr;
    }
    public List<IrInstruction> OptimizeCanonicalizationOnly(List<IrInstruction> originalIr)
    {
        var finalIr = new List<IrInstruction>();
        var currentBlock = new List<IrInstruction>();

        foreach (var inst in originalIr)
        {
            currentBlock.Add(inst);
            if (inst.Command == "END")
            {
                finalIr.AddRange(CanonicalizeOrderForBlock(currentBlock));
                currentBlock.Clear();
            }
        }
        return finalIr;
    }

    public List<IrInstruction> OptimizeBoth(List<IrInstruction> originalIr)
    {
        var step1 = OptimizeDeduplicationOnly(originalIr);
        return OptimizeCanonicalizationOnly(step1);
    }

    private List<IrInstruction> RemoveDuplicatesFromBlock(List<IrInstruction> block)
    {
        var optimized = new List<IrInstruction>();
        var seenCases = new HashSet<string>();
        int newIndex = 0;

        foreach (var inst in block)
        {
            if (inst.Command == "CASE")
            {
                if (seenCases.Add(inst.Name))
                {
                    optimized.Add(new IrInstruction { Command = "CASE", Name = inst.Name, Value = newIndex++ });
                }
            }
            else optimized.Add(inst);
        }
        return optimized;
    }

    private List<IrInstruction> CanonicalizeOrderForBlock(List<IrInstruction> block)
    {
        var optimized = new List<IrInstruction>();
        var caseNames = new List<string>();
        string enumName = "";

        foreach (var inst in block)
        {
            if (inst.Command == "ENUM") enumName = inst.Name;
            if (inst.Command == "CASE") caseNames.Add(inst.Name);
        }

        caseNames.Sort();

        optimized.Add(new IrInstruction { Command = "ENUM", Name = enumName });
        int newIndex = 0;
        foreach (var name in caseNames)
        {
            optimized.Add(new IrInstruction { Command = "CASE", Name = name, Value = newIndex++ });
        }
        optimized.Add(new IrInstruction { Command = "END" });
        return optimized;
    }
}