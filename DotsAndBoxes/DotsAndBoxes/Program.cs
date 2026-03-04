using DotsAndBoxes.Client.Services;
using DotsAndBoxes.Client.State;
using DotsAndBoxes.Components;
using DotsAndBoxes.GameHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Game server services
builder.Services.AddSingleton<RoomManager>();
builder.Services.AddSignalR();

// Client services that the server DI must resolve when activating WASM component routes.
// With prerender:false, lifecycle methods never run so no actual connection is made,
// but the server routing pipeline still resolves @inject properties.
builder.Services.AddScoped<MatchSession>();
builder.Services.AddScoped<GameHubService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(DotsAndBoxes.Client._Imports).Assembly);

app.MapHub<GameHub>("/hub/game");

app.Run();
