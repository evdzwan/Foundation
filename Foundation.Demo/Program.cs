using Foundation;
using Foundation.Components;
using Foundation.Scripts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFoundation()
                .AddScripts([typeof(CanvasPageScript).Assembly])
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
