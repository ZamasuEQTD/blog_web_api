using Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using MediatR;
using Application.Behaviors;
using FluentValidation;
using Persistence.Configuration;
using WebApi.Extensions;
using WebApi.Configuration;
using Infraestructure.Configuration;
using Microsoft.Extensions.FileProviders;
using Application.Features.Hilos;
using Application.Features.Hilos.Abstractions;
using Infraestructure.Authentication;
using Application.Hilos;
using Infraestructure.Hubs.Home;
using Domain.Usuarios;
using Microsoft.AspNetCore.Identity;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<Usuario, Role>()
.AddRoles<Role>()
.AddEntityFrameworkStores<BlogDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddApplication().AddInfraestructure().AddPersistence(builder.Configuration).AddSwaggerBearerTokenSupport(); ;
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;})
 .AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSignalR();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehavior<,>));

builder.Services.AddSingleton<HomeConnectionManager>();
builder.Services.AddScoped<IHomeHubService, HomeHubService>();

builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});


builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapHub<HomeHub>("/home-hub");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Media")
    ),

    RequestPath = "/media"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Static")
    ),
    RequestPath = "/Static"
});
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.Run();

public partial class Program
{

}