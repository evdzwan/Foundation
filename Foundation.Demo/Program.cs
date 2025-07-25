using Foundation;
using Foundation.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFoundation()
                .AddRazorComponents()
                .AddInteractiveServerComponents();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true)
       .UseHsts();
}

app.UseHttpsRedirection()
   .UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
