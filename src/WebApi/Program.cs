using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Yoli.App.Authorization;
using Yoli.App.Repositories;
using Yoli.App.Services;
using Yoli.Infraestructure;
using Yoli.Infraestructure.Persistance;
using Yoli.Infraestructure.Services;
using Yoli.Shared.Authentication;
using Yoli.Shared.Extensions;
using Yoli.Shared.Middlewares;
using Yoli.WebApi.Authentication;
using Yoli.WebApi.Installers.Interfaces;
using Yoli.WebApi.Settings;
using Yoli.WebApi.Swagger;

{
    byte[] Zip(params string[] values)
    {
        var list = new List<string>();
        foreach (var item in values)
        {
            list.Add(item);
        }
        return new byte[1];
    }


    IEnumerable<string> l = new List<string>()
    {
        "B",
        "C",
        "A",
        "A",
        null
    };

    var re = Zip(l.ToArray());

    var repeated = l.GroupBy(i =>
    {
        return i;
    }).Any(g =>
    {
        bool more = g.Count() > 1;
        return more;
    });

    var test = new Test();
    test.List.Add(new Test2 { Name = "", Test3 = new Test3 { Name = "3" } });
    var x = GetXElement(test);
    var xStr = x.ToString();
    System.Xml.Linq.XElement GetXElement<T>(T obj)
    {
        using var ms = new MemoryStream();
        using TextWriter streamWriter = new StreamWriter(ms);
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        xmlSerializer.Serialize(streamWriter, obj);
        var xElement = System.Xml.Linq.XElement.Parse(System.Text.Encoding.ASCII.GetString(ms.ToArray()));
        return xElement;
    }
    
}

var builder = WebApplication.CreateBuilder(args);

// Settings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenService, TokenService>();

// Dbcontex
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<YoliDbContext>(opt =>
    {
        opt.UseInMemoryDatabase(Guid.NewGuid().ToString());
    });
}

// Third party services
builder.Services.AddMailKit(config =>
{
    var mailKitOptions = builder.Configuration.GetSection("Email").Get<MailKitOptions>();
    config.UseMailKit(mailKitOptions);
    // For debug purposes
    // https://github.com/ChangemakerStudios/Papercut-SMTP
});

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
builder.Services.AddMemoryCache();


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

while(true)
{
    string resTask = await Task.FromResult<string>(null);
    var cache = app.Services.GetRequiredService<IMemoryCache>();
    var entryRes = cache.GetOrCreate("1", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        return Guid.NewGuid().ToString();
    });
}


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();