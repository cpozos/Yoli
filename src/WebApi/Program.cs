using WebApi.Installations;
using Yoli.Core.App.Repositories;
using Yoli.Core.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add custom services
var installers = typeof(Program).Assembly.ExportedTypes
    .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    ?.Cast<IInstaller>()
    ?.ToList();

installers?.ForEach(x => x.InstallServices(builder.Configuration, builder.Services));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();