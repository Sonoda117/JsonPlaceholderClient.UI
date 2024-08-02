using JsonPlaceholderClient.UI.Components;

var Builder = WebApplication.CreateBuilder(args);
var APIBaseAddress = Builder.Configuration["JsonPlaceholder:Address"] ?? "http://localhost:7012/";

Builder.Services.AddHttpClient("JsonPlaceholder.API", Client =>
{
    Client.BaseAddress = new Uri(APIBaseAddress);
});

Builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var App = Builder.Build();

if (!App.Environment.IsDevelopment())
{
    App.UseExceptionHandler("/Error", createScopeForErrors: true);
    App.UseHsts();
}

App.UseHttpsRedirection();

App.UseStaticFiles();

App.UseAntiforgery();

App.MapRazorComponents<App>().AddInteractiveServerRenderMode();

App.Run();