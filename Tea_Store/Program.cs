using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tea_Store.Mappings;
using Tea_Store.Models;
using Microsoft.EntityFrameworkCore;
using Tea_Store.Data;
using Tea_Store.Services;
using Microsoft.Extensions.Options;
using Tea_Store;
using Swashbuckle.AspNetCore.SwaggerGen;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Options;
using Tea_Store;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TeaDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0))));

//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
builder.Services.AddAutoMapper(typeof(TeaMappingProfile));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeaService, TeaService>();

builder.Services.AddScoped<IShoppingCart, ShoppingCartService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "TeaStoreIssue",
        ValidAudience = "TeaStoreAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourVeryStrongAndSecureSecretKeyForTeaStore!2024@#SuperSecureKey"))
    };
});

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
