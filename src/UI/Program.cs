using JsonPlaceholderClient.UI.Components;

var Builder = WebApplication.CreateBuilder(args);

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