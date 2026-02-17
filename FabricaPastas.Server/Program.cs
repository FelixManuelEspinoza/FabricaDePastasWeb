using FabricaPastas.BD.Data;
using FabricaPastas.Client.Servicios;
using FabricaPastas.Server.Data;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Server.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region codigo para ignorar ciclos
//-----------------------------------------------------------------------------
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//-----------------------------------------------------------------------------
#endregion

#region Client
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//#region ConnectionString
//builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));
//#endregion

builder.Services.AddDbContext<Context>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


#region Automapper
builder.Services.AddAutoMapper(typeof(Program));
#endregion


#region Inyección de dependencias de la interfaz IRepositorio
builder.Services.AddScoped<ICategoria_ProductoRepositorio, Categoria_ProductoRepositorio>();
builder.Services.AddScoped<IDetalle_Lista_PrecioRepositorio, Detalle_Lista_PrecioRepositorio>();
builder.Services.AddScoped<IDetalle_PedidoRepositorio, Detalle_PedidoRepositorio>();
builder.Services.AddScoped<IEstado_PedidoRepositorio, Estado_PedidoRepositorio>();
builder.Services.AddScoped<IForma_PagoRepositorio, Forma_PagoRepositorio>();
builder.Services.AddScoped<ILista_PrecioRepositorio, Lista_PrecioRepositorio>();
builder.Services.AddScoped<IMetodo_EntregaRepositorio, Metodo_EntregaRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IPromocion_ProductoRepositorio, Promocion_ProductoRepositorio>();
//builder.Services.AddScoped<IPromocion_UsuarioRepositorio, Promocion_UsuarioRepositorio>();
builder.Services.AddScoped<IPromocionRepositorio, PromocionRepositorio>();
builder.Services.AddScoped<IRolRepositorio, RolRepositorio>();
builder.Services.AddScoped<ITipo_ClienteRepositorio, Tipo_ClienteRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<CarritoServicio>();
builder.Services.AddScoped<IPromocion_RangoRepositorio, Promocion_RangoRepositorio>();
builder.Services.AddScoped<IPromocionServicio, PromocionServicio>();

#endregion

// ======================
// CONFIGURACION JWT
// ======================
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = jwtSection["Key"];
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,

        ValidateAudience = true,
        ValidAudience = audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };
});

builder.Services.AddAuthorization();


var app = builder.Build();



// ======================
// SEED DE DATOS INICIALES (Roles, Tipo_Cliente, Lista_Precio)
// ======================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    await DbSeeder.SeedAsync(context);
}
// ======================


// ======================
// SEED DE DATOS INICIALES
// ======================
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<Context>();
//    DbInitializer.Seed(context);
//}
// ======================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // 👈 IMPORTANTE: habilita lectura del JWT
app.UseAuthorization();    // 👈 valida roles y permisos

app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
