namespace Foundation.Scripting;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ScriptAttribute(string scriptPath) : Attribute
{
    public string ScriptPath { get; } = scriptPath;
}
