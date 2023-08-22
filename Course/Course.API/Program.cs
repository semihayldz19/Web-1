using Course.Data;
using Course.Enums;
using Course.Interfaces;
using Course.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUser, RUser>(); 
builder.Services.AddScoped<ICompany, RCompany>();

// Add services to the container.

var configuration = builder.Configuration;
var environment = builder.Environment;

//var cs = configuration.GetSection("ConnectionString:connStr").Value;

//var env = configuration.GetSection("Environment").Value;
string jwtKey = configuration.GetSection("Jwt:Key").Value;
string jwtIssuer = configuration.GetSection("Jwt:Issuer").Value;
string jwtAudience = configuration.GetSection("Jwt:Audience").Value;
builder.Services.Configure<AppSettings>(o =>
{
    o.MysqlInfo.Database = configuration.GetSection("Mysql:Database").Value;
    o.MysqlInfo.Port = configuration.GetSection("Mysql:Port").Value;
    o.MysqlInfo.Password = configuration.GetSection("Mysql:Password").Value;
    o.MysqlInfo.Server = configuration.GetSection("Mysql:Server").Value;
    o.MysqlInfo.User = configuration.GetSection("Mysql:User").Value;
    o.Environment = configuration.GetSection("Environment").Value;

    o.Jwt.Key = jwtKey;
    o.Jwt.Issuer = jwtIssuer;
    o.Jwt.Audience = jwtAudience;

});

string mySqlConnectionStr = "server=" + configuration.GetSection("Mysql:Server").Value + ";" +
                  "port=" + configuration.GetSection("Mysql:Port").Value + ";" +
                  "database=" + configuration.GetSection("Mysql:Database").Value + ";" +
                  "user=" + configuration.GetSection("Mysql:User").Value + ";" +
                  "password=" + configuration.GetSection("Mysql:Password").Value + "";

builder.Services.AddDbContextPool<DataContext>(o =>
{
    o.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
});

builder.Services.AddAuthentication(
    options =>
    {
        object JwtBearerDefaults = null;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx =>
            {
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = ctx =>
            {
                Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServisTakip.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServisTakip.API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
