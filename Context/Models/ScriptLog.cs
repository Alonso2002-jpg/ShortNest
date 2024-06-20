namespace Context.Models;

public class ScriptLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ScriptName { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}