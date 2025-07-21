using Foundation.Expressions;
using Humanizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System.Reflection;

namespace Foundation.Components;

public abstract class Field<TModel> : Component
{
    [CascadingParameter] EditContext? FormContext { get; set; }
    [Parameter] public bool AutoFocus { get; set; }
    [Parameter, EditorRequired] public required Expression<Func<TModel, object?>> Expression { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    protected object? Value
    {
        get
        {
            if (ValueProperty is { CanRead: true } property && FormContext?.Model is TModel { } model)
            {
                return property.GetValue(model);
            }

            return default;
        }
        set
        {
            if (ValueProperty is { CanWrite: true } property && FormContext?.Model is TModel { } model)
            {
                property.SetValue(model, value);
            }
        }
    }

    protected Expression<Func<object?>>? ValueExpression { get; private set; }
    protected PropertyInfo? ValueProperty { get; private set; }

    protected override void OnParametersSet()
    {
        ValueExpression ??= FormContext?.Model is TModel { } model ? Expression.ReplaceParameter(model) : null;
        ValueProperty ??= ValueExpression?.GetPropertyOrDefault();
        Title ??= ValueProperty?.Name.Humanize(LetterCasing.Title);
    }
}
