using AutoParts.Data.Context;
using AutoParts.Repositories.Implementations;
using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Implementations;
using AutoParts.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoParts.Data.Seed;
using Serilog;
using AutoParts.Middleware;
using HealthChecks.UI.Client;
using Serilog.Debugging;
using AutoParts.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

SelfLog.Enable(msg => Console.Error.WriteLine(msg));

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()

                // Esconde logs do ASP.NET
                .MinimumLevel.Override(
                    "Microsoft",
                    Serilog.Events.LogEventLevel.Warning)

                // Esconde logs do EF Core
                .MinimumLevel.Override(
                    "Microsoft.EntityFrameworkCore",
                    Serilog.Events.LogEventLevel.Warning)

                // Esconde logs do Kestrel
                .MinimumLevel.Override(
                    "Microsoft.AspNetCore",
                    Serilog.Events.LogEventLevel.Warning)

                // Esconde logs do System
                .MinimumLevel.Override(
                    "System",
                    Serilog.Events.LogEventLevel.Warning)

                .MinimumLevel.Override(
                    "Microsoft.EntityFrameworkCore.Database.Command",
                    Serilog.Events.LogEventLevel.Error)

                .Enrich.FromLogContext()

                .WriteTo.Console()

                .WriteTo.File(
                    "logs/autoparts-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30)

                // .WriteTo.Seq("http://localhost:5341") vs
                .WriteTo.Seq(
                    builder.Configuration["Serilog:WriteTo:0:Args:serverUrl"] ?? "http://seq:80") //docker

                .CreateLogger();

var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://*:{port}");
}

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

Log.Information("Aplicação AutoParts iniciada");

// Log.Information("===== TESTE SEQ =====");
// Log.CloseAndFlush();

// Thread.Sleep(5000);

// try
// {
//     Log.Information("TESTE SEQ {Data}", DateTime.Now);

//     Log.CloseAndFlush();

//     Console.WriteLine("Evento enviado ao Seq.");
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex);
// }

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
})
.AddApplicationPart(typeof(Program).Assembly);

//Repositories
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();

//Services
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";

    options.Cookie.Name = "AutoParts.Auth";

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

builder.Services
    .AddHealthChecks()
    .AddSqlServer(connectionString!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapStaticAssets();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
    //.WithStaticAssets();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//O container continua vivo mesmo que o banco demore
try
{
    using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();

    await DbInitializer.SeedAsync(context);
    await IdentityInitializer.SeedAsync(services);

    Log.Information("Banco inicializado.");
}
catch (Exception ex)
{
    Log.Error(ex, "Erro ao inicializar banco.");
}


Log.Information("AutoParts iniciado com sucesso.");

var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();

Console.WriteLine("========== ENDPOINTS ==========");

foreach (var endpoint in endpointDataSource.Endpoints)
{
    if (endpoint is RouteEndpoint routeEndpoint)
    {
        Console.WriteLine(
            $"{routeEndpoint.DisplayName} => {routeEndpoint.RoutePattern.RawText}");
    }
    else
    {
        Console.WriteLine(endpoint.DisplayName);
    }
}

Console.WriteLine("================================");

var dataSource = app.Services.GetRequiredService<EndpointDataSource>();

foreach (var endpoint in dataSource.Endpoints)
{
    Console.WriteLine(endpoint.DisplayName);
}

app.Run();
