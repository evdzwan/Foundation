using Foundation.Collections;
using System.Collections;

namespace Foundation;

public sealed class ClassList : IReadOnlyList<string>
{
    readonly List<string> Items = [];
    readonly Lock UpdateLock = new();

    public int Count => Items.Count;
    public string this[int index] => Items[index];

    public ClassList Add(string className, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                Items.Add(className);
            }
        }

        return this;
    }

    public ClassList AddRange(IEnumerable<string> classNames, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                Items.AddRange(classNames);
            }
        }

        return this;
    }

    public ClassList AddUnmatched(IReadOnlyDictionary<string, object?>? unmatchedAttributes, bool condition = true)
    {
        if (condition && unmatchedAttributes?.GetValueOrDefault("class") is string { Length: > 0 } classNames)
        {
            using (UpdateLock.EnterScope())
            {
                Items.AddRange(CreateRange(classNames));
            }
        }

        return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<string> GetEnumerator()
        => Items.GetEnumerator();

    public ClassList Remove(string className, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                Items.Remove(className);
            }
        }

        return this;
    }

    public ClassList RemoveRange(IEnumerable<string> classNames, bool condition = true)
    {
        if (condition)
        {
            using (UpdateLock.EnterScope())
            {
                classNames.ForEach(className => Items.Remove(className));
            }
        }

        return this;
    }

    public ClassList RemoveUnmatched(IReadOnlyDictionary<string, object?>? unmatchedAttributes, bool condition = true)
    {
        if (condition && unmatchedAttributes?.GetValueOrDefault("class") is string { Length: > 0 } classNames)
        {
            using (UpdateLock.EnterScope())
            {
                CreateRange(classNames).ForEach(className => Items.Remove(className));
            }
        }

        return this;
    }

    public override string ToString()
        => string.Join(' ', Items);

    static string[] CreateRange(string classNames)
        => classNames.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public static implicit operator string(ClassList @this) => @this.ToString();
    public static ClassList operator +(ClassList @this, string className) => @this.Add(className);
    public static ClassList operator +(ClassList @this, IEnumerable<string> classNames) => @this.AddRange(classNames);
    public static ClassList operator +(ClassList @this, IReadOnlyDictionary<string, object?>? unmatchedAttributes) => @this.AddUnmatched(unmatchedAttributes);

    public static implicit operator ClassList(string @this) => new ClassList().AddRange(CreateRange(@this));
    public static ClassList operator -(ClassList @this, string className) => @this.Remove(className);
    public static ClassList operator -(ClassList @this, IEnumerable<string> classNames) => @this.RemoveRange(classNames);
    public static ClassList operator -(ClassList @this, IReadOnlyDictionary<string, object?>? unmatchedAttributes) => @this.RemoveUnmatched(unmatchedAttributes);
}
