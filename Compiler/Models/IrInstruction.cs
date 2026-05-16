public class IrInstruction
{
    public string Command { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }

    public override string ToString()
    {
        if (Command == "ENUM") return $"DECLARE_ENUM \"{Name}\"";
        if (Command == "CASE") return $"  ALLOC_CASE \"{Name}\" = {Value}";
        if (Command == "END") return $"END_ENUM";
        return $"{Command} {Name}";
    }
}