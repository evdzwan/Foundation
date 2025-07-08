using Foundation.Components;
using Foundation.Extensions;
using Foundation.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFoundation(config => config.AddScripts([typeof(Program).Assembly]))
                .AddScoped<IAuthorRepository, AuthorRepository>()
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IGenreRepository, GenreRepository>()
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
