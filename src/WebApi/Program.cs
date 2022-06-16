using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Shared.Middlewares;
using WebApi.Settings;
using WebApi.Swagger;
using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Infraestructure;
using Yoli.Infraestructure.Services;
using Yoli.Shared.Extensions;
using Yoli.WebApi.Installers;


using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using FluentValidation;
using Yoli.WebApi.Requests;
using Yoli.WebApi.Validations;
using Microsoft.AspNetCore.Authorization;
using Yoli.WebApi.Authentication;
using Yoli.App.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Settings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddMailKit(config =>
{
    var mailKitOptions = builder.Configuration.GetSection("Email").Get<MailKitOptions>();
    config.UseMailKit(mailKitOptions);
    // For debug purposes
    // https://github.com/ChangemakerStudios/Papercut-SMTP
});

builder.Services.AddTransient<IValidator<YoliSignInRequest>, YoliSignInRequestValidator>();
builder.Services.AddTransient<IYoliValidatorFactory, YoliValidatorFactory>();

// Authentication
builder.Services
    .AddAuthentication(o => 
    {
        o.DefaultScheme = SchemesNames.TokenAuthenticationDefaultScheme;
        o.RequireAuthenticatedSignIn = true;
    })
    .AddScheme<YoliAuthenticationOptions, YoliAuthenticationHandler>(SchemesNames.TokenAuthenticationDefaultScheme, o => 
    { 
    });

// Athorization
builder.Services.AddSingleton<IAuthorizationHandler, HasAccessHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(YoliPolicy.MustHaveAccessPolicy, policy => policy.Requirements.Add(new HasAccessRequirement()));
});

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

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.UseAuthentication(); //app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();


app.Run();