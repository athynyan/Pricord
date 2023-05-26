using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Pricord.Web;
using Pricord.Web.Features.Authentication.Services;
using Pricord.Web.Features.BattleRecords.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5180") });

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBattleRecordService, BattleRecordService>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
