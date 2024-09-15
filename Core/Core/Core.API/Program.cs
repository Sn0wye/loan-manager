using System.Globalization;
using Core.Errors;
using Core.Infrastructure;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();

// Services
builder.Services.AddScoped<LoanService>();
builder.Services.AddScoped<UserService>();

// Repositories
builder.Services.AddScoped<LoanRepository>();
builder.Services.AddScoped<UserRepository>();

// Adapters
builder.Services.AddHttpClient<RiskAdapter>(client => { client.BaseAddress = new Uri("http://risk:8081"); })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Ignore SSL errors
        };
        return handler;
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API V1");
    c.RoutePrefix = string.Empty;
});
// }

// app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.Run();