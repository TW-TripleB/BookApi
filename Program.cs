using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 讀取 JWT 設定
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
JwtSettings jwtSettings = new JwtSettings();
builder.Configuration.Bind("JwtSettings", jwtSettings);
Console.WriteLine($"JWT Loaded - Issuer: {jwtSettings.Issuer}, Key: {jwtSettings.SecretKey}");

// JWT 驗證設定
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
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddControllers(); // ✅ 加入 Controller 支援
builder.Services.AddSingleton<MongoDbService>(); 
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();   // ✅ 驗證
app.UseAuthorization();    // ✅ 授權

app.UseHttpsRedirection();

app.MapControllers(); // ✅ 註冊 Controllers（讓 BooksController 可用）

app.Run();
