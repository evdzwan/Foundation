namespace Foundation;

public sealed class ClassList
{
    readonly List<string> ClassNames = [];

    public ClassList Add(string classNames, bool condition = true)
    {
        if (condition)
        {
            ClassNames.AddRange(classNames.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
        }

        return this;
    }

    public ClassList Add(IReadOnlyDictionary<string, object?>? attributes, bool condition = true)
    {
        if (attributes?.GetValueOrDefault("class") is string { Length: > 0 } classNames)
        {
            return Add(classNames, condition);
        }

        return this;
    }

    public static ClassList Create(string classNames, bool condition = true)
        => new ClassList().Add(classNames, condition);

    public static ClassList Create(IReadOnlyDictionary<string, object?>? attributes, bool condition = true)
        => new ClassList().Add(attributes, condition);

    public override string ToString() => string.Join(' ', ClassNames.Distinct());
}
