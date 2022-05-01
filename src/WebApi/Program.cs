using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WebApi.Extensions;
using WebApi.Swagger;
using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Infraestructure;
using Yoli.Infraestructure.Services;
using Yoli.WebApi.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configure Swagger
    // "Bearer" is the name for this definition. Any other name could be used
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Use bearer token to authorize. Enter Bearer {token}",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    });
    c.OperationFilter<AddAuthHeaderOperationFilter>();
});

// Add services to the container.

builder.Services.AddControllers();


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
//{
//    opt.TokenValidationParameters = new()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});
//builder.Services.AddAuthorization(config =>
//{
//    config.AddPolicy("authenticated", config =>
//    {
//        config.RequireAuthenticatedUser();
//    });
//});

// Add custom services
typeof(Program).Assembly.ExportedTypes
    .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    .Select(x => Activator.CreateInstance(x))
    .Cast<IInstaller>()
    .ForEach(x => x.InstallServices(builder.Configuration, builder.Services));

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();


app.Run();