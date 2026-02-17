using FabricaPastas.Client;
using FabricaPastas.Client.Servicios;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Inyección de servicios
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IHTTPServicio, HTTPServicio>();

// Si CarritoServicio existe en tu proyecto, dejalo. Si no existe, borrá esta línea.
builder.Services.AddScoped<CarritoServicio>();
#endregion

await builder.Build().RunAsync();
