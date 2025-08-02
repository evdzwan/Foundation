[Script("./_content/My.Project/Components/MyComponent.razor.js")]
sealed partial class MyComponentScript
{
    public partial ValueTask Attach<TValue>(DotNetObjectReference<TValue> component, ElementReference element, CancellationToken cancellationToken = default) where TValue : class;
    public partial ValueTask Detach<TValue>(DotNetObjectReference<TValue> component, CancellationToken cancellationToken = default) where TValue : class;
}
