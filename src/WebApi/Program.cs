using WebApi.Extensions;
using Yoli.Core.App.Repositories;
using Yoli.Core.Infraestructure;
using Yoli.Core.WebApi.Installers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add custom services
typeof(Program).Assembly.ExportedTypes
    .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    .Select(x => Activator.CreateInstance(x))
    .Cast<IInstaller>()
    .ForEach(x => x.InstallServices(builder.Configuration, builder.Services));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();