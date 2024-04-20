using Mess_Api;
using Mess_Api.Repositories.Auth;
using Mess_Api.Repositories.User;
using Mess_Api.Services.Auth;
using Mess_Api.Services.Jwt;
using Mess_Api.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Mess_Api.Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MessApiContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])),
            ValidateIssuer = true, // Set to true and define ValidIssuer if needed
            ValidateAudience = true, // Set to true and define ValidAudience if needed
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero // Optional: Reduce or remove tolerance of token expiration
        };
    });

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(),
                              "MessApiImportantLogs.json",
                              restrictedToMinimumLevel: LogEventLevel.Warning)
                .WriteTo.File("logs/MessApiLogs.txt",
                              rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("chat-hub");
app.MapPost("broadcast", async (string messageToSend, IHubContext<ChatHub, IChatClient> context) =>
{
    await context.Clients.All.ReceiveMessage(messageToSend);
    return Results.NoContent();
});

app.MapControllers();

app.Run();
