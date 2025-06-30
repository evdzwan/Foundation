using Foundation;
using Foundation.Components;
using Foundation.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFoundation(config => config.Scan([typeof(Program).Assembly]))
                .AddScoped<ProductService>()
                .AddRazorComponents()
                .AddInteractiveServerComponents();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true)
       .UseHsts();
}

app.UseHttpsRedirection()
   .UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
