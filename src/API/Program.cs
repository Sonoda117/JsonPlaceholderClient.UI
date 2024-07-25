using JsonPlaceholderClient.API.Endpoints;
using JsonPlaceholderClient.API.Extensions;

var Builder = WebApplication.CreateBuilder(args);

Builder.Services.AddCustomProblemDetails();
Builder.Services.AddJsonPlaceholderServices();

var App = Builder.Build();

App.UseExceptionHandler();

App.UseHttpsRedirection();

App.MapJsonPlaceholderEndpoints();

App.Run();