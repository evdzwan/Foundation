using Foundation.Collections;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Foundation;

public sealed class StyleList : IReadOnlyDictionary<string, object?>
{
    readonly Dictionary<string, object?> Items = new(StringComparer.OrdinalIgnoreCase);
    readonly Lock UpdateLock = new();

    public int Count => Items.Count;
    public IEnumerable<string> Keys => Items.Keys;
    public IEnumerable<object?> Values => Items.Values;
    public object? this[string key] => Items[key];

    public StyleList Add(string key, object? value, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                Items.Add(key, value);
            }
        }

        return this;
    }

    public StyleList AddRange(IEnumerable<KeyValuePair<string, object?>> attributes, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                attributes.ForEach(attribute => Items.Add(attribute.Key, attribute.Value));
            }
        }

        return this;
    }

    public StyleList AddUnmatched(IReadOnlyDictionary<string, object?>? unmatchedAttributes, bool condition = true)
    {
        if (condition && unmatchedAttributes?.GetValueOrDefault("style") is string { Length: > 0 } attributes)
        {
            using (UpdateLock.EnterScope())
            {
                CreateRange(attributes).ForEach(attribute => Items.Add(attribute.Key, attribute.Value));
            }
        }

        return this;
    }

    public bool ContainsKey(string key)
        => Items.ContainsKey(key);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        => Items.GetEnumerator();

    public StyleList Remove(string key, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                Items.Remove(key);
            }
        }

        return this;
    }

    public StyleList RemoveRange(IEnumerable<string> keys, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                keys.ForEach(key => Items.Remove(key));
            }
        }

        return this;
    }

    public StyleList RemoveUnmatched(IReadOnlyDictionary<string, object?>? unmatchedAttributes, bool condition = true)
    {
        if (condition && unmatchedAttributes?.GetValueOrDefault("style") is string { Length: > 0 } attributes)
        {
            using (UpdateLock.EnterScope())
            {
                CreateRange(attributes).ForEach(attribute => Items.Remove(attribute.Key));
            }
        }

        return this;
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object? value)
        => Items.TryGetValue(key, out value);

    public override string ToString()
        => string.Join(';', Items.Select(item => $"{item.Key}:{item.Value}"));

    static IEnumerable<KeyValuePair<string, object?>> CreateRange(string attributes)
        => attributes.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                     .Select(attribute => attribute.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                     .Select(attribute => new KeyValuePair<string, object?>(key: attribute[0], value: attribute[1]));

    public static StyleList operator +(StyleList @this, KeyValuePair<string, object?> attribute) => @this.Add(attribute.Key, attribute.Value);
    public static StyleList operator +(StyleList @this, IEnumerable<KeyValuePair<string, object?>> attributes) => @this.AddRange(attributes);
    public static StyleList operator +(StyleList @this, IReadOnlyDictionary<string, object?>? unmatchedAttributes) => @this.AddUnmatched(unmatchedAttributes);
    
    public static StyleList operator -(StyleList @this, string key) => @this.Remove(key);
    public static StyleList operator -(StyleList @this, IEnumerable<string> keys) => @this.RemoveRange(keys);
    public static StyleList operator -(StyleList @this, IReadOnlyDictionary<string, object?>? unmatchedAttributes) => @this.RemoveUnmatched(unmatchedAttributes);
}
