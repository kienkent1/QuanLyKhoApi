using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyKhoApi.Data;
using QuanLyKhoApi.Services;
using Scalar.AspNetCore;
using System.Text;
using QuanLyKhoApi.Helper;
using static QuanLyKhoApi.Helper.GitHubImageService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = null;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplicationServices();

builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection(GitHubOptions.GitHub));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true,
        };


    })
    .AddCookie();
    //.AddGoogle(option =>
    //{
    //    var clientId = builder.Configuration["Authentication:Google:ClientId"];
    //    if (clientId is null) throw new ArgumentNullException("ClientId is null");
    //    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    //    if (clientSecret is null) throw new ArgumentNullException("ClientSecret is null");

    //    option.ClientId = clientId;
    //    option.ClientSecret = clientSecret;
    //    option.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    option.Scope.Add("profile");
    //    option.Scope.Add("email");
    //    option.Scope.Add("openid");

    //    option.ClaimActions.MapJsonKey("picture", "picture");

    //});
var app = builder.Build();
app.UseCors("CorPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin-allow-popups";
    ctx.Response.Headers["Cross-Origin-Embedder-Policy"] = "unsafe-none";
    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
