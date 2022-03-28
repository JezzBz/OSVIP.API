using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Osvip.Api.Data;
using System.Net;

using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
/// <summary>
/// Строк подключения к базе данных
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
/// <summary>
/// Параметры валидации токена 
/// </summary>
var tokenValidationParameters= new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = AuthOptions.AUDIENCE,
    ValidIssuer = AuthOptions.ISSUER,
    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};
/// <summary>
/// Добавления отслеживания контроллеров приложением
/// </summary>
builder.Services.AddControllers();
/// <summary>
/// Добавление отслеживания конечных точек
/// </summary>
builder.Services.AddEndpointsApiExplorer();
/// <summary>
/// Доюавления Debug интерфейса 
/// </summary>
builder.Services.AddSwaggerGen();
/// <summary>
/// Добавка CORS политики в приложение
/// </summary>
builder.Services.AddCors();
/// <summary>
/// Подключение к базе данных
/// </summary>
builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
/// <summary>
/// Использование редиректа с http  на https
/// </summary>
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 7239;
});
/// <summary>
/// Добавление аутентификации
/// </summary>
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            })
            ;



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/// <summary>
/// Настройка политики Cors
/// </summary>
app.UseCors(x => x.WithOrigins(new[] { builder.Configuration.GetSection("AllowedOrigins")["UI Server"]})
                .AllowAnyMethod()
                .AllowAnyHeader()    
                .AllowCredentials());
/// <summary>
/// Добавление отслеживания аутентификации
/// </summary>
app.UseAuthentication();
/// <summary>
/// Добавление отслеживания авторизации
/// </summary>
app.UseAuthorization();
/// <summary>
/// Использование папки wwwroot в качестве хранилища файлов
/// </summary>
app.UseStaticFiles();
/// <summary>
/// Настройка ендпоинтов по контроллерам
/// </summary>
app.MapControllers();
/// <summary>
/// Запуск приложения
/// </summary>
app.Run();
