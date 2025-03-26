using Dokypets.Infrastructure;
using Dokypets.Application.UseCases;
using Dokypets.Api.Extensions.Middleware;
using Dokypets.Api.Injection;
using Dokypets.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Dokypets.Infrastructure.Contexts;
using Dokypets.Domain.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Custom Extensions
builder.Services.AddInjectionPersistence(builder.Configuration);
builder.Services.AddJwtInjection(builder.Configuration);
builder.Services.AddInjectionApplication();


var corsName = "MyCorsEnable";
builder.Services.AddInjectionCors(builder.Configuration, corsName);

var app = builder.Build();

using (IServiceScope? scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = service.GetRequiredService<ApplicationDbContext>();
        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRoles.SeedRoles(roleManager);
        await DefaultUsersSeed.SeedUsers(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(corsName);
app.UseStaticFiles();

app.UseAuthorization();

//Fluent middleware
app.AddMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
