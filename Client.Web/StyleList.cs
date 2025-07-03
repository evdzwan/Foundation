namespace Foundation;

public sealed class StyleList
{
    readonly Dictionary<string, object?> Values = new(StringComparer.OrdinalIgnoreCase);

    public StyleList Add(string key, object? value, bool condition = true)
    {
        if (condition)
        {
            Values[key] = value;
        }

        return this;
    }

    public StyleList Add(IReadOnlyDictionary<string, object?>? attributes, bool condition = true)
    {
        if (attributes?.GetValueOrDefault("style") is string { Length: > 0 } style)
        {
            foreach (var (key, value) in style.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                              .Select(entry => entry.Split(':', StringSplitOptions.TrimEntries))
                                              .Where(entry => entry.Length == 2)
                                              .Select(entry => (entry[0], entry[1])))
            {
                Add(key, value, condition);
            }
        }

        return this;
    }

    public static StyleList Create(string key, object? value, bool condition = true)
        => new StyleList().Add(key, value, condition);

    public static StyleList Create(IReadOnlyDictionary<string, object?>? attributes, bool condition = true)
        => new StyleList().Add(attributes, condition);

    public override string ToString() => string.Join("; ", Values.Select(entry => $"{entry.Key}: {entry.Value}"));
}
