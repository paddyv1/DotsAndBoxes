using DotsAndBoxes.Client.Services;
using DotsAndBoxes.Client.State;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<GameHubService>();
builder.Services.AddScoped<MatchSession>();



await builder.Build().RunAsync();
