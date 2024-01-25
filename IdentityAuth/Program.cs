using Autofac.Extensions.DependencyInjection;
using Autofac;
using Serilog;
using Serilog.Events;
using IdentityAuth;
using System;
using IdentityAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IdentityAuth.Extensions;
using IdentityAuth.Membership;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341")
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();


try
{
    builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

    // Add services to the container.

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule());
    });
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString));


    builder.Services.AddIdentity();
    builder.Services.AddCookieAuthentication();
    builder.Services.AddAuthorizationBuilder();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception Ex)
{
    Log.Fatal(Ex, "Host Terminated Unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
